/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace FFStudio
{
	public class ScaleTween : BaseTween
	{
#region Fields (Inspector Interface)
	[ Title( "Parameters" ) ]
    	public Vector3 targetScale;
		public float duration;
#endregion

#region Fields (Inspector Interface)
		private Vector3 startScale;
#endregion

#region Properties
#endregion

#region Unity API
		protected override void Awake()
		{
			base.Awake();

            startScale = transform.localScale;
		}
#endregion

#region API
#endregion

#region Implementation
		protected override void CreateAndStartTween( bool isReversed = false )
		{
			recycledTween.Recycle( transform.DOScale( targetScale, duration ), OnTweenComplete );

			recycledTween.Tween.SetEase( easing )
				 .SetLoops( loop ? -1 : 0, loopType );

#if UNITY_EDITOR
			recycledTween.Tween.SetId( name + "_ff_scale_tween" );
#endif
		}
#endregion

#region EditorOnly
#if UNITY_EDITOR
#endif
#endregion
	}
}