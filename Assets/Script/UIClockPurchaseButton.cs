/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FFStudio;
using Sirenix.OdinInspector;

public class UIClockPurchaseButton : MonoBehaviour
{
#region Fields
  [ Title( "Shared" ) ]
    [ SerializeField ] ClockPurchase clock_purchase;
    [ SerializeField ] GameEvent event_clock_spawn;

  [ Title( "Components" ) ]
    [ SerializeField ] Image _image;
    [ SerializeField ] TextMeshProUGUI _textRenderer;
#endregion

#region Properties
#endregion

#region Unity API
    private void Start()
    {
		UpdateVisual();
	}
#endregion

#region API
    public void OnButtonClick()
    {
		event_clock_spawn.Raise();
		UpdateVisual();
	}
#endregion

#region Implementation
	void UpdateVisual()
	{
		_textRenderer.text = clock_purchase.GetClockPurchaseCost().FormatValue();
		_image.sprite      = clock_purchase.GetClockSprite();
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
