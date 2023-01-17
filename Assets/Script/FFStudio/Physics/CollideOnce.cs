/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FFStudio
{
	public class CollideOnce : MonoBehaviour
	{
#region Fields
    [ SerializeField ] UnityEvent< Collision > onCollisionUnityEvent;
    Dictionary< int, Collider > collider_dictionary = new Dictionary< int, Collider >( 128 );
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
        public void OnCollision( Collision collision )
        {
			var collider   = collision.collider;
			var instanceID = collider.GetInstanceID();

            if( !collider_dictionary.ContainsKey( instanceID ) )
            {
                collider_dictionary.Add( instanceID, collider );
                onCollisionUnityEvent.Invoke( collision );

#if UNITY_EDITOR
            if( collider_dictionary.Count > 128 )
                FFLogger.Log( "Size Exceeded", gameObject );
#endif
            }
        }
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}