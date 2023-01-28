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

	  [ Title( "Setup" ) ]
		[ SerializeField ] SharedStringNotifier save_string;

		[ ShowInInspector ] SaveData save_data;
		static SaveSystem instance;
#endregion

#region Properties
		public SaveData SaveData => save_data;
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
			FFStudio.FFLogger.Log( "Savemanager: Saved Succesfully. Data saved: " + save );
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

		[ Button() ]
		public void CreateSaveDataAndSave()
		{
			if( save_data == null )
				save_data = new SaveData();

			save_data.slot_spawn_clock_level_array = new int[ list_slot_spawn_all.itemDictionary.Count ];

			foreach( var slot in list_slot_spawn_all.itemDictionary.Values )
			{
				save_data.slot_spawn_clock_level_array[ slot.SlotIndex ] = slot.IsClockPresent() ? slot.CurrentClockLevel() - 1 : -1;
			}

			SaveOverride( JsonUtility.ToJson( save_data ) );
		}

		[ Button() ]
		public void LoadSaveData()
		{
			var json = LoadSave();

			if( json != null )
				save_data = JsonUtility.FromJson< SaveData >( json ) as SaveData;
		}

		public void NULLOutSaveData()
		{
			save_data = null;
		}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}

[ System.Serializable ]
public class SaveData
{
	public int[] slot_spawn_clock_level_array;
}