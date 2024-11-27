using ArrendatarioPilasApp.Models;
using ArrendatarioPilasApp.Conexiones;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;
using System.Linq;
using Firebase.Database;
using System.Text.RegularExpressions;

namespace ArrendatarioPilasApp.Datos
{
    public class Dusuarios
    {
        #region Variables
        string rutafoto;
        public string _IdUsuario;
        #endregion
        #region Insertar
        //TODO: no lee el id usuario

        public async Task<string> InserUsuario(Musuario parametros)
        {
            try
            {
                var data = await Conexiones.Conexiones.firebase
               .Child("Usuario")
               .PostAsync(new Musuario()
               {
                   Admin = parametros.Admin,
                   //FotoUsuario = parametros.FotoUsuario,
                   Apellido = parametros.Apellido,
                   Contrasenia = parametros.Contrasenia,
                   Correo = parametros.Correo,
                   Estado = true,
                   Nombre = parametros.Nombre,
                   NumEncuesta = parametros.NumEncuesta,
                   EncuestasEliminadas = parametros.EncuestasEliminadas,
                   //IdAdministrador = ""
               });
                _IdUsuario = data.Key;
                return _IdUsuario;
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        #endregion
        #region Actualizar
        public async Task AddNumEncuesta(Musuario p)
        {
            var obtenerData = (await Conexiones.Conexiones.firebase
                .Child("Usuario")
                .OnceAsync<Musuario>()).Where(a => a.Object.Correo == p.Correo)
                .FirstOrDefault();

            var usuario = obtenerData.Object;
            usuario.NumEncuesta++;

            await Conexiones.Conexiones.firebase
               .Child("Usuario")
            .Child(obtenerData.Key)
               .PutAsync(usuario);
        }
        public async Task AddNumVaneoEncuesta(Musuario p)
        {
            try
            {


                var obtenerData = (await Conexiones.Conexiones.firebase
                    .Child("Usuario")
                    .OnceAsync<Musuario>()).Where(a => a.Key == p.IdUsuario)
                    .FirstOrDefault();

                var usuario = obtenerData.Object;
                usuario.EncuestasEliminadas++;

                await Conexiones.Conexiones.firebase
                   .Child("Usuario")
                   .Child(obtenerData.Key)
                   .PutAsync(usuario);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public async Task UsuarioVaneado(Musuario p)
        {
            try
            {



                var obtenerData = (await Conexiones.Conexiones.firebase
                    .Child("Usuario")
                    .OnceAsync<Musuario>()).Where(a => a.Key == p.IdUsuario)
                    .FirstOrDefault();

                var usuario = obtenerData.Object;
                usuario.Estado = false;

                await Conexiones.Conexiones.firebase
                   .Child("Usuario")
                   .Child(obtenerData.Key)
                   .PutAsync(usuario);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task EncuestaVaneada(Musuario p)
        {
            try
            {
                var obtenerData = (await Conexiones.Conexiones.firebase
                    .Child("Usuario")
                    .OnceAsync<Musuario>()).Where(a => a.Key == p.IdUsuario)
                    .FirstOrDefault();

                if (obtenerData != null)
                {
                    var encuesta = obtenerData.Object;
                    encuesta.Estado = false;

                    await Conexiones.Conexiones.firebase
                       .Child("Usuario")
                       .Child(obtenerData.Key)
                       .PutAsync(encuesta);
                }

            }
            catch (Exception e)
            {

                throw e;
            }

        }

        #endregion
        #region Mostrar

        List<Musuario> ListUsuario = new List<Musuario>();

        public async Task<string> ListaUsuarios()
        {
            var usuarioBdd = await Conexiones.Conexiones.firebase.
                Child("Usuario")
                .OrderByKey()
                .OnceAsync<Musuario>();

            if (usuarioBdd != null)
            {
                var usuarios = usuarioBdd.Select(item => new Musuario
                {
                    IdUsuario = item.Key,
                    Nombre = item.Object.Nombre,
                    Correo = item.Object.Correo,
                    Apellido = item.Object.Apellido,
                    Contrasenia = item.Object.Contrasenia,
                    Estado = item.Object.Estado,
                }).ToList();

                // 👈 Pon el breakpoint aquí
                //var json = System.Text.Json.JsonSerializer.Serialize(usuarios);

                // O aquí 👈 para ver el resultado final

                return usuarios.ToString();
            }

            return "[]";
        }
        public async Task<List<Musuario>> MostUsuario()
        {
            var usuarios = (await Conexiones.Conexiones.firebase
           .Child("Usuario")
           .OnceAsync<Musuario>()).Select(item => new Musuario
           {
               IdUsuario = item.Key,
               Admin = item.Object.Admin,
               Apellido = item.Object.Apellido,
               Contrasenia = item.Object.Contrasenia,
               FotoUsuario = item.Object.FotoUsuario,
               Correo = item.Object.Correo,
               Estado = item.Object.Estado,
               IdAdministrador = item.Object.IdAdministrador,
               Nombre = item.Object.Nombre,
               NumEncuesta = item.Object.NumEncuesta,
               EncuestasEliminadas = item.Object.EncuestasEliminadas

           }).ToList();

            // Convertir a JSON
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(usuarios);

            // Imprimir en la consola para depuración
            System.Diagnostics.Debug.WriteLine(json);

            return usuarios;
        }
        public async Task<List<Musuario>> MostUsuarioXcorreo(Musuario p)
        {
            return (await Conexiones.Conexiones.firebase
                .Child("Usuario")
                .OnceAsync<Musuario>())
                .Where(a => a.Object.Correo == p.Correo && a.Object.Estado == true)
                .Select(item => new Musuario
                {
                    IdUsuario = item.Key,
                    Admin = item.Object.Admin,
                    FotoUsuario = item.Object.FotoUsuario,
                    Apellido = item.Object.Apellido,
                    Contrasenia = item.Object.Contrasenia,
                    Correo = item.Object.Correo,
                    Estado = item.Object.Estado,
                    IdAdministrador = item.Object.IdAdministrador,
                    Nombre = item.Object.Nombre,
                    NumEncuesta = item.Object.NumEncuesta

                }).ToList();
        }


        public async Task<List<Musuario>> MostUsuarioXId(string p)
        {
            var usuario = await Conexiones.Conexiones.firebase
                .Child("Usuario")
            .Child(p)
                .OnceSingleAsync<Musuario>();
            return new List<Musuario> { usuario };
        }

        #endregion


        #region Multimedia

        public async Task<string> SubirFotoUser(Stream imagen, string nombreUsuario)
        {

            /*var storageImagen = await new FirebaseStorage("huecasapp-d8da1.appspot.com")
                .Child("Usuarios")
                .Child(nombreUsuario + ".jpg")
                .PutAsync(imagen);
            rutafoto = storageImagen;
            */
            return rutafoto;
        }

        #endregion
    }
}
