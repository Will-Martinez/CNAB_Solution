import { GetTransactions } from "../../APICalls/APICalls.js";
$(document).ready(async function () {

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

    async function LoadTable() {
        try {
            const transactionTable = $("#transactionsTable").DataTable({
                language: { url: 'https://cdn.datatables.net/plug-ins/1.10.25/i18n/Portuguese-Brasil.json' },
                columns: [
                    {
                        data: null,
                        title: "Ações",
                        render: function (data, type, row) {
                            return type == "display" ?
                                '<button class="button is-success is-outlined is-rounded is-small">Detalhes</button>': data
                        }
                    },
                    { data: "transaction_type", title: "Tipo da transação" },
                    { data: "transaction_date", title: "Data da ocorrência" },
                    { data: "amount", title: "Valor" },
                    { data: "cpf", title: "CPF" },
                    { data: "card_number", title: "Cartão" },
                    { data: "store_owner", title: "Dono da loja" },
                    { data: "store_name", title: "Nome da loja" }
                ],
                responsive: true,
            });

            const transactions = await GetTransactionsData();
            await DrawTableData(transactions, transactionTable);
        } catch (error) {
            console.error("Failed trying to load transaction table: ", error);
            throw error.message;
        }
    }

    LoadTable();

});