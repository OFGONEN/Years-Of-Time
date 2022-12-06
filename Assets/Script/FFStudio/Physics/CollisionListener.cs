/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public abstract class CollisionListener : ColliderListener< CollisionMessage, Collision >
	{
#region Fields
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
#endregion

#region Implementation
        protected override void InvokeEvent( Collision collision )
		{
			if( isEnabled == false )
				return;
				
			unityEvent.Invoke( collision );
		}
#endregion
	}
}