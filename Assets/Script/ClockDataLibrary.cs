/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ CreateAssetMenu( fileName = "shared_clock_data_library", menuName = "FF/Game/Clock Data Library" ) ]
public class ClockDataLibrary : ScriptableObject
{
    [ SerializeField ] ClockData[] clock_data_library;

    public ClockData GetClockData( int level )
    {
		return clock_data_library[ Mathf.Clamp( level - 1, 0, clock_data_library.Length - 1 ) ];
	}
}