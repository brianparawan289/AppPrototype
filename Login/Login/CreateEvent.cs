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

namespace Login
{
    [Activity(Label = "CreateEvent")]
    public class CreateEvent : Activity
    {
        private static string AccessToken;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.CreateEvent);

            AccessToken = Intent.GetStringExtra("token");

            Button btnSelectLocation = (Button) FindViewById(Resource.Id.btnSelectLocation);
            btnSelectLocation.Click += BtnSelectLocation_Click;
        }

        private void BtnSelectLocation_Click(object sender, EventArgs e)
        {
            Intent activityIntent = new Intent(this, typeof(SelectLocation));
            StartActivityForResult(activityIntent, 0);
        }
    }
}