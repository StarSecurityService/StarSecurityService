const bars = document.querySelector("#barsBtn");
const closeBtn = document.querySelector("#closeBtn");
const overlay = document.querySelector(".overlay");
const navBars = document.querySelector(".navBar-details");

function handleNav() {
  overlay.classList.toggle("active");
  navBars.classList.toggle("active");
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