var dataTable;

$(document).ready(function () {
    cargarDatatable();
});


function cargarDatatable() {
    dataTable = $("#tblMessages").DataTable({
        "ajax": {
            // En la URL area/controlador/método
            "url": "/Admin/MsjContact/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "7%" },
            { "data": "lastName", "width": "7%" },
            { "data": "email", "width": "10%" },
            { "data": "message", "width": "60%" },
            {
                "data": "id",
                "render": function(data, type, full, meta) {
                    // 'full' contiene todos los datos de la fila
                    if (full.answered == false) {
                        return `<div class="text-center">
                                <a onclick=Change("/Admin/MsjContact/Change/${data}") class="btn btn-success text-white" style="cursor:pointer; width:140px;">
                                 ❌ No Gestionado </a>
                            </div>
                            `;
                    } else {
                        return `<div class="text-center">
                                <a onclick=Change("/Admin/MsjContact/Change/${data}") class="btn btn-success text-white" style="cursor:pointer; width:140px;">
                                ✔️ Gestionado </a>
                            </div>
                            `;
                    }
                }, "width": "11%"
            }
        ],
        "language": {
            "decimal": "",
            "emptyTable": "No hay registros",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    });
}

function Change(url) {
    $.ajax({
        type: 'Post', // Cambiado a 'GET' o el método que corresponda
        url: url,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            } else {
                toastr.error(data.message);
            }
        }
    });
}

