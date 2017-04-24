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
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Login.Resources.layout;
using Newtonsoft.Json;
using Object = Java.Lang.Object;

namespace Login
{
    [Activity(Label = "Friends")]
    public class Friends : Activity
    {
        private static string AccessToken;
        TextView tvFriendsTest;
        ListView listFriends;
        private dynamic jsonData;
        protected  async override void OnCreate(Bundle savedInstanceState)
        {
            this.Title = "Friends List";
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Friends);

            AccessToken = Intent.GetStringExtra("token");

            tvFriendsTest = (TextView)FindViewById(Resource.Id.tvFriendsTest);
            listFriends = (ListView) FindViewById(Resource.Id.listViewFriends);
            listFriends.FastScrollEnabled = true;
            string url = GetString(Resource.String.IP) + "api/friends";
            //Get friends list
            try
            {
                
                string serializedResponse = await MakeGetRequest(url);
                jsonData = JsonConvert.DeserializeObject(serializedResponse);
            }
            catch (Exception e)
            {
                tvFriendsTest.Text = "ERROR"; 
            }

            if (jsonData == null)
            {
                tvFriendsTest.Text = "ERROR";
            }


            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, jsonData);
            // Bind the adapter to the ListView.
            listFriends.Adapter = adapter;
            listFriends.ItemClick += ListFriends_ItemClick;

        }

        protected async override void OnResume()
        {
            base.OnResume();

            string url = GetString(Resource.String.IP) + "api/friends";
            //Get friends list
            try
            {

                string serializedResponse = await MakeGetRequest(url);
                jsonData = JsonConvert.DeserializeObject(serializedResponse);
            }
            catch (Exception e)
            {
                tvFriendsTest.Text = "ERROR";
            }
            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, jsonData);
            // Bind the adapter to the ListView.
            listFriends.Adapter = adapter;
        }

        private void ListFriends_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent toFriendProfile = new Intent(this, typeof(FriendProfile));
            var selectedFriend = jsonData[e.Position];
            string serializedFriend = JsonConvert.SerializeObject(selectedFriend);
            toFriendProfile.PutExtra("friend", serializedFriend);
            toFriendProfile.PutExtra("token", AccessToken);
            StartActivity(toFriendProfile);
        }

        public static async Task<string> MakeGetRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + AccessToken);

            var response = await request.GetResponseAsync();
            var respStream = response.GetResponseStream();
            respStream.Flush();

            using (StreamReader sr = new StreamReader(respStream))
            {
                //Need to return this response 
                string strContent = sr.ReadToEnd();
                respStream = null;
                return strContent;
            }
        }
    }
}