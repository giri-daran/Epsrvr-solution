$(document).ready(function () {
    /* Script for drag and drop in home page */
    baSlider("#ba-slider");
    $("button.navbar-toggler").click(function () {
        $(this).toggleClass("navbar-toggler btn-close");
        $(this).siblings().toggleClass("expanded");
    });
    $('.referencespop-centent .btn-close-desktop').click(function () {
        $('.modal-backdrop.fade.in').css('z-index', '0');
    });
    $('.modal-dialog .btn-grey').click(function () {
        $('.modal-backdrop.fade.in').css('z-index', '0');
    });

    if ($(window).width() < 1024) {
        $("#bannerPane").css("padding-top", '0px');
        $(".home-banner-section #bannerPane").css("padding-top", '0px');

    } else {
        $("#bannerPane").css("padding-top", '0px');
        $(".home-banner-section #bannerPane").css("padding-top", '0px');
        //$(".wrapper").css("position",'absolute');
    }
    /* Script for talking to your doctor page */
    $('.siliq-quiz input[type="radio"]').on("click", function (e) {
        $(this).parents(".row").find("label").removeClass("border-active"),
            $(this).parent().toggleClass("border-active")
    });
    $('.siliq-quiz input[type="checkbox"]').on("click", function (e) {
        $(this).parent().toggleClass("border-active")
    });
    /*$(".navbar .navbar-collapse .navbar-nav .nav-item").slice(-2).hide();*/
    /* Script for active menu */
    // this will get the full URL at the address bar
    var url = window.location.href;

    // passes on every "a" tag
    $(".navbar-nav li.nav-item")[0].classList.add("submenu-1");
    $(".navbar-nav li.nav-item")[1].classList.add("submenu-2");
    $(".navbar-nav li.nav-item")[2].classList.add("submenu-3");
    if (url.includes('success-stories')) {
        $("header .navbar .navbar-nav .submenu-1").addClass("btns-active");
        $("header .navbar .navbar-nav .submenu-1 .dropdown-menu li:nth-child(2)").addClass("btns-active");
    }
    if (url.includes('savings-support')) {
        $("header .navbar .navbar-nav .submenu-2").addClass("btns-active");
    }
    if (url.includes('faqs')) {
        $("header .navbar .navbar-nav .submenu-1").addClass("btns-active");
        $("header .navbar .navbar-nav .submenu-1 .dropdown-menu li:nth-child(3)").addClass("btns-active");
    }
    if (url.includes('find-a-doctor')) {
        $("header .navbar .navbar-nav .submenu-3").addClass("btns-active");
    }
    $(".navbar-nav a").each(function () {
        // checks if its the same on the address bar
        if (url == (this.href)) {
            $(this).closest("li").addClass("btns-active");
            //for dropdown in submenu
            $(this).closest("div").parent("li").addClass("btns-active");
            //for making parent of submenu active
            $(this).closest("li").parent().parent().addClass("btns-active");
        }
    });
    $(".indications-section .external-link").click(function () {
        $("#externalSite").modal('show');
    });
    $("#sticky-isi-module .external-link").click(function () {
        $("#externalSite").modal('show');
    });
    $(".header-top-links .reference").click(function () {
        $("#referencesPop").modal('show');
    });
    $(".header-top-links .leavesite").click(function () {
        $("#leavingSite").modal('show');
    });
    $(".btn-close-desktop").click(function () {
        $("#referencesPop").modal('hide');
    });
    $("#leavingSite .externalLink").click(function () {
        $("#leavingSite").modal('hide');
    });
    $("#leavingSite .external-btn").click(function () {
        $("#leavingSite").modal('hide');
    });
    $("#externalSite .externalLink").click(function () {
        $("#externalSite").modal('hide');
    });
    $("#externalSite .external-close").click(function () {
        $("#externalSite").modal('hide');
    });
    /* Script for slick slider for befor and after */
    $('.slider-for').slick({
        slidesToShow: 2,
        slidesToScroll: 1,
        arrows: false,
        fade: true,
        asNavFor: '.slider-nav'
    });
    $('.slider-nav').slick({
        slidesToShow: 2,
        slidesToScroll: 2,
        dots: true,
        focusOnSelect: true
    });

    $('a[data-slide]').click(function (e) {
        e.preventDefault();
        var slideno = $(this).data('slide');
        $('.slider-nav').slick('slickGoTo', slideno - 1);
    });
    /* Script for video */
    $(".open-video").on("click", function () {
        $("body").addClass("open-popup");
        var e = $(this).attr("data-video")
            , t = Wistia.api($(this).data("video"));
        $(".video_popup[data-id='" + e + "']").show(),
            t.play()
    }),
        $(".videoclosebutton").on("click", function () {
            $("body").removeClass("open-popup");
            var e = Wistia.api($(this).data("video"));
            e.bind("timechange", function () {
                return e.time("0s"),
                    e.unbind
            }),
                $(".video_popup").hide()
        });
    $(window).scroll(function () {
        if ($(window).scrollTop() < 0) {
            $("header").css("top", "0px");
        }
        else {
            $("header").css("top", "-100px");
        }
    });
    $('.blk-para a[data-href]').each(function () {
        var href = $(this).data('href');
        $(this).attr('href', href);
    });
    /* Script for find a doctor page scroll */
    var r = $(".search_fields").position();
    $(".pagination .page-item").click(function () {
        $(window).scrollTop(r.top);
    });
    /* Ends here */
    /* Adding alt tag in block images */
    $(".section-tired .blk-container .blk-img > img").attr("alt", "Patient searching for psoriasis treatments online");
    $(".gray-back .blk-container .blk-img > img").attr("alt", "Psoriasis patient photos: 92% BSA before vs completely clear at week 12 with SILIQ");
    $(".siliq-savings .blk-container .blk-img > img").attr("alt", "Savings offer for SILIQ: $25/month*†");
    $(".find-a-doctor-sec .blk-container .blk-img > img").attr("alt", "Find dermatologist for SILIQ treatment");
    $(".patient-result .blk-container .blk-img > img").attr("alt", "Psoriasis patient with BSA 0% at 3 months with SILIQ");
    $(".darkblue-sec .blk-container .blk-img > img").attr("alt", "~4/6 patients were 100% clear at 1 and 2 years");
    $(".have-questions .blk-container .blk-img > img").attr("alt", "Psoriasis patient discussing SILIQ treatment with dermatologist");
    /* Ends here */

    /* Append p tag inside the block image div */
    $(".section-tired .blk-container .blk-img").append("<p class='blk-img-text'>Not actual patients.</p>");
    $(".have-questions .blk-container .blk-img").append("<p class='blk-img-text'>Not an actual patient.</p>");
    /* Ends here */
    
    
});