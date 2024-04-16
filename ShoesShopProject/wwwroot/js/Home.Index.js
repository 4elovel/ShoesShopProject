window.addEventListener('scroll', () => {
    if (window.scrollY + window.innerHeight >= document.documentElement.scrollHeight) {
        loadCards();
    }
});
document.getElementById("main-page").classList.add('active');