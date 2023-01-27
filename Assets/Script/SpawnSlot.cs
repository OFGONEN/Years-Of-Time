/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class SpawnSlot : MonoBehaviour, ISlotEntity
{
#region Fields
  [ Title( "Shared" ) ]
	[ SerializeField ] ListSpawnSlot list_slot_spawn_empty;
	[ SerializeField ] ListSlot list_slot;
	[ SerializeField ] PoolClock pool_clock;
	[ SerializeField ] ClockDataLibrary clock_data_library;

	Clock clock_current;
#endregion

#region Properties
#endregion

#region Unity API
	private void OnEnable()
	{
		list_slot_spawn_empty.AddList( this );
		list_slot.AddList( this );
	}

	private void OnDisable()
	{
		list_slot_spawn_empty.RemoveList( this );
		list_slot.RemoveList( this );
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

	}

	public void OnCurrentClockDeparted()
	{
		clock_current = null;
	}
  //ISlotInterface END

	public void SpawnClock( int level )
	{
		var clock     = pool_clock.GetEntity();
		var clockData = clock_data_library.GetClockData( level );

		clock.UpdateClockData( clockData );
		clock.UpdateVisuals();
		clock.SpawnIntoSpawnSlot( this );

		clock_current = clock;
		list_slot_spawn_empty.RemoveList( this );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
