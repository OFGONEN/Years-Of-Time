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

  [ Title( "Component" ) ]
	[ SerializeField ] Disc _disc;
	[ SerializeField ] Rectangle _rectangle_background;
	[ SerializeField ] Rectangle _rectangle_foreground;

  [ Title( "Shared" ) ]
	// [ SerializeField ] ListSpawnSlot list_slot_spawn_all; // This includes all spawn slots
	// [ SerializeField ] ListSpawnSlot list_slot_spawn_empty; // This includes only empty spawn slots
	[ SerializeField ] ListSlot list_slot_all; // This includes all slots
	[ SerializeField ] PoolClock pool_clock;
	[ SerializeField ] ClockDataLibrary clock_data_library;
	[ SerializeField ] SaveSystem system_save;

	Clock clock_current;
#endregion

#region Properties
#endregion

#region Unity API
    private void OnEnable()
    {
		list_slot_all.itemList.Add( this );
	}

    private void OnDisable()
    {
		list_slot_all.itemList.Remove( this );
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
	void LoadClock( int index )
	{
		var clock     = pool_clock.GetEntity();
		var clockData = clock_data_library.GetClockData( index );

		clock.SpawnIntoClockSlot( this, clockData );
		clock_current = clock;
	}

	void CacheInComingClock( Clock incoming )
	{
		clock_current = incoming;
		incoming.OccupyClockSlot();
	}

	void MergeCurrentClock( Clock incoming )
	{
		incoming.ReturnToPool();
		clock_current.UpgradeInClockSlot();
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion

}
