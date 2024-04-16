document.addEventListener('DOMContentLoaded', function () {
    const fullPriceInput = document.getElementById('FullPrice');
    const saleInput = document.getElementById('Sale');
    const salePriceInput = document.getElementById('SalePrice');

    function calculateSalePrice() {
        const fullPriceValue = fullPriceInput.value;
        const saleValue = saleInput.value;

        // Перевірка на пусті значення
        if (!fullPriceValue || !saleValue) {
            salePriceInput.value = '';
            return;
        }

        const fullPrice = parseFloat(fullPriceValue);
        const sale = parseFloat(saleValue) / 100; // Знижка у відсотках
        const salePrice = fullPrice - (fullPrice * sale);

        // Округлення до двох знаків після коми
        salePriceInput.value = salePrice.toFixed(2);
    }

    // Слухачі подій для полів FullPrice та Sale
    fullPriceInput.addEventListener('input', calculateSalePrice);
    saleInput.addEventListener('input', calculateSalePrice);
});