var table = document.querySelector(".table");
var elements = document.querySelectorAll(".table>.row");
var Clients = [];

var connection = new signalR.HubConnectionBuilder().withUrl("/MainHub").build();
connection.on("UpdateStatus", UpdateStatus);

function UpdateStatus(ip, name, status) {
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
    if (status === "false" && ip == ConnectedIP) {
        ConnectedIP = "";
    }
    ResetTable();
    GenerateTable(Clients);
}

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
					<button class="button" onclick="Connect('${v.ip}')">Connect</button>
				</div>` +
                (v.ip == ConnectedIP ?
                `<div class="element">
					<input type="range" min="0" max="100" step="1" oninput="ChangeAxis(this.value)" />
					<input type="range" min="0" max="100" step="1" oninput="ChangeMovDeg(this.value)" />
				</div>` : `` )
            + `</div>`;
    });
}

var ConnectedIP = "";
var Connect = (e) => {
    Clients.forEach(v => UpdateStatus(v.ip, "", "false"));
    ConnectedIP = e;
    UpdateStatus(e, "", "true");
}
var CameraMoveControl = {
    Axis: 0,
    MovDeg: 0
}
var ChangeAxis = (e) => {
    CameraMoveControl.Axis = e;
    SendData(ConnectedIP);
}
var ChangeMovDeg = (e) => {
    CameraMoveControl.MovDeg = e;
    SendData(ConnectedIP);
}

var SendData = (ipPort) => {
    var json = {
        IpPort: ipPort,
        Data: JSON.stringify(CameraMoveControl)
    }
    connection.invoke("SendData", JSON.stringify(json));
}



connection.start();