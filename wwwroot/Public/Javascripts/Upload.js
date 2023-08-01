// Importa a função de updload file para enviar o arquivo de padrão cnab
import { UploadFile } from "../../APICalls/APICalls.js"
document.addEventListener("DOMContentLoaded", function () {

    // Definição de alguns elementos html para uso do DOM
    const saveFile = document.getElementById("saveFile");
    const fileInput = document.getElementById("fileInput");
    const fileNameUploaded = document.getElementById("fileNameUploaded");
    const cancellFileUpload = document.getElementById("cancellFileUpload");

    // Evento responsável por mostrar o nome do arquivo selecionado
    fileInput.addEventListener("change", function () {
        const selectedFile = fileInput.files[0];
        const fileName = selectedFile.name;
        fileNameUploaded.innerText = fileName;
    });

    // Evento responsável por chamar a api de envio de arquivo cnab
    saveFile.addEventListener("click", async function () {
        try {
            if (fileInput.value == "" || fileInput == null) {
                alert("Nenhum arquivo selecionado.");
                return;
            }
            // coleta os dados do arquivo antes de enviar por requisição na API
            const file = fileInput.files[0];
            const formData = new FormData();
            formData.append("arquivo", file);
            const response = await UploadFile(formData);
            if (response.status == 200 && response.statusText == "OK") {
                // Caso queira verificar o resultado...
                //const responseJson = await response.json();
                //console.log("responseJson: ", responseJson);
                alert("Arquivo enviado e dados salvos com sucesso!");
                fileInput.value = "";
                fileNameUploaded.innerText = "Nenhum selecionado...";
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

    // evento para limpar e cancelar o arquivo selecionado pelo usuário
    cancellFileUpload.addEventListener("click", function () {
        fileInput.value = "";
        fileNameUploaded.innerText = "Nenhum selecionado...";
    });
});