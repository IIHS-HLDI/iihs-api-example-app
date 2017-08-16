using Android.Views;
using Android.Widget;
using IIHSApiApp.Activities;
using IIHSApiApp.Models;

namespace IIHSApiApp.Controls
{
    public class GampGroupRatingHeader : GroupControl<RatingGroupHeader>
    {
        public override int LayoutId
        {
            get
            {
                return Resource.Layout.GampGroupRatingHeader;
            }
        }

        protected override void RenderView(View view, RatingGroupHeader model)
        {
            this.UpdateImage(view, Resource.Id.GAMP, model.ImageResourceId);
            this.UpdateText(view, Resource.Id.group, model.Text);
            this.UpdateText(view, Resource.Id.qualifierText, model.QualifyingText, true);
        }

    }

}