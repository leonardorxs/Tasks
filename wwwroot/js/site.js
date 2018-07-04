// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//MENU DROPDOWN
$('li.nav-item.dropdown').hover(function () {
    $(this).find('.dropdown-menu').stop(true, true).delay(110).fadeIn(110);
}, function () {
    $(this).find('.dropdown-menu').stop(true, true).delay(110).fadeOut(110);
});