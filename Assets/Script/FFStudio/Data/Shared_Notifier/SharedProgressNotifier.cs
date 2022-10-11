/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using FFStudio;

[ CreateAssetMenu( fileName = "notif_progress_", menuName = "FF/Data/Shared/Notifier/Progress" ) ]
public class SharedProgressNotifier : SharedFloatNotifier
{
#region Fields
    float numerator, denominator;
#endregion

#region Unity API
#endregion

#region API
    public void SetDenominator( float denominator )
    {
		this.denominator = denominator;
	}
    
    public void SetNumerator( float numerator )
    {
		this.numerator = numerator;

		SharedValue = numerator / denominator;
	}

	public void Complete()
	{
		SetNumerator( denominator );
	}
#endregion

#region Implementation
#endregion
}
