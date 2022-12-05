/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace FFStudio
{
	public class UIMovementTweenData : TweenData
	{
#region Fields
	[ Title( "UI Movement Tween" ) ]
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ) ] public bool useNotifier = true;
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ) ] public Vector3 offset_world = Vector3.zero;
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ), LabelText( "Target" ), ShowIf( "useNotifier" ) ] public SharedReferenceNotifier notifier_transform_target;
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ), LabelText( "Target" ), HideIf( "useNotifier" ) ] public Transform transform_target;
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ) ] public float duration;
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ) ] public SharedReferenceNotifier notifier_transform_camera;
#endregion

#region Properties
		public Transform Target => useNotifier ? notifier_transform_target.sharedValue as Transform : transform_target;
#endregion

#region Unity API
#endregion

#region API
		public override Tween CreateTween( bool isReversed = false )
		{
			var camera = ( notifier_transform_camera.sharedValue as Transform ).GetComponent< Camera >();

			var targetScreenPosition = camera.WorldToScreenPoint( Target.position + offset_world );

			recycledTween.Recycle( transform.DOMove( isReversed ? -targetScreenPosition : targetScreenPosition, duration ),
								   unityEvent_onCompleteEvent.Invoke );

#if UNITY_EDITOR
			recycledTween.Tween.SetId( "_ff_ui_movement_tween___" + description );
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