const local = "[APICall]";

export async function UploadFile(file) {
        try {
            return await fetch(`http://localhost:5075/api/sendFile`, {
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
            return await fetch("http://localhost:5075/api/getTransactions", {
                headers: { "Content-Type": "application.json" }
            });
        } catch (error) {
            console.error(`${local} - Failed trying to get transactions: `, error);
            throw error.message;
        }
}