/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class SpawnSlot : MonoBehaviour, ISlotEntity
{
#region Fields
	[ SerializeField ] ListSpawnSlot list_slot_spawn;
#endregion

#region Properties
#endregion

#region Unity API
	private void OnEnable()
	{
		list_slot_spawn.AddList( this );
	}

	private void OnDisable()
	{
		list_slot_spawn.RemoveList( this );
	}
#endregion

#region API
    public Vector3 GetPosition()
    {
		return transform.position;
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
