using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Final_Project
{
    [Activity(Label = "Conversions", Theme = "@style/AppTheme")]
    public class ConverterActivity : AppCompatActivity
    {
        protected string from_units;
        protected string to_units;
        protected double result;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.converter_activity);

            Button general_button = FindViewById<Button>(Resource.Id.general_button);
            Button converter_button = FindViewById<Button>(Resource.Id.converter_button);
            Button history_button = FindViewById<Button>(Resource.Id.history_button);
            Button conversion_button = FindViewById<Button>(Resource.Id.conversion_button);
            Spinner from_spinner = FindViewById<Spinner>(Resource.Id.from_spinner);
            Spinner to_spinner = FindViewById<Spinner>(Resource.Id.to_spinner);
            EditText input_text = FindViewById<EditText>(Resource.Id.input_text);
            EditText output_text = FindViewById<EditText>(Resource.Id.output_text);

            from_spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(FromSpinnerSelected);
            var from_adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.from_array, Android.Resource.Layout.SimpleSpinnerItem);
            from_adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            from_spinner.Adapter = from_adapter;

            to_spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(ToSpinnerSelected);
            var to_adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.to_array, Android.Resource.Layout.SimpleSpinnerItem);
            to_adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            to_spinner.Adapter = to_adapter;

            //Set display field to show 0
            input_text.Text = "0";

            //Restore saved data on screen rotate
            if (savedInstanceState != null)
            {
                from_units = savedInstanceState.GetString("from_units");
                to_units = savedInstanceState.GetString("to_units");
                result = savedInstanceState.GetDouble("result");
            }

            //Change to general activity
            general_button.Click += delegate
            {
                Intent intent = new Intent(this, typeof(GeneralActivity));
                StartActivity(intent);
            };

            //Change to history activity
            history_button.Click += delegate
            {
                Intent intent = new Intent(this, typeof(HistoryActivity));
                StartActivity(intent);
            };

            //Do conversion
            conversion_button.Click += delegate
            {
                double conversion_factor = 0;

                if (to_units.Length > 0 && from_units.Length > 0)
                {
                    if (to_units == "Kilometers" && from_units == "Miles")
                    {
                        conversion_factor = 1.61;
                    }

                    if (to_units == "Miles" && from_units == "Kilometers")
                    {
                        conversion_factor = 0.61;
                    }

                    double.TryParse(input_text.Text, out result);
                    result = result * conversion_factor;
                    output_text.Text = result.ToString() + " " + to_units;

                    Log.Debug("ConverterActivity", "Conversion Performed: " + input_text.Text + " " + from_units + " to " + to_units + " = " + result.ToString() + to_units);

                    string operation_entry = input_text.Text + " " + from_units + " to " + to_units;
                    string result_entry = result.ToString() + " " + to_units;

                    Database.InsertOperation(Database.database, operation_entry, result_entry);
                }
            };
        }

        private void FromSpinnerSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            from_units = spinner.GetItemAtPosition(e.Position).ToString();
        }

        private void ToSpinnerSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            to_units = spinner.GetItemAtPosition(e.Position).ToString();
        }

        //Save data on screen rotate
        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutString("from_units", from_units);
            outState.PutString("to_units", to_units);
            outState.PutDouble("result", result);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}