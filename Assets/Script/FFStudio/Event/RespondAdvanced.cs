/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FFStudio
{
	public class RespondAdvanced : MonoBehaviour
	{
#region Fields
        [ SerializeReference ] EventListenerGenericUnityEventResponseBase[] respond_data_array = new EventListenerGenericUnityEventResponseBase[ 0 ];
#endregion

#region Properties
#endregion

#region Unity API
        void OnEnable()
		{
			for( var i = 0; i < respond_data_array.Length; i++ )
				respond_data_array[ i ].OnEnable();
		}

		void OnDisable()
		{
			for( var i = 0; i < respond_data_array.Length; i++ )
				respond_data_array[ i ].OnDisable();
		}
#endregion

#region API
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}