/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace FFStudio
{
    public class InvokeUnityEvent : MonoBehaviour
    {
#region Fields
        public string description;
        public UnityEvent onEvent;
#endregion

#region Unity API
#endregion

#region API
        [ Button ]
        public void Invoke()
        {
            onEvent.Invoke();
        }
#endregion

#region Implementation
#endregion
    }
}
