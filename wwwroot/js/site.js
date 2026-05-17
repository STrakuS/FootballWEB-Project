// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function toggleMenu() {
    const navbar = document.querySelector('.navbar');
    // 'active' veya 'show' sınıfını ekleyip çıkarır
    navbar.classList.toggle('active');
}