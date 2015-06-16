using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Crosscutting.EntityTenant;
using MongoDB.Bson;

namespace DAL
{
    public class DALCalificacionEF : IDALCalificacion
    {
        MongoClient client;
        MongoServer server;
        MongoDatabase database;
        MongoCollection<CalificacionMongo> collection;

        public void ConectarDB(String tenant)
        {
            string connectionString = "mongodb://localhost";
            client = new MongoClient(connectionString);
            server = client.GetServer();
            database = server.GetDatabase(tenant);
            collection = database.GetCollection<CalificacionMongo>("Calificaciones");
        }

        public void AgregarCalificacion(String tenant, CalificacionMongo calificacion)
        {
            ConectarDB(tenant);
            this.collection.Insert(calificacion);
            //Articulo articulo = new Articulo { Titulo = this.txtTitulo.Text, Contenido = this.txtContenido.Text };
        }


        public CalificacionMongo ObtenerCalificacion(String tenant, int calificacionId)
        {
            ConectarDB(tenant);
            var query = Query<CalificacionMongo>.Where(c => Convert.ToInt32(c.Id) == calificacionId);
            CalificacionMongo calificacion = collection.FindOne(query);
            return calificacion;
        }


        public List<CalificacionMongo> ObtenerCalificacionesComprador(String tenant, int usuarioId)
        {
            ConectarDB(tenant);
            var query = Query<CalificacionMongo>.Where(c => c.id_Usuario == usuarioId && c.vendedor == false);
            List<CalificacionMongo> calificaciones = collection.Find(query).ToList<CalificacionMongo>();
            return calificaciones;
        }


        public List<CalificacionMongo> ObtenerCalificacionesVendedor(String tenant, int usuarioId)
        {
            ConectarDB(tenant);
            var query = Query<CalificacionMongo>.Where(c => c.id_Usuario == usuarioId && c.vendedor == true);
            List<CalificacionMongo> calificaciones = collection.Find(query).ToList<CalificacionMongo>();
            return calificaciones;
        }


        public CalificacionMongo ObtenerCalificacionDelVendedor(String tenant, int subastaId)
        {
            ConectarDB(tenant);
            var query = Query<CalificacionMongo>.Where(c => c.id_Subasta == subastaId && c.vendedor == true);
            CalificacionMongo calificacion = collection.FindOne(query);
            return calificacion;
        }


        public CalificacionMongo ObtenerCalificacionDelComprador(String tenant, int subastaId)
        {
            ConectarDB(tenant);
            var query = Query<CalificacionMongo>.Where(c => c.id_Subasta == subastaId && c.vendedor == false);
            CalificacionMongo calificacion = collection.FindOne(query);
            return calificacion;
        }

    }
}
