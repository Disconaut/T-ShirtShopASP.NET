// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function switchTheme(theme) {
    var variableContainer = document.querySelector(":root");
    if (theme === "white") {
        variableContainer.style.setProperty("--first-color", "#ffffff");
        variableContainer.style.setProperty("--secondary-color", "#ff6200");
        variableContainer.style.setProperty("--text-default-color", "#000000");
        variableContainer.style.setProperty("--border-color", "#e5e5e5");
        variableContainer.style.setProperty("--popup-back-color", "rgba(85, 85, 85, 0.75)");
        variableContainer.style.setProperty("--popup-close-button", "#a1a1a1");
        variableContainer.style.setProperty("--form-foreground", "#575757");
        variableContainer.style.setProperty("--chart-label-color", "#777");
        variableContainer.style.setProperty("--chart-axis-color", "#000000");
    } else if (theme === "dark") {
        variableContainer.style.setProperty("--first-color", "rgb(24, 26, 27)");
        variableContainer.style.setProperty("--secondary-color", "rgb(204, 78, 0)");
        variableContainer.style.setProperty("--text-default-color", "#ffffff");
        variableContainer.style.setProperty("--border-color", "rgb(56, 56, 56)");
        variableContainer.style.setProperty("--popup-back-color", "rgba(61, 64, 67, 0.75)");
        variableContainer.style.setProperty("--popup-close-button", "rgb(186, 181, 171)");
        variableContainer.style.setProperty("--form-foreground", "#575757");
        variableContainer.style.setProperty("--chart-label-color", "rgb(189, 184, 175)");
        variableContainer.style.setProperty("--chart-axis-color", "rgb(102, 102, 102)");
    }
    logo.setTextColor(variableContainer.style.getPropertyValue("--first-color"));
    logo.draw(logoAnimationDuration);
}

var themeSwitcher = document.getElementById("theme-changer");
var logoAnimationDuration = 3000; //ms
var logo = new AnimatedLogo(document.getElementById("logo"), "VETOL", bounceOut);

themeSwitcher.addEventListener("click", function() {
    if (themeSwitcher.checked) switchTheme("dark");
    else switchTheme("white");
});

logo.draw(logoAnimationDuration);

