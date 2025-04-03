"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/hub").build();
connection.on("ReloadData", () => {
    //Reload all client side
    location.reload();
})

connection.start().then().catch(err => console.error(err));


// "use strict";
//
// const connection = new signalR.HubConnectionBuilder()
//     .withUrl("/hub")
//     .configureLogging(signalR.LogLevel.Information)
//     .build();
//
// connection.on("ProductCreated", (product) => {
//     console.log("New product created:", product);
//     // Add code here to update UI dynamically instead of reloading
//     // For example:
//     // const productList = document.getElementById("productList");
//     // productList.innerHTML += `<li>${product.productName}</li>`;
// });
//
// connection.on("ReloadData", () => {
//     console.log("Received reload signal");
//     location.reload();
// });
//
// async function start() {
//     try {
//         await connection.start();
//         console.log("SignalR Connected");
//     } catch (err) {
//         console.error("Connection failed: ", err);
//         setTimeout(start, 5000); // Retry after 5s
//     }
// }
//
// // Handle reconnection
// connection.onclose(async () => {
//     await start();
// });
//
// start();