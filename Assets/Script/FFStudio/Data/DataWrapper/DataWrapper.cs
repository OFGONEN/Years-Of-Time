/* Created by and for usage of FF Studios (2021). */
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FFStudio
{
	[ System.Serializable ]
	public abstract class DataWrapper< DataType >
	{
		public abstract DataType Data { get; }
	}
}