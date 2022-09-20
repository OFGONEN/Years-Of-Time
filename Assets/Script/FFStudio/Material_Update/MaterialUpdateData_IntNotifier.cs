/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class MaterialUpdateData_IntNotifier: MaterialUpdateData
{
#region Fields
[ BoxGroup( "Data", false ) ]
    [ SerializeField, LabelText( "Data (Notifier)" ) ] SharedIntNotifier notifier;
#endregion

#region Unity API
#endregion

#region API
#endregion

#region Implementation
    protected override void UpdateParameterImplementation( MaterialPropertyBlock propertyBlock )
	{
        propertyBlock.SetFloat( id, notifier.sharedValue );
	}
#endregion
}
