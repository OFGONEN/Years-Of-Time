/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FFStudio
{
	public class ParticleEffectOnStop : MonoBehaviour
	{
#region Fields
        ParticleEffect _particleEffect;
#endregion

#region Properties
#endregion

#region Unity API
        private void Awake()
        {
            _particleEffect = GetComponentInParent< ParticleEffect >();
        }

		void OnParticleSystemStopped()
        {
			_particleEffect.OnParticleStopped();
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