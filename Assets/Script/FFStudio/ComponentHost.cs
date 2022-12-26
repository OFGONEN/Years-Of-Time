/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace FFStudio
{
	public class ComponentHost : MonoBehaviour
	{
#region Fields
		[ LabelText( "Host Component" ), SerializeField ] Component _component;

        public Component HostComponent => _component;
#endregion
	}
}