/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ CreateAssetMenu( fileName = "shared_clock_data_library", menuName = "FF/Game/Clock Data Library" ) ]
public class ClockDataLibrary : ScriptableObject
{
    [ SerializeField ] ClockData[] clock_data_library;

	public int ClockMaxLevel => clock_data_library.Length - 1;

	public ClockData GetClockData( int index )
    {
		return clock_data_library[ Mathf.Clamp( index, 0, ClockMaxLevel ) ];
	}
}