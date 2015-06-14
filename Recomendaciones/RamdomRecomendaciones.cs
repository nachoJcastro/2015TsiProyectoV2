using BusinessLogicLayer.Controllers;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.TenantControllers;
using BusinessLogicLayer.TenantInterfaces;
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

        public override List<Subasta> getRecomendaciones(String tenant, int idUsuario, List<TprodXCant> tipoProd, int cantRetorno)
        {

            IBLTipoProducto tprodIBL = new BLTipoProducto();
            IBLSubasta subIBL = new BLSubasta();
            Random rnd = new Random();
            List<Subasta> subastasRecomendadas = new List<Subasta>();

            if (tipoProd.Count > 0)
            {
                List<Subasta> subastasRecomendadasAux = new List<Subasta>();
                int totalDeNFavoritos = 0;

                foreach (var auxTotal in tipoProd)
                {
                    totalDeNFavoritos = totalDeNFavoritos + auxTotal.cantidad;
                }

                foreach (var item in tipoProd)
                {
                    int calculo = item.cantidad * cantRetorno / totalDeNFavoritos;//calcula el "porcentaje" de ese tipo de producto en la lista

                    subastasRecomendadasAux = subIBL.ObtenerSubastasByTipoProducto(tenant, item.idTProd);

                    for (int i = 0; i < calculo; i++)
                    {
                        subastasRecomendadas.Add(subastasRecomendadasAux.ElementAt(rnd.Next(subastasRecomendadasAux.Count - 1)));//obtiene subastas de forma ramdom
                    }
                }
            }
            else { }//obtener recomendaciones genericas

            return subastasRecomendadas;
        }
    }
}