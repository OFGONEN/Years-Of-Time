/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace FFStudio
{
	public class ScaleTweenData : TweenData
	{
#region Fields
	[ Title( "Scale Tween" ) ]
    	[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ) ] public Vector3 targetScale;
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ) ] public float duration;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
		public override Tween CreateTween( bool isReversed = false )
		{
			recycledTween.Recycle( transform.DOScale( targetScale, duration ), unityEvent_onCompleteEvent.Invoke );
			
#if UNITY_EDITOR
			recycledTween.Tween.SetId( "_ff_scale_tween___" + description );
#endif

			return base.CreateTween();
		}
#endregion

#region Implementation
#endregion

#region EditorOnly
#if UNITY_EDITOR
#endif
#endregion
	}
}