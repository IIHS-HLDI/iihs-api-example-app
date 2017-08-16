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

    public interface IGroupControl
    {
        int LayoutId { get; }
        void Render(View view, object model);
    }

    public abstract class GroupControl<T>:IGroupControl where T: class
    {
        public abstract int LayoutId
        {
            get;
        }

        protected abstract void RenderView(View view, T model);

        public void Render(View view, object model)
        {
            T modelTyped = model as T;
            RenderView(view, modelTyped);
        }


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

        protected TextView UpdateText(View view, int resourceId, string text, bool makeInvisibleIfEmpty)
        {
            TextView textView = view.FindViewById<TextView>(resourceId);
            if (textView != null)
            {
                if (string.IsNullOrEmpty(text))
                {
                    textView.Text = string.Empty;
                    textView.Visibility = ViewStates.Gone;
                }
                else
                {
                    textView.Text = text;
                    textView.Visibility = ViewStates.Visible;
                }
                
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