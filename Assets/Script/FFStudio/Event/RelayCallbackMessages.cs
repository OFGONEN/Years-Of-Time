/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace FFStudio
{
    [ TypeInfoBox( "This component simply relays calls to Unity's known callbacks to other desired Systems-as-Assets or Monobehaviours, by use of Unity Events." ) ]
	public class RelayCallbackMessages : MonoBehaviour
	{
#region Fields
    [ Title( "Relayed Unity Events" ) ]
        public UnityEvent onEnableEvent;
        public UnityEvent onDisableEvent;
        public UnityEvent onAwakeEvent;
        public UnityEvent onStartEvent;
#endregion

#region Properties
#endregion

#region Unity API
        void OnEnable()
        {
			onEnableEvent.Invoke();
		}

        void OnDisable()
        {
			onDisableEvent.Invoke();
		}

        void Awake()
        {
			onAwakeEvent.Invoke();
		}

        void Start()
        {
			onStartEvent.Invoke();
		}
#endregion

#region API
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}