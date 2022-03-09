using Android.App;
using Android.Content;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;

namespace Final_Project
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            //Initalize history database
            SQLiteOpenHelper database_helper = new Database(this);
            Database.database = database_helper.ReadableDatabase;

            //Immediately default to general calculator
            Intent intent = new Intent(this, typeof(GeneralActivity));
            StartActivity(intent);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}