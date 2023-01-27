/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "notif_clock_purchase", menuName = "FF/Game/Clock Purchase" ) ]
public class ClockPurchase : SharedBoolNotifier
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] float purchase_cost_base;
    [ LabelText( "Purchase Level Range" ), SerializeField ] int[] purchase_level_range;

  [ Title( "Shared" ) ]
    [ SerializeField ] Currency currency;
    [ SerializeField ] ClockDataLibrary clock_data_library;

    int purchase_count;
	int purchase_level;
#endregion

#region Properties
	public int PurchaseLevel => purchase_level;
#endregion

#region Unity API
#endregion

#region API
    public void LoadPurchaseCount( int defaultValue )
    {
		purchase_count = PlayerPrefsUtility.Instance.GetInt( ExtensionMethods.Key_ClockPurchaseCount, defaultValue );
		SetPurchaseLevel();
	}

    public void SavePurchaseCount()
    {
        PlayerPrefsUtility.Instance.SetInt( ExtensionMethods.Key_ClockPurchaseCount, purchase_count );
    }

    public void CheckClockPurchase()
    {
		SetValue_NotifyAlways( currency.sharedValue >= GetClockPurchaseCost() );
	}

	public void ClockPurchased()
	{
		currency.SharedValue -= GetClockPurchaseCost();

		purchase_count++;
		SetPurchaseLevel();

		CheckClockPurchase();
	}

    public Sprite GetClockSprite()
    {
		return clock_data_library.GetClockData( purchase_level ).ClockTexture;
	}
#endregion

#region Implementation
    float GetClockPurchaseCost()
    {
        return purchase_cost_base + Mathf.Pow( purchase_count, 1.25f ) - purchase_count;
    }

	void SetPurchaseLevel()
	{
		purchase_level = 0;

		for( var i = 0; i < purchase_level_range.Length; i++ )
		{
			if( purchase_count >= purchase_level_range[ i ] )
				purchase_level = i;
		}

		purchase_level = Mathf.Max( purchase_level, clock_data_library.ClockMaxLevel );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
	private void OnValidate()
	{
		if( purchase_level_range == null || purchase_level_range.Length < 1 || purchase_level_range[ 0 ] != 0 )
		{
			FFLogger.LogError( "Purchase Level Ranges first value MUST be ZERO!" );
		}
	}
#endif
#endregion
}