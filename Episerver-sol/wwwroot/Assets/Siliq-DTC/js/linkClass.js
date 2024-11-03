$(document).ready(function(){ 
$(".nav-link:nth-child(2) a").attr({ "href": "https://www.arestinprofessional.com/", class: "thirdParty" });
$("div.navbar-nav").append(
	'<div class="socialLinkMobile"><a href="https://www.youtube.com/@ARESTIN_us" title="Youtube" class="thirdParty"> <img src="/siteassets/img/icon-36x36-youtube.svg" alt="Youtube Icon"> </a> <a href="https://www.facebook.com/ARESTIN.usa" title="Facebook" class="thirdParty"> <img src="/siteassets/img/icon-36x36-facebook.svg" alt="Facebook Icon"> </a> <a href="https://www.instagram.com/arestin.us/?hl=en" title="Instagram" class="thirdParty"> <img src="/siteassets/img/icon-36x36-insta.svg" alt="Instagram Icon"> </a> </div>'
)
})