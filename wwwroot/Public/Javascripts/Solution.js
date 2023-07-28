import { GetAccountById } from "../../APICalls/APICalls.js";

document.addEventListener("DOMContentLoaded", function () {
    

    const fileButoon = document.getElementById("saveFile");
    fileButoon.addEventListener("click", async function () {
        const accountData = await GetAccountById("ABC");
        console.log("accountData result: ", accountData);
    });
});