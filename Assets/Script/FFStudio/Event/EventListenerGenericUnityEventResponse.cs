/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FFStudio
{
	[ System.Serializable ]
	public abstract class EventListenerGenericUnityEventResponseBase : EventListener
	{
	}

	public abstract class EventListenerGenericUnityEventResponse< GameEventType, ArgumentType > : EventListenerGenericUnityEventResponseBase where GameEventType: GameEvent
	{
		public GameEventType gameEvent;
        public UnityEngine.Events.UnityEvent< ArgumentType > unityEvent;

		public override void OnEnable()
		{
			gameEvent.RegisterListener( this );
		}

		public override void OnDisable()
		{
			gameEvent.UnregisterListener( this );
		}
	}

	public class BasicGameEventResponse : EventListenerGenericUnityEventResponse< GameEvent, int >
	{
		public override void OnEventRaised()
		{
			unityEvent.Invoke( 0 );
		}
	}

	public class BoolGameEventResponse : EventListenerGenericUnityEventResponse< BoolGameEvent, bool >
	{
		public override void OnEventRaised()
		{
			unityEvent.Invoke( gameEvent.eventValue );
		}
	}

	public class IntGameEventResponse : EventListenerGenericUnityEventResponse< IntGameEvent, int >
	{
		public override void OnEventRaised()
		{
			unityEvent.Invoke( gameEvent.eventValue );
		}
	}

	public class FloatGameEventResponse : EventListenerGenericUnityEventResponse< FloatGameEvent, float >
	{
		public override void OnEventRaised()
		{
			unityEvent.Invoke( gameEvent.eventValue );
		}
	}

	public class ReferenceGameEventResponse : EventListenerGenericUnityEventResponse< ReferenceGameEvent, object >
	{
		public override void OnEventRaised()
		{
			unityEvent.Invoke( gameEvent.eventValue );
		}
	}
}