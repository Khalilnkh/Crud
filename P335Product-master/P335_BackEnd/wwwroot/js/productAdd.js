const imgInputs = document.querySelectorAll('.img-input');

imgInputs.forEach((imgInput, index) => {
    imgInput.addEventListener('change', (e) => {
        const img = e.target.files[0];
        const blobUrl = URL.createObjectURL(img);
        const imgPreview = document.querySelectorAll('.img-preview')[index];
        imgPreview.setAttribute('src', blobUrl);
    });
});