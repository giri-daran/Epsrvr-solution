$(document).ready(function () {
    $(".see-more-links").click(function () {
        $(this).parents("#sticky-isi-module").toggleClass("expanded"), $("body").toggleClass("isi-open")
    }), $("#IndicationSection").height();
    var i = Math.round($("#IndicationSection").position().top),
        o = Math.round($("#footer").position().top),
        t = $(window).height() + $(window).scrollTop();
    t > i + 150 && t < o ? $("#sticky-isi-module").hide() : $("#sticky-isi-module").show(), $(window).scroll(function () {
        $("#IndicationSection").height();
        var i = Math.round($("#IndicationSection").position().top),
            o = Math.round($("#footer").position().top),
            t = $(window).height() + $(window).scrollTop();
        t > i + 150 && t < o ? $("#sticky-isi-module").hide() : $("#sticky-isi-module").show()
    })
});