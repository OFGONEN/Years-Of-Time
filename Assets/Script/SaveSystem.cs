/* Created by and for usage of FF Studios (2021). */

using System.IO;
using UnityEngine;
using Sirenix.OdinInspector;

namespace FFStudio
{
	[ CreateAssetMenu( fileName = "system_save", menuName = "FF/System/Save" ) ]
	public class SaveSystem : ScriptableObject
	{
#region Fields
	  [ Title( "Save Targets" ) ]
	  	[ SerializeField ] ListSpawnSlot list_slot_spawn_all;
	  	[ SerializeField ] ListClockSlot list_slot_clock_row;
	  	[ SerializeField ] ListClockSlot list_slot_clock_column;
	  	[ SerializeField ] ListItem list_item_index;

	  [ Title( "Setup" ) ]
		[ SerializeField ] SharedStringNotifier save_string;
		[ SerializeField ] Currency notif_currency;

		[ SerializeField ] SaveData save_data;

		Cooldown cooldown = new Cooldown();
		static SaveSystem instance;

		const int slot_spawn_default = -1;
		const int slot_clock_default = -2;
		const int item_default = -2;
#endregion

#region Properties
		public SaveData SaveData          => save_data;
		public static SaveSystem Instance => instance;
#endregion

#region Unity API
#endregion

#region API
		public void Initialize()
		{
			instance = this;

			// Create folder.
			if( Directory.Exists( ExtensionMethods.SAVE_PATH ) == false )
				Directory.CreateDirectory( ExtensionMethods.SAVE_PATH );
		}

		public void SaveOverride_WithSharedString()
		{
			File.WriteAllText( ExtensionMethods.SAVE_PATH + "save.txt", save_string.sharedValue );
			FFStudio.FFLogger.Log( "Savemanager: Saved Succesfully. Data saved: " + save_string.sharedValue );
		}

		public void SaveOverride( string save )
		{
			File.WriteAllText( ExtensionMethods.SAVE_PATH + "save.txt", save );
			// FFStudio.FFLogger.Log( "Savemanager: Saved Succesfully. Data saved: " + save );
		}

		public string LoadSave()
		{
			if( File.Exists( ExtensionMethods.SAVE_PATH + "save.txt" ) == false )
				return null;

			var json = File.ReadAllText( ExtensionMethods.SAVE_PATH + "save.txt" );
			FFStudio.FFLogger.Log( "SaveSystem: Loaded Succesfully. Data read: " + json );

			return json;
		}

		public void LoadSave_ToSharedString()
		{
			if( File.Exists( ExtensionMethods.SAVE_PATH + "save.txt" ) == false )
			{
				save_string.SharedValue = null;
				return;
			}

			save_string.SharedValue = File.ReadAllText( ExtensionMethods.SAVE_PATH + "save.txt" );
			FFStudio.FFLogger.Log( "SaveSystem: Loaded Succesfully. Data read: " + save_string.sharedValue );
		}

		public void DeleteSave()
		{
			if( File.Exists( ExtensionMethods.SAVE_PATH + "save.txt" ) )
			{
				FFStudio.FFLogger.Log( "SaveSystem: Found save file. Deleting it." );
				File.Delete( ExtensionMethods.SAVE_PATH + "save.txt" );
			}
		}

		public void StartSaveCooldown()
		{
			cooldown.Start( GameSettings.Instance.game_save_cooldown, OnSaveCooldownComplete );
		}

		void OnSaveCooldownComplete()
		{
			SaveGameState();
			notif_currency.SaveToPlayerPrefs();

			StartSaveCooldown();
		}

