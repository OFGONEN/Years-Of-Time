/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "notif_income_cofactor", menuName = "FF/Game/Income Cofactor" ) ]
public class IncomeCofactor : SharedFloatNotifier
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] IncomeData[] income_data_array;

  [ Title( "Shared" ) ]
    [ SerializeField ] Currency notif_currency;
    int income_index;
#endregion

#region Properties
    public float IncomeCost     => income_data_array[ income_index ].income_cost;
    public float IncomeValue    => income_data_array[ income_index ].income_value;
    public float IncomeDataSize => income_data_array.Length;
#endregion

#region Unity API
#endregion

#region API
    public void LoadIncomeCofactor()
    {
		income_index = PlayerPrefsUtility.Instance.GetInt( ExtensionMethods.Key_Income, 0 );
		SharedValue  = IncomeValue;
	}

    public void UnlockIncome()
    {
		notif_currency.SharedValue -= IncomeCost;

		income_index++;
		PlayerPrefsUtility.Instance.SetInt( ExtensionMethods.Key_Income, income_index );
		SharedValue = IncomeValue;
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}

[ System.Serializable ]
public struct IncomeData
{
	public float income_cost;
	public float income_value;
}