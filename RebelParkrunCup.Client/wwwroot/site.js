window.showPasswordDialog = async function () {
    return new Promise((resolve) => {
        let dialog = document.createElement("dialog");
        dialog.style.border = "1px solid #ccc"; // Thin border
        dialog.style.padding = "15px"; // Reduce padding
        dialog.style.borderRadius = "8px"; // Slightly rounded corners
        dialog.style.boxShadow = "0px 4px 6px rgba(0,0,0,0.1)"; // Soft shadow
        dialog.innerHTML = `
            <form method="dialog">
                <label for="passwordInput">Enter Password:</label>
                <input type="password" id="passwordInput" style="width: 100%; padding: 6px; margin-top: 5px;" autofocus />
                <div style="margin-top: 10px; text-align: right;">
                    <button type="submit" id="submitPassword">Submit</button>
                    <button type="button" id="cancelPassword">Cancel</button>
                </div>
            </form>
        `;
        document.body.appendChild(dialog);
        dialog.showModal();
        document.getElementById("submitPassword").onclick = function () {
            let password = document.getElementById("passwordInput").value;
            dialog.close();
            document.body.removeChild(dialog);
            resolve(password);
        };
        document.getElementById("cancelPassword").onclick = function () {
            dialog.close();
            document.body.removeChild(dialog);
            resolve(null);
        };
    });
};