/* ======================== BACK Pre-Load START ======================== */
// Remove preloader
$(window).on("load", () => {
  this.setTimeout((_) => {
    $("#loader-wrapper").fadeOut();
  }, 2000);
});
/* ======================== BACK Pre-Load ENDs ======================== */

/* ======================== Scroll STARTs ======================== */

/* ======================== Scroll ENDs ======================== */

/* ======================== BACK Header START ======================== */
$(window).on("scroll", function () {
  let infoHeader = $("#head");
  let currentScroll = $(window).scrollTop();
  let conatctInfoHeader = $("#head #contact-info");
  if (currentScroll != 0) {
    conatctInfoHeader.slideUp(250);
    // infoHeader.addClass("fixed");
  } else {
    conatctInfoHeader.slideDown(250);
    // infoHeader.removeClass("fixed");
  }
});
/* ======================== BACK Header ENDs ======================== */

/* ======================== BACK NavIcon START ======================== */
let mobileMenuIsActive = false;
let stopScrolling = (state) => {
  switch (state) {
    case "toggle":
      $("body").toggleClass("stop-scrolling");
      break;
    case "remove":
      $("body").toggleClass("stop-scrolling");
      break;
    case "add":
      $("body").addClass("stop-scrolling");
      break;
  }
};

$("#show-menu-icon").on("click", function () {
  $("#mobile-menu")
    .addClass("visible")
    .removeClass("invisible")
    .css({ transform: "translateX(0%)" });
  $("#hide-menu-icon").fadeIn().css({
    "-webkit-transform": "rotate(-180deg)",
    "-ms-transform": "rotate(-180deg)",
    transition: " all .5s ease",
    transform: "rotate(360deg)",
  });
  stopScrolling("toggle");
  mobileMenuIsActive = true;
});

$("#hide-menu-icon").on("click", function () {
  $("#mobile-menu")
    .removeClass("visible")
    .addClass("invisible")
    .css({ transform: "translateX(101%)" });
  $(this).fadeOut(300).css({
    "-webkit-transform": "rotate(0deg)",
    "-ms-transform": "rotate(0deg)",
    transition: " all .5s ease",
  });

  $("#mobile-menu .navbar-container .link").removeClass("open current");
  $("#mobile-menu .navbar-container .link .arrow").removeClass("rotate");
  $("#mobile-menu .navbar-container .item .mobile-dropdown-menu").slideUp();

  stopScrolling("remove");
  mobileMenuIsActive = false;
});

$("#mobile-menu li a").on("click", function () {
  $("#hide-menu-icon").click();
});
/* ======================== BACK NavIcon ENDs ======================== */

/* ======================== BACK Mobile Navbar START ======================== */
let arrowMenuShow = $(
  "#mobile-menu .menu-container .navbar-container .link .arrow"
);
// $('#mobile-menu .menu-container .navbar-container .link .arrow').on('click', function() {
arrowMenuShow.on("click", function () {
  $(this).parent().toggleClass("current");
  $(this).toggleClass("rotate");
  $(this).parent().toggleClass("open");
  $(this).parent().next().children().slideToggle(500);
  arrowMenuShow.not(this).each(function () {
    $(this).parent().removeClass("current");
    $(this).removeClass("rotate");
    $(this).parent().removeClass("open");
    $(this).parent().next().children().slideUp(500);
  });
});

/* ======================== BACK Mobile Navbar ENDs ======================== */

/* ======================== PART Sign In Page START  ======================== */
$("#to-login-btn").on("click", function () {
  backToTopBtn.click();
  LoginForm.fadeIn();
  $("body").toggleClass("stop-scrolling");
});
/* ======================== PART Sign In Page END  ======================== */

/* ======================== BACK TO TP PART START ======================== */
let backToTopBtn = $("#back-to-top-container");
$(window).on("scroll", function () {
  let windowHeight = $(window).height();
  let currentScroll = $(window).scrollTop();
  if (currentScroll > windowHeight / 2) {
    backToTopBtn.css({ visibility: "visible" });
  } else {
    backToTopBtn.css({ visibility: "hidden" });
  }
});
backToTopBtn.on("click", function () {
  $("html, body").animate({ scrollTop: 0 }, "1000");
});
/* ======================== BACK TO TP PART ENDs ======================== */

/* ======================== Inscreption Page STARTs ======================== */
// Kid Photo
  $("#KidPhoto").on("change", function () {
      let inputFile = $(this)[0].files;
      if (inputFile.length == 1) {
          $(this).next().html(inputFile[0].name);
      } else {
          $(this).next().html(`${inputFile.length} files selected..`);
      }
  })
  // Guardian Cin Copy
  $("#CinCopy").on("change", function () {
      let inputFile = $(this)[0].files;
      if (inputFile.length == 1) {
          $(this).next().html(inputFile[0].name);
      } else {
          $(this).next().html(`${inputFile.length} files selected..`);
      }
  })
  // Guardian Photo
  $("#GuardianPhoto").on("change", function () {
      let inputFile = $(this)[0].files;
      if (inputFile.length == 1) {
          $(this).next().html(inputFile[0].name);
      } else {
          $(this).next().html(`${inputFile.length} files selected..`);
      }
  })
/* ======================== Inscreption Page ENDs ======================== */



/* ======================== BACK  START ======================== */
/* ======================== BACK  ENDs ======================== */
