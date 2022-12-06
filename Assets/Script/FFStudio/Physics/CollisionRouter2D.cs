/* Created by and for usage of FF Studios (2021). */

using System.Collections.Generic;
using UnityEngine;

namespace FFStudio
{
	public class CollisionRouter2D : MonoBehaviour
	{
#region Fields
		[ SerializeField ] Collision2DRespondData[] collision_respond_data_array;

		Dictionary< int, int > collision_respond_data_dictionary;
#endregion

#region Properties
#endregion

#region Unity API
		private void Awake()
		{
			collision_respond_data_dictionary = new Dictionary< int, int >( collision_respond_data_array.Length );

			for( var i = 0; i < collision_respond_data_array.Length; i++ )
				collision_respond_data_dictionary.Add( collision_respond_data_array[ i ].collision_layer, i );
		}
#endregion

#region API
		public void OnCollision2D( Collision2D collision )
		{
			int index;

			if( collision_respond_data_dictionary.TryGetValue( collision.gameObject.layer, out index ) )
				collision_respond_data_array[ index ].collision_event.Invoke( collision );
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