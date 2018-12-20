using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.IO;
using System.Text;
using System;
using Android.Util;
using Android;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.Design.Widget;
using Android.Views;

namespace App3
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        View layout;
        static string[] PERMISSIONS_READWRITE = {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage
        };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            layout = FindViewById(Resource.Id.sample_main_layout);
            CheckWriteFliePermissions();

        }

        private void CheckWriteFliePermissions()
        {
            // Check if the Camera permission is already available.
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted
                || ActivityCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != (int)Permission.Granted)
            {

                // Write permission has not been granted
                RequestWriteFilePermissions();

            }
            else
            {
                // Wrie File permissions is already available
                Log.Info("MainActicity", "Write file permission has already been granted.");
                ProcessFiles();
            }
        }

        void RequestWriteFilePermissions()
        {
            Log.Info("MainActicity", "Write file permission has NOT been granted. Requesting permission.");

            if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.ReadExternalStorage)
                || ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.WriteExternalStorage))
            {
                // Provide an additional rationale to the user if the permission was not granted
                // and the user would benefit from additional context for the use of the permission.
                // For example if the user has previously denied the permission.
                Log.Info("MainActicity", "Displaying Write file permission rationale to provide additional context.");

                Snackbar.Make(layout, "Write file permission is needed to write files.",
                    Snackbar.LengthIndefinite).SetAction("OK", new Action<View>(delegate (View obj) {
                        ActivityCompat.RequestPermissions(this, PERMISSIONS_READWRITE, 0);
                    })).Show();
            }
            else
            {
                // Camera permission has not been granted yet. Request it directly.
                ActivityCompat.RequestPermissions(this, PERMISSIONS_READWRITE, 0);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == 0)
            {
                // Received permission result for camera permission.
                Log.Info("MainActivity", "Received response for write file permission request.");

                // Check if the only required permission has been granted
                if (PermissionUtil.VerifyPermissions(grantResults))
                {
                    // All required permissions have been granted, display contacts fragment.
                    ProcessFiles();
                }
                else
                {
                    Log.Info("MainActivity", "Contacts permissions were NOT granted.");
                }
            }

            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }

        public void ProcessFiles()
        {

            var exoFilepath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/test1.txt";
            Log.Debug("MainActivity", "exoFilepath"+ exoFilepath);
            FileStream fs = new FileStream(exoFilepath,
                               FileMode.Create,
                               FileAccess.ReadWrite,
                               FileShare.ReadWrite,
                               4096,
                               FileOptions.Asynchronous);


            byte[] bytes = Encoding.ASCII.GetBytes("FileStream Test");
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

        }
    }
}