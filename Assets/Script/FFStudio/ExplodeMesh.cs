/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

public class ExplodeMesh : MonoBehaviour
{
#region Fields
    [ SerializeField, LabelText( "Force Range" ) ] Vector2 force_range = new Vector2( 1, 10 );
    [ SerializeField ] bool hideUnfragmentedMesh;
	[ SerializeField, ShowIf( "hideUnfragmentedMesh" ) ] Transform unfragmentedMesh;
	[ SerializeField ] float explosionRadius = 1.0f;
	[ SerializeField ] float upwardsModifier = 0.0f;
	[ SerializeField ] ForceMode forceMode = ForceMode.Force;

	Rigidbody[] rigidbodies;
#endregion

#region Unity API
    void Awake()
    {
        rigidbodies = GetComponentsInChildren< Rigidbody >( true );
    }
#endregion

#region API
    [ Button ]
	//// CALLED BY A UNITY EVENT ////
    public void Execute()
    {
        if( hideUnfragmentedMesh && unfragmentedMesh != null )
			unfragmentedMesh.gameObject.SetActive( false );
            
		var force = Random.Range( force_range.x, force_range.y );

		for( var i = 0; i < rigidbodies.Length; i++ )
		{
			var rb = rigidbodies[ i ];
			rb.gameObject.SetActive( true );
			rb.isKinematic = false;
			rb.AddExplosionForce( force, transform.position, explosionRadius, upwardsModifier, forceMode );
		}
	}
#endregion

#region Implementation
#endregion
}
