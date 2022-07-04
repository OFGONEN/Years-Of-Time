/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Sirenix.OdinInspector;

namespace FFStudio
{
	public class PostProcessManager : MonoBehaviour
	{
#region Fields
	[ Title( "Setup" ) ]
		[ SerializeField ] Volume volume;

		// Post-Process Effects.
		Vignette volume_vignette;
#endregion

#region Properties
#endregion

#region Unity API
		void Awake()
		{
			volume.profile.TryGet< Vignette >( out volume_vignette );
		}
#endregion

#region API
		public void UpdateVignetteIntensity( float value )
		{
			volume_vignette.intensity.value = value;
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