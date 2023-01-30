/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class UIItemUnlock : MonoBehaviour
{
#region Fields
  [ Title( "Shared" ) ]
    [ SerializeField ] ListItem list_item_index;
    [ SerializeField ] SharedIntNotifier notif_item_unlock_index;

  [ Title( "Components" ) ]
    [ SerializeField ] Button _button; 
    [ SerializeField ] TextMeshProUGUI _textRenderer; 

    Item item_current;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnStart()
    {
		TryToPlace();
	}

    public void OnCurrencyChange( float currency )
    {
		_button.interactable = currency >= item_current.ItemData.ItemCost;
	}

    public void OnClick()
    {
		notif_item_unlock_index.SharedValue++;
		notif_item_unlock_index.SaveToPlayerPrefs();

		var item = item_current;

		TryToPlace();

		item.Unlock();
	}
#endregion

#region Implementation
    void TryToPlace()
    {
        if( notif_item_unlock_index.sharedValue >= list_item_index.itemDictionary.Count )
        {
		    list_item_index.itemDictionary.TryGetValue( 0, out item_current );
			gameObject.SetActive( false );
		}
        else
			PlaceOnTargetItem();
    }

    void PlaceOnTargetItem()
    {
		item_current = FindCurrentItem();

		UpdateVisual();
		UpdatePosition();
    }

    Item FindCurrentItem()
    {
		Item item;
		list_item_index.itemDictionary.TryGetValue( notif_item_unlock_index.sharedValue, out item );

		return item;
	}

    void UpdateVisual()
    {
		_textRenderer.text = item_current.ItemData.ItemCost.FormatValue();
	}

    void UpdatePosition()
    {
		transform.position = item_current.transform.position.OffsetY( GameSettings.Instance.item_unlock_height );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}