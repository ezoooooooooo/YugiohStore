document.addEventListener("DOMContentLoaded", function () {
    const cards = document.querySelectorAll(".card");

    cards.forEach(card => {
        card.addEventListener("mouseenter", () => {
            card.style.transform = "scale(1.1)";
            card.style.boxShadow = "0 0 15px rgba(255, 215, 0, 0.8)";
            card.style.transition = "transform 0.3s ease-in-out, box-shadow 0.3s ease-in-out";
        });

        card.addEventListener("mouseleave", () => {
            card.style.transform = "scale(1)";
            card.style.boxShadow = "none";
        });
    });
});
document.addEventListener("DOMContentLoaded", function () {
    const registerForm = document.querySelector("form[asp-action='Register']");
    const loginForm = document.querySelector("form[asp-action='Login']");

    if (registerForm) {
        registerForm.addEventListener("submit", function (e) {
            let isValid = true;
            const username = document.querySelector("input[name='Username']").value.trim();
            const password = document.querySelector("input[name='Password']").value.trim();

            // Username Validation
            if (username.length < 3 || username.length > 20 || !/^[a-zA-Z0-9]+$/.test(username)) {
                alert("Username must be between 3 and 20 characters and contain only alphanumeric characters.");
                isValid = false;
            }

            // Password Validation
            if (password.length < 6 || !/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$/.test(password)) {
                alert("Password must be at least 6 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.");
                isValid = false;
            }

            if (!isValid) {
                e.preventDefault(); // Prevent form submission if validation fails
            }
        });
    }

    if (loginForm) {
        loginForm.addEventListener("submit", function (e) {
            const username = document.querySelector("input[name='username']").value.trim();
            const password = document.querySelector("input[name='password']").value.trim();

            if (!username || !password) {
                alert("Username and Password cannot be empty.");
                e.preventDefault(); // Prevent form submission
            }
        });
    }
});

