/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "shared_clock_data_", menuName = "FF/Game/Clock Data" ) ]
public class ClockData : ScriptableObject
{
#region Fields
    [ Tooltip( "Reduces the Duration of an Item" ), SerializeField ] float clock_speed;
    [ SerializeField ] float clock_hand_speed_second;
    [ SerializeField ] float clock_hand_speed_minute;
    [ SerializeField ] float clock_hand_speed_hour;
    [ SerializeField ] Material clock_material;
    [ SerializeField ] Mesh clock_circle;
    [ SerializeField ] Mesh clock_hand_second;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
