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

		public void LoadFromPlayerPrefs()
		{
			SharedValue = PlayerPrefsUtility.Instance.GetFloat( name, 0 );
		}

		public void LoadFromPlayerPrefs_DefaultCustom( float customDefaultValue )
		{
			SharedValue = PlayerPrefsUtility.Instance.GetFloat( name, customDefaultValue );
		}

		public void SaveToPlayerPrefs()
		{
			PlayerPrefsUtility.Instance.SetFloat( name, sharedValue );
		}

		public void SaveToPlayerPrefs( string key )
		{
			PlayerPrefsUtility.Instance.SetFloat( key, sharedValue );
		}
	}
}