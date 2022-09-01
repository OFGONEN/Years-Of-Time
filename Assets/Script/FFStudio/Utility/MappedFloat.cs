/* Created by and for usage of FF Studios (2021). */

using System;

namespace FFStudio
{
    public class MappedFloat : Attribute
    {
		public MappedFloat()
		{
			Minimum      = 0;
			Maximum      = 100;
			RangeCeiling = 100;
			SuffixLabel  = "%";
		}
        
        public MappedFloat( float minimum, float rangeCeiling, float maximum, string suffixLabel = "%" )
        {
			Minimum      = minimum;
			Maximum      = maximum;
			RangeCeiling = rangeCeiling;
			SuffixLabel  = suffixLabel;
		}
        
        public float value;
        public float Minimum { get; private set; }
        public float Maximum { get; private set; }
		public float RangeCeiling { get; private set; }
		public string SuffixLabel { get; private set; }
    }
}
