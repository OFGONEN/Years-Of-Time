/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class TestClockReduceSize : MonoBehaviour
{
#region Fields
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    [ Button() ]
    public void ReduceSize( float size, float duration, Ease ease )
    {
		transform.DOScale( Vector3.one * size, duration ).SetEase( ease );
	}

    [ Button() ]
    public void RevertSize()
    {
		transform.localScale = Vector3.one;
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
