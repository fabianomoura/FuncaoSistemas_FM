$(document).ready(function () {
    $("#doc").inputmask("999.999.999-99");
    $("#CPF").inputmask("999.999.999-99");
    $("#btn-salvar").on("click", function (e) {
        e.preventDefault();

        $.ajax({
            url: "/Cliente/Incluir"
            , data: $('#formCadastro *').serialize()
            , type: 'POST'
            , dataType: 'json'
            , success: function (json) {
                alert("Teste Sucesso");
                ModalDialog("Sucesso", json.responseText);
                $("formCadastro")[0].reset();
                return true;
            }
            , error: function (json) {
                alert("Teste Erro")
                if (json.status == 400) {
                    ModalDialog("Ocorreu um erro", json.responseText);
                    return false;
                } else if (json.status == 500) {
                    ModalDialog("Ocorreu um erro", "Erro Interno no Servidor");
                    return false;
                } else {
                    ModalDialog("Erro Desconhecido", json.responseText);
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

    var qtdBeneficiarios = 0;

    $("#btn-incluir-beneficiario").click(function (e) {
        e.preventDefault();

        var blocoBeneficiario = '<div class="row">' +
            '    <div class="col-md-4">' +
            '        <input type="text" name="Beneficiarios[' + qtdBeneficiarios + '].CPF" maxlength="14" placeholder="CPF" class="form-control txt-cpf"/>' +
            '    </div>' +
            '    <div class="col-md-6">' +
            '        <input type="text" name="Beneficiarios[' + qtdBeneficiarios + '].Nome" placeholder="Nome" class="form-control txt-nome" />' +
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

        $(this).parent().parent().remove();

        qtdBeneficiarios--;

        $("#div-beneficiarios .row").each(function (indice, elemento) {
            $(elemento).find(".txt-cpf").attr("name", "Beneficiarios[" + indice + "].CPF").inputmask("999.999.999-99");
            $(elemento).find(".txt-nome").attr("name", "Beneficiarios[" + indice + "].Nome");
        });
    });


});