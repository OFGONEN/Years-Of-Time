/* Created by and for usage of FF Studios (2021). */

using UnityEngine.Events;

namespace FFStudio
{
    [ System.Serializable ]
	public class MultipleEventListenerUnityEventResponse : EventListener
	{
#region Fields
		public GameEvent[] gameEvent_array;
		public UnityEvent response;
#endregion

#region API
        public override void OnEnable()
        {
			for( int i = 0; i < gameEvent_array.Length; i++ )
				gameEvent_array[ i ].RegisterListener( this );
        }
        
        public override void OnDisable()
        {
			for( int i = 0; i < gameEvent_array.Length; i++ )
				gameEvent_array[ i ].UnregisterListener( this );
        }

        public override void OnEventRaised()
        {
			response.Invoke();
		}
#endregion
	}
}