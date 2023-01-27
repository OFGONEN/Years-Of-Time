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
	[ SerializeField ] PoolClock pool_clock;
	[ SerializeField ] ClockDataLibrary clock_data_library;
#endregion

#region Properties
#endregion

#region Unity API
	private void OnEnable()
	{
		list_slot_spawn_empty.AddList( this );
	}

	private void OnDisable()
	{
		list_slot_spawn_empty.RemoveList( this );
	}
#endregion

#region API
    public Vector3 GetPosition()
    {
		return transform.position;
	}

	public void SpawnClock( int level )
	{
		var clock     = pool_clock.GetEntity();
		var clockData = clock_data_library.GetClockData( level );

		clock.UpdateClockData( clockData );
		clock.UpdateVisuals();
		clock.SetIdlePosition( transform.position );
		clock.SpawnIntoSpawnSlot(); 

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
