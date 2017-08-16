using Android.Views;
using Android.Widget;
using IIHSApiApp.Activities;
using IIHSApiApp.Models;

namespace IIHSApiApp.Controls
{
    public class AebGroupRatingHeader:GroupControl<RatingGroupHeader>
    {
        public override int LayoutId
        {
            get
            {
                return Resource.Layout.AebGroupRatingHeader;
            }
        }

        protected override void RenderView(View view, RatingGroupHeader model)
        {
            this.UpdateText(view, Resource.Id.aebText, model.RatingText);
            this.UpdateText(view, Resource.Id.aebSubtext, model.QualifyingText);
            this.UpdateImage(view, Resource.Id.carating, model.ImageResourceId);
            this.UpdateText(view, Resource.Id.group, model.Text);            
        }
    }

}