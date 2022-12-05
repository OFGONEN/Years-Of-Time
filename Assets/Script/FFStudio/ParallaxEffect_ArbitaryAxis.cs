/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public class ParallaxEffect_ArbitaryAxis : MonoBehaviour
	{
#region Fields
		public SharedReferenceNotifier notif_target_reference;
		public float parallax_speed;
		public Vector3 parallax_ratio_axis_X;
		public Vector3 parallax_ratio_axis_Y;
		public Vector3 parallax_ratio_axis_Z;

		Transform target_transform;
		Vector3 position_start;
		Vector3 target_position_start;
#endregion

#region Unity API
		void Start()
		{
			target_transform = notif_target_reference.SharedValue as Transform;

			position_start        = transform.position;
			target_position_start = target_transform.position;
		}

		void Update()
		{
			var diff = target_transform.position - target_position_start;

			var final = position_start;

			final.x += Vector3.Scale( diff, parallax_ratio_axis_X ).ComponentSum();
			final.y += Vector3.Scale( diff, parallax_ratio_axis_Y ).ComponentSum();
			final.z += Vector3.Scale( diff, parallax_ratio_axis_Z ).ComponentSum();

			transform.position = Vector3.MoveTowards( transform.position, final, Time.deltaTime * parallax_speed);
		}
#endregion

#region API
#endregion

#region Implementation
#endregion
	}
}