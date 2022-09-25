/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public class SharedReferenceSetter : MonoBehaviour
	{
#region Fields
		[ SerializeField ] SharedReferenceNotifier sharedReferenceProperty;
		[ SerializeField ] Component referenceComponent;
#endregion

#region UnityAPI
		void OnEnable()
		{
			sharedReferenceProperty.SharedValue = referenceComponent;
		}

		void OnDisable()
		{
			sharedReferenceProperty.SharedValue = null;
		}
#endregion
	}
}