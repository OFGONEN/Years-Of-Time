/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

public class SpawnSlot : MonoBehaviour, ISlotEntity
{
#region Fields
#endregion

#region Properties
#endregion

#region Unity API
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
