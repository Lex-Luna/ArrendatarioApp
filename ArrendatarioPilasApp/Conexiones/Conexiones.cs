using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArrendatarioPilasApp.Conexiones
{
    public class Conexiones
    {
        public static FirebaseClient firebase = new FirebaseClient("https://arrendatariopilas-default-rtdb.firebaseio.com/");
        public const string WebapyFirebase = "\r\nAIzaSyCPoJ-mAfk_u74Q2TzIx6u6QtYhQSqBZBs";
        //public static string storage = "huecasapp-d8da1.appspot.com";

        //public const string GoogleMapsApiKey = "AIzaSyDkbzFWQC_jmqn6NY1_F92aOLVfoKbrRr8";
    }
}
