/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

public class MaterialUpdateData_Float : MaterialUpdateData
{
#region Fields
[ BoxGroup( "Data", false ) ]
    [ SerializeField ] float data;
#endregion

#region Unity API
#endregion

#region API
#endregion

#region Implementation
    protected override void UpdateParameterImplementation( MaterialPropertyBlock propertyBlock )
	{
        propertyBlock.SetFloat( id, data );
	}
#endregion
}
