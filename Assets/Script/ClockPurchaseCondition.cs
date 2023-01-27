/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

[ CreateAssetMenu( fileName = "notif_clock_purchase_condition", menuName = "FF/Game/Clock Purchase Condition" ) ]
public class ClockPurchaseCondition : SharedBoolNotifier
{
	bool condition_currency;
	bool condition_slot;

    public void SetConditionCurrency( bool condition )
    {
		condition_currency = condition;
		UpdateCondition();
	}

	public void SetConditionSlot( bool condition )
	{
		condition_slot = condition;
		UpdateCondition();
	}

    void UpdateCondition()
    {
		SharedValue = condition_slot && condition_currency;
    }
}