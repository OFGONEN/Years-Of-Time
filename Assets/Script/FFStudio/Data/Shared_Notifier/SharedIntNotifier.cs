/* Created by and for usage of FF Studios (2021). */

using Sirenix.OdinInspector;
using UnityEngine;

namespace FFStudio
{
	[ CreateAssetMenu( fileName = "notif_", menuName = "FF/Data/Shared/Notifier/Integer" ) ]
	public class SharedIntNotifier : SharedDataNotifier< int >
	{
		public void Increment()
		{
			SharedValue++;
		}

		public void Decrement()
		{
			SharedValue--;
		}

		public void OffsetValue( int amount )
		{
			SharedValue += amount;
		}

		public void LoadFromPlayerPrefs()
		{
			SharedValue = PlayerPrefsUtility.Instance.GetInt( name, 0 );
		}

		public void LoadFromPlayerPrefs_DefaultCustom( int customDefaultValue )
		{
			SharedValue = PlayerPrefsUtility.Instance.GetInt( name, customDefaultValue );
		}

		[ Button() ]
		public void SaveToPlayerPrefs()
		{
			PlayerPrefsUtility.Instance.SetInt( name, sharedValue );
		}

		public void SaveToPlayerPrefs( string key )
		{
			PlayerPrefsUtility.Instance.SetInt( key, sharedValue );
		}

	}
}