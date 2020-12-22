using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Util;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using System.Threading.Tasks;
using Android.Widget;

[assembly: ExportRenderer(typeof(Entry), typeof(BirthDayApp.Droid.WithoutSolidCustomEntryRender))]
[assembly: ExportRenderer(typeof(Xamarin.Forms.DatePicker), typeof(BirthDayApp.Droid.WithoutSolidCustomDatePickerRender))]
[assembly: ExportRenderer(typeof(SearchBar), typeof(BirthDayApp.Droid.WithoutSolidCustomSearchBarRender))]
[assembly: ExportRenderer(typeof(NavigationPage), typeof(BirthDayApp.Droid.CNavigationPageRenderer))]
namespace BirthDayApp.Droid
{
    public class WithoutSolidCustomEntryRender:EntryRenderer
    {
        public WithoutSolidCustomEntryRender(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
    public class WithoutSolidCustomDatePickerRender : DatePickerRenderer
    {
        public WithoutSolidCustomDatePickerRender(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);
            Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
    public class WithoutSolidCustomSearchBarRender : SearchBarRenderer
    {
        public WithoutSolidCustomSearchBarRender(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            SearchView searchView = Control as SearchView;
            Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
            int searchIconId = Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
            ImageView searchViewIcon = (ImageView)searchView.FindViewById<ImageView>(searchIconId);
            searchViewIcon.SetImageDrawable(null);
        }
    }
    public class CNavigationPageRenderer : NavigationPageRenderer
    {
        public CNavigationPageRenderer(Context context) : base(context)
        {

        }

        protected override Task<bool> OnPushAsync(Page view, bool animated = false)
        {
            return base.OnPushAsync(view, animated);
        }

        protected override Task<bool> OnPopViewAsync(Page page, bool animated = false)
        {
            return base.OnPopViewAsync(page, animated);
        }

        protected override Task<bool> OnPopToRootAsync(Page page, bool animated = false)
        {
            return base.OnPopToRootAsync(page, animated);
        }
    }

}