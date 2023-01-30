/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FFStudio;
using Sirenix.OdinInspector;

public class TestSpawnAnimation : MonoBehaviour
{
#region Fields
    public PunchScaleTween punchScaleTween; 

    RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    [ Button() ]
    public void DoSpawn()
    {
		recycledTween.Recycle( punchScaleTween.CreateTween( transform ) );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
