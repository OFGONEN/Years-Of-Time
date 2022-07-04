/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using DG.Tweening;
using UnityEditor;
using Sirenix.OdinInspector;

namespace FFStudio
{
	public class CameraTweenInterface : MonoBehaviour
	{
#region Fields
		[ SerializeField ] SharedReferenceNotifier camera_reference;
		[ SerializeField ] CameraTweenData[] cameraTweenData_array;

		int lastIndex = -1;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
		public void PlayCameraTween( int index )
		{
			if( index == lastIndex ) 
			{
				if( cameraTweenData_array[ index ].event_complete_alwaysInvoke )
					cameraTweenData_array[ index ].event_complete.Invoke();

				return;
			}

			lastIndex = index;

			var camera = camera_reference.SharedValue as Transform;
			var data   = cameraTweenData_array[ index ];

			var sequence = DOTween.Sequence();

			if( data.change_position )
				sequence.Join( camera.DOMove( data.target_transform.position, data.tween_duration ).SetEase( data.ease_position ) );

			if( data.change_rotation )
				sequence.Join( camera.DORotate( data.target_transform.eulerAngles, data.tween_duration ).SetEase( data.ease_rotation ) );

			sequence.OnComplete( data.event_complete.Invoke );
		}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
		Vector3 default_position;
		Vector3 default_rotation;

		[ Button() ]	
		void ReturnDefaultPosition()
		{
			var camera = Camera.main;
			camera.transform.position    = default_position;
			camera.transform.eulerAngles = default_rotation;
		}

		[ Button() ]
		void SetCameraPosition( int index )
		{
			var camera = Camera.main;

			default_position = camera.transform.position;
			default_rotation = camera.transform.eulerAngles;

			var data = cameraTweenData_array[ index ];

			camera.transform.position = data.target_transform.position;
			camera.transform.rotation = data.target_transform.rotation;
		}


		void OnDrawGizmos()
		{
			for( var i = 0; i < cameraTweenData_array.Length; i++ )
			{
				var data = cameraTweenData_array[ i ];

				if( data.target_transform == null ) continue;

				Handles.ArrowHandleCap( -1, data.target_transform.position, data.target_transform.rotation, 0.5f, EventType.Repaint );
				Handles.Label( data.target_transform.position, data.target_transform.name );
			}
		}
#endif
#endregion
	}
}