/* Created by and for usage of FF Studios (2021). */

using System.IO;
using UnityEditor;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using System.Reflection;

namespace FFEditor
{
	public static class FFShortcutUtility
	{
		static private TransformData currentTransformData;
		static private string path_playerPrefsTracker = "Assets/Editor/tracker_playerPrefs.asset";

		[ MenuItem( "FFShortcut/TakeScreenShot #F12" ) ]
		public static void TakeScreenShot()
		{
			int counter = 0;
			var path = Path.Combine( Application.dataPath, "../", "ScreenShot_" + counter + ".png" );

			while( File.Exists( path ) ) // If file is not exits new screen shot will be a new file
			{
				counter++;
				path = Path.Combine( Application.dataPath, "../", "ScreenShot_" + counter + ".png" ); // ScreenShot_1.png
			}

			ScreenCapture.CaptureScreenshot( "ScreenShot_" + counter + ".png" );
			AssetDatabase.SaveAssets();

			Debug.Log( "ScreenShot Taken: " + "ScreenShot_" + counter + ".png" );
		}
		
		[ MenuItem( "FFShortcut/Select PlayerPrefsTracker _F7" ) ]
		static private void SelectPlayerPrefsTracker()
		{
			var tracker = AssetDatabase.LoadAssetAtPath( path_playerPrefsTracker, typeof( ScriptableObject ) );
			( tracker as PlayerPrefsTracker ).Refresh();
			Selection.SetActiveObjectWithContext( tracker, tracker );
		}

		[MenuItem( "FFShortcut/Delete Save File _F8" )]
		static void DeleteSaveFile()
		{
			if( File.Exists( ExtensionMethods.SAVE_PATH + "save.txt" ) )
			{
				FFStudio.FFLogger.Log( "SaveSystem: Found save file. Deleting it." );
				File.Delete( ExtensionMethods.SAVE_PATH + "save.txt" );
			}

			if( File.Exists( ExtensionMethods.SAVE_PATH + "save.txt" ) )
				FFStudio.FFLogger.LogError( "SaveSystem: Failed to delete save file." );
			else
				FFStudio.FFLogger.Log( "SaveSystem: Successfully deleted save file." );
		}

		[ MenuItem( "FFShortcut/Delete PlayerPrefs _F9" ) ]
		static private void ResetPlayerPrefs()
		{
			PlayerPrefsUtility.Instance.DeleteAll();
			Debug.Log( "PlayerPrefs Deleted" );
		}

		[ MenuItem( "FFShortcut/Previous Level _F10" ) ]
		static private void PreviousLevel()
		{
			var currentLevel = PlayerPrefs.GetInt( "Level" );

			currentLevel = Mathf.Max( currentLevel - 1, 1 );

			PlayerPrefs.SetInt( "Level", currentLevel );
			PlayerPrefs.SetInt( "Consecutive Level", currentLevel );

			Debug.Log( "Level Set:" + currentLevel );
		}

		[ MenuItem( "FFShortcut/Next Level _F11" ) ]
		static private void NextLevel()
		{
			var nextLevel = PlayerPrefs.GetInt( "Level" ) + 1;

			PlayerPrefs.SetInt( "Level", nextLevel );
			PlayerPrefs.SetInt( "Consecutive Level", nextLevel );

			Debug.Log( "Level Set:" + nextLevel );

		}

		[ MenuItem( "FFShortcut/Save All Assets _F12" ) ]
		static private void SaveAllAssets()
		{
			AssetDatabase.SaveAssets();
			Debug.Log( "AssetDatabase Saved" );
		}

#if UNITY_EDITOR_WIN
		[ MenuItem( "FFShortcut/Select Game Settings &1" ) ]
#elif UNITY_EDITOR_OSX
		[ MenuItem( "FFShortcut/Select Game Settings %1" ) ]
#endif
		static private void SelectGameSettings()
		{
			var gameSettings = Resources.Load( "game_settings" );

			Selection.SetActiveObjectWithContext( gameSettings, gameSettings );
		}

#if UNITY_EDITOR_WIN
		[ MenuItem( "FFShortcut/Select Level Data &2" ) ]
#elif UNITY_EDITOR_OSX
		[ MenuItem( "FFShortcut/Select Level Data %2" ) ]
#endif
		static private void SelectLevelData()
		{
			var levelData = Resources.Load( "level_data_1" );

			Selection.SetActiveObjectWithContext( levelData, levelData );
		}

#if UNITY_EDITOR_WIN
		[ MenuItem( "FFShortcut/Select App Scene &3" ) ]
#elif UNITY_EDITOR_OSX
		[ MenuItem( "FFShortcut/Select App Scene %3" ) ]
#endif
		static private void SelectAppScene()
		{
			var appScene = AssetDatabase.LoadAssetAtPath( "Assets/Scenes/app.unity", typeof( SceneAsset ) );

			Selection.SetActiveObjectWithContext( appScene, appScene );
		}

#if UNITY_EDITOR_WIN
		[ MenuItem( "FFShortcut/Select Play Mode Settings &4" ) ]
#elif UNITY_EDITOR_OSX
		[ MenuItem( "FFShortcut/Select Play Mode Settings %4" ) ]
#endif
		static private void SelectPlayModeSettings()
		{
			var playModeSettings = AssetDatabase.LoadAssetAtPath( "Assets/Editor/PlayModeUtilitySettings.asset", typeof( ScriptableObject ) );

			Selection.SetActiveObjectWithContext( playModeSettings, playModeSettings );
		}

#if UNITY_EDITOR_WIN
		[ MenuItem( "FFShortcut/Copy Global Transform &c" ) ]
#elif UNITY_EDITOR_OSX
		[ MenuItem( "FFShortcut/Copy Global Transform %c" ) ]
#endif
		static private void CopyTransform()
		{
			currentTransformData = Selection.activeGameObject.transform.GetTransformData();
		}

#if UNITY_EDITOR_WIN
		[ MenuItem( "FFShortcut/Paste Global Transform &v" ) ]
#elif UNITY_EDITOR_OSX
		[ MenuItem( "FFShortcut/Paste Global Transform %v" ) ]
#endif
		static private void PasteTransform()
		{
			var gameObject = Selection.activeGameObject.transform;
			gameObject.SetTransformData( currentTransformData );
		}

#if UNITY_EDITOR_WIN
		[ MenuItem( "FFShortcut/Kill All Tweens %#t" ) ]
#elif UNITY_EDITOR_OSX
		[ MenuItem( "FFShortcut/Kill All Tweens ^#t" ) ]
#endif
		private static void KillAllTweens()
		{
			DOTween.KillAll();
			FFLogger.Log( "[FF] DOTween: Kill All" );
		}

#if UNITY_EDITOR_WIN
		[ MenuItem( "FFShortcut/Clear Console %#x" ) ]
#elif UNITY_EDITOR_OSX
		[ MenuItem( "FFShortcut/Clear Console ^#x" ) ]
#endif
		private static void ClearLog()
		{
			var assembly = Assembly.GetAssembly( typeof( UnityEditor.Editor ) );
			var type = assembly.GetType( "UnityEditor.LogEntries" );
			var method = type.GetMethod( "Clear" );
			method.Invoke( new object(), null );
		}
	}
}