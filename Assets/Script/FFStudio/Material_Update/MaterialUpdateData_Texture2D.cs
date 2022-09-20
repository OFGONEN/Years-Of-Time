/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

public class MaterialUpdateData_Texture2D : MaterialUpdateData
{
#region Fields
[ BoxGroup( "Data", false )]
	[ SerializeField ] Texture2D data;
#endregion

#region Unity API
#endregion

#region API
#endregion

#region Implementation
    protected override void UpdateParameterImplementation( MaterialPropertyBlock propertyBlock )
	{
        propertyBlock.SetTexture( id, data );
	}
#endregion
}
