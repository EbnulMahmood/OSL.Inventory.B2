// modal
const showModal = (url, PlaceHolderElement) => {
    const decodeUrl = decodeURIComponent(url);
    $.get(decodeUrl).done(function (data) {
        PlaceHolderElement.html(data);
        PlaceHolderElement.find('.modal').modal('show');
    })
}
const modalAction = (PlaceHolderElement, that) => {
    const form = that.parents('.modal').find('form');
    const actionUrl = form.attr('action');
    const sendData = form.serialize();
    $.post(actionUrl, sendData).done(function (data) {
        console.log(data);
        PlaceHolderElement.find('.modal').modal('hide');
        window.location.reload();
    })
}

const Modal = (url, idElement, placeHolderId, btnDeleteClass) => {
    const placeHolderElement = $(placeHolderId);

    $(document).on('click', btnDeleteClass, function () {
        const id = $(this).attr(idElement);
        showModal(`${url}/${id}`, placeHolderElement);
    })

    placeHolderElement.on('click', '[data-bs-save="modal"]', function () {
        modalAction(placeHolderElement, $(this));
    })
}