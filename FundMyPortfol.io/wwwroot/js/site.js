// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("#ddProjectCategory").change(function () {
    var category = $(this).val();
    
    if (category === null) {
        url = "https://localhost:44367/Projects";
    }
    else {
        url = "https://localhost:44367/Projects?category=" + $(this).val(); 
    }
    $(location).attr("href", url);
}); 