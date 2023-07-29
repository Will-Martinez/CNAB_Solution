import { UploadFile } from "../../APICalls/APICalls.js";
document.addEventListener("DOMContentLoaded", function () {
    
    const saveFile = document.getElementById("saveFile");
    const fileInput = document.getElementById("fileInput");
    const fileNameUploaded = document.getElementById("fileNameUploaded");
    const cancellFileUpload = document.getElementById("cancellFileUpload");

    fileInput.addEventListener("change", function () {
        const selectedFile = fileInput.files[0];
        const fileName = selectedFile.name;
        fileNameUploaded.innerText = fileName;
    });


    saveFile.addEventListener("click", async function () {
        if (fileInput.value == "" || fileInput == null) {
            alert("Nenhum arquivo selecionado.");
            return;
        }
        const file = fileInput.files[0];
        const formData = new FormData();
        formData.append("arquivo", file);
        const sendFile = await UploadFile(formData);
    });

    cancellFileUpload.addEventListener("click", function () {
        fileInput.value = "";
        fileNameUploaded.innerText = "Nenhum selecionado...";
    });
});