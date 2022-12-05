/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ TypeInfoBox( "This component fits 'Transform to Fit' to given camera's view on Execute()." ) ]
public class FitToView : MonoBehaviour
{
#region Fields
[ Title( "Setup" ) ]
    [ SerializeField, LabelText( "Source Width"  ) ] float source_width  = 828;
	[ SerializeField, LabelText( "Source Height" ) ] float source_height = 1792; 
    [ SerializeField, LabelText( "Main Camera Notifier" ) ] SharedReferenceNotifier notifier_camera_main;
    [ SerializeField, LabelText( "Transform to Fit" ) ] Transform transform_toFit;
#endregion

#region Unity API
#endregion

#region API
    [ Button ]
    public void Execute()
	{
        // Info: Not caching this on Awake because that may be too early/late.
		var camera = ( notifier_camera_main.sharedValue as Transform ).GetComponent< Camera >();

		float currentAspectRatio = ( float )Screen.width / Screen.height;

		float worldHeight = camera.orthographicSize * 2;
		float worldWidth  = worldHeight * currentAspectRatio;

		var widthChangeMagnitude  = Mathf.Abs( ( float )Screen.width  - source_width  ) / source_width;
		var heightChangeMagnitude = Mathf.Abs( ( float )Screen.height - source_height ) / source_height;

        if( widthChangeMagnitude > heightChangeMagnitude )
		{
			var scalingFactor = ( float )Screen.width / source_width;
            if( scalingFactor < 1 )
				scalingFactor = 1.0f / scalingFactor;
			transform_toFit.transform.localScale = new Vector3( worldWidth, worldHeight * scalingFactor, 1 );
		}
		else
		{
			var scalingFactor = ( float )Screen.height / source_height;
			if( scalingFactor < 1 )
				scalingFactor = 1.0f / scalingFactor;
			transform_toFit.transform.localScale = new Vector3( worldWidth * scalingFactor, worldHeight, 1 );
		}
	}
#endregion

#region Implementation
#endregion
}
