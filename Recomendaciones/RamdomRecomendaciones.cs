using BusinessLogicLayer;
using BusinessLogicLayer.Controllers;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.TenantControllers;
using BusinessLogicLayer.TenantInterfaces;
using Crosscutting.Entity;
using Crosscutting.EntityTenant;
using Crosscutting.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recomendaciones
{
    public class RamdomRecomendaciones : Recomendaciones
    {

        public override List<Subasta> getRecomendaciones(int tenantId, int idUsuario, List<TprodXCant> tipoProd, int cantRetorno)
        {
            IBLUsuario usuIBL = new BLUsuario();
            IBLTipoProducto tprodIBL = new BLTipoProducto();
            IBLSubasta subIBL = new BLSubasta();
            IBLTiendaVirtual tienda = new BLTiendaVirtual();
            Random rnd = new Random();
            List<Subasta> subastasRecomendadas = new List<Subasta>();
            List<Subasta> subastasRecomendadasAux = new List<Subasta>();
            TiendaVirtualDTO tenant = tienda.ObtenerTienda(tenantId);

            if (tipoProd.Count > 0)
            {

                int totalDeNFavoritos = 0;

                foreach (var auxTotal in tipoProd)
                {
                    totalDeNFavoritos = totalDeNFavoritos + auxTotal.cantidad;
                }

                foreach (var item in tipoProd)
                {
                    int calculo = item.cantidad * cantRetorno / totalDeNFavoritos;//calcula el "porcentaje" de ese tipo de producto en la lista

                    subastasRecomendadasAux = subIBL.ObtenerSubastasByTipoProducto(tenant.Dominio, item.idTProd);
                    if (subastasRecomendadasAux.Count > 0)
                    {
                        for (int i = 0; i < calculo; i++)
                        {
                            if (subastasRecomendadasAux.Count == 1)
                            {
                                subastasRecomendadas.Add(subastasRecomendadasAux.ElementAt(rnd.Next(subastasRecomendadasAux.Count)));//obtiene subastas de forma ramdom
                            }
                            else
                            {
                                subastasRecomendadas.Add(subastasRecomendadasAux.ElementAt(rnd.Next(subastasRecomendadasAux.Count - 1)));//obtiene subastas de forma ramdom
                            }
                        }
                    }
                }
            }
            if (subastasRecomendadas.Count == 0)//obtener recomendaciones genericas
            {
                var usuario = usuIBL.GetUsuario(tenant.Dominio, idUsuario);

                if (usuario != null)
                {
                    char[] delimiterChars = { ';' };
                    string preferencias = usuario.preferencias;
                    string[] categoriasPreferentes = preferencias.Split(delimiterChars);
                    int calculo = cantRetorno / categoriasPreferentes.Length;

                    IBLCategoria catIBL = new BLCategoria();
                    
                    List<TipoProductoDTO> listTipoProd = new List<TipoProductoDTO>();
                    List<Subasta> listSubastas = new List<Subasta>();

                    foreach (string c in categoriasPreferentes)
                    {
                        var categoria = catIBL.ObtenerCategoriaByNombre(tenantId, c);
                       // listTipoProd = tprodIBL.ObtenerTipoPorCategoria(categoria.CategoriaId);

                       // foreach (var tProd in listTipoProd)
//{
                            listSubastas = subIBL.ObtenerSubastasActivasxCategoria(tenant.Dominio, categoria.CategoriaId);
                            foreach (var sub in listSubastas)
                            {
                                subastasRecomendadasAux.Add(sub);
                            }
                        }

                        if (subastasRecomendadasAux.Count > 0)
                        {
                            for (int i = 0; i < calculo; i++)
                            {
                                if (subastasRecomendadasAux.Count == 1)
                                {
                                    subastasRecomendadas.Add(subastasRecomendadasAux.ElementAt(rnd.Next(subastasRecomendadasAux.Count)));//obtiene subastas de forma ramdom
                                }
                                else 
                                {
                                    subastasRecomendadas.Add(subastasRecomendadasAux.ElementAt(rnd.Next(subastasRecomendadasAux.Count - 1)));//obtiene subastas de forma ramdom
                                }
                            }
                        }
                        subastasRecomendadasAux = null;
                    }
                }
                if (subastasRecomendadas.Count == 0)
                {
                    List<Subasta> listSubastas = subIBL.ObtenerSubastasActivas(tenant.Dominio);
                    if (listSubastas.Count > 0)
                    {
                        for (int i = 0; i < cantRetorno; i++)
                        {
                            if (listSubastas.Count == 1)
                            {
                                subastasRecomendadas.Add(listSubastas.ElementAt(rnd.Next(listSubastas.Count)));//obtiene subastas de forma ramdom
                            }
                            else
                            {
                                subastasRecomendadas.Add(listSubastas.ElementAt(rnd.Next(listSubastas.Count - 1)));//obtiene subastas de forma ramdom
                            }
                        }
                    }
                }

            
            return subastasRecomendadas;
        }
    }
}