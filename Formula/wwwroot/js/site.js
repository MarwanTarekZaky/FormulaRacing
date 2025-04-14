// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $(".slider-for").slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: true,
        asNavFor: ".slider-nav"
    });

    $(".slider-nav").slick({
        slidesToShow: 5,
        slidesToScroll: 1,
        asNavFor: ".slider-for",
        dots: false,
        arrows: false,
        focusOnSelect: true,
        vertical: true,
        responsive: [
            {
                breakpoint: 768,
                settings: {
                    arrows: true,
                    slidesToShow: 5
                }
            }
        ]
    });
});
