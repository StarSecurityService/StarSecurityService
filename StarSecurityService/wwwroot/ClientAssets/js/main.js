const bars = document.querySelector("#barsBtn");
const closeBtn = document.querySelector("#closeBtn");
const overlay = document.querySelector(".overlay");
const navBars = document.querySelector(".navBar-details");
const contact = document.querySelector("#contact");
const formDisplay = document.querySelector("#formDisplay");
const contactUs = document.querySelector(".contact");
const formOrder = document.querySelector("#formOrder");
const order = document.querySelector("#createBtn");
const overlay1 = document.querySelector(".overlay1");

function handleNav() {
  overlay.classList.toggle("active");
  navBars.classList.toggle("active");
}

function handleContact() {
  overlay.classList.toggle("active");
  formDisplay.classList.toggle("display");
}

function handleOrder() {
  overlay1.classList.toggle("active");
  formOrder.classList.toggle("visible");
}

bars.onclick = function () {
  handleNav();
}

overlay.onclick = function () {
  handleNav();
}

closeBtn.onclick = function () {
  handleNav();
}

contact.onclick = function () {
  handleContact();
}

overlay.onclick = function () {
  handleContact();
}

contactUs.onclick = function () {
  handleContact();
}

order.onclick = function () {
  handleOrder();
}

overlay1.addEventListener('click', handleOrder)
