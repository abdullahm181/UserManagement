import KanbanAPI from "./Kanban/api/KanbanApi.js";
$("#LoginUser").on("submit", () => {
	var data = {};
	$('#LoginUser').serializeArray().map(function (x) { data[x.name] = x.value; });
	KanbanAPI.Methode("POST", "Auth/login", data, function (d) {
		console.log(d);
		if (d.result == 200) {
			window.location = "/Home/index";
		} else {
			Swal.fire({
				icon: 'error',
				title: d.result,
				text: d.message,
			})
		}
	});

	event.preventDefault();

});

$("#RegisterUser").on("submit", () => {
	var data = {};
	$('#RegisterUser').serializeArray().map(function (x) { data[x.name] = x.value; });
	KanbanAPI.Methode("POST", "Auth/register", data, function (d) {
		console.log(d);
		if (d.result == 200) {
			document.getElementById("RegisterUser").reset();
			Swal.fire(
				'Registered!',
				d.message,
				'success'
			);
		} else {
			Swal.fire({
				icon: 'error',
				title: d.result,
				text: d.message,
			});
		}
	});

	event.preventDefault();

});