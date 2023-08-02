// importa as 3 funções responsáveis por chamar as API's
import { GetTransactions, DeleteTransaction, CreateTransaction } from "../../APICalls/APICalls.js";
$(document).ready(async function () {

    // Definindo globalmente os inputs do form;
    const transaction_type_input = $("#transaction_type_input");
    const transaction_date_input = $("#transaction_date_input");
    const amount_input = $("#amount_input");
    const cpf_input = $("#cpf_input");
    const card_number_input = $("#card_number_input");
    const store_owner_input = $("#store_owner_input");
    const store_name_input = $("#store_name_input");

    // Função para coletar todas as transações na base de dados
    async function GetTransactionsData() {
        try {
            const transactionsData = await GetTransactions();
            const resultJson = await transactionsData.json();
            return resultJson;
        } catch (error) {
            console.error("Failed trying to get transactions: ", error);
            throw error.message;
        }
    }

    // Função para desenhar os dados na tabela
    async function DrawTableData(transactions, table) {
        for (const data of transactions) {
            table.row.add({
                "transaction_type": FormatTransactionType(data.type),
                "transaction_date": moment(data.transaction_date).format("DD/MM/YYYY"),
                "amount": FormatBrlCurrency(data.amount),
                "cpf": data.cpf,
                "card_number": data.card_number,
                "store_owner": data.store_owner,
                "store_name": data.store_name,
                "_id": data.id,
            });
        }

        table.draw();
    }

    // Função para formatar o valor da transação para a moeda brasileira
    function FormatBrlCurrency(amount) {
        return Number(amount).toLocaleString("pt-BR", { style: "currency", currency: "BRL" });
    }

    // Função para formatar os tipos de transação e exibir ao cliente de forma legível
    function FormatTransactionType(transactionType) {
        switch (transactionType) {
            case "1":
                return "Débito";
            case "2":
                return "Crédito";
            case "3":
                return "Pix";
            case "4":
                return "Financiamento";
            default:
                return "não identificado";
        }
    }

    // Função para abrir o modal com os dados da linha no qual o botão de detalhes foi clicado
    function InputModalData(modalData) {
        transaction_type_input.val(modalData.transaction_type);
        transaction_date_input.val(modalData.transaction_date);
        amount_input.val(modalData.amount);
        cpf_input.val(modalData.cpf);
        card_number_input.val(modalData.card_number);
        store_owner_input.val(modalData.store_owner);
        store_name_input.val(modalData.store_name);
    }

    // Função para limpar os campos do modal
    function ClearModal() {
        transaction_type_input.val("");
        transaction_date_input.val("");
        amount_input.val("");
        cpf_input.val("");
        card_number_input.val("");
        store_owner_input.val("");
        store_name_input.val("");
    }

    // remove os o bloqueio de somente leitura para leitura e escrita nos campos de input do formulário
    function EnableFormEdit() {
        transaction_type_input.removeAttr("readonly");
        transaction_date_input.removeAttr("readonly");
        amount_input.removeAttr("readonly");
        cpf_input.removeAttr("readonly");
        card_number_input.removeAttr("readonly");
        store_owner_input.removeAttr("readonly");
        store_name_input.removeAttr("readonly");
    }

    function BlockFormEdit() {
        transaction_type_input.attr("readonly", true);
        transaction_date_input.attr("readonly", true);
        amount_input.attr("readonly", true);
        cpf_input.attr("readonly", true);
        card_number_input.attr("readonly", true);
        store_owner_input.attr("readonly", true);
        store_name_input.attr("readonly", true);
    }

    // Função para validar se algum campo do modal está em branco no momento de salvar os mesmos
    function ValidateFormInput() {
        if (
            transaction_type_input.val() == "" ||
            transaction_date_input.val() == "" ||
            amount_input.val() == "" ||
            cpf_input.val() == "" ||
            card_number_input.val() == "" ||
            store_owner_input.val() == "" ||
            store_name_input.val() == ""
        ) {
            alert("Nenhuma informação pode ser cadastrada em branco");
            return true;
        } else {
            return false;
        }
    }

    // Função principal que carrega a tabela na view de transactions
    async function LoadTable() {
        const detailsModal = $("#detailsModal");
        const transactionsRowData = $("#transactionsTable tbody");
        const closeModal = $("#closeModal");
        const deleteTransaction = $("#deleteTransaction");
        const registerTransaction = $("#registerTransaction");
        const saveTransaction = $("#saveTransaction");

        // carregando a tabela e seus parametros
        try {
            const transactionTable = $("#transactionsTable").DataTable({
                language: { url: 'https://cdn.datatables.net/plug-ins/1.10.25/i18n/Portuguese-Brasil.json' },
                columns: [
                    {
                        data: null,
                        title: "Detalhes",
                        render: function (data, type, row) {
                            return type == "display" ?
                                `<button class="button is-success is-outlined is-rounded is-small" onclick="${JSON.stringify(row)})>Detalhes</button>` : data
                        }
                    },
                    { data: "transaction_type", title: "Tipo da transação" },
                    { data: "transaction_date", title: "Data da ocorrência" },
                    { data: "amount", title: "Valor" },
                    { data: "cpf", title: "CPF" },
                    { data: "card_number", title: "Cartão" },
                    { data: "store_owner", title: "Dono da loja" },
                    { data: "store_name", title: "Nome da loja" },
                ],
                responsive: true,
            });

            // chamando a função de desenhas os dados no corpo da tabela
            const transactions = await GetTransactionsData();
            await DrawTableData(transactions, transactionTable);

            // evento de click no botão de detalhes para mostrar o modal com os dados dalinha onde foi clicado
            transactionsRowData.on("click", "button", async function () {
                BlockFormEdit();
                const rowData = await transactionTable.row($(this).closest("tr")).data();
                deleteTransaction.css("display", "block");
                saveTransaction.css("display", "none");
                detailsModal.show();
                InputModalData(rowData);

                // evento de click para deletar a transação caso assim seja necessário
                deleteTransaction.on("click", async function () {
                    const confirmTransactionRemove = confirm("Tem certeza que deseja remover essa transação ?");
                    if (confirmTransactionRemove == true) {
                        const removeTransaction = await DeleteTransaction(rowData._id);
                        if (removeTransaction.status == 200) {
                            window.location.reload();
                            detailsModal.hide();
                        } else {
                            alert("Falha ao tentar deletar a transação da base de dados.");
                            window.location.reload();
                        }
                        window.location.reload();
                    } else {
                        window.location.reload();
                    }
                });

            });

            // evento de click para alterar alguns atributos de botão como mostrar e esconder o botão de salvar
            registerTransaction.on("click", function () {
                EnableFormEdit();
                deleteTransaction.css("display", "none");
                saveTransaction.css("display", "block");
                detailsModal.show();
            });

            // evento de click para criar uma nova transação sem o uso de um arquivo de padrão cnab
            saveTransaction.on("click", async function () {

                const newTransaction = new Object();
               
                newTransaction.type = transaction_type_input.val();
                newTransaction.transaction_date = transaction_date_input.val();
                newTransaction.amount = amount_input.val();
                newTransaction.cpf = cpf_input.val();
                newTransaction.card_number = card_number_input.val();
                newTransaction.store_owner = store_owner_input.val();
                newTransaction.store_name = store_name_input.val();

                // validando os campos do modal antes de enviar uma requisição de post via API
                const formHasEmptyData = ValidateFormInput();
                if (formHasEmptyData == true) {
                    return
                } else {
                    const response = await CreateTransaction(newTransaction);
                    if (response.status == 200) {
                        detailsModal.hide();
                        alert("Transação cadastrada com sucesso.");
                    }
                }
                window.location.reload();
            });

            // limpa o modal ao ser fechado para que os dados não sobreescrevam os campos
            closeModal.on("click", function () {
                ClearModal();
                detailsModal.hide();
            });
        } catch (error) {
            console.error("Failed trying to load transaction table: ", error);
            throw error.message;
        }
    }

    // Chama a funcção principal da tabela
    LoadTable();

});