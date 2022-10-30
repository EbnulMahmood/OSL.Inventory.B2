// DataTable
$(document).ready(function () {
    // server side
    // Developer
    const CategoryDataTable = $('#categoryDatatable').DataTable({
        processing: true,
        bServerSide: true,
        serverSide: true,
        sort: true,
        searching: false,
        columnDefs: [
            { orderable: false, targets: -1 }
        ],
        dom: '<"top"l>rt<"bottom"ip><"clear">',
        ajax: {
            url: "Category/ListCategoriesAsync",
            type: "POST",
            dataType: "json",
            data: (data) => {
                return $.extend({}, data, {
                    "filter_keywords": $("#search-input").val().toLowerCase(),
                    "filter_option": $("#sort-by").val().toLowerCase(),
                })
            }
        },
    });
    CategoryDataTable.draw();
    $("#search-input, #sort-by").bind("keyup change clear", () => CategoryDataTable.draw());
});