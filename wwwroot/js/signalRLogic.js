"use strict";

var conn = new signalR.HubConnectionBuilder().withUrl("/main").build();

conn.start(() => {
}).catch(err => console.error(err));

function LikePost(id) {
    conn.invoke('LikePost', id);
}

function AddComment(postId,email) {
    var msg = {
        postId: postId,
        from: email,
        comment: document.getElementsByClassName("comm-box")[0].value
    }
    conn.invoke('AddComment', JSON.stringify(msg));
}

conn.on('LikePresent', dt => {
    alert("Polubienie juz obecne!")
});

conn.on('NewComment', dt => {
    var elem = document.getElementsByClassName("comm-div")[0];
    alert("NJU");
    var obj = JSON.parse(dt);
    elem.innerHTML +=`<div class="card text-dark mt-3 m-2">
                    <div class="card-header">
                        ${obj.From}
                    </div>
                    <div class="card-body">
                        ${obj.Comment}
                    </div>
                </div>`;
});