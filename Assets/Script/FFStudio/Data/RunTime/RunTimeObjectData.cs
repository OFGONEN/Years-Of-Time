/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FFStudio
{
	public class RunTimeObjectData : RunTimeData< object >
	{
#region Fields
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
		public override void OffsetData( object value )
		{
			runTimeData = value;
			onUpdateEvent.Invoke( runTimeData );
		}

		public override void CompareData( object value )
		{
			if( runTimeData == value )
				onComparisonEvent.Invoke( runTimeData );
		}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}