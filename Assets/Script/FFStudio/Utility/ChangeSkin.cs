/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

namespace FFStudio
{
    // Info: Updates current renderer skin with target renderer skin.
	public class ChangeSkin : MonoBehaviour 
	{
#region Fields
	[ Title( "Setup" ) ]
        [ Tooltip( "Renderer With Target Skin" ) ] public SkinnedMeshRenderer skinRenderer_target; 
        [ SerializeField ] SkinnedMeshRenderer skinRenderer_current; 
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
        [ Button ]
        public void ChangeTargetSkin() 
        {
			gameObject.UpdateSkinnedMeshRenderer( skinRenderer_current, skinRenderer_target );
		}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}