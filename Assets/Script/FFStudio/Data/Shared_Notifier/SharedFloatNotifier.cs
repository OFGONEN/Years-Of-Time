/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	[ CreateAssetMenu( fileName = "notif_", menuName = "FF/Data/Shared/Notifier/Float" ) ]
	public class SharedFloatNotifier : SharedDataNotifier< float >
	{
		public void IncrementBy1()
		{
			SharedValue += 1.0f;
		}

		public void DecrementBy1()
		{
			SharedValue -= 1.0f;
		}

		public void IncrementByAmount( float amount )
		{
			SharedValue += amount;
		}

		public void DecrementByAmount( float amount )
		{
			SharedValue -= amount;
		}
	}
}