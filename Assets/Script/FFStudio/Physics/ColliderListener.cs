/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace FFStudio
{
	public abstract class ColliderListener< DelegateType, CallbackArgumentType > : MonoBehaviour
	{
#region Fields (Inspector Interface)
	[ Title( "Setup" ) ]
		// Info: We can't use Component.enabled because it does not show on Inspector without Update() etc.
		[ SerializeField ] protected bool isEnabled = true;
		[ SerializeField ] Component attachedComponent;

		public Component AttachedComponent => attachedComponent;
		public Collider AttachedCollider => attachedCollider;
		
		public UnityEvent< CallbackArgumentType > unityEvent;

		Collider attachedCollider;
#endregion

#region UnityAPI
		void Awake()
		{
			attachedCollider = GetComponent< Collider >();
		}
#endregion

#region API
		public void SetColliderActive( bool active )
		{
			attachedCollider.enabled = active;
		}

		public void SetAttachedComponent( Component component )
		{
			attachedComponent = component;
		}
#endregion

#region Implementation
        protected virtual void InvokeEvent( CallbackArgumentType physicsCallbackArgument )
		{
			if( isEnabled == false )
				return;

			unityEvent.Invoke( physicsCallbackArgument );
		}
#endregion
	}
}