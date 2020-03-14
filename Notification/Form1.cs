using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Notification
{
    public partial class Form1 : Form
    {
        public FirebaseMessaging messaging;
        public Form1()
        {
            InitializeComponent();

        }


        private void button2_Click(object sender, EventArgs e)
        {
            string SERVER_API_KEY = "AAAA9_hwQQs:APA91bHb-3C4gpIGK999bZTBl93xYf-pXl7M6gJTRROhmyahqKkgjdCZpUNqrl2EfjJkW0DXS2AZHZgy_2gu6oXyOSRM-IDw6rHZXnSkdqkxopJjS4hgZabJxaywpOHYOTrCxY_u6iZC";
            var SENDER_ID = "1065025028363";

            WebRequest tRequest;
            tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");

            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));
            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));


            var data = new
            {
                notification = new
                {
                    title = textBoxTitle.Text,
                    body = textBoxBody.Text,
                }
                  ,
                to = "/topics/" + textBoxTopic.Text
            };

            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(data);
            Byte[] byteArray = Encoding.UTF8.GetBytes(json);
            tRequest.ContentLength = byteArray.Length;

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();
            dataStream = tResponse.GetResponseStream();
            StreamReader tReader = new StreamReader(dataStream);
            labelResponse.Text = tReader.ReadToEnd();
            tReader.Close();
            dataStream.Close();
            tResponse.Close();
        }

    }
}
