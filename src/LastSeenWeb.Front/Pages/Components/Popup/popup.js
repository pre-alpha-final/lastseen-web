﻿var modal = document.getElementById('popup');

var btn = document.getElementById("popupButton");

btn.onclick = function () {
	modal.style.display = "block";
}

window.onclick = function (event) {
	if (event.target == modal) {
		modal.style.display = "none";
	}
}
