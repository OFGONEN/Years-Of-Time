/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FFStudio
{
	public class RespondAdvanced : MonoBehaviour
	{
#region Fields
        [ SerializeReference ] EventResponseDataBase[] respond_data_array = new EventResponseDataBase[ 0 ];
#endregion

#region Properties
#endregion

#region Unity API
        void OnEnable()
		{
			for( var i = 0; i < respond_data_array.Length; i++ )
				respond_data_array[ i ].eventListener.OnEnable();
		}

		void OnDisable()
		{
			for( var i = 0; i < respond_data_array.Length; i++ )
				respond_data_array[ i ].eventListener.OnDisable();
		}

		void Awake()
		{
			for( var i = 0; i < respond_data_array.Length; i++ )
				respond_data_array[ i ].Pair();
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