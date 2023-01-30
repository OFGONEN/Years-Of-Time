/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Lean.Touch;

[ CreateAssetMenu( fileName = "shared_input_finger_position", menuName = "FF/Game/Input - Finger Position" ) ]
public class InputFingerPosition : SharedVector2
{
    public void OnLeanFingerUpdate( LeanFinger finger )
    {
		sharedValue = finger.ScreenPosition;
	}
}