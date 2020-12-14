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
                if (ValidarCPFCliente(model.CPF, model.Id))
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
                        CPF = Models.Util.FormataCPF(model.CPF)
                    });

                    if (model.Beneficiarios.Count() > 0)
                    {
                        long ClienteID = model.Id;

                        foreach (var beneficiario in model.Beneficiarios)
                        {
                            if (ValidarCPFBeneficiario(beneficiario.CPF, ClienteID, beneficiario.Id))
                            {
                                beneficiario.Id = boben.Incluir(new Beneficiario()
                                {
                                    CPF = Models.Util.FormataCPF(beneficiario.CPF),
                                    Nome = beneficiario.Nome,
                                    ClienteModelID = ClienteID
                                });
                            }
                            else
                            {
                                Response.StatusCode = 400;
                                var msg = "CPF do Beneficiario " + beneficiario.Nome + " inválido ou já cadastrado";
                                return Json(msg);
                            }
                        }
                    }

                    Response.StatusCode = 200;
                    return Json("Cadastro efetuado com sucesso");
                }
                else
                {
                    Response.StatusCode = 400;
                    return Json("CPF Inválido ou já cadastrado!");
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
            long idCliente = model.Id;

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
                if (ValidarCPFCliente(model.CPF, model.Id))
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
                        foreach (var beneficiario in model.Beneficiarios)
                        {
                            if (ValidarCPFBeneficiario(beneficiario.CPF, model.Id, beneficiario.Id))
                            {
                                if (beneficiario.Id == 0)
                                {
                                    beneficiario.Id = boben.Incluir(new Beneficiario()
                                    {
                                        CPF = Models.Util.FormataCPF(beneficiario.CPF),
                                        Nome = beneficiario.Nome,
                                        ClienteModelID = model.Id
                                    });
                                }
                                else
                                {
                                    boben.Alterar(new Beneficiario()
                                    {
                                        Id = beneficiario.Id,
                                        CPF = Models.Util.FormataCPF(beneficiario.CPF),
                                        Nome = beneficiario.Nome
                                    });

                                }
                            }
                            else
                            {
                                Response.StatusCode = 400;
                                var msg = "CPF do Beneficiario " + beneficiario.Nome + " inválido ou já cadastrado";
                                return Json(msg);
                            }
                        }
                    }
                    else
                    {
                        foreach (var benef in boben.Listar(idCliente))
                        {
                            BeneficiarioModel ben1 = new BeneficiarioModel();
                            ben1.CPF = Models.Util.FormataCPF(benef.CPF);
                            ben1.Nome = benef.Nome;
                            ben1.Id = benef.Id;
                            ben1.ClienteModelID = benef.ClienteModelID;
                            model.Beneficiarios.Add(ben1);
                        }
                    }

                    return Json("Cadastro alterado com sucesso");
                }
                else
                {
                    Response.StatusCode = 400;
                    return Json("CPF inválido ou já cadastrado!");
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
                    CPF = Models.Util.FormataCPF(cliente.CPF)
                };

                if (model.Beneficiarios.Count() > 0)
                {
                    foreach (var beneficiario in model.Beneficiarios)
                    {
                        if (beneficiario.Id == 0)
                        {
                            beneficiario.Id = boben.Incluir(new Beneficiario()
                            {
                                CPF = Models.Util.FormataCPF(beneficiario.CPF),
                                Nome = beneficiario.Nome,
                                ClienteModelID = cliente.Id
                            });
                        }
                        else
                        {
                            boben.Alterar(new Beneficiario()
                            {
                                Id = beneficiario.Id,
                                CPF = Models.Util.FormataCPF(beneficiario.CPF),
                                Nome = beneficiario.Nome
                            });
                        }
                    }
                }
                else
                {
                    foreach (var benef in cliente.Beneficiarios)
                    {
                        BeneficiarioModel ben1 = new BeneficiarioModel();
                        ben1.CPF = Models.Util.FormataCPF(benef.CPF);
                        ben1.Nome = benef.Nome;
                        ben1.Id = benef.Id;
                        ben1.ClienteModelID = benef.ClienteModelID;
                        model.Beneficiarios.Add(ben1);
                    }
                }

            }
            return View(model);
        }

        [HttpPost]
        public void RemoverBeneficiario(int id)
        {
            BoBeneficiario boben = new BoBeneficiario();

            boben.Remover(id);
        }

        [HttpPost]
        public Boolean ValidarCPF(string Cpf)
        {
            return Models.Util.ValidaCPF(Cpf);
        }

        [HttpPost]
        public Boolean ValidarCPFCliente(string Cpf, long Id)
        {
            if (Models.Util.ValidaCPF(Cpf))
            {
                BoCliente bo = new BoCliente();
                if (Id != 0)
                {
                    return !bo.VerificarExistencia(Models.Util.FormataCPF(Cpf), Id);
                }
                else
                {
                    return !bo.VerificarExistencia(Models.Util.FormataCPF(Cpf));
                }
            }
            else
                return false;
        }

        [HttpPost]
        public Boolean ValidarCPFBeneficiario(string Cpf, long Idcliente, long IdBeneficiario)
        {
            if (Models.Util.ValidaCPF(Cpf))
            {
                BoBeneficiario boben = new BoBeneficiario();
                if (IdBeneficiario != 0)
                {
                    return !boben.VerificarExistencia(Models.Util.FormataCPF(Cpf), Idcliente, IdBeneficiario);
                }
                else
                {
                    return !boben.VerificarExistencia(Models.Util.FormataCPF(Cpf), Idcliente);
                }
            }
            else
                return false;
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