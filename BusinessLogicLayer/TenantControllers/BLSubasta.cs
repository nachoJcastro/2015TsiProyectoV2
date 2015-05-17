﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crosscutting.EntityTenant;
using BusinessLogicLayer.TenantInterfaces;
using DAL;
using Crosscutting.Enum;


namespace BusinessLogicLayer.TenantControllers
{
    public class BLSubasta : IBLSubasta{

        
        private IDALSubasta _dal = new DALSubastaEF();


        public BLSubasta(IDALSubasta dal)
        {
            this._dal = dal;
        }

        public BLSubasta() { }

        //********************
        public void AltaSubasta(string tenant, Subasta subasta) {

            _dal.AltaSubasta (tenant, subasta);
        }



        public void AgregarSubasta(String tenant,Subasta subasta)
        {
            _dal.AgregarSubasta(tenant,subasta);
        }


        public Subasta ObtenerSubasta(int subastaId)
        {
            return _dal.ObtenerSubasta(subastaId);
        }


        public List<Subasta> ObtenerSubastas(string tenant)
        {
            return _dal.ObtenerSubastas(tenant);
        }


        public void ActualizarSubasta(Subasta subasta)
        {
            _dal.ActualizarSubasta(subasta);
        }


        public void EliminarSubasta(int subastaId)
        {
            _dal.EliminarSubasta(subastaId);
        }


        public List<Oferta> ObtenerOfertas(int subastaId)
        {
            return _dal.ObtenerOfertas(subastaId);
        }


        public void FinalizarSubastaPorTiempo(int subastaId)
        {
            var subasta = ObtenerSubasta(subastaId);
            var ofertaGanadora = subasta.Oferta.LastOrDefault();
            subasta.id_Comprador = ofertaGanadora.id_Usuario;
            subasta.valor_Actual = ofertaGanadora.Monto;
            subasta.estado = EstadoTransaccion.Cerrada;
            subasta.finalizado = TipoFinalizacion.Subasta;
            _dal.ActualizarSubasta(subasta);

            //enviarMails(Uvendedor, Ucomprador);
        }


        public void FinalizarSubastaCompraDirecta(int subastaId)
        {
            var subasta = ObtenerSubasta(subastaId);
            subasta.estado = EstadoTransaccion.Cerrada;
            //subasta.id_Comprador = USUARIO LOGUEADO;
            subasta.valor_Actual = subasta.precio_Compra.Value;
            subasta.finalizado = TipoFinalizacion.Compra_directa;
            _dal.ActualizarSubasta(subasta);

            //enviarMails(Uvendedor, Ucomprador);
        }
    }
}
