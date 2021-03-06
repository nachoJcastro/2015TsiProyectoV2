﻿using Crosscutting.EntityTareas;
using Crosscutting.EntityTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDALSubasta
    {
        void AgregarSubasta(String tenant,Subasta subasta); //Create
        Subasta ObtenerSubasta(String tenant,int subastaId); //Read
        List<Subasta> ObtenerSubastas(String tenant);
        void ActualizarSubasta(String tenant, Subasta subasta); //Update
        void EliminarSubasta(int subastaId); //Delete

        List<Oferta> ObtenerOfertas(int subastaId);
        List<Subasta> ObtenerSubastasByTipoProducto(String tenant, int idTipoProducto);

        void AltaSubasta(string tenant, Subasta subasta);
        Boolean ActualizarMonto(string valor_tenant, int id_subasta, double monto);

        // funciones correo
        List<Correo> correoCompraDirecta(String tenant,Subasta sub);


        List<Correo> correoCompraSubasta(String tenant, Subasta sub);


        Correo correoVendedor(String tenant,Subasta sub);
        Correo correoComprador(String tenant,Subasta sub);
        // funciones correo

        Correo correoSinOfertas(string tenant, Subasta subasta);

        List<Subasta> ObtenerSubastasActivas(string tenant);
        void AgregarImagen(string tenant, Imagen img);
        List<Imagen> ObtenerImagenes(string tenant, int id);
        int AgregarSubasta_ID(String tenant, Subasta subasta);
    
        List<Subasta> ObtenerSubastasActivasxCategoria(string tenant, int idCat);

        List<Subasta> ObtenerSubastasPorCriterio(string tenant, int idCat, string criterio, int? tipo, string min, string max);

        List<Subasta> ObtenerVentasbyUsuario(string tenant, int idUsuario);

        List<Subasta> ObtenerComprasbyUsuario(string tenant, int idUsuario);
    
        List<Subasta> ObtenerSubastasCompleto(string tenant, int idCat, string criterio, int? tipo, string min, string max, int page, int rows);
    }
}
