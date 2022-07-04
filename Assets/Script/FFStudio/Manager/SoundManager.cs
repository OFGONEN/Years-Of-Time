/* Created by and for usage of FF Studios (2021). */

using System.Collections.Generic;
using UnityEngine;

namespace FFStudio
{
    public class SoundManager : MonoBehaviour
    {
        public SoundEvent[] soundEvents;

        List< EventListenerDelegateResponse > soundEventListeners;
        Dictionary< int, AudioSource > audioSources;
        
#region Unity API
        void Awake()
		{
			soundEventListeners = new List< EventListenerDelegateResponse >( soundEvents.Length );
			audioSources = new Dictionary< int, AudioSource >( soundEvents.Length );

			for( int i = 0; i < soundEvents.Length; i++ )
			{
				var audioComponent = gameObject.AddComponent<AudioSource>();
				audioComponent.playOnAwake = false;
				audioComponent.loop = false;

				var audioEvent = soundEvents[ i ];

				audioComponent.clip = audioEvent.audioClip;
				audioSources.Add( audioEvent.GetInstanceID(), audioComponent );

				var listener = new EventListenerDelegateResponse();
				listener.gameEvent = audioEvent;
				soundEventListeners.Add( listener );
			}
		}

		void OnEnable()
		{
			for( int i = 0; i < soundEvents.Length; i++ )
				soundEventListeners[ i ].OnEnable();
		}

		void OnDisable()
		{
			for( int i = 0; i < soundEvents.Length; i++ )
				soundEventListeners[ i ].OnDisable();
		}

		void Start()
		{
			foreach( var soundEventListener in soundEventListeners )
				soundEventListener.response = ( () => PlaySound( soundEventListener.gameEvent.GetInstanceID() ) );
        }
#endregion

#region Implementation
        void PlaySound( int instanceId )
        {
            AudioSource source;
			audioSources.TryGetValue( instanceId, out source );

			if( source != null && !source.isPlaying )
				source.Play();
        }
    }
#endregion
}