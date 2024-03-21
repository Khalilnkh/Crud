const productsSection = document.querySelector(".products");

const descBtn = document.querySelector(".desc-btn");
const ascBtn = document.querySelector(".asc-btn");
let pageBtns;
initializePagination()




descBtn.addEventListener('click', () => {
    setQueryParameter("order", "desc")
    renderProducts()
})

ascBtn.addEventListener('click', () => {
    setQueryParameter("order", "asc")
    renderProducts()
})



function renderProducts() {
    const page = getQueryParameter('page') ?? 1;
    const order = getQueryParameter('order') ?? 'desc';

    fetch(`https://localhost:7246/shop/filter?page=${page}&order=${order}`)
        .then(x => x.text())
        .then(x => {
            productsSection.innerHTML = '';
            productsSection.innerHTML = x;
            initializePagination();
        })
}

function setQueryParameter(key, value) {
    const url = new URL(window.location.href);
    url.searchParams.set(key, value);
    window.history.replaceState(null, null, url);
}


function getQueryParameter(key) {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(key);
}

function initializePagination() {
    pageBtns = document.querySelectorAll(".page-btn");

    pageBtns.forEach(x => {
        x.addEventListener('click', () => {
            const dataPage = x.getAttribute('data-page')
            setQueryParameter("page", dataPage)
            renderProducts()
        })
    })
}