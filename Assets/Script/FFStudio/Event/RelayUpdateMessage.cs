/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace FFStudio
{
    [ TypeInfoBox( "This component simply relays calls to Unity's known Update() callback to other desired Systems-as-Assets or Monobehaviours, by use of Unity Events." ) ]
	public class RelayUpdateMessage : MonoBehaviour
	{
#region Fields
    [ Title( "Relayed Unity Events" ) ]
        public UnityEvent onUpdateEvent;
#endregion

#region Properties
#endregion

#region Unity API
        void Update()
        {
			onUpdateEvent.Invoke();
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