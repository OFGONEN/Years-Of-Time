/* Created by and for usage of FF Studios (2021). */

namespace FFStudio
{
    [ System.Serializable ]
    public class EventListenerDelegateResponse : EventListener
    {
#region Fields
        public GameEvent gameEvent;

        public UnityEngine.Events.UnityAction response;
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
			response();
		}
#endregion
    }
}