var modelo = {
        
    NombreServicio: '',
    DescripcionServicio: '',
    CodigoServicio: '',
    VManoObra: null,          
    PorcentajeComision: null, 
    materiales: []
}


function render(){

    //Limpierza de pantalla
    $("#listado > tbody").html("");
    //Enanche Formulario
    //$("#CodigoServicio").val(modelo.codigoServicio);
    //$("#NombreServicio").val(modelo.nombreServicio);

    modelo.materiales.forEach(material => {
        var _trMaterial = $("<tr>")
        
        var _tdMaterial = $("<td>").append(material.nombre)
        _trMaterial.append(_tdMaterial);

        var _tdUM = $("<td>").append(material.unidadMedida)
        _trMaterial.append(_tdUM);

        var _tdPrecio = $("<td>").append(material.precio.toMoney())
        _trMaterial.append(_tdPrecio);

        var _tdStockA = $("<td>").append(material.stock)
        _trMaterial.append(_tdStockA);



        $("#listado > tbody").append(_trMaterial);
    });
}

$(function () {
            
    


    $('.btn-guardar-material').on('click', function(){
        const material = {
            materialId: $('#material').val(),
            cantidad: $("#cantidad").val(),
            nombre: $('#material option:selected').text(),
            stock: $('#material option:selected').data('stocka'),
            unidadMedida: $('#material option:selected').data('um'),
            precio: parseInt($('#material option:selected').data('valor')),
            valorTotal: parseInt($('#material option:selected').data('valor')) * parseInt($("#cantidad").val()) 
        }
        
        modelo.materiales.push(material);


        render();

        console.log(modelo);
    });






    
    $('#frm-ingreso-servicios').on("submit", function () {
        
        var mdl = $(this).serializeFormJSON();
        Object.assign(modelo, mdl);
        
        $.ajax({
            type: "POST",
            url: "/api/mantenedor/servicios",
            data: JSON.stringify(modelo),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) { 
                console.log(data); 
                location.reload();
                
            },
            failure: function (errMsg) {
                console.log(errMsg);
            }
        });

        return false;
    });

});