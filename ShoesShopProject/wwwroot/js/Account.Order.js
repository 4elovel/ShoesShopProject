
function decreaseCounter(counterClass) {
    const counterElements = document.querySelectorAll('.' + counterClass);
    counterElements.forEach(function (counterElement) {
        let currentValue = parseInt(counterElement.value);
        if (currentValue > 1) {
            currentValue--;
            counterElement.value = currentValue;
            counterElement.setAttribute("value", currentValue);
        }
    });
    updateTotal();
}

function increaseCounter(counterClass) {
    const counterElements = document.querySelectorAll('.' + counterClass);
    counterElements.forEach(function (counterElement) {
        let currentValue = parseInt(counterElement.value);
        currentValue++;
        counterElement.value = currentValue;
        counterElement.setAttribute("value", currentValue);
    });
    updateTotal();
}
async function deleteGoodFromOrder(slug, elementClass) {
    try {
        const response = await fetch(`/Account/DeleteGoodFromOrder/${slug}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
            })
        });
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const elements = document.querySelectorAll('.' + elementClass);
        elements.forEach(element => {
            element.remove();
        });
        updateTotal();
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
}
$(document).ready(function () {
    $('.add-order-button').click(function () {
        var temp = document.getElementsByClassName("textfield")[0];
        var countryCode = window.getComputedStyle(temp, ':before').content;

        var phoneNumber = $('#phonenumber').val();

        var products = [];
        var slugs = document.getElementsByClassName("slug");
        var sizes = document.getElementsByClassName("size");
        var counts = document.getElementsByClassName("count");
        for (var i = 0; i < slugs.length; i++) {
            var bufSlug = slugs[i].getAttribute("value");
            var bufSize = sizes[i].getAttribute("value");
            var bufCount = counts[i].getAttribute("value");
            products.push({ Slug: bufSlug, Size: bufSize, Count: bufCount });
        }

        $.ajax({
            url: '/Account/Order',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                CountryCode: countryCode,
                PhoneNumber: phoneNumber,
                Products: products
            }),
            success: function (response) {
                $('.alert').addClass('show');
            }
        });
    });
});
function calculateTotal() {
    var counts = [];
    var prices = [];

    $('.count').each(function () {
        counts.push(parseInt($(this).val()));
    });
    $('.sale-price').each(function () {
        prices.push(parseFloat($(this).attr('value')));
    });
    if (counts.length === prices.length && counts.length > 0) {
        var total = 0;
        for (var i = 0; i < counts.length; i++) {
            total += counts[i] * prices[i];
        }
        return total;
    }
    return 0;
}
var total = calculateTotal();
if (total !== null) {
    console.log('Total:', total);
} else {

    console.log('Invalid input');
}
function updateTotal() {
    var total = calculateTotal();

    if (total !== null) {
        $('#total').text(total.toFixed(2));
    } else {
        console.log('Invalid input');
    }
}
$(document).ready(function () {
    updateTotal();
});