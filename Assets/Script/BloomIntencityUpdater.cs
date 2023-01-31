/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

public class BloomIntencityUpdater : MonoBehaviour
{
#region Fields
    [ SerializeField ] TweenableFloatNotifier notif_tweenable_value;
    [ SerializeField ] SharedFloatNotifier notif_intensity;
    [ SerializeField ] Vector2 intensity_range;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnTweenableValueChanged()
    {
		notif_intensity.SharedValue = intensity_range.ReturnProgress( notif_tweenable_value.ValueProgress );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
