using ArrendatarioPilasApp.Models;
using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ArrendatarioPilasApp.Datos
{
    public class DCrearCuenta
    {
        
            string _correo;
            bool _admin;
            public string Correo
            {
                get => _correo;
                set
                {
                    _correo = value;

                }
            }
            public bool Admin
            {
                get => _admin;
                set
                {
                    _admin = value;

                }
            }



            public async Task CrearCuenta(string correo, string pass)
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Conexiones.Conexiones.WebapyFirebase));
                var auth =await authProvider.CreateUserWithEmailAndPasswordAsync(correo, pass);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(auth));

        }
            public async Task ValidCuenta(string correo, string pass)
            {
                try
                {
                    var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Conexiones.Conexiones.WebapyFirebase));
                    var auth = await authProvider.SignInWithEmailAndPasswordAsync(correo, pass);
                    /*vamos a generar un nuevo token*/
                    var serializartoken = JsonConvert.SerializeObject(auth);
                    Preferences.Set("MyFirebaseRefreshToken", serializartoken);
                    var guardarId = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.
                        Get("MyFirebaseRefreshToken", ""));
                    //await App.Current.MainPage
                    var refrescarCOntenido = await authProvider.RefreshAuthAsync(guardarId);
                    Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(refrescarCOntenido));

                    Correo = guardarId.User.Email;
                    var f = new Dusuarios();
                    var p = new Musuario();
                    p.Correo = Correo;
                    var data = await f.MostUsuarioXcorreo(p);
                    Admin = data[0].Admin;
                    if (Admin == false)
                    {
                        //Application.Current.MainPage = new NavigationPage(new Contenedor());
                    }
                    else
                    {
                        //Application.Current.MainPage = new NavigationPage(new MenuAdmin());
                    }

                }
                catch (Exception er)
                {
                    throw er;
                }

            }

        }
    
}
