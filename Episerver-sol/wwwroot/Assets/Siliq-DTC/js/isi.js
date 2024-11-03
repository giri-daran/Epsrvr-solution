$(document).ready(function () {
    $(".see-more-links").click(function () {
        $(this).parents("#sticky-isi-module").toggleClass("expanded");
        $("body").toggleClass("isi-open");
    });
    /* ISI show / hide on page load */
    var h = $("#IndicationSection").height();
    var p = Math.round($("#IndicationSection").position().top);
    var f = Math.round($("#footer").position().top);
    var scrollPosition = $(window).height() + $(window).scrollTop();
    if (scrollPosition > p + 150 && scrollPosition < f) {
        $('#sticky-isi-module').hide();
    }
    else {
        $('#sticky-isi-module').show();
    }
    /* ISI show / hide on page scroll */
    $(window).scroll(function () {
        var h = $("#IndicationSection").height();
        var p = Math.round($("#IndicationSection").position().top);
        var f = Math.round($("#footer").position().top);
        var scrollPosition = $(window).height() + $(window).scrollTop();
        if (scrollPosition > p + 150 && scrollPosition < f) {
            $('#sticky-isi-module').hide();
        }
        else {
            $('#sticky-isi-module').show();
        }
    });
});