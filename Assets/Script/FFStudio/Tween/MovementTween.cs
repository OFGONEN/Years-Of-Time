/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using DG.Tweening;
using Shapes;

namespace FFStudio
{
	public class MovementTween : BaseTween
	{
		public enum MovementMode { Local, World }

#region Fields (Inspector Interface)
	[ Title( "Parameters" ) ]
		public Vector3 deltaPosition;
		public float velocity;
		public MovementMode movementMode;
#endregion

#region Fields (Private)
		private float Duration => Mathf.Abs( deltaPosition.magnitude / velocity );
		
		private Vector3 startPosition;
		private Vector3 targetPosition;
#endregion

#region Properties
#endregion

#region Unity API
		protected override void Awake()
		{
			base.Awake();

			if( movementMode == MovementMode.Local )
				startPosition = transform.localPosition;
			else
				startPosition = transform.position;
		}
#endregion

#region API
#endregion

#region Implementation
		protected override void CreateAndStartTween( bool isReversed = false )
		{
			if( movementMode == MovementMode.Local )
				recycledTween.Recycle( transform.DOLocalMove( isReversed ? -deltaPosition : deltaPosition, Duration ), OnTweenComplete );
			else
				recycledTween.Recycle( transform.DOMove( isReversed ? -deltaPosition : deltaPosition, Duration ), OnTweenComplete );

			recycledTween.Tween
				.SetRelative()
				.SetLoops( loop ? -1 : 0, loopType )
				.SetEase( easing );

#if UNITY_EDITOR
			recycledTween.Tween.SetId( name + "_ff_movement_tween" );
#endif
		}
#endregion

#region EditorOnly
#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			Vector3 startPos = startPosition;

			if( Application.isPlaying )
			{
				if( movementMode == MovementMode.Local && transform.parent != null )
					startPos = transform.parent.TransformPoint( startPosition );
			}
			else
				startPos = transform.position;

			Color color = new Color( 1.0f, 0.75f, 0.0f );
			Draw.LineDashed( startPos, startPos + deltaPosition, new DashStyle( 1 ), 0.125f, LineEndCap.None, color );
			var direction = deltaPosition.normalized;
			var deltaMagnitude = deltaPosition.magnitude;
			var coneLength = 0.2f;
			var conePos = Vector3.Lerp( startPos, startPos + deltaPosition, 1.0f - coneLength / deltaMagnitude );
			Draw.Cone( conePos, deltaPosition.normalized, 0.2f, 0.2f, color );
		}
#endif
#endregion
	}
}