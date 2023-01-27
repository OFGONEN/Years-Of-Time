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
        [ SerializeField ] ListSpawnSlot list_slot_spawn;
		[ SerializeField ] SaveSystem system_save;
		[ SerializeField ] ClockPurchase notif_clock_purchase;
		[ SerializeField ] ClockPurchaseCondition notif_clock_purchase_condition;
        
      [ Header( "Level Releated" ) ]
        public SharedProgressNotifier notifier_progress;

      [ Header( "Fired Events" ) ]
        public GameEvent levelFailedEvent;
        public GameEvent levelCompleted;
		public GameEvent event_clock_purchase;
#endregion

#region UnityAPI
		private void Start()
		{
			var spawnSlotArray = system_save.SaveData.slot_spawn_clock_level_array;
			var isAvailable = false;

			for( var i = 0; i < spawnSlotArray.Length; i++ )
			{
				isAvailable |= spawnSlotArray[ i ] == 0;
			}

			notif_clock_purchase_condition.SetConditionSlot( isAvailable );
		}
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

        [ Button() ]
        public void OnClockSpawn()
        {
			var spawnSlot = list_slot_spawn.itemList.ReturnRandom();
			spawnSlot.SpawnClock( notif_clock_purchase.PurchaseLevel );

			notif_clock_purchase_condition.SetConditionSlot( list_slot_spawn.itemList.Count > 0 );
			event_clock_purchase.Raise();
		}
#endregion

#region Implementation
#endregion
    }
}