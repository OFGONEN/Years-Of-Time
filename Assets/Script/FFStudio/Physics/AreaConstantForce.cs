/* Created by and for usage of FF Studios (#CREATION_YEAR#). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

namespace FFStudio
{
	public class AreaConstantForce : MonoBehaviour
	{
#region Fields
		[ SerializeField ] ForceMode force_mode;
		[ SerializeField ] float force_cofactor = 1f;
		[ SerializeField ] Vector3 force;
		[ SerializeField ] Vector3 force_relative;
		[ SerializeField ] Vector3 torque;
		[ SerializeField ] Vector3 torque_relative;
#endregion

#region Unity API
#endregion

#region API
		void OnTriggerStay( Collider other )
		{
			var rb = other.attachedRigidbody;

			rb.AddForce( force * force_cofactor, force_mode );
			rb.AddRelativeForce( force_relative * force_cofactor, force_mode );
			rb.AddTorque( torque * force_cofactor, force_mode );
			rb.AddRelativeTorque( torque_relative * force_cofactor, force_mode );
		}

		public void ChangeCofactor( float value )
		{
			force_cofactor = value;
		}
#endregion

#region Implementation
#endregion
	}
}