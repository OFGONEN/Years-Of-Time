/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

namespace FFStudio
{
    public class CameraFollow : MonoBehaviour
    {
#region Fields
    [ Title( "Event Listeners" ) ]
        [ SerializeField ] EventListenerDelegateResponse levelRevealEventListener;
        [ SerializeField ] MultipleEventListenerDelegateResponse levelEndEventListener;
        
    [ Title( "Setup" ) ]
        [ SerializeField ] SharedReferenceNotifier notifier_reference_transform_target;

        Transform transform_target;
        Vector3 followOffset;

        UnityMessage updateMethod;
#endregion

#region Properties
#endregion

#region Unity API
        void OnEnable()
        {
            levelRevealEventListener.OnEnable();
            levelEndEventListener.OnEnable();
        }

        void OnDisable()
        {
            levelRevealEventListener.OnDisable();
            levelEndEventListener.OnDisable();
        }

        void Awake()
        {
            levelRevealEventListener.response = LevelRevealedResponse;
            levelEndEventListener.response    = LevelCompleteResponse;

            updateMethod = ExtensionMethods.EmptyMethod;
        }

        void Update()
        {
            updateMethod();
        }
#endregion

#region API
#endregion

#region Implementation
        void LevelRevealedResponse()
        {
            transform_target = notifier_reference_transform_target.SharedValue as Transform;

            followOffset = transform_target.position - transform.position;

            updateMethod = FollowTarget;
        }

        void LevelCompleteResponse()
        {
            updateMethod = ExtensionMethods.EmptyMethod;
        }

        void FollowTarget()
        {
            // Info: Simple follow logic.
            var player_position = transform_target.position;
            var target_position = transform_target.position - followOffset;

            target_position.x = 0;
            target_position.z = Mathf.Lerp( transform.position.z, target_position.z, Time.deltaTime * GameSettings.Instance.camera_follow_speed_depth );
            transform.position = target_position;
        }
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
    }
}