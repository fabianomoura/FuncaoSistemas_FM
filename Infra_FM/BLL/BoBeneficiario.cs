﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra_FM.BLL
{
    public class BoBeneficiario
    {
        public long Incluir(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario ben = new DAL.DaoBeneficiario();
            return ben.Incluir(beneficiario);
        }
        
        public void Alterar(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario ben = new DAL.DaoBeneficiario();
            ben.Alterar(beneficiario);
        }

        public DML.Beneficiario Consultar(string Cpf, long Idcliente)
        {
            DAL.DaoBeneficiario ben = new DAL.DaoBeneficiario();
            return ben.Consultar(Cpf, Idcliente);
        }

        public void Excluir(string Cpf, long Idcliente)
        {
            DAL.DaoBeneficiario ben = new DAL.DaoBeneficiario();
            ben.Excluir(Cpf, Idcliente);
        }

        public List<DML.Beneficiario> Listar(long Idcliente)
        {
            DAL.DaoBeneficiario ben = new DAL.DaoBeneficiario();
            return ben.Listar(Idcliente);
        }

        public List<DML.Beneficiario> Pesquisa(long idcliente, int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DAL.DaoBeneficiario ben = new DAL.DaoBeneficiario();
            return ben.Pesquisa(idcliente, iniciarEm, quantidade, campoOrdenacao, crescente, out qtd);
        }

        public bool VerificarExistencia(string CPF, long idCliente)
        {
            string valor = CPF;
            if (CPF.Length > 11)
            {
                valor = valor.Replace(".", "");
                valor = valor.Replace("-", "");
            }

            DAL.DaoBeneficiario ben = new DAL.DaoBeneficiario();

            return ben.VerificarExistencia(valor, idCliente);
        }
    }
}