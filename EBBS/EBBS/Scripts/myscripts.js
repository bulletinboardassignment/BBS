$(document).ready(function () { //
    $(".longString").each(function () {
        var maxwidth = 50;
        if ($(this).text().length > maxwidth) {
            $(this).text($(this).text().substring(0, maxwidth));
            $(this).html($(this).html() + '...');
        }
    });
});

$(document).ready(function () {
    function Contains(text_one, text_two) {
        if (text_one.indexOf(text_two) != -1)
            return true;

    }

    $("#search").keyup(function () {
        var searchText = $("#search").val().toLocaleLowerCase();
        $(".searchRecord").each(function () {
            if (!Contains($(this).text().toLowerCase(), searchText)) {
                $(this).hide();
            } else {
                $(this).show();
            }
        });

    });

});

$(document).ready(function () {
    $('input[data-val-length-max]').each(
        function (index) {

            $(this).attr('maxlength', $(this).attr('data-val-length-max'));
        });
});


