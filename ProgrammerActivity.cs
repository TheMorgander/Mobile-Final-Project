using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Final_Project
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class ProgrammerActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.programmer_activity);

            Button general_button = FindViewById<Button>(Resource.Id.general_button);
            Button converter_button = FindViewById<Button>(Resource.Id.converter_button);
            Button programmer_button = FindViewById<Button>(Resource.Id.programmer_button);
            Button history_button = FindViewById<Button>(Resource.Id.history_button);

            //Change to general activity
            general_button.Click += delegate
            {
                Intent intent = new Intent(this, typeof(GeneralActivity));
                StartActivity(intent);
            };

            //Change to converter activity
            converter_button.Click += delegate
            {
                Intent intent = new Intent(this, typeof(ConverterActivity));
                StartActivity(intent);
            };

            //Change to programmer activity
            programmer_button.Click += delegate
            {

            };

            //Change to history activity
            history_button.Click += delegate
            {
                Intent intent = new Intent(this, typeof(HistoryActivity));
                StartActivity(intent);
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}