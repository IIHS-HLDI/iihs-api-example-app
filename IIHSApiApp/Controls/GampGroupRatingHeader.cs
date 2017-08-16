using Android.Views;
using Android.Widget;
using IIHSApiApp.Activities;

namespace IIHSApiApp.Controls
{
    public class GampGroupRatingHeader : GroupControlHeader<RatingGroupHeader>
    {
        public override int LayoutId
        {
            get
            {
                return Resource.Layout.GampGroupRatingHeader;
            }
        }

        public override void RenderView(View view, RatingGroupHeader model)
        {
            this.UpdateImage(view, Resource.Id.GAMP, model.ImageResourceId);
            this.UpdateText(view, Resource.Id.group, model.Text);
        }

    }

}