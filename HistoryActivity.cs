﻿using Android.App;
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
    [Activity(Label = "History", Theme = "@style/AppTheme")]
    public class HistoryActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.history_activity);

            Button clear_history_button = FindViewById<Button>(Resource.Id.clear_history_button);

            //Grab history from database and convert to table format
            ListView history_list = FindViewById<ListView>(Resource.Id.history_listview);
            IList<HistoryEntry> history = Database.RetreiveOperations();
            history_list.Adapter = new HistoryBaseAdapter(this, history);

            //Change to general activity
            clear_history_button.Click += delegate
            {
                Database.ClearHistory();
                //Grab history from database and convert to table format
                ListView history_list = FindViewById<ListView>(Resource.Id.history_listview);
                IList<HistoryEntry> history = Database.RetreiveOperations();
                history_list.Adapter = new HistoryBaseAdapter(this, history);
            };

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}