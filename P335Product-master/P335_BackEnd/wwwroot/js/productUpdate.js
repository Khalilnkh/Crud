
const imgInputs = document.querySelectorAll('.img-input');

imgInputs.forEach((imgInput, index) => {
    imgInput.addEventListener('change', (e) => {
        const img = e.target.files[0];
        const blobUrl = URL.createObjectURL(img);
        const imgPreview = document.querySelectorAll('.img-preview')[index];
        imgPreview.setAttribute('src', blobUrl);
    });
});

const deleteButtons = document.querySelectorAll('.delete-btn');

deleteButtons.forEach((deleteBtn) => {
    deleteBtn.addEventListener('click', (e) => {
        const imgPreview = deleteBtn.previousElementSibling;
        imgPreview.setAttribute('src', '');
        deleteBtn.remove();
    });
});
