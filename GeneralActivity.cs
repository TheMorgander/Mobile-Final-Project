﻿using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using Android.Util;
using Android.Content;
using System.Collections.Generic;

namespace Final_Project
{
    [Activity(Label = "General Calculator", Theme = "@style/AppTheme")]
    public class GeneralActivity : AppCompatActivity
    {
        public string input;
        public string operand_1;
        public string operand_2;
        public string operation;
        public double result;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.general_activity);

            //Data pointing to layout elements
            Button general_button = FindViewById<Button>(Resource.Id.general_button);
            Button converter_button = FindViewById<Button>(Resource.Id.converter_button);
            Button history_button = FindViewById<Button>(Resource.Id.history_button);
            EditText text_result = FindViewById<EditText>(Resource.Id.result_text);
            Button button_1 = FindViewById<Button>(Resource.Id.button_1);
            Button button_2 = FindViewById<Button>(Resource.Id.button_2);
            Button button_3 = FindViewById<Button>(Resource.Id.button_3);
            Button button_4 = FindViewById<Button>(Resource.Id.button_4);
            Button button_5 = FindViewById<Button>(Resource.Id.button_5);
            Button button_6 = FindViewById<Button>(Resource.Id.button_6);
            Button button_7 = FindViewById<Button>(Resource.Id.button_7);
            Button button_8 = FindViewById<Button>(Resource.Id.button_8);
            Button button_9 = FindViewById<Button>(Resource.Id.button_9);
            Button button_0 = FindViewById<Button>(Resource.Id.button_0);
            Button button_decimal = FindViewById<Button>(Resource.Id.button_decimal);
            Button button_addition = FindViewById<Button>(Resource.Id.button_addition);
            Button button_subtraction = FindViewById<Button>(Resource.Id.button_subtraction);
            Button button_multiplication = FindViewById<Button>(Resource.Id.button_multiplication);
            Button button_division = FindViewById<Button>(Resource.Id.button_division);
            Button button_equals = FindViewById<Button>(Resource.Id.button_equals);
            Button button_clear = FindViewById<Button>(Resource.Id.button_clear);

            //Set display field to show 0
            text_result.Text = "0";

            //Restore saved data on screen rotate
            if (savedInstanceState != null)
            {
                input = savedInstanceState.GetString("input");
                operand_1 = savedInstanceState.GetString("operand_1");
                operand_2 = savedInstanceState.GetString("operand_2");
                operation = savedInstanceState.GetString("operation");
                result = savedInstanceState.GetDouble("result");
                text_result.Text = input;
            }

            //Change to converter activity
            converter_button.Click += delegate
            {
                Intent intent = new Intent(this, typeof(ConverterActivity));
                StartActivity(intent);
            };

            //Change to history activity
            history_button.Click += delegate
            {
                Intent intent = new Intent(this, typeof(HistoryActivity));
                StartActivity(intent);
            };

            //General calculator buttons
            button_1.Click += delegate
            {
                input += "1";
                text_result.Text = input;
            };
            button_2.Click += delegate
            {
                input += "2";
                text_result.Text = input;
            };
            button_3.Click += delegate
            {
                input += "3";
                text_result.Text = input;
            };
            button_4.Click += delegate
            {
                input += "4";
                text_result.Text = input;
            };
            button_5.Click += delegate
            {
                input += "5";
                text_result.Text = input;
            };
            button_6.Click += delegate
            {
                input += "6";
                text_result.Text = input;
            };
            button_7.Click += delegate
            {
                input += "7";
                text_result.Text = input;
            };
            button_8.Click += delegate
            {
                input += "8";
                text_result.Text = input;
            };
            button_9.Click += delegate
            {
                input += "9";
                text_result.Text = input;
            };
            button_0.Click += delegate
            {
                input += "0";
                text_result.Text = input;
            };
            button_decimal.Click += delegate
            {
                input += ".";
                text_result.Text = input;
            };
            button_addition.Click += delegate
            {
                operand_1 = input;
                operation = "+";
                text_result.Text = input + " +";
                input = string.Empty;
            };
            button_subtraction.Click += delegate
            {
                operand_1 = input;
                operation = "-";
                text_result.Text = input + " -";
                input = string.Empty;
            };
            button_multiplication.Click += delegate
            {
                operand_1 = input;
                operation = "*";
                text_result.Text = input + " *";
                input = string.Empty;
            };
            button_division.Click += delegate
            {
                operand_1 = input;
                operation = "/";
                text_result.Text = input + " /";
                input = string.Empty;
            };
            button_equals.Click += delegate
            {
                operand_2 = input;
                double num1;
                double num2;
                double.TryParse(operand_1, out num1);
                double.TryParse(operand_2, out num2);

                switch (operation)
                {
                    case "+":
                        {
                            result = num1 + num2;
                            text_result.Text = result.ToString();
                            break;
                        }
                    case "-":
                        {
                            result = num1 - num2;
                            text_result.Text = result.ToString();
                            break;
                        }
                    case "*":
                        {
                            result = num1 * num2;
                            text_result.Text = result.ToString();
                            break;
                        }
                    case "/":
                        {
                            if (num2 != 0)
                            {
                                result = num1 - num2;
                                text_result.Text = result.ToString();
                            }
                            else
                            {
                                text_result.Text = "Divide by 0 Error";
                            }
                            break;
                        }
                }

                Log.Debug("GeneralActivity", "General Operation Performed: " + operand_1 + operation + operand_2 + "=" + result.ToString());
                
                string operation_entry = operand_1 + operation + operand_2;
                string result_entry = result.ToString();
                
                Database.InsertOperation(Database.database, operation_entry, result_entry);
            };
            button_clear.Click += delegate
            {
                operand_1 = string.Empty;
                operand_2 = string.Empty;
                operation = string.Empty;
                input = string.Empty;
                text_result.Text = "0";
            };
        }

        //Save data on screen rotate
        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutString("input", input);
            outState.PutString("operand_1", operand_1);
            outState.PutString("operand_2", operand_2);
            outState.PutString("operation", operation);
            outState.PutDouble("result", result);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}