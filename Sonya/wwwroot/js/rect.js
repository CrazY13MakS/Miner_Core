const connectionRect = new signalR.HubConnectionBuilder()
    .withUrl("/moveShapeHub")
    .build();

connectionRect.on("UpdateModel", (user, message) => {
    console.log(message);
    const msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    const encodedMsg = user + " says " + msg;
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});
$shape = $("#shape");
// Send a maximum of 10 messages per second 
// (mouse movements trigger a lot of messages)
messageFrequency = 10;
// Determine how often to send messages in
// time to abide by the messageFrequency
updateRate = 1000 / messageFrequency;
shapeModel = {
    left: 0,
    top: 0
},
connectionRect.start().catch(err => console.error(err.toString()));
$shape.draggable({
    drag: function () {
        shapeModel = $shape.offset();
        moved = true;
    }
});
setInterval(updateServerModel, updateRate);
function updateServerModel() {
    // Only update server if we have a new movement
    if (moved) {
        moveShapeHub.server.updateModel(shapeModel);
        moved = false;
    }
}
