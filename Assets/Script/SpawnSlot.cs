/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;
using Shapes;

public class SpawnSlot : MonoBehaviour, ISlotEntity
{
#region Fields
  [ Title( "Setup" ) ]
	[ SerializeField ] int slot_index;

  [ Title( "Component" ) ]
	[ SerializeField ] Rectangle _rectangle;

  [ Title( "Shared" ) ]
	[ SerializeField ] ListSpawnSlot list_slot_spawn_all; // This includes all spawn slots
	[ SerializeField ] ListSpawnSlot list_slot_spawn_empty; // This includes only empty spawn slots
	[ SerializeField ] ListSlot list_slot_all; // This includes all slots
	[ SerializeField ] PoolClock pool_clock;
	[ SerializeField ] ClockDataLibrary clock_data_library;
	[ SerializeField ] SaveSystem system_save;
	[ SerializeField ] ClockPurchaseCondition notif_clock_purchase_condition;

	[ ShowInInspector, ReadOnly ] Clock clock_current;
#endregion

#region Properties
	public int SlotIndex => slot_index;
#endregion

#region Unity API
	private void OnEnable()
	{
		list_slot_spawn_all.AddDictionary( slot_index, this );
		list_slot_spawn_empty.AddList( this );
		list_slot_all.AddList( this );
	}

	private void OnDisable()
	{
		list_slot_spawn_all.RemoveDictionary( slot_index );
		list_slot_spawn_empty.RemoveList( this );
		list_slot_all.RemoveList( this );
	}

	private void Start()
	{
		if( system_save.SaveData.slot_spawn_array[ slot_index ] > -1 )
			LoadClock( system_save.SaveData.slot_spawn_array[ slot_index ] );
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
		list_slot_spawn_empty.AddList( this );
		notif_clock_purchase_condition.SetConditionSlot( true );
	}

	public void HighlightPositive()
	{
		_rectangle.Color = GameSettings.Instance.slot_spawn_highlight_color_positive;
	}

	public void HighlightNegative()
	{
		_rectangle.Color = GameSettings.Instance.slot_spawn_highlight_color_negative;
	}

	public void HighlightDefault()
	{
		_rectangle.Color = GameSettings.Instance.slot_spawn_highlight_color_default;
	}
  //ISlotInterface END

	public void SpawnClock( int index )
	{
		var clock     = pool_clock.GetEntity();
		var clockData = clock_data_library.GetClockData( index );

		clock.SpawnIntoSpawnSlot( this, clockData );

		clock_current = clock;
		list_slot_spawn_empty.RemoveList( this );
	}

	public void LoadClock( int index )
	{
		var clock         = pool_clock.GetEntity();
		var clockData     = clock_data_library.GetClockData( index );
		    clock_current = clock;

		clock.SpawnIntoSpawnSlot( this, clockData );

		list_slot_spawn_empty.RemoveList( this );
	}
#endregion

#region Implementation
	void CacheInComingClock( Clock incoming )
	{
		clock_current = incoming;
		incoming.OccupySpawnSlot();

		list_slot_spawn_empty.RemoveList( this );
	}

	void MergeCurrentClock( Clock incoming )
	{
		incoming.ReturnToPool();
		clock_current.UpgradeInSpawnSlot();
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
