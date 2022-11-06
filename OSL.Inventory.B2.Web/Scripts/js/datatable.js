// DataTable
const DataTable = (tableId, url, options) => {

    const configFilterSearchOptions = () => {
        const configOption = {};
        for (const [key, value] of Object.entries(options())) {
            configOption[key] = $(value).val().toLowerCase();
        }
        return configOption;
    }

    const idsToBind = () => {
        let idsGrop = "";
        for (const [_, value] of Object.entries(options())) {
            idsGrop += `${value}, `;
        }
        return idsGrop.slice(0, -2);
    }

    const customDataTable = $(tableId).DataTable({
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
            url: url,
            type: "POST",
            dataType: "json",
            data: (data) => {
                const configOption = configFilterSearchOptions();
                return $.extend({}, data, configOption)
            }
        },
    });

    customDataTable.draw();

    $(idsToBind()).bind("keyup change clear", () => customDataTable.draw());
}
