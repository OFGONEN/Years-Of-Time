/* Created by and for usage of FF Studios (2021). */

using UnityEngine.Events;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace FFStudio
{
	public class DelayedCallTweenData : TweenData
	{
#region Fields
	[ Title( "Delayed Call Tween" ) ]
        [ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ), SuffixLabel( "Seconds" ) ] public float duration;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
#endregion

#region Implementation
		protected override void CreateAndStartTween( UnityMessage onComplete = null, bool isReversed = false )
		{
			recycledTween.Recycle( DOVirtual.DelayedCall( duration, ExtensionMethods.EmptyMethod ), onComplete );

#if UNITY_EDITOR
			recycledTween.Tween.SetId( "_ff_delayedCall_tween___" + description );
#endif

			base.CreateAndStartTween( onComplete, isReversed );
		}
#endregion

#region EditorOnly
#if UNITY_EDITOR
		public override bool HideBaseClassLoopCheckBox() => true;
#endif
#endregion
	}
}