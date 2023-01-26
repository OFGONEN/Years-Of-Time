/* Created by and for usage of FF Studios (2021). */

using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FFStudio
{
    public class LevelManager : MonoBehaviour
    {
#region Fields
      [ Title( "Shared" ) ]
        [ SerializeField ] ClockDataLibrary clock_data_library;
        [ SerializeField ] PoolClock pool_clock;
        
      [ Header( "Level Releated" ) ]
        public SharedProgressNotifier notifier_progress;

      [ Header( "Fired Events" ) ]
        public GameEvent levelFailedEvent;
        public GameEvent levelCompleted;
#endregion

#region UnityAPI
#endregion

#region API
        // Info: Called from Editor.
        public void LevelLoadedResponse()
        {
			var levelData = CurrentLevelData.Instance.levelData;
            // Set Active Scene.
			if( levelData.scene_overrideAsActiveScene )
				SceneManager.SetActiveScene( SceneManager.GetSceneAt( 1 ) );
            else
				SceneManager.SetActiveScene( SceneManager.GetSceneAt( 0 ) );
		}

        // Info: Called from Editor.
        public void LevelRevealedResponse()
        {

        }

        // Info: Called from Editor.
        public void LevelStartedResponse()
        {

        }

        public void OnClockSpawn( int level )
        {

        }
#endregion

#region Implementation
#endregion
    }
}