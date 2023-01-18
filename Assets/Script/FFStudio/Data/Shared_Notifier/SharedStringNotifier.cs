/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	[ CreateAssetMenu( fileName = "notif_", menuName = "FF/Data/Shared/Notifier/String" ) ]
	public class SharedStringNotifier : SharedDataNotifier< string >
	{
		public void LoadFromPlayerPrefs()
		{
			SharedValue = PlayerPrefsUtility.Instance.GetString( name, string.Empty );
		}

		public void LoadFromPlayerPrefs_DefaultCustom( string customDefaultValue )
		{
			SharedValue = PlayerPrefsUtility.Instance.GetString( name, customDefaultValue );
		}

		public void SaveToPlayerPrefs()
		{
			PlayerPrefsUtility.Instance.SetString( name, sharedValue );
		}

		public void SaveToPlayerPrefs( string key )
		{
			PlayerPrefsUtility.Instance.SetString( key, sharedValue );
		}

	}
}