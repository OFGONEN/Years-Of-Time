/* Created by and for usage of FF Studios (2021). */

namespace FFStudio
{
    public class UI_Update_Text_Float_Format : UI_Update_Text< SharedFloatNotifier, float >
    {
        public string format = "{0:0.##}";
        
        protected override void OnSharedDataChange()
        {
			ui_Text.text = System.String.Format( format, sharedDataNotifier.SharedValue );
		}
    }
}
