using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace IIHSApiApp.Controls
{
    public class GampChildRowItem: Java.Lang.Object
    {
        public int GampResourceId { get; set; }
        public string Text { get; set; }
    }

    public class DataChildRowItem : Java.Lang.Object
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }

    public static class RatingChildRowFactory
    {
        public static IGroupControl Create(object rowItem)
        {
            if (rowItem is DataChildRowItem)
                return new DataChildRow();

            if (rowItem is GampChildRowItem)
                return new GampChildRow();

            return null;
        }             
    }

    public class GampChildRow : GroupControl<GampChildRowItem>, IGroupControl
    {
        public override int LayoutId => Resource.Layout.GampChildRow;

        protected override void RenderView(View view, GampChildRowItem model)
        {
            this.UpdateText(view, Resource.Id.item, model.Text);
            this.UpdateImage(view, Resource.Id.GAMP, model.GampResourceId);
        }
    }

    public class DataChildRow : GroupControl<DataChildRowItem>, IGroupControl
    {
        public override int LayoutId => Resource.Layout.DataChildRow;

        protected override void RenderView(View view, DataChildRowItem model)
        {
            this.UpdateText(view, Resource.Id.item, model.Text);
            this.UpdateText(view, Resource.Id.DataValue, model.Value);
        }
    }
}