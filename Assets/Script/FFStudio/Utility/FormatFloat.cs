/* Created by and for usage of FF Studios (2023). */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FFStudio
{
	public class FormatFloat : MonoBehaviour
	{
#region Fields
		[ SerializeField ] UnityEvent<string> onFormatFloatEvent;
		[ SerializeField ] string suffix;
		[ SerializeField ] string prefix;

		private static readonly int charA = Convert.ToInt32( 'a' );
		private static readonly Dictionary<int, string> units = new Dictionary<int, string>
		{
			{ 0, ""  } ,
			{ 1, "K" },
			{ 2, "M" },
			{ 3, "B" },
			{ 4, "T" }
		};
#endregion

#region Unity API
#endregion

#region API
		public void UpdateTextRenderer( float value )
		{
			onFormatFloatEvent.Invoke( suffix + FormatNumber( value ) + prefix );
		}
#endregion

#region Implementation
		public static string FormatNumber( double value )
		{
			if( value < 1d )
			{
				return "0";
			}

			var n = ( int )Math.Log( value, 1000 );
			var m = value / Math.Pow( 1000, n );
			var unit = "";

			if( n < units.Count )
			{
				unit = units[ n ];
			}
			else
			{
				var unitInt = n - units.Count;
				var secondUnit = unitInt % 26;
				var firstUnit = unitInt / 26;
				unit = Convert.ToChar( firstUnit + charA ).ToString() + Convert.ToChar( secondUnit + charA ).ToString();
			}

			// Math.Floor(m * 100) / 100) fixes rounding errors
			return ( Math.Floor( m * 100 ) / 100 ).ToString( "0.##" ) + unit;
		}
#endregion
	}
}