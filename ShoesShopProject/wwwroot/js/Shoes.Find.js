window.addEventListener('scroll', () => {
    if (window.scrollY + window.innerHeight >= document.documentElement.scrollHeight) {
        loadCards(new URLSearchParams(new URL(window.location.href).search).toString());
    }
});
