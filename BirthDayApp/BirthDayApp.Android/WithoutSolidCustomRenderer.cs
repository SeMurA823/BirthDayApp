﻿using System;
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

[assembly: ExportRenderer(typeof(Entry), typeof(BirthDayApp.Droid.WithoutSolidCustomEntryRender))]
[assembly: ExportRenderer(typeof(DatePicker), typeof(BirthDayApp.Droid.WithoutSolidCustomDatePickerRender))]
[assembly: ExportRenderer(typeof(SearchBar), typeof(BirthDayApp.Droid.WithoutSolidCustomSearchBarRender))]
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
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
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
            Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }

}