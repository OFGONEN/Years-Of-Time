/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace FFStudio
{
	public class UnityMessageEvents : MonoBehaviour
	{
#region Fields
    [ Title( "Setup" ) ]
        public UnityEvent onEnableEvent;
        public UnityEvent onDisableEvent;
        public UnityEvent onAwakeEvent;
        public UnityEvent onStartEvent;
#endregion

#region Properties
#endregion

#region Unity API
        private void OnEnable()
        {
			onEnableEvent.Invoke();
		}

        private void OnDisable()
        {
			onDisableEvent.Invoke();
		}

        private void Awake()
        {
			onAwakeEvent.Invoke();
		}

        private void Start()
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