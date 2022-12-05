/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public class ParallaxEffect : MonoBehaviour
	{
#region Fields
		public SharedReferenceNotifier notif_target_reference;
		public Vector3 parallax_ratio;
		public float parallax_speed;

		Transform target_transform;
		Vector3 position_start;
		Vector3 target_position;
#endregion

#region Unity API
		void Start()
		{
			target_transform = notif_target_reference.SharedValue as Transform;

			position_start  = transform.position;
			target_position = target_transform.position;
		}

		void Update()
		{
			var diff = target_transform.position - target_position;
			diff.Scale( parallax_ratio );

			transform.position = Vector3.MoveTowards( transform.position, position_start + diff, Time.deltaTime * parallax_speed );
		}
#endregion

#region API
#endregion

#region Implementation
#endregion
	}
}