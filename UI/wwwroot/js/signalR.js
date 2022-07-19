var connection = new signalR.HubconnectionBulider().withUrl("/reailtime").build();
connection.on("AddLink", function () {
    location.reload();
});
connection.start();
   

