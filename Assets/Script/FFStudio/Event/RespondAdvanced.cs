/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FFStudio
{
	public class RespondAdvanced : MonoBehaviour
	{
#region Fields
        [ SerializeReference ] List< EventListenerUnityEventResponseBase >  respond_data_array = new List< EventListenerUnityEventResponseBase >();
#endregion

#region Properties
#endregion

#region Unity API
        void OnEnable()
		{
			for( var i = 0; i < respond_data_array.Count; i++ )
				respond_data_array[ i ].OnEnable();
		}

		void OnDisable()
		{
			for( var i = 0; i < respond_data_array.Count; i++ )
				respond_data_array[ i ].OnDisable();
		}
#endregion

#region API
		public void EnableRespond( int index )
		{
			respond_data_array[ index ].isEnabled = true;
			respond_data_array[ index ].OnEnable();
		}

		public void DisableRespond( int index )
		{
			respond_data_array[ index ].isEnabled = false;
			respond_data_array[ index ].OnDisable();
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