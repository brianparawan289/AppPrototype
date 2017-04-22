using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Android.Provider;

namespace Login
{
    [Activity(Label = "FriendProfile")]
    public class FriendProfile : Activity
    {
        private static dynamic friend;
        private TextView tvFPName;
        private string AccessToken;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.FriendProfile);
            // Link the views
            tvFPName = (TextView) FindViewById(Resource.Id.tvFPName);
            TextView tvFPEmail = (TextView) FindViewById(Resource.Id.tvFPEmail);
            TextView tvFPPhone = (TextView) FindViewById(Resource.Id.tvFPPhone);
            Button btnFPEmail = (Button) FindViewById(Resource.Id.btnFPEmail);
            Button btnFPRemove = (Button) FindViewById(Resource.Id.btnFPRemove);
            Button btnSMS = (Button) FindViewById(Resource.Id.btnSMS);

            // Get the serialization
            string serializedFriend = Intent.GetStringExtra("friend");
            friend = JsonConvert.DeserializeObject(serializedFriend);

            AccessToken = Intent.GetStringExtra("token");
            

            tvFPName.Text = friend.FirstName + ' ' + friend.LastName;
            tvFPEmail.Text = friend.userName;
            tvFPPhone.Text = friend.PhoneNumber;

            //if (friend.PhoneNumber)
            //{
                
            //}

            btnFPEmail.Click += BtnFPEmail_Click;
            btnFPRemove.Click += BtnFPRemove_Click;
        }

        private async void BtnFPRemove_Click(object sender, EventArgs e)
        {
            /*Working on getting the delete request to work*/ 
        }

        private void BtnFPEmail_Click(object sender, EventArgs e)
        {
            
        }
    }
}