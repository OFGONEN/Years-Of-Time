/* Created by and for usage of FF Studios (2021). */

using FFStudio;
using UnityEngine;
using Sirenix.OdinInspector;

[ System.Serializable ]
public abstract class MaterialUpdateData
{
#region Fields
[ BoxGroup( "General", false ), PropertyOrder( int.MinValue ) ]
    [ SerializeField ] string name;
    [ HideInInspector ] public int id;
#endregion

#region Unity API
#endregion

#region API
    public void UpdateParameter( MaterialPropertyBlock propertyBlock )
    {
		UpdateParameterImplementation( propertyBlock );
	}
    
    public void SetIDFromHash()
    {
		id = Shader.PropertyToID( name );
	}
#endregion

#region Implementation
    protected abstract void UpdateParameterImplementation( MaterialPropertyBlock propertyBlock );
#endregion


}
