$(function () {
    var qtdBeneficiarios = 0;

    $("#btn-add-beneficiario").click(function (e) {
        e.preventDefault();

        var blocoBeneficiario = '<div class="row">' +
            '    <div class="col-md-2">' +
            '        <input type="number" name="Telefones[' + qtdTelefones + '].DDD" maxlength="2" placeholder="DDD" class="form-control txt-ddd" />' +
            '    </div>' +
            '    <div class="col-md-6">' +
            '        <input type="text" name="Telefones[' + qtdTelefones + '].Numero" placeholder="Número" class="form-control txt-numero" />' +
            '    </div>' +
            '    <div class="col-md-3">' +
            '        <select name="Telefones[' + qtdTelefones + '].Tipo" class="form-control sel-tipo">' +
            '            <option value="0">Residencial</option>' +
            '            <option value="1">Comercial</option>' +
            '            <option value="2">Celular</option>' +
            '            <option value="3">Recado</option>' +
            '        </select>' +
            '    </div>' +
            '    <div class="col-md-1">' +
            '        <button class="btn btn-danger btn-remover-telefone">' +
            '            <span class="glyphicon glyphicon-trash"></span>' +
            '        </button>' +
            '    </div>' +
            '</div>';

        $("#div-telefones").append(blocoTelefone);

        qtdTelefones++;
    });

    $("#div-telefones").on("click", ".btn-remover-telefone", function (e) {
        e.preventDefault();

        $(this).parent().parent().remove();

        qtdTelefones--;

        $("#div-telefones .row").each(function (indice, elemento) {
            $(elemento).find(".txt-ddd").attr("name", "Telefones[" + indice + "].DDD");
            $(elemento).find(".txt-numero").attr("name", "Telefones[" + indice + "].Numero");
            $(elemento).find(".sel-tipo").attr("name", "Telefones[" + indice + "].Tipo");
        });
    });
});