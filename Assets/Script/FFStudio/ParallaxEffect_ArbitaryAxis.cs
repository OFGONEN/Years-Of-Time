/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public class ParallaxEffect_ArbitaryAxis : MonoBehaviour
	{
#region Fields
		public SharedReferenceNotifier targetReference;
		public float parallaxSpeed;
		public Vector3 parallaxRatio_X_Axis;
		public Vector3 parallaxRatio_Y_Axis;
		public Vector3 parallaxRatio_Z_Axis;

		Transform targetTransform;
		Vector3 startPosition;
		Vector3 target_StartPosition;
#endregion

#region Unity API
		void Start()
		{
			targetTransform = ( targetReference.SharedValue as Rigidbody ).transform;

			startPosition = transform.position;
			target_StartPosition = targetTransform.position;
		}

		void Update()
		{
			var diff = targetTransform.position - target_StartPosition;

			var final = startPosition;

			final.x += Vector3.Scale( diff, parallaxRatio_X_Axis ).ComponentSum();
			final.y += Vector3.Scale( diff, parallaxRatio_Y_Axis ).ComponentSum();
			final.z += Vector3.Scale( diff, parallaxRatio_Z_Axis ).ComponentSum();

			transform.position = Vector3.MoveTowards( transform.position, final, Time.deltaTime * parallaxSpeed);
		}
#endregion

#region API
#endregion

#region Implementation
#endregion
	}
}