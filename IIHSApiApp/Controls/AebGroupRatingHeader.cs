using Android.Views;
using Android.Widget;
using IIHSApiApp.Activities;

namespace IIHSApiApp.Controls
{
    public class AebGroupRatingHeader:GroupControlHeader<RatingGroupHeader>
    {
        public override int LayoutId
        {
            get
            {
                return Resource.Layout.AebGroupRatingHeader;
            }
        }

        public override void RenderView(View view, RatingGroupHeader model)
        {
            this.UpdateText(view, Resource.Id.aebText, model.RatingText);
            this.UpdateText(view, Resource.Id.aebSubtext, model.RatingSubtext);
            this.UpdateImage(view, Resource.Id.carating, model.ImageResourceId);
            this.UpdateText(view, Resource.Id.group, model.Text);            
        }
    }

}