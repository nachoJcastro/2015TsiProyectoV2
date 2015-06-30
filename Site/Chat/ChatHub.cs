using Crosscutting.EntityTenant;
using Microsoft.AspNet.SignalR;
using Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Site.Chat
{
    //[HubName("chatHubX")]
    public class ChatHub : Hub
    {
        #region Data Members
        static List<UsuarioChat> UsuariosConectados = new List<UsuarioChat>();
        static List<DetalleMensaje> MensajesActuales = new List<DetalleMensaje>();

        //TiendaTenant t;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //static Dictionary<string, string> tenants_idChat = new Dictionary<string, string>();
        #endregion

        #region Methods
        // ME UNO A LA SALA
        public Task EntrarSala(string sala,string userName)
        {
            var id = Context.ConnectionId;
            if (UsuariosConectados.Count(x => x.IdConexion_Chat == id) == 0)
            {
                UsuariosConectados.Add(new UsuarioChat { IdConexion_Chat = id, NombreUsuario = userName });
                
                // send to caller
                Clients.Caller.onConnected(id, userName, UsuariosConectados, MensajesActuales);
                // send to all except caller client
                Clients.AllExcept(id).onNewUserConnected(id, userName);
                
                Clients.Group(sala).addMessage("Usuario " + userName + " ha entrado a la sala.");
                log.Warn("Paso Clients.AllExcept(id).onNewUserConnected(id, userName) - id conexion =" + id);

               
            }
            return Groups.Add(id, sala);
        }
        // SALGO DE LA SALA
        public Task LeaveRoom(string sala,string userName)
        {
            var user = UsuariosConectados.FirstOrDefault(x => x.NombreUsuario == userName);
            UsuariosConectados.Remove(user);
            Clients.Group(sala).addChatMessage("Usuario " + userName + " ha abandonado la sala.");
            return Groups.Remove(user.IdConexion_Chat, sala);
        }

        // INICIO LA SALA DEL TENANT
       /* public void Conectar_Site(string userName)
        {
            try
            {
               // var id = Context.ConnectionId;
                log.Warn("Entro ..Conectar_Site(string userName) " + userName);
               // t = System.Web.HttpContext.Current.Session["datosTienda"] as TiendaTenant;

                if(tenants_idChat==null){
                    log.Warn(" paso ...tenants_idChat==null)");
                }

                log.Warn("Antes de ...tenants_idChat.ContainsKey(t.Nombre.ToLower())");
                if (!tenants_idChat.ContainsKey("nachofacebook"))
                {
                    var id = Context.ConnectionId;
                    tenants_idChat.Add("nachofacebook", id.ToLower());

                    log.Warn("Paso tenants_idChat.Add - id conexion =" + id);

                    if (UsuariosConectados.Count(x => x.IdConexion_Chat == id) == 0)
                    {
                        UsuariosConectados.Add(new UsuarioChat { IdConexion_Chat = id, NombreUsuario = userName });
                        // send to caller
                        Clients.Caller.onConnected(id, userName, UsuariosConectados, MensajesActuales);
                        // send to all except caller client
                        Clients.AllExcept(id).onNewUserConnected(id, userName);

                        log.Warn("Paso Clients.AllExcept(id).onNewUserConnected(id, userName) - id conexion =" + id);
                    }

              }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw e;
            }
            
            
          //fin  
        }*/

        /* public void Conectar(string userName)
        {
            try
            {
                log.Warn("Entro ..Conectar(string userName) " + userName);
                //var id = Context.ConnectionId;
               // var t = System.Web.HttpContext.Current.Session["datosTienda"] as TiendaTenant;

              //  if (tenants_idChat.ContainsKey("nachofacebook"))
               // {
                    var id = tenants_idChat["nachofacebook"];
                    log.Warn("Id conexion " + id);
                    if (UsuariosConectados.Count(x => x.IdConexion_Chat == id) == 0)
                    {
                        UsuariosConectados.Add(new UsuarioChat { IdConexion_Chat = id, NombreUsuario = userName });
                        // send to caller
                        Clients.Caller.onConnected(id, userName, UsuariosConectados, MensajesActuales);
                        // send to all except caller client
                        Clients.AllExcept(id).onNewUserConnected(id, userName);
                        log.Warn("Paso Clients.AllExcept(id).onNewUserConnected(id, userName) - id conexion =" + id);
                    }
              //  }

            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw e;
            }
             
            
             //fin
        }*/

        public void MensajeSala(string sala,string userName, string message)
        {
            try
            {
                log.Warn("Salgo ..Mensaje Sala " + sala + " Usuario : " + userName + " Mensaje : " + message);
                // Agrego mensaje GRUPAL
                AgregarMensajeCache(sala,userName, message);
                // Broad cast message
                //Clients.All.messageReceived(userName, message);


               // Clients.Group(groupName, connectionId1, connectionId2).addChatMessage(name, message);

                Clients.Group(sala).addMessage(userName + " dice : " + message);
                log.Warn("Salgo ..Mensaje Sala " +sala+ " Usuario : " + userName + " Mensaje : " + message);
                foreach (var item in MensajesActuales)
                {
                    log.Warn("Usuario : "+ item.NombreUsuario.ToString()+ " Mensaje = " + item.Mensaje.ToString());  
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw e;
            }
         }

        public void MensajePrivado(string toUserId, string message)
        {
            try
            {
                log.Warn("Entro .. MensajePrivado(string toUserId, string message) a Usuario " + toUserId + "mensaje " + message);
                string fromUserId = Context.ConnectionId;
                var toUser = UsuariosConectados.FirstOrDefault(x => x.IdConexion_Chat == toUserId);
                var fromUser = UsuariosConectados.FirstOrDefault(x => x.IdConexion_Chat == fromUserId);
                if (toUser != null && fromUser != null)
                {
                    // send to 
                    Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.NombreUsuario, message);
                    // send to caller user
                    Clients.Caller.sendPrivateMessage(toUserId, fromUser.NombreUsuario, message);
                }
                log.Warn("Salgo .. MensajePrivado(string toUserId, string message) a Usuario " + toUserId + "mensaje " + message);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw e;
            }
        
        }

        public override Task OnDisconnected()
        {
            try
            {
                var item = UsuariosConectados.FirstOrDefault(x => x.IdConexion_Chat == Context.ConnectionId);
                if (item != null)
                {
                    UsuariosConectados.Remove(item);
                    var id = Context.ConnectionId;
                    Clients.All.onUserDisconnected(id, item.NombreUsuario);
                }
                /* }
                 else
                 {
                     // This server hasn't heard from the client in the last ~35 seconds.
                     // If SignalR is behind a load balancer with scaleout configured, 
                     // the client may still be connected to another SignalR server.
                 }*/
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw e;
            }
                
            return base.OnDisconnected();
        }

        #endregion

        #region private Messages
        // GUARDO MENSAJES
        private void AgregarMensajeCache(string sala, string userName, string message){
            try{
                log.Warn("Entro Msj Sala = " + sala + " usuario " + userName + "mensaje " + message);
                MensajesActuales.Add(new DetalleMensaje { Sala = sala, NombreUsuario = userName, Mensaje = message });
                if (MensajesActuales.Count > 100)
                    MensajesActuales.RemoveAt(0);
                log.Warn("Salgo Agrego Msj Sala = "+sala+" usuario "  + userName + "mensaje " + message);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw e;
            }
        }
        #endregion
    }

}