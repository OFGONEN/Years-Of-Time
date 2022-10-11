/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace FFStudio
{
    [ TypeInfoBox( "This component simply relays calls to Unity's known FixedUpdate() callback to other desired Systems-as-Assets or Monobehaviours, by use of Unity Events." ) ]
	public class RelayFixedUpdateMessage : MonoBehaviour
	{
#region Fields
    [ Title( "Relayed Unity Events" ) ]
        public UnityEvent onFixedUpdateEvent;
#endregion

#region Properties
#endregion

#region Unity API
        void FixedUpdate()
        {
			onFixedUpdateEvent.Invoke();
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