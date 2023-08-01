const local = "[APICalls]";

export async function UploadFile(file) {
        try {
            return await fetch("/api/sendFile", {
                method: "POST",
                body: file,
            });
        } catch (error) {
            console.error(`${local} - Failed trying to send file: `, error);
            throw error.message;
        }
    }

export async function GetTransactions() {
        try {
            return await fetch("/api/getTransactions", {
                headers: { "Content-Type": "application.json" }
            });
        } catch (error) {
            console.error(`${local} - Failed trying to get transactions: `, error);
            throw error.message;
        }
}

export async function DeleteTransaction(transactionID) {
    try {
        return await fetch(`/api/deleteTransaction/${transactionID}`, {
            headers: { "Content-Type": "application.json" },
            method: "DELETE",
        });
    } catch (error) {
        console.error(`${local} - Failed trying to delete transaction: `, error);
    }
}

export async function CreateTransaction(transaction) {
    try {
        return await fetch("/api/createTransaction", {
            headers: { "Content-Type": "application/json" },
            method: "POST",
            body: JSON.stringify(transaction),
        })
    } catch (error) {
        console.error(`${local} - Failed trying to create a new transaction: `, error);
        throw error.message;
    }
}