window.onload = load;

function load() {

    GetCategories();

}

function AddCategory() {
    var newCategory = new Object;
    newCategory.nombre = $('#nombreCategoria').val();
    newCategory.precio = $('#precioCategoria').val();


    $.ajax({
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(newCategory),
        url: "https://localhost:44310/api/Categoria/AddCategoria",
        success: function (res) {
            location.reload();
        },
        error: function (data) {

        }
    });
}

function UpdateCategory() {
    var newCategory = new Object;
    newCategory.numero = $('#updateNumeroCategoria').val();
    newCategory.nombre = $('#updateNombreCategoria').val();
    newCategory.precio = $('#updatePrecioCategoria').val();


    $.ajax({
        method: "PUT",
        contentType: "application/json",
        data: JSON.stringify(newCategory),
        url: "https://localhost:44310/api/Categoria/UpdateCategoria",
        success: function (res) {
            location.reload();
        },
        error: function (data) {

        }
    });
}

function DeleteCategory() {


    $.ajax({
        method: "DELETE",
        contentType: "application/json",
        url: "https://localhost:44310/api/Categoria/DeleteCategoria?numCategoria=" + $('#updateNumeroCategoria').val(),
        success: function (res) {
            location.reload();
        },
        error: function (data) {

        }
    });
}

function GetCategories() {

    $.ajax({
        method: "GET",
        contentType: "application/json",
        url: "https://localhost:44310/api/Categoria/GetAllCategorias",
        success: function (res) {
            generateCategoriesTable(JSON.parse(res));
        },
        error: function (data) {

        }
    })
}


function generateCategoriesTable(categories) {

    var categoriesData = [];


    categories.forEach(element => {

        var aux = [];

        aux.push(String(element.numero));
        aux.push(String(element.nombre));
        aux.push("$" + String(element.precio));


        categoriesData.push(aux);
    })

    $(document).ready(function () {
        var table = $('#categoriesTable').DataTable({
            data: categoriesData,
            responsive: true,
            ordering: true,
            language: {
                lengthMenu: "",
                info: "",
                infoEmpty: "",
                infoFiltered: "",
                infoPostFix: "",
                loadingRecords: "",
                zeroRecords: "",
                emptyTable: "",
                paginate: {
                    first: "Inicio",
                    previous: "Anterior",
                    next: "Siguiente",
                    last: "Final"
                },
                search: "",
                searchPlaceholder: "Buscar...",
                select: {
                    cells: "",
                    columns: "",
                    rows: "",
                },

            },
        })
            .columns.adjust()
            .responsive.recalc();
        $('#categoriesTable tr').click(function () {
            var data = table.row($(this)).data();
            openUpdateCategoryModal(data[0], data[1], data[2].substring(1));
        })

    });
};

function openUpdateCategoryModal(numC, NombC, PreC) {
    $("#updateCategoryModal").modal({
        fadeDuration: 100,
        clickClose: true,
        showClose: false,
        closeExisting: false,
        modalClass: "w-1/2 rounded-lg bg-gray-800 px-2 py-4"
    });

    $('#updateNumeroCategoria').val(numC);
    $('#updateNombreCategoria').val(NombC);
    $('#updatePrecioCategoria').val(PreC);
}
