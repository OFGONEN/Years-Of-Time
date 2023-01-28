/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;
using Shapes;

public class ClockSlot : MonoBehaviour, ISlotEntity
{
#region Fields
  [ Title( "Setup" ) ]
	[ SerializeField ] int slot_index;
	[ SerializeField ] bool slot_row;

  [ Title( "Component" ) ]
	[ SerializeField ] Disc _disc;
	[ SerializeField ] Rectangle _rectangle_background;
	[ SerializeField ] Rectangle _rectangle_foreground;

  [ Title( "Shared" ) ]
	// [ SerializeField ] ListSpawnSlot list_slot_spawn_all; // This includes all spawn slots
	// [ SerializeField ] ListSpawnSlot list_slot_spawn_empty; // This includes only empty spawn slots
	[ SerializeField ] ListSlot list_slot_all; // This includes all slots
	[ SerializeField ] ListClockSlot list_slot_axis; // This clock of row or column
	[ SerializeField ] PoolClock pool_clock;
	[ SerializeField ] ClockDataLibrary clock_data_library;
	[ SerializeField ] SaveSystem system_save;
	[ SerializeField ] ListItem list_item;

	[ ShowInInspector, ReadOnly ] Clock clock_current;
	[ ShowInInspector, ReadOnly ] Item[] item_array;
#endregion

#region Properties
	public int SlotIndex => slot_index;
#endregion

#region Unity API
	private void OnEnable()
	{
		// list_slot_all.AddList( this );
		list_slot_axis.AddDictionary( slot_index, this );
	}

    private void OnDisable()
    {
		list_slot_all.RemoveList( this );
		list_slot_axis.RemoveDictionary( slot_index );
	}

	private void Start()
	{
		item_array = new Item[ GameSettings.Instance.playArea_size_count ];

		//Cache Item
		if( slot_row )
		{
			for( var i = 0; i < GameSettings.Instance.playArea_size_count; i++ )
			{
				Item item;
				list_item.itemDictionary.TryGetValue( new Vector2Int( slot_index, i ).GetCustomHashCode(), out item );
				item_array[ i ] = item;
			}
		}
		else
		{
			for( var i = 0; i < GameSettings.Instance.playArea_size_count; i++ )
			{
				Item item;
				list_item.itemDictionary.TryGetValue( new Vector2Int( i, slot_index ).GetCustomHashCode(), out item );
				item_array[ i ] = item;
			}
		}

		if( slot_row )
		{
			var saveValue = system_save.SaveData.slot_clock_array_row[ slot_index ];

			if( saveValue > -1 )
				StartWithClock( saveValue );
			else if( saveValue == -1 )
				StartUnlocked();
		}
		else if( !slot_row )
		{
			var saveValue = system_save.SaveData.slot_clock_array_column[ slot_index ];

			if( saveValue > -1 )
				StartWithClock( saveValue );
			else if( saveValue == -1 )
				StartUnlocked();
		}
	}
#endregion

#region API
  //ISlotInterface START
    public Vector3 GetPosition()
    {
		return transform.position;
	}

	public bool IsClockPresent()
	{
		return clock_current != null;
	}

	public int CurrentClockLevel()
	{
		return clock_current.ClockData.ClockLevel;
	}

	public void HandleIncomingClock( Clock incoming )
	{
		if( clock_current == null )
			CacheInComingClock( incoming );
		else
			MergeCurrentClock( incoming );

		HighlightDefault();
	}

	public void OnCurrentClockDeparted()
	{
		clock_current = null;
		RemoveClockFromItems();
	}

	public void HighlightPositive()
	{
		_rectangle_background.enabled = true;
		_rectangle_foreground.enabled = true;

		_rectangle_background.Color = GameSettings.Instance.slot_clock_highlight_color_positive.SetAlpha( GameSettings.Instance.slot_clock_highlight_alpha_cofactor );
		_rectangle_foreground.Color = GameSettings.Instance.slot_clock_highlight_color_positive;
	}

	public void HighlightNegative()
	{
		_rectangle_background.enabled = true;
		_rectangle_foreground.enabled = true;

		_rectangle_background.Color = GameSettings.Instance.slot_clock_highlight_color_negative.SetAlpha( GameSettings.Instance.slot_clock_highlight_alpha_cofactor );
		_rectangle_foreground.Color = GameSettings.Instance.slot_clock_highlight_color_negative;
	}

	public void HighlightDefault()
	{
		_rectangle_background.enabled = false;
		_rectangle_foreground.enabled = false;
	}
  //ISlotInterface END
#endregion

#region Implementation
	void StartUnlocked()
	{
		list_slot_all.AddList( this );
		_disc.enabled = true;
	}

	void StartWithClock( int level )
	{
		list_slot_all.AddList( this );
		_disc.enabled = true;

		LoadClock( level );
		AssignClockToItems();
	}

	void LoadClock( int index )
	{
		var clock     = pool_clock.GetEntity();
		var clockData = clock_data_library.GetClockData( index );

		clock.LoadIntoClockSlot( this, clockData );
		clock_current = clock;
	}

	void CacheInComingClock( Clock incoming )
	{
		clock_current = incoming;
		incoming.OccupyClockSlot();
		AssignClockToItems();
	}

	void MergeCurrentClock( Clock incoming )
	{
		incoming.ReturnToPool();
		clock_current.UpgradeInClockSlot();
	}

	void AssignClockToItems()
	{
		for( var i = 0; i < item_array.Length; i++ )
			item_array[ i ].AssignClockSlot( this );
	}

	void RemoveClockFromItems()
	{
		for( var i = 0; i < item_array.Length; i++ )
			item_array[ i ].RemoveClockSlot( this );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion

}