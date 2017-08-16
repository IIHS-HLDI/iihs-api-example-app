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
    public abstract class GroupControlHeader<T>
    {
        public abstract int LayoutId
        {
            get;
        }

        public abstract void RenderView(View view, T model);

        protected TextView UpdateText(View view, int resourceId, string text)
        {            
            TextView textView = view.FindViewById<TextView>(resourceId);
            if (textView != null)
            {
                textView.Text = text;
                return textView;
            }
            else
            {
                return null;
            }
        }

        protected ImageView UpdateImage(View view, int imageResourceId, int srcImageResourceId)
        {
            ImageView imageView = view.FindViewById<ImageView>(imageResourceId);

            if (imageView != null)
            {
                imageView.SetImageResource(srcImageResourceId);
                return imageView;
            }
            else
            {
                return null;
            }
        }

    }

}