

$('.card-checkbox').change(function (e) {
    console.log(e.target.getAttribute('key'))
    var productId = e.target.getAttribute('key')
    if (e.target.checked) {
        $('form.order-form').prepend(`<input type="hidden" name="idList" value="${productId}" />`);
    }
    else {
        $(`form.order-form input[value="${productId}"`).remove()
    }
})