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
    public float NextIncomeCost  => income_data_array[ Mathf.Min( income_index + 1, income_data_array.Length - 1 ) ].income_cost;
    public float NextIncomeValue => income_data_array[ Mathf.Min( income_index + 1, income_data_array.Length - 1 ) ].income_value;
    public bool IsIncomeMaxed    => income_index == income_data_array.Length - 1;
#endregion

#region Unity API
#endregion

#region API
    public void LoadIncomeCofactor()
    {
		income_index = PlayerPrefsUtility.Instance.GetInt( ExtensionMethods.Key_Income, 0 );
		SharedValue  = income_data_array[ income_index ].income_value;
	}

    public void UnlockIncome()
    {
		var cost = NextIncomeCost;

		income_index++;
		PlayerPrefsUtility.Instance.SetInt( ExtensionMethods.Key_Income, income_index );

		SharedValue = income_data_array[ income_index ].income_value;

		notif_currency.SharedValue -= cost;
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