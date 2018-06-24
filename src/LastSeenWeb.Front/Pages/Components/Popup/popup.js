var _popup = {};

(popup => {
	var modal = document.getElementById('popup');
	window.onclick = function (event) {
		if (event.target == modal) {
			popup.close();
		}
	}

	popup.open = async id => {
		await ajaxGetAndReplace('/Popup/Content/' + id, '#popupContent')
		modal.style.display = "flex";
	}

	popup.close = () => {
		modal.style.display = "none";
	}
})(_popup);
