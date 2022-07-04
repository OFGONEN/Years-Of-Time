/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.UI;

namespace FFStudio
{
	public class UI_Update_Image_FillAmount_Tiled : MonoBehaviour
	{
#region Fields
		[ Range( 0.0f, 1.0f ) ]
		public float stepSize;
		public SharedFloatNotifier sharedDataNotifier;

		public Image ui_Image;
#endregion

#region Properties
#endregion

#region Unity API
		void OnEnable()
		{
			sharedDataNotifier.Subscribe( OnSharedDataChange );
		}

		void OnDisable()
		{
			sharedDataNotifier.Unsubscribe( OnSharedDataChange );
		}

		void Awake()
		{
			OnSharedDataChange();
		}
#endregion

#region API
#endregion

#region Implementation
		void OnSharedDataChange()
		{
			ui_Image.fillAmount = Mathf.FloorToInt( ( 1.0f - sharedDataNotifier.SharedValue ) / stepSize ) * stepSize;
		}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}