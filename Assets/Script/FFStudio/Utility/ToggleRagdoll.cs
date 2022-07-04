/* Created by and for usage of FF Studios (2021). */

using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

namespace FFStudio
{
    public class ToggleRagdoll : MonoBehaviour
    {
#region Fields
        [ SerializeField ] bool includeRigidbodyOnThisGameObject;
        [ SerializeField ] bool deactivateOnStart = true;
        [ SerializeField ] Rigidbody[] excludeTheseRigidbodies;

        [ SerializeField ] Rigidbody[] ragdollRigidbodies;
        [ SerializeField ] Collider[] ragdollRigidbody_Colliders;

        Rigidbody ragdollRigidbody_Main;
#endregion

#region Properties
#endregion

#region Unity API
        void Awake()
        {
            if( includeRigidbodyOnThisGameObject )
                ragdollRigidbodies = GetComponentsInChildren< Rigidbody >();
            else
            {
                var ragdollRigidbodies_Temporary = GetComponentsInChildren< Rigidbody >();
                ragdollRigidbodies = ragdollRigidbodies_Temporary.Skip( 1 ).Take( ragdollRigidbodies_Temporary.Length - 1 ).ToArray();
            }

            ragdollRigidbodies    = ragdollRigidbodies.Except( excludeTheseRigidbodies ).ToArray();
            ragdollRigidbody_Main = ragdollRigidbodies[ 0 ];

			ragdollRigidbody_Colliders = new Collider[ ragdollRigidbodies.Length ];

			for( var i = 0; i < ragdollRigidbodies.Length; i++ )
				ragdollRigidbody_Colliders[ i ] = ragdollRigidbodies[ i ].GetComponent<Collider>();

            if( deactivateOnStart )
                Deactivate();
        }
#endregion

#region API
        [ Button() ]
        public void Activate()
        {
			for( var i = 0; i < ragdollRigidbodies.Length; i++ )
			{
				ragdollRigidbodies        [ i ].isKinematic = false;
				ragdollRigidbodies        [ i ].useGravity  = true;
				ragdollRigidbody_Colliders[ i ].enabled     = true;
			}
        }
        
        [ Button()]
        public void Deactivate()
        {
			for( var i = 0; i < ragdollRigidbodies.Length; i++ )
			{
				ragdollRigidbodies        [ i ].isKinematic = true;
				ragdollRigidbodies        [ i ].useGravity  = false;
				ragdollRigidbody_Colliders[ i ].enabled     = false;
			}        
        }

        [ Button() ]
		public void GiveForce( Vector3 force, ForceMode mode )
		{
			ragdollRigidbody_Main.AddForce( force, mode );
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