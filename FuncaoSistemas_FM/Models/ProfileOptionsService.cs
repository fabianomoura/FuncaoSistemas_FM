using System.Collections.Generic;
using System.Web.Mvc;

namespace FuncaoSistemas_FM.Models
{
    public static class ProfileOptionsService
    {
        public static ICollection<SelectListItem> ListaEstados()
        {
            List<SelectListItem> items = new List<SelectListItem>();            
            items.Insert(0, (new SelectListItem() { Value = "", Text = "Selecione" }));
            items.Insert(1, (new SelectListItem() { Value = "AC", Text = "Acre" }));
            items.Insert(2, (new SelectListItem() { Value = "AL", Text = "Alagoas" }));
            items.Insert(3, (new SelectListItem() { Value = "AP", Text = "Amapá" }));
            items.Insert(4, (new SelectListItem() { Value = "AM", Text = "Amazonas" }));
            items.Insert(5, (new SelectListItem() { Value = "BA", Text = "Bahia" }));
            items.Insert(6, (new SelectListItem() { Value = "CE", Text = "Ceará" }));
            items.Insert(7, (new SelectListItem() { Value = "DF", Text = "Distrito Federal" }));
            items.Insert(8, (new SelectListItem() { Value = "ES", Text = "Espírito Santo" }));
            items.Insert(9, (new SelectListItem() { Value = "GO", Text = "Goiás" }));
            items.Insert(10, (new SelectListItem() { Value = "MA", Text = "Maranhão" }));
            items.Insert(11, (new SelectListItem() { Value = "MT", Text = "Mato Grosso" }));
            items.Insert(12, (new SelectListItem() { Value = "MS", Text = "Mato Grosso do Sul" }));
            items.Insert(13, (new SelectListItem() { Value = "MG", Text = "Minas Gerais" }));
            items.Insert(14, (new SelectListItem() { Value = "PA", Text = "Pará" }));
            items.Insert(15, (new SelectListItem() { Value = "PB", Text = "Paraíba" }));
            items.Insert(16, (new SelectListItem() { Value = "PR", Text = "Paraná" }));
            items.Insert(17, (new SelectListItem() { Value = "PE", Text = "Pernambuco" }));
            items.Insert(18, (new SelectListItem() { Value = "PI", Text = "Piauí" }));
            items.Insert(19, (new SelectListItem() { Value = "RJ", Text = "Rio de Janeiro" }));
            items.Insert(20, (new SelectListItem() { Value = "RN", Text = "Rio Grande do Norte" }));
            items.Insert(21, (new SelectListItem() { Value = "RS", Text = "Rio Grande do Sul" }));
            items.Insert(22, (new SelectListItem() { Value = "RO", Text = "Rondônia" }));
            items.Insert(23, (new SelectListItem() { Value = "RR", Text = "Roraima" }));
            items.Insert(24, (new SelectListItem() { Value = "SC", Text = "Santa Catarina" }));
            items.Insert(25, (new SelectListItem() { Value = "SP", Text = "São Paulo" }));
            items.Insert(26, (new SelectListItem() { Value = "SE", Text = "Sergipe" }));
            items.Insert(27, (new SelectListItem() { Value = "TO", Text = "Tocantins" }));

            return items;
        }
    }
}