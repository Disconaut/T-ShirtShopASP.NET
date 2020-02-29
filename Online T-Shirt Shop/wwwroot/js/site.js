// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var addPop = document.getElementById("add-popup");
var addItemBut = document.getElementById("add-button");
var acceptBut = document.getElementById("accept-button");
var closeAddBut = document.getElementById("close-add");
var shopTable = document.getElementById("new-arrivals-table-body");

function generateTableItem(name, iconPath, price) {
    let td = document.createElement("td");
    td.classList.add("shop-item-container");
    let div = document.createElement("div");
    div.classList.add("shop-item");

    let itemImg = document.createElement("img");
    itemImg.src = `${iconPath}`;
    itemImg.alt = "I'm a product";
    itemImg.classList.add("item-img");

    let itemDetails = document.createElement("div");
    itemDetails.classList.add("item-details");

    let itemName = document.createElement("h4");
    itemName.innerText = name;
    itemName.classList.add("item-name");

    let itemPrice = document.createElement("span");
    itemPrice.innerText = `$${price}`;
    itemPrice.classList.add("item-price");

    itemDetails.appendChild(itemName);
    itemDetails.appendChild(itemPrice);

    div.appendChild(itemImg);
    div.appendChild(itemDetails);

    td.appendChild(div);

    return td;
}

function openAddPopup() {
    addPop.style.display = "flex";
}

function closeAddPopup() {
    addPop.style.display = "none";
}

function acceptAdd() {
    closeAddPopup();
    let name = document.getElementById("product-name").value;
    let imgPath = document.getElementById("product-img-path").value;
    let price = document.getElementById("product-price").value;

    if (shopTable.lastElementChild === null || shopTable.lastElementChild.childElementCount === 4) {
        shopTable.insertRow();
    }

    shopTable.lastElementChild.appendChild(generateTableItem(name, imgPath, price));
}

addItemBut.addEventListener("click", openAddPopup);
closeAddBut.addEventListener("click", closeAddPopup);
acceptBut.addEventListener("click", acceptAdd);