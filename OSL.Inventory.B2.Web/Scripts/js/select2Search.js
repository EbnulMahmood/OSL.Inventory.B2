const Select2Search = (elementId, url, placeHolderText) => {
    $(elementId).select2({
        placeholder: placeHolderText,
        allowClear: true,
        async: true,
        ajax: {
            type: "GET",
            dataType: 'json',
            url: url,
            data: function (params) {
                return {
                    term: params.term || '',
                    page: params.page || 1
                }
            },
            cache: true
        },
        processResults: function (data) {
            console.log(data);
            return {
                results: $.map(data, function (item) {
                    return {
                        text: item.Text,
                        id: item.Id,
                    }
                })
            }
        },
    });
}