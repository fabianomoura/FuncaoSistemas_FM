using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infra_FM.DML;

namespace Infra_FM.DAL
{
    internal class DaoBeneficiario : AcessoDados
    {
        internal long Incluir(DML.Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiario.Nome));                        
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IDCLIENTE", beneficiario.ClienteModelID));

            DataSet ds = base.Consultar("FI_SP_IncBeneficiario", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        internal DML.Beneficiario Consultar(string Cpf, long Idcliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", Cpf));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IDCLIENTE", Idcliente));

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiario", parametros);
            List<DML.Beneficiario> ben = Converter(ds);

            return ben.FirstOrDefault();
        }

        internal bool VerificarExistencia(string CPF, long idCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IDCLIENTE", idCliente));

            DataSet ds = base.Consultar("FI_SP_VerificaBeneficiario", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        internal List<Beneficiario> Pesquisa(long idcliente, int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("idCliente", idcliente));
            parametros.Add(new System.Data.SqlClient.SqlParameter("iniciarEm", iniciarEm));
            parametros.Add(new System.Data.SqlClient.SqlParameter("quantidade", quantidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("campoOrdenacao", campoOrdenacao));
            parametros.Add(new System.Data.SqlClient.SqlParameter("crescente", crescente));

            DataSet ds = base.Consultar("FI_SP_PesqCliente", parametros);
            List<DML.Beneficiario> ben = Converter(ds);

            int iQtd = 0;

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iQtd);

            qtd = iQtd;

            return ben;
        }

        internal List<DML.Beneficiario> Listar(long Idcliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
          
            parametros.Add(new System.Data.SqlClient.SqlParameter("IDCLIENTE", Idcliente));

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiarioV2", parametros);
            List<DML.Beneficiario> ben = Converter(ds);

            return ben;
        }

        internal void Alterar(DML.Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiario.Nome));            
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", beneficiario.Id));            

            base.Executar("FI_SP_AltBenef", parametros);
        }

        internal void Excluir(string Cpf, long Idcliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("IDCLIENTE", Idcliente));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", Cpf));            

            base.Executar("FI_SP_DelBeneficiario", parametros);
        }

        private List<DML.Beneficiario> Converter(DataSet ds)
        {
            List<DML.Beneficiario> lista = new List<DML.Beneficiario>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Beneficiario ben = new DML.Beneficiario();
                    ben.Id = row.Field<long>("Id");
                    ben.ClienteModelID = row.Field<int>("IDCLIENTE");
                    ben.Nome = row.Field<string>("Nome");                    
                    ben.CPF = row.Field<string>("CPF");
                    lista.Add(ben);
                }
            }

            return lista;
        }
    }
}
