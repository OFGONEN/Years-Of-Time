/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "shared_clock_data_", menuName = "FF/Game/Clock Data" ) ]
public class ClockData : ScriptableObject
{
#region Fields
  [ Title( "Attributes" ) ]
    [ Tooltip( "Level" ), SerializeField ] int clock_level;
    [ Tooltip( "Reduces the Duration of an Item" ), SerializeField ] float clock_speed;
    [ SerializeField ] float clock_hand_speed_second;
    [ SerializeField ] float clock_hand_speed_minute;
    [ SerializeField ] float clock_hand_speed_hour;

  [ Title( "Visual" ) ]
    [ SerializeField ] Material clock_material;
    [ SerializeField ] Mesh clock_circle;
    [ SerializeField ] Mesh clock_hand_second;
#endregion

#region Properties
	public float ClockSpeed           => clock_speed;
	public float ClockHandSecondSpeed => clock_speed;
	public float ClockHandMinuteSpeed => clock_speed;
	public float ClockHandHourSpeed   => clock_speed;
	public Material ClockMaterial     => clock_material;
	public Mesh ClockCircleMesh       => clock_circle;
	public Mesh ClockSecondHandMesh   => clock_hand_second;
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}