export async function GetAccountById(account_id) {
    try {
        const response = await fetch(`http://localhost:5075/api/GetAccountById/${account_id}`, {
            "headers": { "Content-Type": "application/json" },
        });
        return await response.json();
        
    } catch (error) {
        console.error("Failed calling account api: ", error);
        throw error.message;
    }
}