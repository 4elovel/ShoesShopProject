function changeOrderStatus(selectElement) {
    const orderId = selectElement.options[selectElement.selectedIndex].getAttribute('data-order-id');
    const status = selectElement.options[selectElement.selectedIndex].getAttribute('data-status');

    // Створюємо об'єкт з даними для передачі на сервер
    const data = {
        id: orderId,
        status: status
    };

    // Виконуємо POST-запит до сервера
    fetch('/Orders/ChangeStatus', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        body: `id=${orderId}&status=${status}`
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            // Обробляємо успішний відгук від сервера
            console.log('Order status changed successfully:', data);
        })
        .catch(error => {
            // Обробляємо помилки
            console.error('There was a problem with the fetch operation:', error.message);
        });
}