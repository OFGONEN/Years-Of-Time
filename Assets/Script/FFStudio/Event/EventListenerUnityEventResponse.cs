/* Created by and for usage of FF Studios (2021). */

using UnityEngine.Events;

namespace FFStudio
{
    [ System.Serializable ]
    public class EventListenerUnityEventResponse : EventListener
    {
#region Fields
        public GameEvent gameEvent;
		public UnityEvent response;
#endregion
    
#region API
        public override void OnEnable()
        {
            gameEvent.RegisterListener( this );
        }
        
        public override void OnDisable()
        {
            gameEvent.UnregisterListener( this );
        }

        public override void OnEventRaised()
        {
            response.Invoke();
        }
#endregion    
    }
}