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
