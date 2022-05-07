/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
    [ CreateAssetMenu( fileName = "logger_so", menuName = "FF/Utility/Logger" ) ]
    public class Logger_SOBased : ScriptableObject
    {
#region Fields
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
        public void Log( string text )
        {
            FFLogger.Log( text );
        }
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
    }
}