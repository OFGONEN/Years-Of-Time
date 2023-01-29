/* Created by and for usage of FF Studios (2021). */
#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using Sirenix.OdinInspector;
using FFStudio;

[ CreateAssetMenu( fileName = "tool_level_creator", menuName = "FF/Game/Tool/Level Creator" ) ]
public class ToolLevelCreator : ScriptableObject
{
#region Fields
    [ FoldoutGroup( "Spawn Slot" ) ] public SpawnSlot prefab_slot_spawn;
    [ FoldoutGroup( "Spawn Slot" ) ] public int slot_spawn_count;
    [ FoldoutGroup( "Spawn Slot" ) ] public int slot_spawn_row_count;
    [ FoldoutGroup( "Spawn Slot" ) ] public Vector3 slot_spawn_origin;
    [ FoldoutGroup( "Spawn Slot" ) ] public Vector3 slot_spawn_offset;

    [ FoldoutGroup( "Clock Slot" ) ] public ClockSlot prefab_slot_clock_row;
    [ FoldoutGroup( "Clock Slot" ) ] public ClockSlot prefab_slot_clock_column;
    [ FoldoutGroup( "Clock Slot" ) ] public int slot_clock_count;
    [ FoldoutGroup( "Clock Slot" ) ] public Vector3 slot_clock_origin;
    [ FoldoutGroup( "Clock Slot" ) ] public Vector3 slot_clock_offset;

    [ FoldoutGroup( "Item" ) ] public Item prefab_item;
    [ FoldoutGroup( "Item" ) ] public int item_count_row;
    [ FoldoutGroup( "Item" ) ] public int item_count_column;
    [ FoldoutGroup( "Item" ) ] public float item_size;
    [ FoldoutGroup( "Item" ) ] public Vector3 item_origin;
    [ FoldoutGroup( "Item" ) ] public Vector3 item_offset;

#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    [ Button() ]
    public void SpawnSpawnSlot()
    {
		EditorSceneManager.MarkAllScenesDirty();

		var startIndex = GameObject.Find( "--- Slot_Spawn_Start ---" ).transform.GetSiblingIndex();
		var endIndex   = GameObject.Find( "--- Slot_Spawn_End ---" ).transform.GetSiblingIndex();

		DeleteGameObjectsAtRange( startIndex, endIndex );

		int     columnCount = Mathf.CeilToInt( ( float )slot_spawn_count / slot_spawn_row_count );
		Vector3 spawnOrigin = Vector3.zero;
		int     spawnCount  = 0;

		for( var x = 0; x < slot_spawn_row_count; x++ )
        {
			spawnOrigin = slot_spawn_origin + Vector3.forward * slot_spawn_offset.z * x;

			for( var y = 0; y < columnCount; y++ )
            {
		        var spawnSlot = PrefabUtility.InstantiatePrefab( prefab_slot_spawn ) as SpawnSlot;

			    spawnSlot.gameObject.name = "slot_spawn_" + ( spawnCount + 1 );
			    spawnSlot.transform.SetSiblingIndex( startIndex + spawnCount + 1 );
			    spawnSlot.transform.position = spawnOrigin + Vector3.right * slot_spawn_offset.x * y;

			    spawnSlot.SetSlotIndex( spawnCount );

				spawnCount++;
			}           
        }
	}

	[ Button() ]
	public void SpawnClockSlots()
	{
		EditorSceneManager.MarkAllScenesDirty();

		var playArea = GameObject.Find( "area_clock" ).transform;

		EditorUtility.SetDirty( playArea.gameObject );
		playArea.DestroyAllChildren();

		// Column
		for( var i = 0; i < slot_clock_count; i++ )
		{
			var clockSlotColumn = PrefabUtility.InstantiatePrefab( prefab_slot_clock_column ) as ClockSlot;

			clockSlotColumn.gameObject.name    = "slot_clock_column_" + i;
			clockSlotColumn.transform.position = slot_clock_origin +  Vector3.right * ( i + 1 ) * slot_clock_offset.x;

			clockSlotColumn.transform.SetParent( playArea );
			clockSlotColumn.transform.localScale = Vector3.one;

			clockSlotColumn.SetSlotIndex( i );
		}

		// Row
		for( var i = 0; i < slot_clock_count; i++ )
		{
			var clockSlotRow = PrefabUtility.InstantiatePrefab( prefab_slot_clock_row ) as ClockSlot;

			clockSlotRow.gameObject.name    = "slot_clock_row_" + i;
			clockSlotRow.transform.position = slot_clock_origin + Vector3.forward * ( i + 1 ) * slot_clock_offset.z;

			clockSlotRow.transform.SetParent( playArea );
			clockSlotRow.transform.localScale = Vector3.one;

			clockSlotRow.SetSlotIndex( i );
		}
	}

	[ Button() ]
	public void SpawnItems()
	{
		EditorSceneManager.MarkAllScenesDirty();

		var playArea = GameObject.Find( "area_item" ).transform;

		EditorUtility.SetDirty( playArea.gameObject );
		playArea.DestroyAllChildren();

		int spawnCount = 0;
		Vector3 origin = item_origin + Vector3.right * item_offset.x + Vector3.forward * item_offset.z;

		for( var y = 0; y < item_count_column; y++ )
		{
			for( var x = 0; x < item_count_row; x++ )
			{
				var item = PrefabUtility.InstantiatePrefab( prefab_item ) as Item;

				item.gameObject.name = "item_" + spawnCount;
				item.transform.position = origin + Vector3.right * x * item_offset.x + Vector3.forward * y * item_offset.z;

				item.transform.localScale = new Vector3( item_size, 1, item_size );
				item.transform.SetParent( playArea );

				item.SetItemIndex( spawnCount );
				item.SetItemCoordinate( new Vector2Int( x, y ) );

				spawnCount++;				
			}
		}
	}
#endregion

#region Implementation
    [ Button() ]
    void DeleteGameObjectsAtRange( int start, int end )
    {
		EditorSceneManager.MarkAllScenesDirty();

		var go_array = EditorSceneManager.GetActiveScene().GetRootGameObjects();

        var go_list = new List< GameObject >();

        for( var i = 1; i <= end - start - 1; i++ )
			go_list.Add( go_array[ start + i ] );

        for( var i = 0; i < go_list.Count; i++ )
			DestroyImmediate( go_list[ i ] );
	}
#endregion
}
#endif