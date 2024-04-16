const container = document.getElementById("appender");
const fetchURL = window.location.protocol + "//" + window.location.host + "/Api/SearchCards/";
let PageIndex = 1;

function loadCards(querry = ``,numCards = 8) {

    let ToFetch;
    querry += `&page=${PageIndex}`
    ToFetch = `${fetchURL}?${querry}`;
    fetch(ToFetch).then(function (response) {
        return response.text();
    }).then(function (html) {
        container.insertAdjacentHTML('beforeend', html);
        PageIndex++;
    }).catch(function (error) {
        console.error('Помилка отримання даних:', error);
    });
}

