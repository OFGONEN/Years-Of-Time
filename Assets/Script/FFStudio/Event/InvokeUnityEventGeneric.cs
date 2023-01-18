/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace FFStudio
{
    public class InvokeUnityEventGeneric< Type > : MonoBehaviour
    {
#region Fields
        public string description;
        public UnityEvent< Type > onEvent;
#endregion

#region Unity API
#endregion

#region API
        [ Button ]
        public void Invoke( Type value )
        {
            onEvent.Invoke( value );
        }
#endregion

#region Implementation
#endregion
    }
}