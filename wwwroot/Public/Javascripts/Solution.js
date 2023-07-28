import { GetAccountById, UploadFile } from "../../APICalls/APICalls.js";

document.addEventListener("DOMContentLoaded", function () {
    
    const saveFile = document.getElementById("saveFile");
    const fileInput = document.getElementById("fileInput");

    saveFile.addEventListener("click", async function () {
        const file = fileInput.files[0]; // Obter o arquivo selecionado
        const formData = new FormData();
        formData.append("arquivo", file); // Adicionar o arquivo ao FormData

        // Faça o que precisar com o formData, por exemplo, enviar para a API usando fetch ou axios
        console.log("formData result: ", formData);

        const sendFile = await UploadFile(formData);
        console.log("sendFile result.");
        for (const content in sendFile) {
            console.log(content);
        }
    });
});