/* ======================== LIB ParticlesJS  STARTs ======================== */
particlesJS("particles-js", {
  particles: {
    number: {
      value: 88,
      density: {
        enable: true,
        value_area: 700,
      },
    },
    color: {
      value: ["#aa73ff", "#f8c210", "#83d238", "#33b1f8"],
    },
    shape: {
      type: "circle",
      stroke: {
        width: 0,
        color: "#000000",
      },
      polygon: {
        nb_sides: 10,
      },
    },
    opacity: {
      value: 0.85,
      random: false,
      anim: {
        enable: false,
        speed: 1.5,
        opacity_min: 0.15,
        sync: false,
      },
    },
    size: {
      value: 2.5,
      random: false,
      anim: {
        enable: true,
        speed: 2,
        size_min: 0.15,
        sync: false,
      },
    },
    line_linked: {
      enable: true,
      distance: 110,
      color: "#33b1f8",
      opacity: 0.25,
      width: 1,
    },
    move: {
      enable: true,
      speed: 1.6,
      direction: "none",
      random: false,
      straight: false,
      out_mode: "out",
      bounce: false,
      attract: {
        enable: false,
        rotateX: 600,
        rotateY: 1200,
      },
    },
  },
  interactivity: {
    detect_on: "canvas",
    events: {
      onhover: {
        enable: false,
        mode: "repulse",
      },
      onclick: {
        enable: true,
        mode: "push",
      },
      resize: true,
    },
    modes: {
      grab: {
        distance: 400,
        line_linked: {
          opacity: 1,
        },
      },
      bubble: {
        distance: 400,
        size: 40,
        duration: 2,
        opacity: 8,
        speed: 3,
      },
      repulse: {
        distance: 200,
        duration: 0.4,
      },
      push: {
        particles_nb: 4,
      },
      remove: {
        particles_nb: 2,
      },
    },
  },
  retina_detect: true,
}); 
/* ======================== LIB ParticlesJS  ENDs ======================== */

/* ======================== LIB OwlCarousel  START ======================== */
///////////// Main Slider /////////////
// Size the header
let myHeader = $("#head");
let myslider = $("#main-slider");

myslider.height($($(window)).height() - myHeader.height());
let res = $($(window)).height() - myHeader.height();

$("#main-slider .owl-carousel").owlCarousel({
      animateOut: 'fadeOut',
      animateIn: 'fadeIn',
			loop:true,
			margin:0,
			nav:true,
			singleItem:true,
			smartSpeed: 1000,
			autoplay: 2000,
			autoplayTimeout:10000,
			navText: [ '<span class="fas fa-angle-left"></span>', '<span class="fas fa-angle-right"></span>' ],
			responsive:{
				0:{
					items: 1
				},
				600:{
					items: 1
				},
				1024:{
					items: 1
				}
  },
});

///////////// AboutUs / Testimonial Slider /////////////
$("#team .owl-carousel").owlCarousel({
  loop: true,
  animateOut: "fadeOut",
  animateIn: "fadeIn",
  autoPlay: 3000,
  smartSpeed: 1000,
  autoplay: 2000,
  stopOnHover: true,
  navigation: true,
  nav:false,
  dots:false,
  paginationSpeed: 1000,
  autoHeight: true,
  responsive: {
    0: {
      items: 1,
    },
    700: {
      items: 2,
    },
    992: {
      items: 3,
    },
  },
});

///////////// AboutUs / Testimonial Slider /////////////
$("#testimonials .owl-carousel").owlCarousel({
  loop: true,
  animateOut: "fadeOut",
  animateIn: "fadeIn",
  autoPlay: 3000,
  smartSpeed: 1000,
  autoplay: 2000,
  stopOnHover: true,
  navigation: true,
  paginationSpeed: 1000,
  autoHeight: true,
  // nav: false,
  // dots: false,
  // navText: [
  //   '<span class="fas fa-angle-left"></span>',
  //   '<span class="fas fa-angle-right"></span>',
  // ],
  responsive: {
    0: {
      items: 1,
    },
    700: {
      items: 2,
    },
    992: {
      items: 3,
    },
  },
});

/* ======================== LIB OwlCarousel ENDs ======================== */

/* ======================== LIB Isotope STARTs ======================== */
const sortableMasonry = (_) => {
  if ($(".sortable-masonry").length) {
    // Needed variables
    let container = $(".sortable-masonry .items-container");
    let filter = $(".filter-btns");

    container.isotope({
      filter: "*",
      masonry: {
        columnWidth: ".masonry-item.small-block",
      },
      animationOptions: {
        duration: 500,
        easing: "linear",
      },
    });

    // Isotope Filter
    filter.find("li").on("click", function () {
      let selector = $(this).attr("data-filter");
      try {
        container.isotope({
          filter: selector,
          animationOptions: {
            duration: 500,
            easing: "linear",
            queue: false,
          },
        });
      } catch (err) {}
      return false;
    });

    // Add Class Active To Active Filter
    let filterItemA = $(".filter-btns li");
    filterItemA.on("click", function () {
      let $this = $(this);
      if (!$this.hasClass("active")) {
        filterItemA.removeClass("active");
        $this.addClass("active");
      }
    });

    // Fix The size Of Filters
    $(window).on("resize", function () {
      if ($(this).width() <= Number(768)) {

        $(".filter-btns li").each(function () {
          let $this = $(this);
          if ($this.hasClass("active")) {
            $this.click();
            setTimeout(function () {
              $this.click();
            }, 1000);
            setTimeout(function () {
              $this.click();
            }, 2000);
          }
        });
      }
    });
  } // End If
}; // End Function

// Fire Isotope Function
sortableMasonry();
/* ======================== LIB Isotope ENDs ======================== */
