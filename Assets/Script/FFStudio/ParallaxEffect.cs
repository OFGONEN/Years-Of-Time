/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public class ParallaxEffect : MonoBehaviour
	{
#region Fields
		public SharedReferenceNotifier targetReference;
		public Vector3 parallaxRatio;
		public float parallaxSpeed;

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
			diff.Scale( parallaxRatio );

			transform.position = Vector3.MoveTowards( transform.position, startPosition + diff, Time.deltaTime * parallaxSpeed );
		}
#endregion

#region API
#endregion

#region Implementation
#endregion
	}
}