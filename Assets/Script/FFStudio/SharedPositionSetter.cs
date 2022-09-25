/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public class SharedPositionSetter : MonoBehaviour
	{
#region Fields
		[ SerializeField ] SharedVector3Notifier notif_position;
		[ SerializeField ] Transform _transform;
#endregion

#region Unity API
		void OnEnable()
		{
			notif_position.SharedValue = _transform.position;
		}

		void OnDisable()
		{
			notif_position.SharedValue = Vector3.zero;
		}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}