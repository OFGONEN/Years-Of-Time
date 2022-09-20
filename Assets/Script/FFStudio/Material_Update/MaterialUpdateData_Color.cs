/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

public class MaterialUpdateData_Color : MaterialUpdateData
{
#region Fields
[ BoxGroup( "Data", false ) ]
    [ SerializeField ] Color data;
#endregion

#region Unity API
#endregion

#region API
#endregion

#region Implementation
    protected override void UpdateParameterImplementation( MaterialPropertyBlock propertyBlock )
	{
        propertyBlock.SetColor( id, data );
	}
#endregion
}
