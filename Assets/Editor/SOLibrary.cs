/* Created by and for usage of FF Studios (2021). */

using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

namespace FFEditor
{
	[ CreateAssetMenu( fileName = "TrackedSOLibrary", menuName = "FFEditor/TrackedSOLibrary" ) ]
    public class SOLibrary : ScriptableObject
    {
        [ ReadOnly ] public List< ScriptableObject > trackedScriptableObjects = null;

        [ ReadOnly ] public int trackedScriptablesObjectCount = 0;

		private void Awake()
		{
			if( trackedScriptableObjects == null )
				trackedScriptableObjects = new List< ScriptableObject >( 8 );

			trackedScriptablesObjectCount = trackedScriptableObjects.Count;
		}

		public void TrackScriptableObject( ScriptableObject sObject )
		{
			if( !trackedScriptableObjects.Contains( sObject ) )
				trackedScriptableObjects.Add( sObject );

			trackedScriptablesObjectCount = trackedScriptableObjects.Count;
		}

		public void UntrackScriptableObject( ScriptableObject sObject )
		{
			trackedScriptableObjects.Remove( sObject );

			trackedScriptablesObjectCount = trackedScriptableObjects.Count;
		}

		public void DeleteEmptyIndexes()
		{
			EditorUtility.SetDirty( this );

			for( var i = 0; i < trackedScriptableObjects.Count; i++ )
			{
				if( trackedScriptableObjects[ i ] == null )
				{
					trackedScriptableObjects.RemoveAt( i );
					i = 0;
				}
			}

			trackedScriptablesObjectCount = trackedScriptableObjects.Count;
			AssetDatabase.SaveAssets();
		}
    }
}