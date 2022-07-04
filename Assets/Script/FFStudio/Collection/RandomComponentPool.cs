/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public class RandomComponentPool< T > : ScriptableObject where T: Component
	{
#region Fields
        [ SerializeField ] ComponentPool< T >[] pool_component;
#endregion

#region API
        public void InitPool( Transform parent, bool active )
        {
            for( var i = 0; i < pool_component.Length; i++ )
				pool_component[ i ].InitPool( parent, active );
        }
		
        public ComponentPool< T > GetRandomPool()
        {
			return pool_component.ReturnRandom();
		}

        public T GetRandomEntity()
        {
			return pool_component.ReturnRandom().GetEntity();
		}
#endregion
	}
}