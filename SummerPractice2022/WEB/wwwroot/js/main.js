var table = document.querySelector(".table");
var elements = document.querySelectorAll(".table>.row");
var Clients = [];

var connection = new signalR.HubConnectionBuilder().withUrl("/MainHub").build();
connection.on("UpdateStatus", function (ip, name, status) {
	var _tempEl = Clients.find(v => v.ip == ip);
	if (_tempEl == undefined) {
		Clients.push({
			ip: ip,
			name: name,
			status: status
		});
	} else {
		_tempEl.status = status;
	}
	ResetTable();
	GenerateTable(Clients);

});
var ResetTable = () => {
	table.innerHTML = `
		<div class="row heading">
			<div class="small">status</div>
			<div class="element">name</div>
			<div class="element">ip</div>
			<div class="element"></div>
		</div>`;
}
var GenerateTable = (clients) => {
	clients.forEach(v => {
		table.innerHTML += `
			<div class="row">
				<div class="small">
					<div class="status" enabled="${v.status}"></div>
				</div>
				<div class="element">${v.name}</div>
				<div class="element">${v.ip}</div>
				<div class="element">
					<button class="button" onclick='SendMessage("${v.ip}")'>Connect</button>
				</div>
			</div>`;
	});
}

var SendMessage = (ipPort) => {
	connection.invoke("SendMessage", ipPort, `Connect to ${ipPort}`);
}

connection.start();