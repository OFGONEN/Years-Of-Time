/* Created by and for usage of FF Studios (2023). */

using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;
using DG.Tweening;

[ CreateAssetMenu( fileName = "notif_tweenable_", menuName = "FF/Data/Shared/Notifier/Tweenable Float" ) ]
public class TweenableFloatNotifier : SharedFloatNotifier
{
#region Fields
[ Title( "Tween Parameters" ) ]
    [ SerializeField, LabelText( "Start Value" ) ] float value_start;
    [ SerializeField, LabelText( "End Value" ) ] float value_end;
    [ SerializeField ] float duration;
    [ SerializeField ] Ease ease;
	
[ Title( "Auto Reverse" ) ]
	[ SerializeField, LabelText( "After How Many Seconds ?" ), SuffixLabel( "seconds" ) ] float autoReverse_cooldown_duration;
    
    RecycledTween recycledTween = new RecycledTween();
	Cooldown cooldown = new Cooldown();
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
	public void Initiate()
    {
		recycledTween.Recycle( DOTween.To( GetValue, SetValue, value_end, duration * ( 1.0f - Mathf.InverseLerp( value_start, value_end, sharedValue ) ) ).SetEase( ease ));
	}

	[ Button() ]
	public void Initiate_AutoReverse()
	{
		cooldown.Kill();

		recycledTween.Recycle( DOTween.To( GetValue, SetValue, value_end, duration * ( 1.0f - Mathf.InverseLerp( value_start, value_end, sharedValue ) ) ).SetEase( ease ) );
		cooldown.Start( autoReverse_cooldown_duration, Initiate_Reverse );
	}
    
    public void Initiate_Reverse()
    {
		recycledTween.Recycle( DOTween.To( GetValue, SetValue, value_start, duration * Mathf.InverseLerp( value_start, value_end, sharedValue ) ).SetEase( ease ) );
	}
    
    public void Terminate()
    {
		recycledTween.Kill();
	}
    
    public void TerminateAndRewind()
    {
		recycledTween.KillAndRewind();
	}
    
    public void TerminateAndComplete()
    {
		recycledTween.CompleteAndKill();
	}
#endregion

#region Implementation
    void SetValue( float newValue ) => SharedValue = newValue;
    float GetValue() => sharedValue;
#endregion
}
