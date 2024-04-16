$(document).ready(function () {
    var scaleCurve = mojs.easing.path('M0,100 L25,99.9999983 C26.2328835,75.0708847 19.7847843,0 100,0');
    var el = document.querySelector('#heart');
    var timeline = new mojs.Timeline();

    var tween1 = new mojs.Burst({
        parent: el,
        radius: { 0: 100 },
        angle: { 0: 45 },
        y: -10,
        count: 10,
        children: {
            shape: 'circle',
            radius: 30,
            fill: ['red', 'white'],
            strokeWidth: 15,
            duration: 500,
        }
    });

    var tween2 = new mojs.Tween({
        duration: 900,
        onUpdate: function (progress) {
            var scaleProgress = scaleCurve(progress);
            el.style.WebkitTransform = el.style.transform = 'scale3d(' + scaleProgress + ',' + scaleProgress + ',1)';
        }
    });

    var tween3 = new mojs.Burst({
        parent: el,
        radius: { 0: 100 },
        angle: { 0: -45 },
        y: -10,
        count: 10,
        children: {
            shape: 'circle',
            radius: 30,
            fill: ['white', 'red'],
            strokeWidth: 15,
            duration: 400,
        }
    });

    timeline.add(tween1, tween2, tween3);

    $('#heart').click(function () { 
        if ($(this).hasClass('active')) {
            $(this).removeClass('active');
        } else {
            timeline.play();
            $(this).addClass('active');
        }
    });
});

document.getElementById("heart").addEventListener("click", async function () {
    try {
        const currentPageUrl = window.location.pathname; // Отримати поточний URL-адресу сторінки
        const id = currentPageUrl.substring(currentPageUrl.lastIndexOf('/') + 1); // Отримати id з URL-адреси

        

        const response = await fetch(`/Shoes/AddToWishlist/${id}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({}) // Пусте тіло, оскільки нічого не потрібно передавати
        });

        if (response.ok) {
            $('#heart').trigger(function () { 
        if ($(this).hasClass('active')) {
            $(this).removeClass('active');
        } else {
            timeline.play();
            $(this).addClass('active');
        }
    });
        }
        else {
            if (response.status === 401) {
                const returnUrlParam = `ReturnUrl=${encodeURIComponent(currentPageUrl)}`;
                window.location.href = `/Identity/Account/Login?${returnUrlParam}`;
            } else {
                throw new Error('Network response was not ok');
            }
        }

        // Робіть що завгодно з отриманим відповіддю response
    } catch (error) {
        console.error('There was an error!', error);
    }
});