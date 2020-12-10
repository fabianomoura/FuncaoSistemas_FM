using FuncaoSistemas_FM.Models;
using Infra_FM.BLL;
using Infra_FM.DML;
using System;
using System.Collections.Generic;
using System.Linq;
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
            BoBeneficiario boben = new BoBeneficiario();

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
                if (Models.Util.ValidaCPF(model.CPF))
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
            BoBeneficiario boben = new BoBeneficiario();

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
                if (Models.Util.ValidaCPF(model.CPF))
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
                        CPF = Models.Util.FormataCPF(model.CPF)
                    });

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
    }
}