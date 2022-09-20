/* Created by and for usage of FF Studios (2021). */

using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MaterialUpdate : MonoBehaviour
{
#region Fields
#if UNITY_EDITOR
	[ TableList( ShowIndexLabels = true ) ]
#endif
    [ SerializeReference ] List< MaterialUpdateData > updateDatas = new List< MaterialUpdateData >();
    
    [ SerializeField ] Renderer renderer_;
    
    MaterialPropertyBlock propertyBlock;
#endregion

#region Unity API
    void Awake()
    {
		propertyBlock = new MaterialPropertyBlock();
	}
    
    void Start()
    {
    }
#endregion

#region API
    public void Execute( int index )
    {
		var updateInfo = updateDatas[ index ];

		renderer_.GetPropertyBlock( propertyBlock );
		updateDatas[ index ].UpdateParameter( propertyBlock );
		renderer_.SetPropertyBlock( propertyBlock );
	}
#endregion

#region Implementation
    
#endregion

#region Editor Only
#if UNITY_EDITOR
    void OnValidate()
    {
        foreach( var updateData in updateDatas )
			updateData.SetIDFromHash();
    }
#endif
#endregion
}
