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
    }
    /* Replace span to <a> on device only */
    if ($(window).width() < 992) {
        $(".navbar-nav li.has-subnav span").replaceWith(function () {
            return $('<a href="/hcp/siliq-rems/about-siliq-rems/">' + $(this).html() + '</a>');
        });
    }
    /* Ends here */

    /* Script for talking to your doctor page */
    $('.siliq-quiz input[type="radio"]').on("click", function (e) {
        $(this).parents(".row").find("label").removeClass("border-active"),
            $(this).parent().toggleClass("border-active")
    });
    $('.siliq-quiz input[type="checkbox"]').on("click", function (e) {
        $(this).parent().toggleClass("border-active")
    });

    $(".d-none.d-lg-block.svg-in-desktop").each((function () {
        var t = $(this).html();
        t.match('<div class="safty">\x3c!--cmt--\x3e</div>') && $(this).html(t.replace('<div class="safty">\x3c!--cmt--\x3e</div>', '<svg height="577.25" viewBox="0 0 887.16 577.25" width="887.16" xmlns="http://www.w3.org/2000/svg"> <g data-name="Group 886" id="prefix__Group_886" transform="translate(-239.42 -1008.75)"> <g data-name="Group 317" id="prefix__Group_317" transform="translate(850.315 1164.75)"> <path class="tgraph1-1" d="M221.88-154.5h53.68v34.555h-53.68zm53.68 0h53.68v34.555h-53.68zm53.68 0h53.68v34.555h-53.68z" data-name="Path 131" id="prefix__Path_131" transform="translate(-430.195)"></path> <path class="tgraph1-2" d="M221.88-154.5h53.68v34.555h-53.68zm53.68 0h53.68v34.555h-53.68zm53.68 0h53.68v34.555h-53.68z" data-name="Path 1096" id="prefix__Path_1096" transform="translate(-269.195)"></path> <path class="tgraph1-3" d="M221.88-154.5h53.68v34.555h-53.68zm53.68 0h53.68v34.555h-53.68zm53.68 0h53.68v34.555h-53.68z" data-name="Path 1097" id="prefix__Path_1097" transform="translate(-107.911)"></path> <path class="tgraph1-3" d="M-608.62-83h182.2v47.789h-182.2zm182.2 0h73.055v47.789h-73.055zm73.055 0h73.055v47.789h-73.055zm73.055 0h73.055v47.789h-73.055z" data-name="Path 132" id="prefix__Path_132" transform="translate(-.775 -36.945)"></path> <path class="tgraph1-1" d="M-608.62-83h72.361v47.789h-72.361zm72.361 0h29.014v47.789h-29.014zm29.014 0h29.014v47.789h-29.014zm29.014 0h29.014v47.789h-29.014z" data-name="Path 1098" id="prefix__Path_1098" transform="translate(401.305 -36.945)"></path> <path class="tgraph1-2" d="M-608.62-83h72.361v47.789h-72.361zm72.361 0h29.014v47.789h-29.014zm29.014 0h29.014v47.789h-29.014zm29.014 0h29.014v47.789h-29.014z" data-name="Path 1099" id="prefix__Path_1099" transform="translate(562.305 -36.945)"></path> <path class="tgraph1-3" d="M-608.62-83h72.361v47.789h-72.361zm72.361 0h29.014v47.789h-29.014zm29.014 0h29.014v47.789h-29.014zm29.014 0h29.014v47.789h-29.014z" data-name="Path 1100" id="prefix__Path_1100" transform="translate(722.225 -36.945)"></path> <path d="M-608.62 15.884h401.364V50.2H-608.62zm401.364 0h160.932V50.2h-160.932zm160.932 0h160.932V50.2H-46.324zm160.932 0H275.54V50.2H114.608zM-608.62 84.51h401.364v34.313H-608.62zm401.364 0h160.932v34.313h-160.932zm160.932 0h160.932v34.313H-46.324zm160.932 0H275.54v34.313H114.608zm-723.228 68.625h401.364v34.192H-608.62zm401.364 0h160.932v34.192h-160.932zm160.932 0h160.932v34.192H-46.324zm160.932 0H275.54v34.192H114.608zm-723.228 68.5h401.364v34.313H-608.62zm401.364 0h160.932v34.313h-160.932zm160.932 0h160.932v34.313H-46.324zm160.932 0H275.54v34.313H114.608zm-723.228 79.788h401.364v34.313H-608.62zm401.364 0h160.932v34.313h-160.932zm160.932 0h160.932v34.313H-46.324zm160.932 0H275.54v34.313H114.608zm-723.228 68.626h401.364v45.467H-608.62zm401.364 0h160.932v45.467h-160.932zm160.932 0h160.932v45.467H-46.324zm160.932 0H275.54v45.467H114.608z" data-name="Path 133" fill="#fff" id="prefix__Path_133" transform="translate(-.775 -88.041)"></path> <path d="M-608.62 86.884h401.364V121.2H-608.62zm401.364 0h160.932V121.2h-160.932zm160.932 0h160.932V121.2H-46.324zm160.932 0H275.54V121.2H114.608zM-608.62 155.51h401.364v34.313H-608.62zm401.364 0h160.932v34.313h-160.932zm160.932 0h160.932v34.313H-46.324zm160.932 0H275.54v34.313H114.608zm-723.228 68.5h401.364v34.313H-608.62zm401.364 0h160.932v34.313h-160.932zm160.932 0h160.932v34.313H-46.324zm160.932 0H275.54v34.313H114.608zm-723.228 68.63h401.364v45.47H-608.62zm401.364 0h160.932v45.47h-160.932zm160.932 0h160.932v45.47H-46.324zm160.932 0H275.54v45.47H114.608zm-723.228 79.783h401.364v34.313H-608.62zm401.364 0h160.932v34.313h-160.932zm160.932 0h160.932v34.313H-46.324zm160.932 0H275.54v34.313H114.608z" data-name="Path 134" fill="#e5e8eb" id="prefix__Path_134" transform="translate(-.775 -124.728)"></path> <path d="M-610.12-154.5h402.089M-609.4-120.67v-33.1m401.364-.725H-47.1m-160.932 33.83v-33.1M-47.1-154.5h160.933M-47.1-120.67v-33.1m160.932-.725H275.49M113.833-120.67v-33.1m160.932 33.1v-33.1m-884.885 33.83h402.089M-609.4-72.882v-46.339m401.364-.725H-47.1m-160.931 47.064v-46.339m160.932-.725h160.932M-47.1-72.882v-46.339m160.932-.725H275.49M113.833-72.882v-46.339m160.932 46.339v-46.339M-610.12-72.157h402.089M-609.4-38.569v-32.863m401.364-.725H-47.1m-160.931 33.588v-32.863m160.931-.725h160.933M-47.1-38.569v-32.863m160.932-.725H275.49M113.833-38.569v-32.863m160.932 32.863v-32.863M-610.12-37.844h402.089M-609.4-4.256v-32.863m401.364-.725H-47.1M-208.031-4.256v-32.863m160.931-.725h160.933M-47.1-4.256v-32.863m160.932-.725H275.49M113.833-4.256v-32.863M274.765-4.256v-32.863M-610.12-3.531h402.089M-609.4 30.057V-2.806m401.364-.725H-47.1m-160.931 33.588V-2.806M-47.1-3.531h160.933M-47.1 30.057V-2.806m160.932-.725H275.49M113.833 30.057V-2.806m160.932 32.863V-2.806M-610.12 30.782h402.089M-609.4 64.37V31.507m401.364-.725H-47.1M-208.031 64.37V31.507m160.931-.725h160.933M-47.1 64.37V31.507m160.932-.725H275.49M113.833 64.37V31.507M274.765 64.37V31.507M-610.12 65.095h402.089M-609.4 98.562V65.82m401.364-.725H-47.1m-160.931 33.467V65.82m160.931-.725h160.933M-47.1 98.562V65.82m160.932-.725H275.49M113.833 98.562V65.82m160.932 32.742V65.82M-610.12 99.287h402.089M-609.4 132.875v-32.863m401.364-.725H-47.1m-160.932 33.588v-32.863m160.932-.725h160.933M-47.1 132.875v-32.863m160.932-.725H275.49m-161.657 33.588v-32.863m160.932 32.863v-32.863M-610.12 133.6h402.089M-609.4 167.187v-32.863m401.364-.725H-47.1m-160.932 33.588v-32.863M-47.1 133.6h160.933M-47.1 167.187v-32.863m160.932-.725H275.49m-161.657 33.588v-32.863m160.932 32.863v-32.863m-884.885 33.588h402.089M-609.4 212.657v-44.02m401.364-.725H-47.1m-160.932 44.744v-44.02m160.932-.725h160.933M-47.1 212.657v-44.02m160.932-.725H275.49m-161.657 44.745v-44.02m160.932 44.02v-44.02m-884.885 44.745h402.089M-609.4 246.97v-32.863m401.364-.725H-47.1m-160.931 33.588v-32.863m160.932-.725h160.932M-47.1 246.97v-32.863m160.932-.725H275.49M113.833 246.97v-32.863m160.932 32.863v-32.863M-610.12 247.7h402.089M-609.4 281.283V248.42m401.364-.725H-47.1m-160.932 33.588V248.42m160.932-.72h160.933M-47.1 281.283V248.42m160.932-.725H275.49m-161.657 33.588V248.42m160.932 32.863V248.42m-884.885 33.588h402.089M-609.4 326.75v-44.017m401.364-.725H-47.1m-160.931 44.742v-44.017m160.932-.725h160.932M-47.1 326.75v-44.017m160.932-.725H275.49M113.833 326.75v-44.017m160.932 44.017v-44.017m-884.885 44.742h402.089m0 0H-47.1m0 0h160.933m0 0H275.49" data-name="Path 135" fill="none" id="prefix__Path_135" stroke="#fff" stroke-miterlimit="10" stroke-width="3px"></path> <text class="tgraph1-7" id="prefix__Placebo" transform="translate(-151.315 -133.738)"> <tspan x="0" y="0">Placebo</tspan> </text> <text data-name="SILIQ 210 mg Q2W*" fill="#fff" font-family="Archivo-BoldItalic, Archivo" font-size="13px" font-style="italic" font-weight="700" id="prefix__SILIQ_210_mg_Q2W_" letter-spacing="-0.018em" transform="translate(-24.315 -133.738)"> <tspan x="0" y="0">SILIQ</tspan><tspan font-family="Archivo-Bold, Archivo" font-style="normal" y="0"> 210 mg Q2W*</tspan> </text> <text class="tgraph1-7" id="prefix__Stelara" transform="translate(171.685 -133.738)"> <tspan x="0" y="0">Stelara</tspan> </text> <text class="tgraph1-7" data-name="Adverse Reaction" id="prefix__Adverse_Reaction" transform="translate(-600.696 -83.292)"> <tspan x="0" y="0">Adverse Reaction</tspan> </text> <text class="tgraph1-7" data-name="N=879" id="prefix__N_879" transform="translate(-145.315 -101.845)"> <tspan x="0" y="0">N=879</tspan> </text> <text class="tgraph1-10" data-name="n (%)" id="prefix__n_" transform="translate(-142.315 -83.292)"> <tspan x="0" y="0">n (%)</tspan> </text> <text data-name="N=1496" fill="#fff" font-family="Archivo-Bold, Archivo" font-size="14px" font-weight="700" id="prefix__N_1496" letter-spacing="-0.01em" transform="translate(8.685 -100.845)"> <tspan x="0" y="0">N=1496</tspan> </text> <text class="tgraph1-10" data-name="n (%)" id="prefix__n_2" transform="translate(17.685 -83.292)"> <tspan x="0" y="0">n (%)</tspan> </text> <text class="tgraph1-7" data-name="N=613" id="prefix__N_613" transform="translate(174.685 -101.845)"> <tspan x="0" y="0">N=613</tspan> </text> <text class="tgraph1-10" data-name="n (%)" id="prefix__n_3" transform="translate(177.684 -83.292)"> <tspan x="0" y="0">n (%)</tspan> </text> <text class="tgraph1-13" id="prefix__Arthralgia" transform="translate(-600.696 -50.512)"> <tspan x="0" y="0">Arthralgia</tspan> </text> <text class="tgraph1-13" id="prefix__Headache" transform="translate(-600.696 -17.203)"> <tspan x="0" y="0">Headache</tspan> </text> <text class="tgraph1-13" id="prefix__Fatigue" transform="translate(-600.696 17.11)"> <tspan x="0" y="0">Fatigue</tspan> </text> <text class="tgraph1-13" id="prefix__Diarrhea" transform="translate(-600.696 51.422)"> <tspan x="0" y="0">Diarrhea</tspan> </text> <text class="tgraph1-13" data-name="Oropharyngeal pain" id="prefix__Oropharyngeal_pain" transform="translate(-600.696 85.675)"> <tspan x="0" y="0">Oropharyngeal pain</tspan> </text> <text class="tgraph1-13" id="prefix__Nausea" transform="translate(-600.696 119.927)"> <tspan x="0" y="0">Nausea</tspan> </text> <text class="tgraph1-13" id="prefix__Myalgia" transform="translate(-600.696 154.24)"> <tspan x="0" y="0">Myalgia</tspan> </text> <text class="tgraph1-13" data-name="Injection site reactions" id="prefix__Injection_site_reactions" transform="translate(-600.696 186.012)"> <tspan x="0" y="0">Injection site reactions</tspan> </text> <text data-name="(pain, erythema, bruising, hemorrhage, pruritus)" fill="#253746" font-family="Archivo-Bold, Archivo" font-size="11px" font-weight="700" id="prefix___pain_erythema_bruising_hemorrhage_pruritus_" letter-spacing="-0.01em" transform="translate(-600.696 202.184)"> <tspan x="0" y="0">(pain, erythema, bruising, hemorrhage, pruritus)</tspan> </text> <text class="tgraph1-13" data-name="Inﬂuenza" id="prefix__In_uenza" transform="translate(-600.696 234.023)"> <tspan x="0" y="0">Inﬂuenza</tspan> </text> <text class="tgraph1-13" id="prefix__Neutropenia" transform="translate(-600.696 269.332)"> <tspan x="0" y="0">Neutropenia</tspan> </text> <text class="tgraph1-13" data-name="Tinea infections" id="prefix__Tinea_infections" transform="translate(-600.696 300.108)"> <tspan x="0" y="0">Tinea infections</tspan> </text> <text data-name="(tinea pedis, versicolor, cruris)" fill="#253746" font-family="Archivo-Bold, Archivo" font-size="12px" font-weight="700" id="prefix___tinea_pedis_versicolor_cruris_" letter-spacing="-0.01em" transform="translate(-600.696 317.28)"> <tspan x="0" y="0">(tinea pedis, versicolor, cruris)</tspan> </text> <text class="tgraph1-12" data-name="29 (3.3)" id="prefix___29_3_3_" transform="translate(-150.315 -51.512)"> <tspan x="0" y="0">29 (3.3)</tspan> </text> <text class="tgraph1-12" data-name="71 (4.7)" id="prefix___71_4_7_" transform="translate(9.685 -51.512)"> <tspan x="0" y="0">71 (4.7)</tspan> </text> <text class="tgraph1-12" data-name="15 (2.4)" id="prefix___15_2_4_" transform="translate(169.685 -51.512)"> <tspan x="0" y="0">15 (2.4)</tspan> </text> <text class="tgraph1-12" data-name="31 (3.5)" id="prefix___31_3_5_" transform="translate(-150.315 -17.203)"> <tspan x="0" y="0">31 (3.5)</tspan> </text> <text class="tgraph1-12" data-name="64 (4.3)" id="prefix___64_4_3_" transform="translate(9.685 -17.203)"> <tspan x="0" y="0">64 (4.3)</tspan> </text> <text class="tgraph1-12" data-name="23 (3.8)" id="prefix___23_3_8_" transform="translate(169.685 -17.203)"> <tspan x="0" y="0">23 (3.8)</tspan> </text> <text class="tgraph1-12" data-name="10 (1.1)" id="prefix___10_1_1_" transform="translate(-150.315 17.11)"> <tspan x="0" y="0">10 (1.1)</tspan> </text> <text class="tgraph1-12" data-name="39 (2.6)" id="prefix___39_2_6_" transform="translate(9.685 17.11)"> <tspan x="0" y="0">39 (2.6)</tspan> </text> <text class="tgraph1-12" data-name="16 (2.6)" id="prefix___16_2_6_" transform="translate(169.685 17.11)"> <tspan x="0" y="0">16 (2.6)</tspan> </text> <text class="tgraph1-12" data-name="10 (1.1)" id="prefix___10_1_1_2" transform="translate(-150.315 51.423)"> <tspan x="0" y="0">10 (1.1)</tspan> </text> <text class="tgraph1-12" data-name="33 (2.2)" id="prefix___33_2_2_" transform="translate(9.685 51.423)"> <tspan x="0" y="0">33 (2.2)</tspan> </text> <text class="tgraph1-12" data-name="5 (0.8)" id="prefix___5_0_8_" transform="translate(171.685 51.423)"> <tspan x="0" y="0">5 (0.8)</tspan> </text> <text class="tgraph1-12" data-name="10 (1.1)" id="prefix___10_1_1_3" transform="translate(-150.315 85.675)"> <tspan x="0" y="0">10 (1.1)</tspan> </text> <text class="tgraph1-12" data-name="31 (2.1)" id="prefix___31_2_1_" transform="translate(9.685 85.675)"> <tspan x="0" y="0">31 (2.1)</tspan> </text> <text class="tgraph1-12" data-name="8 (1.3)" id="prefix___8_1_3_" transform="translate(171.685 85.675)"> <tspan x="0" y="0">8 (1.3)</tspan> </text> <text class="tgraph1-12" data-name="10 (1.1)" id="prefix___10_1_1_4" transform="translate(-150.315 119.927)"> <tspan x="0" y="0">10 (1.1)</tspan> </text> <text class="tgraph1-12" data-name="28 (1.9)" id="prefix___28_1_9_" transform="translate(9.685 119.927)"> <tspan x="0" y="0">28 (1.9)</tspan> </text> <text class="tgraph1-12" data-name="6 (1.0)" id="prefix___6_1_0_" transform="translate(171.685 119.927)"> <tspan x="0" y="0">6 (1.0)</tspan> </text> <text class="tgraph1-12" data-name="3 (0.3)" id="prefix___3_0_3_" transform="translate(-147.315 154.24)"> <tspan x="0" y="0">3 (0.3)</tspan> </text> <text class="tgraph1-12" data-name="26 (1.7)" id="prefix___26_1_7_" transform="translate(9.685 154.24)"> <tspan x="0" y="0">26 (1.7)</tspan> </text> <text class="tgraph1-12" data-name="4 (0.7)" id="prefix___4_0_7_" transform="translate(171.685 154.24)"> <tspan x="0" y="0">4 (0.7)</tspan> </text> <text class="tgraph1-12" data-name="11 (1.3)" id="prefix___11_1_3_" transform="translate(-150.315 194.134)"> <tspan x="0" y="0">11 (1.3)</tspan> </text> <text class="tgraph1-12" data-name="23 (1.5)" id="prefix___23_1_5_" transform="translate(9.685 194.134)"> <tspan x="0" y="0">23 (1.5)</tspan> </text> <text class="tgraph1-12" data-name="12 (2.0)" id="prefix___12_2_0_" transform="translate(169.685 194.134)"> <tspan x="0" y="0">12 (2.0)</tspan> </text> <text class="tgraph1-12" data-name="4 (0.5)" id="prefix___4_0_5_" transform="translate(-146.315 234.023)"> <tspan x="0" y="0">4 (0.5)</tspan> </text> <text class="tgraph1-12" data-name="19 (1.3)" id="prefix___19_1_3_" transform="translate(9.685 234.023)"> <tspan x="0" y="0">19 (1.3)</tspan> </text> <text class="tgraph1-12" data-name="7 (1.1)" id="prefix___7_1_1_" transform="translate(171.685 234.023)"> <tspan x="0" y="0">7 (1.1)</tspan> </text> <text class="tgraph1-12" data-name="4 (0.5)" id="prefix___4_0_5_2" transform="translate(-149.315 268.332)"> <tspan x="0" y="0">4 (0.5)</tspan> </text> <text class="tgraph1-12" data-name="15 (1.0)" id="prefix___15_1_0_" transform="translate(9.685 268.332)"> <tspan x="0" y="0">15 (1.0)</tspan> </text> <text class="tgraph1-12" data-name="5 (0.8)" id="prefix___5_0_8_2" transform="translate(173.685 268.332)"> <tspan x="0" y="0">5 (0.8)</tspan> </text> <text class="tgraph1-12" data-name="2 (0.2)" id="prefix___2_0_2_" transform="translate(-146.315 308.225)"> <tspan x="0" y="0">2 (0.2)</tspan> </text> <text class="tgraph1-12" data-name="15 (1.0)" id="prefix___15_1_0_2" transform="translate(9.685 308.225)"> <tspan x="0" y="0">15 (1.0)</tspan> </text> <text class="tgraph1-12" data-name="3 (0.5)" id="prefix___3_0_5_" transform="translate(173.685 308.225)"> <tspan x="0" y="0">3 (0.5)</tspan> </text> </g> <text class="tgraph1-16" data-name="*Subjects receiving 210 mg of SILIQ at Weeks 0, 1, and 2, followed by treatment every 2 weeks during the 12-week period." id="prefix___Subjects_receiving_210_mg_of_SILIQ_at_Weeks_0_1_and_2_followed_by_treatment_every_2_weeks_during_the_12-week_period" transform="translate(242 1535)"> <tspan x="0" y="12">*Subjects receiving 210 mg of </tspan><tspan fill="#44484b" font-family="Archivo-Italic, Archivo" font-style="italic" y="12">SILIQ</tspan><tspan y="12"> at Weeks 0, 1, and 2, followed by treatment every 2 weeks during the 12-week period.</tspan> </text> <text class="tgraph1-16" data-name="Q2W = once every 2 weeks." id="prefix___Q2W_once_every_2_weeks__" transform="translate(240 1515)"> <tspan x="7" y="12">Q2W, once every 2 weeks.</tspan> </text> </g></svg >'))
    }));
    $(".d-block.d-lg-none").each((function () {
        var t = $(this).html();
        t.match('<div class="safty2"><!--cmt--></div>') && $(this).html(t.replace('<div class="safty2"><!--cmt--></div>', '<svg height="674" viewBox="0 0 336.501 674" width="336.501" xmlns="http://www.w3.org/2000/svg"><g data-name="Group 874" id="mob_prefix__Group_874" transform="translate(-19.999 -780)"> <text class="tgraph1-16" data-name="Q2W = once every 2 weeks. *Subjects receiving 210 mg of SILIQ at Weeks 0, 1, and 2, followed by treatment every 2 weeks during the 12-week period." fill="#494d50" font-family="Helvetica" font-size="16px" id="mob_prefix___Q2W_once_every_2_weeks__Subjects_receiving_210_mg_of_SILIQ_at_Weeks_0_1_and_2_followed_by_treatment_every_2_weeks_during_the_12-week_period_" transform="translate(20 1356)"> <tspan x="7" y="12">Q2W, once every 2 weeks.</tspan><tspan x="0" y="39">*Subjects receiving 210 mg of <tspan fill="#44484b" font-family="Archivo-Italic, Archivo" font-style="italic" y="39">SILIQ</tspan> at Weeks </tspan><tspan x="7" y="60"> 0, 1, and 2, followed by treatment every 2 </tspan><tspan x="7" y="80"> weeks during the 12-week period.</tspan> </text> <g data-name="Group 843" id="mob_prefix__Group_843" transform="translate(0 -38)"> <path class="mob-tgraph1-2" d="M0 0H334V33H0z" data-name="Rectangle 289" id="mob_prefix__Rectangle_289" transform="translate(20 963)"></path> <path class="mob-tgraph1-2" d="M0 0H334V33H0z" data-name="Rectangle 290" id="mob_prefix__Rectangle_290" transform="translate(20 1032)"></path> <path class="mob-tgraph1-2" d="M0 0H334V33H0z" data-name="Rectangle 291" id="mob_prefix__Rectangle_291" transform="translate(20 1101)"></path> <path class="mob-tgraph1-2" d="M0 0H334V33H0z" data-name="Rectangle 293" id="mob_prefix__Rectangle_293" transform="translate(20 1279)"></path> <path class="mob-tgraph1-2" d="M0 0H334V73H0z" data-name="Rectangle 292" id="mob_prefix__Rectangle_292" transform="translate(22 1170)"></path> <path d="M221.88-154.5h21.667v110.789H221.88zm21.667 0h21.667v110.789h-21.667zm21.667 0h21.666v110.789h-21.667z" data-name="Path 131" fill="#797979" id="mob_prefix__Path_131" transform="translate(-63.38 972.5)"></path> <path d="M221.88-154.5h21.667v110.789H221.88zm21.667 0h21.667v110.789h-21.667zm21.667 0h21.666v110.789h-21.667z" data-name="Path 1102" fill="#00ae4f" id="mob_prefix__Path_1102" transform="translate(1.619 972.5)"></path> <path d="M221.88-154.5h21.667v110.789H221.88zm21.667 0h21.667v110.789h-21.667zm21.667 0h21.666v110.789h-21.667z" data-name="Path 1103" fill="#012b46" id="mob_prefix__Path_1103" transform="translate(66.619 972.5)"></path> <path d="M-608.62-83h62.872v47.789h-62.872zm62.872 0h25.209v47.789h-25.209zm25.209 0h25.21v47.789h-25.21zm25.21 0h25.21v47.789h-25.21z" data-name="Path 132" fill="#253746" id="mob_prefix__Path_132" transform="translate(628.619 964)"></path> <text class="mob-tgraph1-7" id="mob_prefix__Placebo" transform="translate(170 855.012)"> <tspan x="0" y="0">Placebo</tspan> </text> <text data-name="SILIQ 210 mg Q2W*" fill="#fff" font-family="Helvetica-Bold, Helvetica" font-size="12px" font-weight="700" id="mob_prefix__SILIQ_210_mg_Q2W_" letter-spacing="-0.018em" transform="translate(257 841.012)"> <tspan font-style="italic" x="-16.795" y="0">SILIQ </tspan><tspan x="-21.697" y="14">210 mg </tspan><tspan x="-15.678" y="28">Q2W*</tspan> </text> <text class="mob-tgraph1-7" id="mob_prefix__Stelara" transform="translate(302 855.012)"> <tspan x="0" y="0">Stelara</tspan> </text> <text data-name="Adverse Reaction" fill="#fff" font-family="AcuminPro-Bold, Acumin Pro" font-size="12px" font-weight="700" id="mob_prefix__Adverse_Reaction" letter-spacing="-0.01em" transform="translate(28.111 918)"> <tspan x="0" y="0">Adverse Reaction</tspan> </text> <text class="mob-tgraph1-10" data-name="N=879" id="mob_prefix__N_879" transform="translate(172 898.905)"> <tspan x="0" y="0">N=879</tspan> </text> <text class="mob-tgraph1-11" data-name="n (%)" id="mob_prefix__n_" transform="translate(177 917)"> <tspan x="0" y="0">n (%)</tspan> </text> <text data-name="N=1496" fill="#fff" font-family="Helvetica-Bold, Helvetica" font-size="14px" font-weight="700" id="mob_prefix__N_1496" letter-spacing="-0.01em" transform="translate(231 899.905)"> <tspan x="0" y="0">N=1496</tspan> </text> <text class="mob-tgraph1-11" data-name="n (%)" id="mob_prefix__n_2" transform="translate(242 917.458)"> <tspan x="0" y="0">n (%)</tspan> </text> <text class="mob-tgraph1-10" data-name="N=613" id="mob_prefix__N_613" transform="translate(302 898.905)"> <tspan x="0" y="0">N=613</tspan> </text> <text class="mob-tgraph1-11" data-name="n (%)" id="mob_prefix__n_3" transform="translate(307 917.458)"> <tspan x="0" y="0">n (%)</tspan> </text> <text class="mob-tgraph1-13" id="mob_prefix__Arthralgia" transform="translate(27.111 950.238)"> <tspan x="0" y="0">Arthralgia</tspan> </text> <text class="mob-tgraph1-13" id="mob_prefix__Headache" transform="translate(27.111 983.547)"> <tspan x="0" y="0">Headache</tspan> </text> <text class="mob-tgraph1-13" id="mob_prefix__Fatigue" transform="translate(27.111 1017.86)"> <tspan x="0" y="0">Fatigue</tspan> </text> <text class="mob-tgraph1-13" id="mob_prefix__Diarrhea" transform="translate(27.111 1052.172)"> <tspan x="0" y="0">Diarrhea</tspan> </text> <text class="mob-tgraph1-13" data-name="Oropharyngeal pain" id="mob_prefix__Oropharyngeal_pain" transform="translate(27.111 1086.425)"> <tspan x="0" y="0">Oropharyngeal pain</tspan> </text> <text class="mob-tgraph1-13" id="mob_prefix__Nausea" transform="translate(27.111 1120.677)"> <tspan x="0" y="0">Nausea</tspan> </text> <text class="mob-tgraph1-13" id="mob_prefix__Myalgia" transform="translate(27.111 1154.99)"> <tspan x="0" y="0">Myalgia</tspan> </text> <text class="mob-tgraph1-13" data-name="Injection site reactions" id="mob_prefix__Injection_site_reactions" transform="translate(27.111 1186.762)"> <tspan x="0" y="0">Injection site reactions</tspan> </text> <text class="mob-tgraph1-13" data-name="(pain, erythema, bruising, hemorrhage, pruritus)" id="mob_prefix___pain_erythema_bruising_hemorrhage_pruritus_" transform="translate(27.111 1202.934)"> <tspan x="0" y="0">(pain, erythema, </tspan><tspan x="0" y="14">bruising, hemorrhage, </tspan><tspan x="0" y="28">pruritus)</tspan> </text> <text class="mob-tgraph1-13" data-name="Inﬂuenza" id="mob_prefix__In_uenza" transform="translate(27.111 1262.773)"> <tspan x="0" y="0">Inﬂuenza</tspan> </text> <text class="mob-tgraph1-13" id="mob_prefix__Neutropenia" transform="translate(27.111 1298.082)"> <tspan x="0" y="0">Neutropenia</tspan> </text> <text class="mob-tgraph1-13" data-name="Tinea infections" id="mob_prefix__Tinea_infections" transform="translate(27.111 1328.858)"> <tspan x="0" y="0">Tinea infections</tspan> </text> <text class="mob-tgraph1-13" data-name="(tinea pedis, versicolor, cruris)" id="mob_prefix___tinea_pedis_versicolor_cruris_" transform="translate(27.111 1346.03)"> <tspan x="0" y="0">(tinea pedis, </tspan><tspan x="0" y="15">versicolor, cruris)</tspan> </text> <text class="mob-tgraph1-14" data-name="29 (3.3)" id="mob_prefix___29_3_3_" transform="translate(168 950.238)"> <tspan x="0" y="0">29 (3.3)</tspan> </text> <text class="mob-tgraph1-14" data-name="71 (4.7)" id="mob_prefix___71_4_7_" transform="translate(233 950.238)"> <tspan x="0" y="0">71 (4.7)</tspan> </text> <text class="mob-tgraph1-14" data-name="15 (2.4)" id="mob_prefix___15_2_4_" transform="translate(298 950.238)"> <tspan x="0" y="0">15 (2.4)</tspan> </text> <text class="mob-tgraph1-14" data-name="31 (3.5)" id="mob_prefix___31_3_5_" transform="translate(168 983.547)"> <tspan x="0" y="0">31 (3.5)</tspan> </text> <text class="mob-tgraph1-14" data-name="64 (4.3)" id="mob_prefix___64_4_3_" transform="translate(233 983.547)"> <tspan x="0" y="0">64 (4.3)</tspan> </text> <text class="mob-tgraph1-14" data-name="23 (3.8)" id="mob_prefix___23_3_8_" transform="translate(298 983.547)"> <tspan x="0" y="0">23 (3.8)</tspan> </text> <text class="mob-tgraph1-14" data-name="10 (1.1)" id="mob_prefix___10_1_1_" transform="translate(168 1017.86)"> <tspan x="0" y="0">10 (1.1)</tspan> </text> <text class="mob-tgraph1-14" data-name="39 (2.6)" id="mob_prefix___39_2_6_" transform="translate(233 1017.86)"> <tspan x="0" y="0">39 (2.6)</tspan> </text> <text class="mob-tgraph1-14" data-name="16 (2.6)" id="mob_prefix___16_2_6_" transform="translate(298 1017.86)"> <tspan x="0" y="0">16 (2.6)</tspan> </text> <text class="mob-tgraph1-14" data-name="10 (1.1)" id="mob_prefix___10_1_1_2" transform="translate(168 1052.173)"> <tspan x="0" y="0">10 (1.1)</tspan> </text> <text class="mob-tgraph1-14" data-name="33 (2.2)" id="mob_prefix___33_2_2_" transform="translate(233 1052.173)"> <tspan x="0" y="0">33 (2.2)</tspan> </text> <text class="mob-tgraph1-14" data-name="5 (0.8)" id="mob_prefix___5_0_8_" transform="translate(300 1052.173)"> <tspan x="0" y="0">5 (0.8)</tspan> </text> <text class="mob-tgraph1-14" data-name="10 (1.1)" id="mob_prefix___10_1_1_3" transform="translate(168 1086.425)"> <tspan x="0" y="0">10 (1.1)</tspan> </text> <text class="mob-tgraph1-14" data-name="31 (2.1)" id="mob_prefix___31_2_1_" transform="translate(233 1086.425)"> <tspan x="0" y="0">31 (2.1)</tspan> </text> <text class="mob-tgraph1-14" data-name="8 (1.3)" id="mob_prefix___8_1_3_" transform="translate(300 1086.425)"> <tspan x="0" y="0">8 (1.3)</tspan> </text> <text class="mob-tgraph1-14" data-name="10 (1.1)" id="mob_prefix___10_1_1_4" transform="translate(168 1120.677)"> <tspan x="0" y="0">10 (1.1)</tspan> </text> <text class="mob-tgraph1-14" data-name="28 (1.9)" id="mob_prefix___28_1_9_" transform="translate(233 1120.677)"> <tspan x="0" y="0">28 (1.9)</tspan> </text> <text class="mob-tgraph1-14" data-name="6 (1.0)" id="mob_prefix___6_1_0_" transform="translate(300 1120.677)"> <tspan x="0" y="0">6 (1.0)</tspan> </text> <text class="mob-tgraph1-14" data-name="3 (0.3)" id="mob_prefix___3_0_3_" transform="translate(172 1154.99)"> <tspan x="0" y="0">3 (0.3)</tspan> </text> <text class="mob-tgraph1-14" data-name="26 (1.7)" id="mob_prefix___26_1_7_" transform="translate(233 1154.99)"> <tspan x="0" y="0">26 (1.7)</tspan> </text> <text class="mob-tgraph1-14" data-name="4 (0.7)" id="mob_prefix___4_0_7_" transform="translate(300 1154.99)"> <tspan x="0" y="0">4 (0.7)</tspan> </text> <text class="mob-tgraph1-14" data-name="11 (1.3)" id="mob_prefix___11_1_3_" transform="translate(168 1186.884)"> <tspan x="0" y="0">11 (1.3)</tspan> </text> <text class="mob-tgraph1-14" data-name="23 (1.5)" id="mob_prefix___23_1_5_" transform="translate(233 1186.884)"> <tspan x="0" y="0">23 (1.5)</tspan> </text> <text class="mob-tgraph1-14" data-name="12 (2.0)" id="mob_prefix___12_2_0_" transform="translate(298 1186.884)"> <tspan x="0" y="0">12 (2.0)</tspan> </text> <text class="mob-tgraph1-14" data-name="4 (0.5)" id="mob_prefix___4_0_5_" transform="translate(172 1262.773)"> <tspan x="0" y="0">4 (0.5)</tspan> </text> <text class="mob-tgraph1-14" data-name="19 (1.3)" id="mob_prefix___19_1_3_" transform="translate(233 1262.773)"> <tspan x="0" y="0">19 (1.3)</tspan> </text> <text class="mob-tgraph1-14" data-name="7 (1.1)" id="mob_prefix___7_1_1_" transform="translate(300 1262.773)"> <tspan x="0" y="0">7 (1.1)</tspan> </text> <text class="mob-tgraph1-14" data-name="4 (0.5)" id="mob_prefix___4_0_5_2" transform="translate(170 1298.082)"> <tspan x="0" y="0">4 (0.5)</tspan> </text> <text class="mob-tgraph1-14" data-name="15 (1.0)" id="mob_prefix___15_1_0_" transform="translate(233 1298.082)"> <tspan x="0" y="0">15 (1.0)</tspan> </text> <text class="mob-tgraph1-14" data-name="5 (0.8)" id="mob_prefix___5_0_8_2" transform="translate(302 1298.082)"> <tspan x="0" y="0">5 (0.8)</tspan> </text> <text class="mob-tgraph1-14" data-name="2 (0.2)" id="mob_prefix___2_0_2_" transform="translate(172 1328.975)"> <tspan x="0" y="0">2 (0.2)</tspan> </text> <text class="mob-tgraph1-14" data-name="15 (1.0)" id="mob_prefix___15_1_0_2" transform="translate(233 1328.975)"> <tspan x="0" y="0">15 (1.0)</tspan> </text> <text class="mob-tgraph1-14" data-name="3 (0.5)" id="mob_prefix___3_0_5_" transform="translate(302 1328.975)"> <tspan x="0" y="0">3 (0.5)</tspan> </text> <path class="mob-tgraph1-15" d="M0 0L0 546" data-name="Line 2" id="mob_prefix__Line_2" transform="translate(223.5 818.5)"></path> <path class="mob-tgraph1-15" d="M0 0L0 546" data-name="Line 5" id="mob_prefix__Line_5" transform="translate(158.5 818.5)"></path> <path class="mob-tgraph1-15" d="M334.5 0L0 0" data-name="Line 4" id="mob_prefix__Line_4" transform="translate(22 880.5)"></path> <path class="mob-tgraph1-15" d="M0 0L0 546" data-name="Line 3" id="mob_prefix__Line_3" transform="translate(288.5 818.5)"></path> </g> </g></svg>'))
    }));
    
    $(".d-block.d-lg-none.mob-pb-40.w-100.mobile-svg-width").each((function () {
        var t = $(this).html();
        t.match('<div class="siliqworks"><!--cmt--></div>') && $(this).html(t.replace('<div class="siliqworks"><!--cmt--></div>', '<svg height="279.134" viewBox="0 0 325.763 279.134" width="325.763" xmlns="http://www.w3.org/2000/svg"><g data-name="Group 323" id="Group_323" transform="translate(1.016 0.175)"><path d="M221.88-156V121.459H545.611V-156Z" data-name="Path 141" fill="#f6f8f9" id="Path_141" transform="translate(-221.88 156)"></path><path d="M223.38-154.5h72.541v43.642H223.38Zm72.541,0h72.541v43.642H295.921Zm72.541,0H441v43.642H368.463Zm72.541,0H545.786v43.642H441Z" data-name="Path 142" fill="#253746" id="Path_142" transform="translate(-222.055 155.825)"></path><path d="M223.38-12.5h72.541V34.533H223.38Zm72.541,0h72.541V34.533H295.921Zm72.541,0H441V34.533H368.463ZM441-12.5H545.786V34.533H441ZM223.38,81.567h72.541V128.6H223.38Zm72.541,0h72.541V128.6H295.921Zm72.541,0H441V128.6H368.463Zm72.541,0H545.786V128.6H441Z" data-name="Path 144" fill="#eef1f4" id="Path_144" transform="translate(-222.055 104.756)"></path><path d="M221.88-154.5h73.106m-72.622,45.05v-44.078m72.622-.972h72.622m-72.622,45.05v-44.078m72.622-.972h72.622m-72.622,45.05v-44.078m72.622-.972H545.611M440.229-109.45v-44.078m104.9,44.078v-44.078M221.88-108.478h73.106m-72.622,45.05v-44.078m72.622-.972h72.622m-72.622,45.05v-44.078m72.622-.972h72.622m-72.622,45.05v-44.078m72.622-.972H545.611M440.229-63.428v-44.078m104.9,44.078v-44.078M221.88-62.455h73.106m-72.622,45.05V-61.483m72.622-.972h72.622m-72.622,45.05V-61.483m72.622-.972h72.622m-72.622,45.05V-61.483m72.622-.972H545.611M440.229-17.405V-61.483m104.9,44.078V-61.483M221.88-16.433h73.106m-72.622,45.05V-15.461m72.622-.972h72.622m-72.622,45.05V-15.461m72.622-.972h72.622m-72.622,45.05V-15.461m72.622-.972H545.611M440.229,28.617V-15.461m104.9,44.078V-15.461M221.88,29.589h73.106m-72.622,45.05V30.562m72.622-.972h72.622m-72.622,45.05V30.562m72.622-.972h72.622m-72.622,45.05V30.562m72.622-.972H545.611M440.229,74.639V30.562m104.9,44.078V30.562M221.88,75.612h73.106m-72.622,45.05V76.584m72.622-.972h72.622m-72.622,45.05V76.584m72.622-.972h72.622m-72.622,45.05V76.584m72.622-.972H545.611m-105.382,45.05V76.584m104.9,44.078V76.584M221.88,121.634h73.106m0,0h72.622m0,0h72.622m0,0H545.611" data-name="Path 145" fill="none" id="Path_145" stroke="#fff" stroke-miterlimit="10" stroke-width="3" transform="translate(-221.88 155.825)"></path><g data-name="Group 404" id="Group_404" transform="translate(12.657 17.346)"> <text fill="#fff" font-family="AcuminPro-Bold, Acumin Pro" font-size="10" font-weight="700" id="Cytokine" letter-spacing="-0.01em" transform="translate(23.294 10)"><tspan x="-20.535" y="0">Cytokine</tspan></text> <text fill="#44484b" font-family="AcuminPro-Regular, Acumin Pro" font-size="9" id="IL-17A" letter-spacing="-0.01em" transform="translate(23.294 55.351)"><tspan x="-12.064" y="0">IL-17A</tspan></text> <text fill="#44484b" font-family="AcuminPro-Regular, Acumin Pro" font-size="9" id="IL-17C" letter-spacing="-0.01em" transform="translate(23.294 104.33)"><tspan x="-12.181" y="0">IL-17C</tspan></text> <text data-name="IL-17E (IL-25)" fill="#44484b" font-family="AcuminPro-Regular, Acumin Pro" font-size="9" id="IL-17E_IL-25_" letter-spacing="-0.01em" transform="translate(23.294 147.299)"><tspan x="-25.731" y="0">IL-17E (IL-25)</tspan></text> <text fill="#44484b" font-family="AcuminPro-Regular, Acumin Pro" font-size="9" id="IL-17F" letter-spacing="-0.01em" transform="translate(23.294 195)"><tspan x="-11.632" y="0">IL-17F</tspan></text> <text data-name="IL-17A/F" fill="#44484b" font-family="AcuminPro-Regular, Acumin Pro" font-size="9" id="IL-17A_F" letter-spacing="-0.01em" transform="translate(23.294 239.253)"><tspan x="-16.524" y="0">IL-17A/F</tspan></text> </g><g data-name="Group 405" id="Group_405" transform="translate(80.142 17.346)"> <text data-name="In Psoriasis" fill="#fff" font-family="AcuminPro-Bold, Acumin Pro" font-size="10" font-weight="700" id="In_Psoriasis" letter-spacing="-0.01em" transform="translate(29.809 10)"><tspan x="-26.34" y="0">In Psoriasis</tspan></text> <text data-name="Elevated 28x13" fill="#44484b" font-family="OpenSans, Open Sans" font-size="9" id="Elevated_28x13" letter-spacing="-0.01em" transform="translate(29.809 55.923)"><tspan x="-28.901" y="0">Elevated 28x</tspan><tspan baseline-shift="2.9997000396711506" font-size="5.25" y="0">10</tspan></text> <text data-name="Elevated 30x13" fill="#44484b" font-family="OpenSans, Open Sans" font-size="9" id="Elevated_30x13" letter-spacing="-0.01em" transform="translate(29.809 103.732)"><tspan x="-28.901" y="0">Elevated 30x</tspan><tspan baseline-shift="2.9997000396711506" font-size="5.25" y="0">10</tspan></text> <text fill="#44484b" font-family="OpenSans, Open Sans" font-size="9" id="Elevated11" letter-spacing="-0.01em" transform="translate(29.808 146.53)"><tspan x="-20.409" y="0">Elevated</tspan><tspan baseline-shift="2.9997000396711506" font-size="5.25" y="0">11</tspan></text> <text data-name="Elevated 33x13" fill="#44484b" font-family="OpenSans, Open Sans" font-size="9" id="Elevated_33x13" letter-spacing="-0.01em" transform="translate(29.809 194.999)"><tspan x="-28.901" y="0">Elevated 33x</tspan><tspan baseline-shift="2.9997000396711506" font-size="5.25" y="0">10</tspan></text> <text fill="#44484b" font-family="OpenSans, Open Sans" font-size="9" id="Elevated12" letter-spacing="-0.01em" transform="translate(29.808 239.141)"><tspan x="-20.409" y="0">Elevated</tspan><tspan baseline-shift="2.9997000396711506" font-size="5.25" y="0">10</tspan></text> </g><g data-name="Group 407" id="Group_407" transform="translate(157.304 17.346)"> <text data-name="Blocked by" fill="#fff" font-family="AcuminPro-Bold, Acumin Pro" font-size="10" font-weight="700" id="Blocked_by" letter-spacing="-0.01em" transform="translate(25 10)"><tspan x="-25.225" y="0">Blocked by</tspan></text> <text fill="#44484b" font-family="AcuminPro-Italic, Acumin Pro" font-size="9" font-style="normal" id="SILIQ7" letter-spacing="-0.01em" transform="translate(25 55.993)"><tspan x="-11.707" y="0">SILIQ</tspan><tspan baseline-shift="2.9997000396711506" font-family="AcuminPro-Regular, Acumin Pro" font-size="5.25" font-style="normal" y="0">1</tspan></text> <text data-name="SILIQ7" fill="#44484b" font-family="AcuminPro-Italic, Acumin Pro" font-size="9" font-style="normal" id="SILIQ7-2" letter-spacing="-0.01em" transform="translate(25 103.81)"><tspan x="-11.707" y="0">SILIQ</tspan><tspan baseline-shift="2.9997000396711506" font-family="AcuminPro-Regular, Acumin Pro" font-size="5.25" font-style="normal" y="0">1</tspan></text> <text data-name="SILIQ7" fill="#44484b" font-family="AcuminPro-Italic, Acumin Pro" font-size="9" font-style="normal" id="SILIQ7-3" letter-spacing="-0.01em" transform="translate(25 146.617)"><tspan x="-11.707" y="0">SILIQ</tspan><tspan baseline-shift="2.9997000396711506" font-family="AcuminPro-Regular, Acumin Pro" font-size="5.25" font-style="normal" y="0">1</tspan></text> <text data-name="SILIQ7" fill="#44484b" font-family="AcuminPro-Italic, Acumin Pro" font-size="9" font-style="normal" id="SILIQ7-4" letter-spacing="-0.01em" transform="translate(25 194.999)"><tspan x="-11.707" y="0">SILIQ</tspan><tspan baseline-shift="2.9997000396711506" font-family="AcuminPro-Regular, Acumin Pro" font-size="5.25" font-style="normal" y="0">1</tspan></text> <text data-name="SILIQ7" fill="#44484b" font-family="AcuminPro-Italic, Acumin Pro" font-size="9" font-style="normal" id="SILIQ7-5" letter-spacing="-0.01em" transform="translate(25 239.246)"><tspan x="-11.707" y="0">SILIQ</tspan><tspan baseline-shift="2.9997000396711506" font-family="AcuminPro-Regular, Acumin Pro" font-size="5.25" font-style="normal" y="0">1</tspan></text> </g><g data-name="Group 406" id="Group_406" transform="translate(243.831 14.345)"><text data-name="ReceptorComplex" fill="#fff" font-family="AcuminPro-Bold, Acumin Pro" font-size="10" font-weight="700" id="Receptor_Complex" letter-spacing="-0.01em" transform="translate(27.729 8)"><tspan x="-20.885" y="0">Receptor</tspan><tspan x="-20.09" y="12">Complex</tspan></text><text data-name="IL-17RA/RC12" fill="#44484b" font-family="AcuminPro-Regular, Acumin Pro" font-size="9" id="IL-17RA_RC12" letter-spacing="-0.01em" transform="translate(27 58.923)"><tspan x="-25.099" y="0">IL-17RA/RC</tspan><tspan baseline-shift="2.9997000396711506" font-size="5.25" y="0">12</tspan></text><text data-name="IL-17RA/RE12" fill="#44484b" font-family="AcuminPro-Regular, Acumin Pro" font-size="9" id="IL-17RA_RE12" letter-spacing="-0.01em" transform="translate(28.063 107.21)"><tspan x="-24.721" y="0">IL-17RA/RE</tspan><tspan baseline-shift="2.9997000396711506" font-size="5.25" y="0">12</tspan></text><text data-name="IL-17RA/RB14" fill="#44484b" font-family="AcuminPro-Regular, Acumin Pro" font-size="9" id="IL-17RA_RB14" letter-spacing="-0.01em" transform="translate(27.765 150.487)"><tspan x="-24.976" y="0">IL-17RA/RB</tspan><tspan baseline-shift="2.9997000396711506" font-size="5.25" y="0">13</tspan></text><text data-name="IL-17RA/RC12" fill="#44484b" font-family="AcuminPro-Regular, Acumin Pro" font-size="9" id="IL-17RA_RC12-2" letter-spacing="-0.01em" transform="translate(27 198.001)"><tspan x="-25.099" y="0">IL-17RA/RC</tspan><tspan baseline-shift="2.9997000396711506" font-size="5.25" y="0">12</tspan></text><text data-name="IL-17RA/RC12" fill="#44484b" font-family="AcuminPro-Regular, Acumin Pro" font-size="9" id="IL-17RA_RC12-3" letter-spacing="-0.01em" transform="translate(27 242.055)"><tspan x="-25.099" y="0">IL-17RA/RC</tspan><tspan baseline-shift="2.9997000396711506" font-size="5.25" y="0">12</tspan></text></g></g></svg >'))
    }));
    $(".external-link").on("click", (function (t) {
        t.preventDefault();
        var a = $(this).attr("href");
        $("#externalSite .externalLink").attr("href", a),
            $("#externalSite").modal("show")
    }
    ));
    $("#externalSite .externalLink").on("click", (function () {
        $("#externalSite").modal("hide")
    }
    ));
    $(".indications-section .external-link").click(function () {
        $("#externalSite").modal('show');
    });
    $("#sticky-isi-module .external-link").click(function () {
        $("#externalSite").modal('show');
    });
    $(".header-top-links .reference").click(function () {
        $("#all-references").modal('show');
    });
    $("#aboutStudyDesign").on("click", (function (t) {
        t.preventDefault(),
            $("#studyDesign").modal("show")
    }
    ));
    $("#stdy-frur").on("click", (function (t) {
        t.preventDefault(),
            $("#studyDsgnfour").modal("show")
    }
    ));

    $("#mob-stdy-frur").on("click", (function (t) {
        t.preventDefault(),
            $("#studyDsgnfour").modal("show")
    }
    ));

    $("#stdy-five").on("click", (function (t) {
        t.preventDefault(),
            $("#studyDesignfive").modal("show")
    }
    ));

    $(".stdy-onetothre").on("click", (function (t) {
        t.preventDefault(),
            $("#stdyDesgnoneetothr").modal("show")
    }
    ));

    $(".mob-stdy-onetoth").on("click", (function (t) {
        t.preventDefault(),
            $("#stdyDesgnoneetothr").modal("show")
    }
    ));

    $("#mob-stdy-five").on("click", (function (t) {
        t.preventDefault(),
            $("#studyDesignfive").modal("show")
    }
    ));

    $("#stdy-sixr").on("click", (function (t) {
        t.preventDefault(),
            $("#stdyDsgnsxers").modal("show")
    }
    ));
    $("#mob-stdy-sixr").on("click", (function (t) {
        t.preventDefault(),
            $("#stdyDsgnsxers").modal("show")
    }
    ));

    $(".navbar  ul.navbar-nav li.submenu-7 > ul.subnav > li:last-child a").on("click", (function (t) {
        t.preventDefault(),
            $("#siliqRems").modal("show")
    }
    ));

    $("#siliqRems .externalLink").on("click", (function () {
        $("#siliqRems").modal("hide")
    }
    ));
    $("#siliqRemsMain .externalLink").on("click", (function () {
        $("#siliqRemsMain").modal("hide")
    }
    ));
    $(".header-top-links .leavesite").click(function () {
        $("#leavingSite").modal('show');
    });
    $(".close-study-pop .btn-default").click(function () {
        $("#all-references").modal('hide');
    });
    /* Script for slick slider for befor and after */
    $('.slider-for').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        dots: true,
        focusOnSelect: true
    });
    $('.slider-nav').slick({
        slidesToShow: 2,
        slidesToScroll: 2,
        dots: true,
        focusOnSelect: true
    });
    /* Script for how siliq works slider images */
    $('.slider-works').not('.slick-initialized').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        dots: true,
        focusOnSelect: true,
        arrows: false
    });
    $('.slider-nav').not('.slick-initialized').slick({
        slidesToShow: 2,
        slidesToScroll: 2,
        dots: true,
        focusOnSelect: true
    });
    $('.slider-works .slick-prev, .slider-works .slick-next').hide();
    /* Script for video */
    $('a[data-slide]').click(function (e) {
        e.preventDefault();
        var slideno = $(this).data('slide');
        $('.slider-nav').slick('slickGoTo', slideno - 1);
    });

    $(".open-video").on("click", function () {
        var e = $(this).attr("data-video")
            , t = Wistia.api($(this).data("video"));
        $(".video_popup[data-id='" + e + "']").show(),
            t.play()
        $('body').addClass("siliq-hcp-scroll");
    }),
        $(".videoclosebutton").on("click", function () {
            var e = Wistia.api($(this).data("video"));
            e.bind("timechange", function () {
                return e.time("0s"),
                    e.unbind
            }),
                $(".video_popup").hide()

            $('body').removeClass("siliq-hcp-scroll");
        });
    $(window).scroll(function () {
        if ($(window).scrollTop() < 0) {
            $("header").css("top", "0px");
        }
        else {
            $("header").css("top", "-100px");
        }
    });
    /* overwrite the href value in block button */
    $('.blk-para a[data-href]').each(function () {
        var href = $(this).data('href');
        $(this).attr('href', href);
    });
    /* Adding alt tag in block images */
    $(".find-a-doctor-sec .blk-container .blk-img > img").attr("alt", "SILIQ certified dermatologist");
    $(".gray-back .blk-container .blk-img > img").attr("alt", "Psoriasis patient photos: 92% BSA before vs completely clear at Week 12 with SILIQ");
    $(".graybgalternate .blk-container .blk-img > img").attr("alt", "Psoriasis patients with clear skin biking");
    $(".ContentPaneone .blk-container .blk-img > img").attr("alt", "Psoriasis patient discusses SILIQ treatment with dermatologist");
    /* Ends here */

    /* Append p tag inside the block image div */
    $(".ContentPaneone .blk-container .blk-img").append("<p class='blk-img-text'>Not an actual patient.</p>");
    $(".graybgalternate .blk-container .blk-img").append("<p class='blk-img-text'>Not actual patients.</p>");
    /* Ends here */
})
$('<img class="find-a-doctor-back-img d-none d-lg-block" src="/siteassets/hcp/img/img-802x181-find-a-doctor.webp" alt="SILIQ certified dermatologist" data-cmp-info="10">').insertAfter('.find-a-doctor-sec .blk-img');
$(function () {
    $('.lazy').lazy({
        effect: "fadeIn",
        effectTime: 1e3,
        threshold: 0
    });
});

