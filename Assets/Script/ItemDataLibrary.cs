/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ CreateAssetMenu( fileName = "shared_item_data_library", menuName = "FF/Game/Item Data Library" ) ]
public class ItemDataLibrary : ScriptableObject
{
    [ SerializeField ] ItemData[] item_data_array;
}