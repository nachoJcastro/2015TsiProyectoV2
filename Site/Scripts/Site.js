$(document).ready(function () {
    $('#CategoriaSeleccionada').change(function () {
        $.ajaxSetup({ cache: false });
        var selectedItem = $(this).val();
        if (selectedItem == "" || selectedItem == 0) {
            //Do nothing or hide...?
        } else {
            $.ajax({
                url: '@Url.Action("ProdList", "Subasta")',
                type: 'POST',
                data: { "idCategoria": selectedItem},
                dataType: 'json',
                success: function (response)
                {
                    var items = "";
                    $.each(response.ls, function (i, item) {
                        items += "<option value=\"" + data.AtributoId + "\">" + data.Nombre + "</option>";
                    });
                    $("#TipoProd").html(items);
                },
                error: function (error) {
                }
 
            });
        }
    })})