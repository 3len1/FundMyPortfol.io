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

$('#submit-data').on('click', function (event) {
 
    event.preventDefault();
    let formData = $('#form-data').serialize();
    $(this).val('Please wait...')
        .attr('disabled', 'disabled');

    var action = $('#form-data').attr("action");

    $.ajax({
        url: action,
        type: 'post',
        data: formData
    }).done(function (data) {
        $('#form-data').hide();
        $("#success-created").append('Project Created Successfully!');
        $("#success-created").removeClass('hidden');
        debugger;
        window.location.href = data.redirectUrl;
    }).fail(function () {
        $('#form-data').hide();
        $("#success-created").append('Project Created failure!');
        $("#success-created").removeClass('hidden');

    });

});