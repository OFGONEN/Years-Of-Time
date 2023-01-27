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

  [ Title( "Shared" ) ]
    [ SerializeField ] Currency currency;

    
    int purchase_count;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void LoadPurchaseCount( int defaultValue )
    {
		purchase_count = PlayerPrefsUtility.Instance.GetInt( ExtensionMethods.Key_ClockPurchaseCount, defaultValue );
	}

    public void SavePurchaseCount()
    {
        PlayerPrefsUtility.Instance.SetInt( ExtensionMethods.Key_ClockPurchaseCount, purchase_count );
    }
#endregion

#region Implementation
    float GetClockPurchaseCost()
    {
        return purchase_cost_base + Mathf.Pow( purchase_count, 1.25f ) - purchase_count;
    }
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
