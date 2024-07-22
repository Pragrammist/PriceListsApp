const pricelisthub = new signalR.HubConnectionBuilder()
    .withUrl("/pricelist")
    .build();

pricelisthub.start();

document.addEventListener("DOMContentLoaded", function () {
    pricelisthub.on("MessageResult", function (messageObj) {
        
    });
});