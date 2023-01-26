/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClockSpeed : MonoBehaviour
{
#region Fields
    public Transform clock_hand_hour;
    public Transform clock_hand_minute;
    public Transform clock_hand_second;

    public float clock_second_speed;
    public float clock_minute_cofactor;
    public float clock_hour_cofactor;
#endregion

#region Properties
#endregion

#region Unity API
    private void Update()
    {
		clock_hand_second.Rotate( Vector3.forward * -1f * clock_second_speed * Time.deltaTime, Space.Self );
		clock_hand_minute.Rotate( Vector3.forward * -1f * clock_second_speed * clock_minute_cofactor * Time.deltaTime, Space.Self );
		clock_hand_hour.Rotate( Vector3.forward * -1f * clock_second_speed * clock_hour_cofactor * Time.deltaTime, Space.Self );
	}
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
