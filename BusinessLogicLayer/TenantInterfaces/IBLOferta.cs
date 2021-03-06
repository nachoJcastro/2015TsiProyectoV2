﻿using Crosscutting.EntityTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.TenantInterfaces
{
    public interface IBLOferta
    {
        //Operaciones CRUD
        void AgregarOferta(String tenant,Oferta oferta); //Insert
        Oferta ObtenerOferta(int ofertaId); //FindOne
        List<Oferta> ObtenerOfertas();//FindAllAs
        void ActualizarOferta(Oferta oferta); //Update
        void EliminarOferta(int ofertaId); //Delete

        List<Oferta> ObtenerOfertasByProducto(String tenant,int id_subasta);

         void correoOferta(String tenant, Oferta oferta);
    }
}
