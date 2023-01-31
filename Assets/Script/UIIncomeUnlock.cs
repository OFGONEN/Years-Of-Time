/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FFStudio;
using Sirenix.OdinInspector;

public class UIIncomeUnlock : MonoBehaviour
{
#region Fields
  [ Title( "Shared" ) ]
    [ SerializeField ] IncomeCofactor income_cofactor; 

  [ Title( "Components" ) ]
    [ SerializeField] Button _button;
    [ SerializeField] Image _image;
    [ SerializeField] Sprite _sprite_inactive;
    [ SerializeField] TextMeshProUGUI _textRenderer_Cost;
    [ SerializeField] TextMeshProUGUI _textRenderer_Value;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnStart()
    {
		UpdateVisual();
	}
    
    public void OnCurrencyChange( float currency )
    {
		_button.interactable = currency >= income_cofactor.NextIncomeCost && !income_cofactor.IsIncomeMaxed;
	}

    public void OnClick()
    {
		income_cofactor.UnlockIncome();
		UpdateVisual();
	}
#endregion

#region Implementation
    void UpdateVisual()
    {
        if( income_cofactor.IsIncomeMaxed )
        {
			FFLogger.Log( "MAXED" );
			_image.sprite            = _sprite_inactive;
			_button.interactable     = false;
			_button.enabled          = false;
			_textRenderer_Cost.text  = "MAX";
			_textRenderer_Value.text = income_cofactor.sharedValue.ToString();
        }
        else
        {
		    _textRenderer_Cost.text  = income_cofactor.NextIncomeCost.FormatValue();
		    _textRenderer_Value.text = income_cofactor.sharedValue.ToString( "F2" ) + " > " + income_cofactor.NextIncomeValue.ToString( "F2" );
        }
    }
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}