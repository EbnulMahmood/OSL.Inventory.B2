// DataTable
$(document).ready(function () {
    // server side
    // Developer
    const myDevTable = $('#myDevTable').DataTable({
        processing: true,
        bServerSide: true,
        serverSide: true,
        sort: false,
        searching: false,
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
    myDevTable.draw();
    $("#search-input, #sort-by").bind("keyup change clear", () => myDevTable.draw());
});