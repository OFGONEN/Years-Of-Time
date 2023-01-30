/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using FFStudio;
using Sirenix.OdinInspector;

public class TestItemProduce : MonoBehaviour
{
#region Fields
    public Image item_foreground;
    public float item_fill_duration;
    public Ease item_fill_ease;

	public PunchScaleTween punchScaleTween;
	public UnityEvent item_fill_complete;

    RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    [ Button() ]
    public void ItemStartToFill()
    {
		item_foreground.DOFillAmount( 1, item_fill_duration )
			.SetEase( item_fill_ease )
			.OnComplete( item_fill_complete.Invoke );
	}

    [ Button() ]
    public void ItemDoPunchScale()
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
