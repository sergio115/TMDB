using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Util;
using Android.Graphics.Drawables;
using TMDB.Controls;
using TMDB.Droid.Renders;

[assembly: ExportRenderer(typeof(SearchBarCustom), typeof(SearchBarCustomAndroidRender))]
namespace TMDB.Droid.Renders
{
    public class SearchBarCustomAndroidRender : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if(e.NewElement != null)
            {                
                var plateId = Resources.GetIdentifier("android:id/search_plate", null, null);
                var plate = Control.FindViewById(plateId);
                plate.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }

        public SearchBarCustomAndroidRender(Context context) : base(context)
        {

        }
    }
}