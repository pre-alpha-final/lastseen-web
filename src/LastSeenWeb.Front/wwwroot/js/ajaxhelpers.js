async function ajaxGet(url, action) {
	return new Promise((resolve, reject) => {
		$.get(url)
			.done((data, textStatus, jqXhr) => {
				action(data);
				resolve(data);
			})
			.fail((jqXhr, textStatus, errorThrown) => {
				reject();
			});
	});
}

async function ajaxGetAndReplace(url, elementToReplace) {
	return new Promise((resolve, reject) => {
		$.get(url)
			.done((data, textStatus, jqXhr) => {
				$(elementToReplace).html(data);
				resolve(data);
			})
			.fail((jqXhr, textStatus, errorThrown) => {
				reject();
			});
	});
}

async function ajaxPost(url, postData, action) {
	return new Promise((resolve, reject) => {
		$.post(url, postData)
			.done((data, textStatus, jqXhr) => {
				action(data);
				resolve(data);
			})
			.fail((jqXhr, textStatus, errorThrown) => {
				reject();
			});
	});
}

async function taskDelay(miliseconds) {
	return new Promise((resolve, reject) => {
		setTimeout(() => resolve(), miliseconds);
	});
}
