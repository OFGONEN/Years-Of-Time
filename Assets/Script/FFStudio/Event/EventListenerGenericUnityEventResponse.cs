/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FFStudio
{
	public abstract class EventListenerUnityEventResponseBase : EventListener
	{
	}

	public abstract class GenericEventListenerUnityEventResponse< GameEventType > : EventListenerUnityEventResponseBase where GameEventType: GameEvent
	{
		public GameEventType gameEvent;

		public override void OnEnable()
		{
			gameEvent.RegisterListener( this );
		}

		public override void OnDisable()
		{
			gameEvent.UnregisterListener( this );
		}
	}

	public class BasicGameEventResponse : GenericEventListenerUnityEventResponse< GameEvent >
	{
		public UnityEvent unityEvent;

		public override void OnEventRaised()
		{
			unityEvent.Invoke();
		}
	}

	public abstract class GenericEventListenerGenericUnityEventResponse< GameEventType, ArgumentType  > : GenericEventListenerUnityEventResponse< GameEventType > where GameEventType : GameEvent 
	{
		public UnityEvent< ArgumentType > unityEvent;
	}

	public class BoolGameEventResponse : GenericEventListenerGenericUnityEventResponse< BoolGameEvent, bool >
	{
		public override void OnEventRaised()
		{
			unityEvent.Invoke( gameEvent.eventValue );
		}
	}

	public class IntGameEventResponse : GenericEventListenerGenericUnityEventResponse< IntGameEvent, int >
	{
		public override void OnEventRaised()
		{
			unityEvent.Invoke( gameEvent.eventValue );
		}
	}

	public class FloatGameEventResponse : GenericEventListenerGenericUnityEventResponse< FloatGameEvent, float >
	{
		public override void OnEventRaised()
		{
			unityEvent.Invoke( gameEvent.eventValue );
		}
	}

	public class ReferenceGameEventResponse : GenericEventListenerGenericUnityEventResponse< ReferenceGameEvent, object >
	{
		public override void OnEventRaised()
		{
			unityEvent.Invoke( gameEvent.eventValue );
		}
	}
}