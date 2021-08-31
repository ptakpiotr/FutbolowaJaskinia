"use strict";

var conn = new signalR.HubConnectionBuilder().withUrl("/chat").build();

conn.start().catch(err => console.error(err));

function AddMessage(userName) {
    var obj = {
        from: userName,
        message: document.getElementById("chat-box").value
    };

    conn.invoke("AddMessage", JSON.stringify(obj));
}

conn.on("NewMessage", dt => {
    var obj = JSON.parse(dt);
    var elem = document.getElementsByClassName("chat-messages")[0];
    elem.innerHTML +=`<div class="bg-dark text-info m-3 p-1">
                    <b>${obj.From}</b> -> ${obj.Message}
                </div>`
});