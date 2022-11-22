/* Created by and for usage of FF Studios (2021). */

using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace FFStudio
{
	public class ParticleManager : MonoBehaviour
	{
#region Fields
	[ Title( "Setup" ) ]
		[ SerializeField ] ParticleEffectPool[] pools_pfx;
		[ SerializeField ] RandomParticlePool[] pools_pfx_random; //Info: All pools needs to be in particleEffectPools aswell.

		Dictionary< string, ParticleEffectPool > dictionary_pool_pfx;
		Dictionary< string, RandomParticlePool > dictionary_pool_pfx_random;
#endregion

#region UnityAPI
		void Awake()
		{
			dictionary_pool_pfx = new Dictionary< string, ParticleEffectPool >( pools_pfx.Length );

			for( int i = 0; i < pools_pfx.Length; i++ )
			{
				pools_pfx[ i ].InitPool( transform, false, ParticleEffectStopped );
				dictionary_pool_pfx.Add( pools_pfx[ i ].pool_entity.alias, pools_pfx[ i ] );
			}

			dictionary_pool_pfx_random = new Dictionary< string, RandomParticlePool >( pools_pfx_random.Length );

			for( var i = 0; i < pools_pfx_random.Length; i++ )
				dictionary_pool_pfx_random.Add( pools_pfx_random[ i ].alias, pools_pfx_random[ i ] );
		}
#endregion

#region API
		public void SpawnParticle( ParticleSpawnEvent spawnEvent )
		{
			ParticleEffectPool pool;

			if( !dictionary_pool_pfx.TryGetValue( spawnEvent.particle_alias, out pool ) )
			{
				FFLogger.Log( "Particle:" + spawnEvent.particle_alias + " is missing!" );
				return;
			}

			var effect = pool.GetEntity();
			effect.PlayParticle( spawnEvent );
		}

		public void SpawnParticleRandom( ParticleSpawnEvent spawnEvent )
		{
			RandomParticlePool randomPool;

			if( !dictionary_pool_pfx_random.TryGetValue( spawnEvent.particle_alias, out randomPool ) )
			{
				FFLogger.Log( "Particle:" + spawnEvent.particle_alias + " is missing!" );
				return;
			}

			var effect = randomPool.GiveRandomEntity();
			effect.PlayParticle( spawnEvent );
		}

		void ParticleEffectStopped( ParticleEffect particleEffect )
		{
			ParticleEffectPool pool;

			if( !dictionary_pool_pfx.TryGetValue( particleEffect.alias, out pool ) )
			{
				FFLogger.Log( "Particle:" + particleEffect.alias + " is missing!", particleEffect.gameObject );
				return;
			}

			pool.ReturnEntity( particleEffect );
		}
#endregion
	}
}