using ArrendatarioPilasApp.Datos;
using ArrendatarioPilasApp.Models;
using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ArrendatarioPilasApp.ViewModels
{
    public class VMCrearCorreo : BaseVM
    {
        #region CONSTRUCTOR
        public VMCrearCorreo(INavigation navigation)
        {
            Navigation = navigation;
            /*CerrarSecion();*/
        }
        #endregion
        #region VARIABLES
        string _IdUsuario;
        string txtNombre;
        string txtApellido;
        string txtCorreo;
        string txtContraseña;
        string rutafoto;
        //MediaFile foto;
        #endregion
        #region OBJETOS 
        public string TxtNombre { get { return txtNombre; } set { SetValue(ref txtNombre, value); } }
        public string TxtApellido { get { return txtApellido; } set { SetValue(ref txtApellido, value); } }
        public string TxtCorreo { get { return txtCorreo; } set { SetValue(ref txtCorreo, value); } }
        public string TxtContraseña { get { return txtContraseña; } set { SetValue(ref txtContraseña, value); } }

        #endregion
        #region PROCESOS
        void CerrarSecion()
        {
            Preferences.Remove("MyFirebaseRefreshToken");

        }
        public async Task btnCrearcuenta()
        {

            try
            {
                    if (!string.IsNullOrEmpty(TxtNombre))
                    {
                        if (!string.IsNullOrEmpty(TxtApellido))
                        {
                            if (!string.IsNullOrEmpty(TxtCorreo))
                            {
                                if (!string.IsNullOrEmpty(TxtContraseña))
                                {

                                    await CrearCuenta();
                                    await ObtenerIdUsuario();
                                    await InsertarUsuario();
                                    await IniciarSesion();
                                    await NavContenedor();
                                }
                                else
                                    await DisplayAlert("Alerta", "Agregue una contraseña", "OK");
                            }
                            else
                                await DisplayAlert("Alerta", "Agregue un correo", "OK");
                        }
                        else
                            await DisplayAlert("Alerta", "Agregue un apellido", "OK");

                    }
                    else
                        await DisplayAlert("Alerta", "Agregue un nombre", "OK");
                
            }
            catch (Exception e)
            {

                throw e;
            }




        }

        public async Task CrearCuenta()
        {
            var funcion = new DCrearCuenta();
            await funcion.CrearCuenta(TxtCorreo, TxtContraseña);
        }

        private async Task IniciarSesion()
        {
            var funcion = new DCrearCuenta();
            await funcion.ValidCuenta(TxtCorreo, TxtContraseña);
        }

        public async Task<string> ObtenerIdUsuario()
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Conexiones.Conexiones.WebapyFirebase));
                var guardarId = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                var refrescarCOntenido = await authProvider.RefreshAuthAsync(guardarId);
                /*Esete es el token que se guarda en el Usuario*/
                /*
                */
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(refrescarCOntenido));
                _IdUsuario = guardarId.User.LocalId;

                respuesta.Exito = 1;
                return _IdUsuario;

            }
            catch (Exception ex)
            {
                {
                    await DisplayAlert("Alerta", "X tu seguridad la sesion se a cerrado", "Ok");
                    respuesta.Exito = 0;
                    respuesta.Mensaje= ex.Message;

                }
                return _IdUsuario;
            }
        }

        public async Task NavContenedor()
        {
            /*await Navigation.PushAsync(new NavigationPage(new Contenedor()));*/
        }

        async Task InsertarUsuario()
        {
            try
            {
                //await SubirFo
                //toUser();
                var funcion = new Dusuarios();
                var parametros = new Musuario();
                parametros.Apellido = TxtApellido;
                //parametros.FotoUsuario = rutafoto;
                parametros.Contrasenia = TxtContraseña;
                parametros.Correo = TxtCorreo;
                parametros.Estado = true;
                //parametros.IdAdministrador = "lUUpQuSwqibNTFqEq4LVQKK8kEG2";
                parametros.Nombre = TxtNombre;
                _IdUsuario = await funcion.InserUsuario(parametros);
            }
            catch (Exception er)
            {

                await DisplayAlert("Alerta", "no se pudo crear el usuario", "Ok" + er);
            }


        }

        public async Task TomarFoto()
        {
            /*var camara = new StoreCameraMediaOptions();
            camara.PhotoSize = PhotoSize.Medium;
            camara.SaveToAlbum = true;*/
            try
            {
                /*foto = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(camara);
                if (foto != null)
                {
                    Foto = ImageSource.FromStream(() =>
                    {
                        return foto.GetStream();
                    });

                }*/
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task SubirFotoUser()
        {
            try
            {
                /*var funcion = new UsuarioD();
                if (rutafoto == null)
                {
                    rutafoto = await funcion.SubirFotoUser(foto.GetStream(), txtNombre);
                }
                else
                {
                    await DisplayAlert("No tomaste foto", "La seccion foto saldra vacia", "Ok");
                    rutafoto = "No hay foto";
                }*/
            }
            catch (Exception)
            {
                await DisplayAlert("No tomaste foto", "La seccion foto saldra vacia", "Ok");
                rutafoto = "No hay foto";
            }

        }




        public async Task Volver()
        {
            await Navigation.PopAsync();
        }



        #endregion
        #region COMANDOS
        public ICommand btnCrearcuentaComand => new Command(async () => await btnCrearcuenta());
        public ICommand Volvercomamd => new Command(async () => await Volver());
        public ICommand TomarFotoComand => new Command(async () => await TomarFoto());

        #endregion
    }
}
