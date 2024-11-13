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
            /*var funcion = new CuentaD();
            await funcion.CrearCuenta(TxtCorreo, TxtContraseña);*/
        }

        private async Task IniciarSesion()
        {
            /*var funcion = new CuentaD();
            await funcion.ValidCuenta(TxtCorreo, TxtContraseña);*/
        }

        public async Task<string> ObtenerIdUsuario()
        {
            try
            {
                /*var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapyFirebase));
                var guardarId = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                var refrescarCOntenido = await authProvider.RefreshAuthAsync(guardarId);*/
                /*Esete es el token que se guarda en el Usuario*/
                /*Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(refrescarCOntenido));
                _IdUsuario = guardarId.User.LocalId;*/
                //TODO: no se si de error en el futuro la linea de abajo
                return _IdUsuario;

            }
            catch (Exception)
            {
                await DisplayAlert("Alerta", "X tu seguridad la sesion se a cerrado", "Ok");

            }
            return _IdUsuario;
        }

        async Task NavContenedor()
        {
            /*await Navigation.PushAsync(new NavigationPage(new Contenedor()));*/
        }

        public async Task InsertarUsuario()
        {
            try
            {
                /*await SubirFotoUser();
                var funcion = new UsuarioD();
                var parametros = new UsuarioM();
                parametros.Apellido = TxtApellido;
                parametros.FotoUsuario = rutafoto;
                parametros.Contrasenia = TxtContraseña;
                parametros.Correo = TxtCorreo;
                parametros.Estado = true;
                parametros.IdAdministrador = "lUUpQuSwqibNTFqEq4LVQKK8kEG2";
                parametros.Nombre = TxtNombre;
                _IdUsuario = await funcion.InserUsuario(parametros);*/
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




        private async Task Volver()
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
