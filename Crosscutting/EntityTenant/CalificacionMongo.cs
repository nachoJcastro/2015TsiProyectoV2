using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Crosscutting.EntityTenant
{
    public class CalificacionMongo
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
       // public ObjectId Id { get; set; }

        public bool vendedor { get; set; }

        public string observacion { get; set; }

        public int puntaje { get; set; }

        public int id_Usuario { get; set; }

        public int id_Subasta { get; set; }
    }
}