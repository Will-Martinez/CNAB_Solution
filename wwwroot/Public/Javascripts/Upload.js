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
        try {
            if (fileInput.value == "" || fileInput == null) {
                alert("Nenhum arquivo selecionado.");
                return;
            }
            const file = fileInput.files[0];
            const formData = new FormData();
            formData.append("arquivo", file);
            const response = await UploadFile(formData);
            if (response.status == 200 && response.statusText == "OK") {
                // Caso queira verificar o resultado...
                //const responseJson = await response.json();
                //console.log("responseJson: ", responseJson);
                alert("Arquivo enviado e dados salvos com sucesso!");
                return;
            } else {
                alert(`Falha no upload do arquivo: ${response.statusText}`);
                return;
            }
        } catch (error) {
            console.error("Failed trying to send cnab file");
            throw error.message;
        }
    });

    cancellFileUpload.addEventListener("click", function () {
        fileInput.value = "";
        fileNameUploaded.innerText = "Nenhum selecionado...";
    });
});