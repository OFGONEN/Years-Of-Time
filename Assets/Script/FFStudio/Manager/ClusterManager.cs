/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public class ClusterManager : MonoBehaviour
	{
#region Fields
		public Cluster[] clusters;
#endregion

#region Properties
#endregion

#region Unity API
		void Awake()
		{
			for( var i = 0; i < clusters.Length; i++ )
				clusters[ i ].Init();
		}

		void Update()
		{
			for( var i = 0; i < clusters.Length; i++ )
				clusters[ i ].UpdateCluster();
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