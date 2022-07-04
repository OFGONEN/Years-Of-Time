/* Created by and for usage of FF Studios (2021). */

using UnityEditor;
using UnityEngine;

namespace FFStudio
{
    [ CreateAssetMenu( fileName = "utility_playerPrefs", menuName = "FF/Utility/PlayerPrefs" ) ]
	public class PlayerPrefsUtility : ScriptableObject
    {
#region Fields
#if UNITY_EDITOR
		PlayerPrefsTracker_Base PlayerPrefsTracker => ( PlayerPrefsTracker_Base )AssetDatabase.LoadAssetAtPath( "Assets/Editor/tracker_playerPrefs.asset",
																											    typeof( PlayerPrefsTracker_Base ) );
#endif

		string key;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
        public void DeleteAll()
        {
#if UNITY_EDITOR
			PlayerPrefsTracker.DeleteAll();
#endif
			PlayerPrefs.DeleteAll();
		}
		
		public void SetKey( string key )
		{
			this.key = key;
		}

		public void SetInt( string key, int value )
		{
#if UNITY_EDITOR
			PlayerPrefsTracker.SetInt( key, value );
#endif
			PlayerPrefs.SetInt( key, value );
		}

		public void SetInt( SharedIntNotifier notifier )
		{
			SetInt( key, notifier.sharedValue );
		}

		public int GetInt( string key, int defaultValue )
		{
			return PlayerPrefs.GetInt( key, defaultValue );
		}
		
		public void GetInt( SharedIntNotifier notifier )
		{
			notifier.SetValue_NotifyAlways( PlayerPrefs.GetInt( key, 0 ) );
		}

        public void SetFloat( string key, float value )
        {
#if UNITY_EDITOR
			PlayerPrefsTracker.SetFloat( key, value );
#endif
			PlayerPrefs.SetFloat( key, value );
		}

		public void SetFloat( SharedFloatNotifier notifier )
		{
			SetFloat( key, notifier.sharedValue );
		}

		public float GetFloat( string key, float defaultValue )
		{
			return PlayerPrefs.GetFloat( key, defaultValue );
		}

		public void GetFloat( SharedFloatNotifier notifier )
		{
			notifier.SetValue_NotifyAlways( PlayerPrefs.GetFloat( key, 0.0f ) );
		}

		public void SetString( string key, string value )
		{
#if UNITY_EDITOR
			PlayerPrefsTracker.SetString( key, value );
#endif
			PlayerPrefs.SetString( key, value );
		}

		public void SetString( SharedStringNotifier notifier )
		{
			SetString( key, notifier.sharedValue );
		}

		public string GetString( string key, string defaultValue )
		{
			return PlayerPrefs.GetString( key, defaultValue );
		}

		public void GetString( SharedStringNotifier notifier )
		{
			notifier.SetValue_NotifyAlways( PlayerPrefs.GetString( key, "" ) );
		}
#endregion

#region Fields (Singleton Related)
        static PlayerPrefsUtility instance;

        delegate PlayerPrefsUtility ReturnPlayerPrefsUtility();
        static ReturnPlayerPrefsUtility returnInstance = LoadInstance;

		public static PlayerPrefsUtility Instance => returnInstance();
#endregion

#region Implementation
        static PlayerPrefsUtility LoadInstance()
		{
			if( instance == null )
				instance = Resources.Load< PlayerPrefsUtility >( "utility_playerPrefs" );

			returnInstance = ReturnInstance;

			return instance;
		}

		static PlayerPrefsUtility ReturnInstance()
        {
            return instance;
        }
#endregion
    }
}