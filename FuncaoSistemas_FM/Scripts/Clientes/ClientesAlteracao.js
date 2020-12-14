$(document).ready(function () {
    $("#doc").inputmask("999.999.999-99");
    $("#CPF").inputmask("999.999.999-99");
    $("#btn-salvar").bind("click", function (e) {
        e.preventDefault();

        $.ajax({
            url: "/Cliente/Alterar"
            , data: $('#formCadastro *').serialize()
            , type: 'POST'
            , dataType: 'json'
            , success: function (data) {
                var retorno = JSON.parse(data);
                if (retorno.Mensagem == null) {
                  retorno.Mensagem = "Alterado com sucesso";
                }
                ModalDialog("Sucesso", retorno.Mensagem);               

                $("formCadastro")[0].reset();
                return true;
            }
            , error: function (data) {
                var retorno = JSON.parse(data);
                if (data.status == 400) {
                    if (retorno.Mensagem == null) {
                      retorno.Mensagem = "Erro desconhecido ou dado informado inválido";
                    }
                    ModalDialog("Ocorreu um erro", retorno.Mensagem);
                    return false;
                } else if (data.status == 500) {
                    if (retorno.Mensagem == null) {
                      retorno.Mensagem = "Erro Interno no Servidor";
                    }
                    ModalDialog("Ocorreu um erro", retorno.Mensagem);
                    return false;
                } else {
                    if (retorno.Mensagem == null) {
                      retorno.Mensagem = "Erro desconhecido";
                    }
                    ModalDialog("Erro Desconhecido", retorno.Mensagem);
                    return false;
                }
            }
        });

        function ModalDialog(titulo, texto) {
            var random = Math.random().toString().replace('.', '');
            var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
                '        <div class="modal-dialog">                                                                                 ' +
                '            <div class="modal-content">                                                                            ' +
                '                <div class="modal-header">                                                                         ' +
                '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
                '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
                '                </div>                                                                                             ' +
                '                <div class="modal-body">                                                                           ' +
                '                    <p>' + texto + '</p>                                                                           ' +
                '                </div>                                                                                             ' +
                '                <div class="modal-footer">                                                                         ' +
                '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
                '                                                                                                                   ' +
                '                </div>                                                                                             ' +
                '            </div><!-- /.modal-content -->                                                                         ' +
                '  </div><!-- /.modal-dialog -->                                                                                    ' +
                '</div> <!-- /.modal -->                                                                                        ';

            $('body').append(texto);
            $('#' + random).modal('show');
        };
    });

    var qtdBeneficiarios = $("#div-beneficiarios .row").length;

    $("#btn-incluir-beneficiario").click(function (e) {
        e.preventDefault();

        var blocoBeneficiario = '<div class="row">' +
            '    <div class="col-md-4">' +
            '        <input type="text" name="Beneficiarios[' + qtdBeneficiarios + '].CPF" maxlength="14" placeholder="CPF" class="form-control txt-cpf" ID="Beneficiarios[' + qtdBeneficiarios + '].CPF"/>' +
            '    </div>' +
            '    <div class="col-md-6">' +
            '        <input type="text" name="Beneficiarios[' + qtdBeneficiarios + '].Nome" placeholder="Nome" class="form-control txt-nome" ID="Beneficiarios[' + qtdBeneficiarios + '].Nome"/>' +
            '    </div>' +
            '    <div class="col-md-2">' +
            '        <button class="btn btn-danger btn-remover-beneficiario">' +
            '            <span class="glyphicon glyphicon-trash"></span>' +
            '        </button>' +
            '    </div>' +
            '</div>';

        $("#div-beneficiarios").append(blocoBeneficiario);

        $("#div-beneficiarios .row").each(function (indice, elemento) {
            $(elemento).find(".txt-cpf").inputmask("999.999.999-99");
        });

        qtdBeneficiarios++;
    });

    $("#div-beneficiarios").on("click", ".btn-remover-beneficiario", function (e) {
        e.preventDefault();

        var id = $(this).attr("data-id");

        if (id)
            $.post("/Cliente/RemoverBeneficiario?id=" + id);

        $(this).parent().parent().remove();

        qtdBeneficiarios--;

        $("#div-beneficiarios .row").each(function (indice, elemento) {
            $(elemento).find(".txt-cpf").attr("name", "Beneficiarios[" + indice + "].CPF");
            $(elemento).find(".txt-nome").attr("name", "Beneficiarios[" + indice + "].Nome");
            $(elemento).find(".txt-cpf").attr("ID", "Beneficiarios[" + indice + "].CPF").inputmask("999.999.999-99");
            $(elemento).find(".txt-nome").attr("ID", "Beneficiarios[" + indice + "].Nome");
        });
    });


});