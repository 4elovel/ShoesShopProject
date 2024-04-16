const tagsContainer = document.getElementById("tags-container");
const tagInput = document.getElementById("tag-input");


function createTag(tagName) {
    const tag = document.createElement('div');
    tag.classList.add('tag');

    const tagInput = document.createElement('input');
    tagInput.setAttribute('type', 'text');
    tagInput.setAttribute('value', tagName);
    tagInput.setAttribute('name', 'categories');
    tagInput.setAttribute('readonly', 'true');
    tagInput.classList.add('tag-input');

    const deleteIcon = document.createElement('span');
    deleteIcon.classList.add('delete-icon');
    deleteIcon.textContent = '×'; // Символ "хрестика"

    // Додаємо обробник події для видалення тега при кліку на хрестик
    deleteIcon.addEventListener('click', function (event) {
        event.stopPropagation(); // Зупиняємо подальше вспливання події
        tag.remove();
    });

    tag.appendChild(tagInput);
    tag.appendChild(deleteIcon);
    tagsContainer.appendChild(tag);
}


tagInput.addEventListener("keydown", function (event) {
    if (event.key === "Enter" && tagInput.value.trim() !== "") {
        event.preventDefault();
        createTag(tagInput.value.trim());
        tagInput.value = ""; // Очистити вміст введення
    }
});