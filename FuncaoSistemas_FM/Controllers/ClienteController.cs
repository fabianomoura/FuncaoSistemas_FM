using FuncaoSistemas_FM.Models;
using Infra_FM.BLL;
using Infra_FM.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FuncaoSistemas_FM.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                if (ValidaCPF(model.CPF))
                {
                    model.Id = bo.Incluir(new Cliente()
                    {
                        CEP = model.CEP,
                        Cidade = model.Cidade,
                        Email = model.Email,
                        Estado = model.Estado,
                        Logradouro = model.Logradouro,
                        Nacionalidade = model.Nacionalidade,
                        Nome = model.Nome,
                        Sobrenome = model.Sobrenome,
                        Telefone = model.Telefone,
                        CPF = model.CPF
                    });

                    if (model.Beneficiarios.Count() > 0)
                    {
                        BoBeneficiario boben = new BoBeneficiario();

                        long ClienteID = model.Id;

                        foreach (var beneficiario in model.Beneficiarios)
                        {
                            beneficiario.Id = boben.Incluir(new Beneficiario()
                            {
                                CPF = beneficiario.CPF,
                                Nome = beneficiario.Nome,
                                ClienteModelID = ClienteID
                            });
                        }
                    }

                    Response.StatusCode = 200;
                    return Json("Cadastro efetuado com sucesso");
                }
                else
                {
                    Response.StatusCode = 400;
                    return Json("CPF Inválido!");
                }
            }
        }

        [HttpPost]
        public JsonResult Salvar(ClienteModel model)
        {
            return Incluir(model);
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                if (ValidaCPF(model.CPF))
                {
                    bo.Alterar(new Cliente()
                    {
                        Id = model.Id,
                        CEP = model.CEP,
                        Cidade = model.Cidade,
                        Email = model.Email,
                        Estado = model.Estado,
                        Logradouro = model.Logradouro,
                        Nacionalidade = model.Nacionalidade,
                        Nome = model.Nome,
                        Sobrenome = model.Sobrenome,
                        Telefone = model.Telefone,
                        CPF = FormataCPF(model.CPF)
                    });

                    if (model.Beneficiarios.Count() > 0)
                    {
                        BoBeneficiario boben = new BoBeneficiario();
                        boben.Excluir(model.Id);

                        foreach (var beneficiario in model.Beneficiarios)
                        {
                            beneficiario.Id = boben.Incluir(new Beneficiario()
                            {
                                CPF = beneficiario.CPF,
                                Nome = beneficiario.Nome,
                                ClienteModelID = beneficiario.ClienteModelID
                            });
                        }
                    }

                    return Json("Cadastro alterado com sucesso");
                }
                else
                {
                    Response.StatusCode = 400;
                    return Json("CPF inválido!");
                }


            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            BoBeneficiario boben = new BoBeneficiario();

            Models.ClienteModel model = null;

            if (cliente != null)
            {
                cliente.Beneficiarios = boben.Listar(id);
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    CPF = cliente.CPF
                };

                if (model.Beneficiarios.Count() > 0)
                {
                    boben.Excluir(model.Id);

                    foreach (var beneficiario in model.Beneficiarios)
                    {
                        beneficiario.Id = boben.Incluir(new Beneficiario()
                        {
                            CPF = beneficiario.CPF,
                            Nome = beneficiario.Nome,
                            ClienteModelID = beneficiario.ClienteModelID
                        });
                    }
                }

            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                Response.StatusCode = 200;
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult AdicionaBeneficiario(BeneficiarioModel beneficiario, ClienteModel cliente)
        {
            try
            {
                cliente.Beneficiarios.Add(beneficiario);
                int qtd = cliente.Beneficiarios.Count();
                Response.StatusCode = 200;
                return Json(new { Result = "OK", Records = cliente.Beneficiarios, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ExcluiBeneficiario(BeneficiarioModel beneficiario, ClienteModel cliente)
        {
            try
            {
                cliente.Beneficiarios.Remove(beneficiario);
                int qtd = cliente.Beneficiarios.Count();
                Response.StatusCode = 200;
                return Json(new { Result = "OK", Records = cliente.Beneficiarios, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult AlteraBeneficiario(BeneficiarioModel beneficiario, ClienteModel cliente)
        {
            try
            {
                cliente.Beneficiarios.Remove(beneficiario);
                cliente.Beneficiarios.Add(beneficiario);
                int qtd = cliente.Beneficiarios.Count();
                Response.StatusCode = 200;
                return Json(new { Result = "OK", Records = cliente.Beneficiarios, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        private bool ValidaCPF(string Cpf)
        {
            string valor;
            if (Cpf.Length > 11)
                valor = FormataCPF(Cpf);
            else
                valor = Cpf;
            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(valor[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
              if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }

        private string FormataCPF(string Cpf)
        {
            string valor = Cpf;
            valor = valor.Replace(".", "");
            valor = valor.Replace("-", "");
            return valor;
        }
    }
}