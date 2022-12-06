/* Created by and for usage of FF Studios (2021). */

using System.Collections.Generic;
using UnityEngine;

namespace FFStudio
{
	public class TriggerRouter2D : MonoBehaviour
	{
#region Fields
		[ SerializeField ] Trigger2DRespondData[] trigger_respond_data_array;

		Dictionary< int, int > trigger_respond_data_dictionary;
#endregion

#region Properties
#endregion

#region Unity API
		private void Awake()
		{
			trigger_respond_data_dictionary   = new Dictionary< int, int >( trigger_respond_data_array.Length );

			for( var i = 0; i < trigger_respond_data_array.Length; i++ )
				trigger_respond_data_dictionary.Add( trigger_respond_data_array[ i ].collision_layer, i );
		}
#endregion

#region API
		public void OnTrigger2D( Collider2D collider )
		{
			int index;

			if( trigger_respond_data_dictionary.TryGetValue( collider.gameObject.layer, out index ) )
				trigger_respond_data_array[ index ].trigger_event.Invoke( collider );
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