		[ Button() ]
		public void SaveGameState()
		{
			// Spawn Slot
			foreach( var slot in list_slot_spawn_all.itemDictionary.Values )
			{
				save_data.slot_spawn_array[ slot.SlotIndex ] = slot.IsClockPresent() ? slot.CurrentClockLevel() - 1 : -1;
			}

			// // Clock Slot Row Default
			// for( var i = 0; i < save_data.slot_clock_array_row.Length; i++ )
			// 	save_data.slot_clock_array_row[ i ] = slot_clock_default;

			// // Clock Slot Column Default
			// for( var i = 0; i < save_data.slot_clock_array_column.Length; i++ )
			// 	save_data.slot_clock_array_column[ i ] = slot_clock_default;

			// Clock Slot Row Current
			foreach( var slot in list_slot_clock_row.itemDictionary.Values )
				save_data.slot_clock_array_row[ slot.SlotIndex ] = slot.ClockSlotState == ClockSlotState.Invisible ? slot_clock_default : slot.IsClockPresent() ? slot.CurrentClockLevel() - 1 : -1;

			// Clock Slot Column Current
			foreach( var slot in list_slot_clock_column.itemDictionary.Values )
				save_data.slot_clock_array_column[ slot.SlotIndex ] = slot.ClockSlotState == ClockSlotState.Invisible ? slot_clock_default : slot.IsClockPresent() ? slot.CurrentClockLevel() - 1 : -1;

			foreach( var item in list_item_index.itemDictionary.Values )
				save_data.item_array[ item.ItemIndex ] = (int)item.ItemState ;

			SaveOverride( JsonUtility.ToJson( save_data ) );
		}

		[ Button() ]
		public void LoadSaveData()
		{
			var json = LoadSave();

			if( json != null )
				save_data = JsonUtility.FromJson< SaveData >( json ) as SaveData;
		}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
		[ Button() ]
		public void CreateDefaultSaveData()
		{
			UnityEditor.EditorUtility.SetDirty( this );

			int spawnSlotCount       = GameSettings.Instance.playArea_spawn_slot_count;
			int clockRowSlotCount    = GameSettings.Instance.playArea_size_count_row;
			int clockColumnSlotCount = GameSettings.Instance.playArea_size_count_column;
			int itemCount            = GameSettings.Instance.PlayAreaSize;

			save_data = new SaveData();

			// Spawn Slot
			save_data.slot_spawn_array = new int[ spawnSlotCount ];

			for( var i = 0; i < spawnSlotCount; i++ )
				save_data.slot_spawn_array[ i ] = slot_spawn_default;

			// Clock Slot Row
			save_data.slot_clock_array_row = new int[ clockRowSlotCount ];

			for( var i = 0; i < clockRowSlotCount; i++ )
				save_data.slot_clock_array_row[ i ] = slot_clock_default;

			// Clock Slot Column
			save_data.slot_clock_array_column = new int[ clockColumnSlotCount ];

			for( var i = 0; i < clockColumnSlotCount; i++ )
				save_data.slot_clock_array_column[ i ] = slot_clock_default;

			// Item
			save_data.item_array = new int[ itemCount ];

			for( var i = 0; i < itemCount; i++ )
				save_data.item_array[ i ] = item_default;

			save_data.slot_clock_array_column[ 0 ] = -1;
			save_data.slot_clock_array_column[ 1 ] = -1;
			save_data.slot_clock_array_column[ 2 ] = -1;

			save_data.slot_clock_array_row[ 0 ] = 0;
			save_data.slot_clock_array_row[ 1 ] = -1;
			save_data.slot_clock_array_row[ 2 ] = -1;

			save_data.item_array[ 0 ] = 0;
			save_data.item_array[ 1 ] = 0;
			save_data.item_array[ 2 ] = 0;
			save_data.item_array[ 3 ] = 0;
			save_data.item_array[ 4 ] = -1;
			save_data.item_array[ 5 ] = -1;
			save_data.item_array[ 6 ] = -1;
			save_data.item_array[ 7 ] = -1;
			save_data.item_array[ 8 ] = -1;

			UnityEditor.AssetDatabase.SaveAssets();
			SaveCurrentSaveData();
		}

		[ Button() ]
		void SaveCurrentSaveData()
		{
			SaveOverride( JsonUtility.ToJson( save_data ) );
		}
#endif
#endregion
	}
}

[ System.Serializable ]
public class SaveData
{
	public int[] slot_spawn_array;
	public int[] slot_clock_array_row;
	public int[] slot_clock_array_column;
	public int[] item_array;
}