/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

namespace FFStudio
{
	public class PopUpTextSpawner : MonoBehaviour
	{
#region Fields
	[ Title( "Setup" ) ] 
		public Pool_UIPopUpText pool_UIPopUpText;
		public PopUpTextData[] popUpTextDatas;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
		[ Button() ]
		public void Spawn( int index )
		{
			var data = popUpTextDatas[ index ];

			Transform parent = data.parent ? transform : null;

			var entity = pool_UIPopUpText.GetEntity();
			entity.Spawn( transform.position + data.offset, data.text, data.size, data.color );
		}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
		void OnDrawGizmosSelected()
		{
			for( var i = 0; i < popUpTextDatas.Length; i++ )
			{
				var data = popUpTextDatas[ i ];
				Handles.Label( transform.position + data.offset, "PopUp:" + data.text );
				Handles.DrawWireCube( transform.position + data.offset, Vector3.one / 4f );
			}
		}
#endif
#endregion
	}
}