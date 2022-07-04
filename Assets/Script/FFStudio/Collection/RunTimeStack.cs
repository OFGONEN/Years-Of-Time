/* Created by and for usage of FF Studios (2021). */

using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace FFStudio
{
	public abstract class RuntimeStack< T > : ScriptableObject
	{
#region Fields
		public int stackSize;
        public Stack< T > Stack => stack;
		
		[ ShowInInspector ] protected Stack< T > stack;
#endregion
	}
}
