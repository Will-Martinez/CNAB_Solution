const local = "[APICall]";
export async function UploadFile(file) {
    try {
        const result = await fetch(`http://localhost:5075/api/sendFile`, {
            method: "POST",
            body: file,
        });
        return result.json();
    } catch (error) {
        console.error(`${local} - Failed trying to send file: `, error);
        throw error.Message;
    }
}