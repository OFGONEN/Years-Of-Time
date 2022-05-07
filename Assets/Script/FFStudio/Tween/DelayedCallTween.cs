/* Created by and for usage of FF Studios (2021). */

using UnityEngine.Events;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace FFStudio
{
	public class DelayedCallTween : BaseTween
	{
#region Fields
	[ Title( "Parameters" ) ]
		public float duration;
        public UnityEvent onDelayComplete;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
#endregion

#region Implementation
		protected override void CreateAndStartTween( bool isReversed = false )
		{
			recycledTween.Recycle( DOVirtual.DelayedCall( duration, onDelayComplete.Invoke ), OnTweenComplete );

#if UNITY_EDITOR
			recycledTween.Tween.SetId( name + "_ff_delayedCall_tween" );
#endif
		}
#endregion

#region EditorOnly
#if UNITY_EDITOR
#endif
#endregion
	}
}