$(".jvsvoid").attr("href", "javascript:void(0)");

$("li.submenu")[0].classList.add("submenu-1");
$("li.submenu")[1].classList.add("submenu-2");
$("li.submenu")[2].classList.add("submenu-3");
$("li.submenu")[3].classList.add("submenu-4");
$("li.submenu")[4].classList.add("submenu-5");
$("li.submenu")[5].classList.add("submenu-6");
$("li.submenu")[6].classList.add("submenu-7");
$("li.submenu")[6].classList.add("has-subnav");

$(function () {
    // this will get the full URL at the address bar
    var url = window.location.href;
    // passes on every "a" tag
    $("li.submenu a").each(function () {
        // checks if its the same on the address bar
        if (url == (this.href)) {
            $(this).closest("li").addClass("btns-active");
            //for making parent of submenu active
            $(this).closest("li").parent().parent().addClass("btns-active");
        }
    });
});
$(document).ready(function () {
    var loc = window.location.href;
    if (loc.includes('efficacy')) {
        $("header .navbar .navbar-nav .submenu-1").addClass("btns-active");
    }
    if (loc.includes('real-success')) {
        $("header .navbar .navbar-nav .submenu-2").addClass("btns-active");
    }
    if (loc.includes('how-siliq-works')) {
        $("header .navbar .navbar-nav .submenu-3").addClass("btns-active");
    }
    if (loc.includes('safety-sec')) {
        $("header .navbar .navbar-nav .submenu-4").addClass("btns-active");
    }
    if (loc.includes('patient-savings')) {
        $("header .navbar .navbar-nav .submenu-5").addClass("btns-active");
    }
    if (loc.includes('resources-sec')) {
        $("header .navbar .navbar-nav .submenu-6").addClass("btns-active");
    }
});