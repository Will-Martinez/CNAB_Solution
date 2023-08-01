﻿import { GetTransactions, DeleteTransaction, CreateTransaction } from "../../APICalls/APICalls.js";
$(document).ready(async function () {

    const transaction_type_input = $("#transaction_type_input");
    const transaction_date_input = $("#transaction_date_input");
    const amount_input = $("#amount_input");
    const cpf_input = $("#cpf_input");
    const card_number_input = $("#card_number_input");
    const store_owner_input = $("#store_owner_input");
    const store_name_input = $("#store_name_input");

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

    function FormatBrlCurrency(amount) {
        return Number(amount).toLocaleString("pt-BR", { style: "currency", currency: "BRL" });
    }

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

    function InputModalData(modalData) {
        transaction_type_input.val(modalData.transaction_type);
        transaction_date_input.val(modalData.transaction_date);
        amount_input.val(modalData.amount);
        cpf_input.val(modalData.cpf);
        card_number_input.val(modalData.card_number);
        store_owner_input.val(modalData.store_owner);
        store_name_input.val(modalData.store_name);
    }

    function ClearModal() {
        transaction_type_input.val("");
        transaction_date_input.val("");
        amount_input.val("");
        cpf_input.val("");
        card_number_input.val("");
        store_owner_input.val("");
        store_name_input.val("");
    }

    async function LoadTable() {
        const detailsModal = $("#detailsModal");
        const transactionsRowData = $("#transactionsTable tbody");
        const closeModal = $("#closeModal");
        const deleteTransaction = $("#deleteTransaction");
        const registerTransaction = $("#registerTransaction");
        const saveTransaction = $("#saveTransaction");

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

            const transactions = await GetTransactionsData();
            await DrawTableData(transactions, transactionTable);
            transactionsRowData.on("click", "button", async function () {
                const rowData = await transactionTable.row($(this).closest("tr")).data();
                detailsModal.show();
                InputModalData(rowData);

                deleteTransaction.on("click", async function () {
                    const confirmTransactionRemove = confirm("Tem certeza que deseja remover essa transação ?");
                    if (confirmTransactionRemove == true) {
                        const removeTransaction = await DeleteTransaction(rowData._id);
                        if (removeTransaction.status == 200) {
                            ClearModal();
                            detailsModal.hide();
                            window.location.reload();
                        } else {
                            alert("Falha ao tentar deletar a transação da base de dados.");
                        }
                    }
                });

            });

            registerTransaction.on("click", function () {
                deleteTransaction.css("display", "none");
                saveTransaction.css("discplay", "block");
                detailsModal.show();
            });

            saveTransaction.on("click", async function () {

                const newTransaction = new Object();
               
                newTransaction.transaction_type = transaction_type_input.val();
                newTransaction.transaction_date = transaction_date_input.val();
                newTransaction.amount = amount_input.val();
                newTransaction.cpf = cpf_input.val();
                newTransaction.card_number = card_number_input.val();
                newTransaction.store_owner = store_owner_input.val();
                newTransaction.store_name = store_name_input.val();

                await CreateTransaction(newTransaction);
            });
            closeModal.on("click", function () {
                ClearModal();
                detailsModal.hide();
            });
        } catch (error) {
            console.error("Failed trying to load transaction table: ", error);
            throw error.message;
        }
    }

    LoadTable();

});