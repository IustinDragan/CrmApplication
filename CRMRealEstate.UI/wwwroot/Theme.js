window.setDarkMode = function (enabled) {
    localStorage.setItem("darkMode", enabled);
};

window.getDarkMode = function () {
    return localStorage.getItem("darkMode") === "true";
};

window.setBodyClass = function (enabled) {
    document.body.classList.toggle("dark-mode", enabled);
};