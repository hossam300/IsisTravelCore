"use strict";

// Show tab click event
const showTab = (event) => {
  event.preventDefault();

  // Get tabs and content
  const tabs = document.querySelectorAll(".tabs .active");

  // Remove active class from items
  tabs.forEach((tab) => {
    tab.classList.remove("active");
  });

  // Add active class to clicked item
  event.target.parentElement.classList.add("active");
  document
    .getElementById(event.target.href.split("#")[1])
    .classList.add("active");
};

// Add event listener to all tab links
const tabLinks = document.querySelectorAll(".tab-list a");
tabLinks.forEach((link) => {
  link.addEventListener("click", showTab);
});

jQuery(window).scroll(function () {
  if (jQuery(window).width() >= 992) {
    if (jQuery(window).scrollTop() > 200) {
      jQuery(".bg-light").css("background", "#fff");

      jQuery(".bg-light").addClass("animated-nav");
    } else {
      jQuery(".bg-light ").css(
        "background",
        " linear-gradient(-90deg, #4e8226a6,#4e8226a6)"
      );
      jQuery(".bg-light").removeClass("animated-nav");
    }

    if (jQuery(window).scrollTop() > 200) {
      jQuery(".navbar-nav .nav-link").css("color", "#909090");

      jQuery(".navbar-nav .nav-link").addClass("animated-nav");
    } else {
      jQuery(".navbar-nav .nav-link ").css("color", "#fff");
      jQuery(".navbar-nav .nav-link").removeClass("animated-nav");
    }

    if (jQuery(window).scrollTop() > 200) {
      jQuery(".navbar-light .navbar-nav .active>.nav-link").css(
        "color",
        "rgb(146, 128, 85)"
      );

      jQuery(".navbar-light .navbar-nav .active>.nav-link").addClass(
        "animated-nav"
      );
    } else {
      jQuery(".navbar-light .navbar-nav .active>.nav-link ").css(
        "color",
        "#faffc6"
      );
      jQuery(".navbar-light .navbar-nav .active>.nav-link").removeClass(
        "animated-nav"
      );
    }

    if (jQuery(window).scrollTop() > 200) {
      jQuery(".logo-navbar").css("opacity", "1");

      jQuery(".logo-navbar").addClass("animated-nav");
    } else {
      jQuery(".logo-navbar").css("opacity", "0");
      jQuery(".logo-navbar").removeClass("animated-nav");
    }

    if (jQuery(window).scrollTop() > 200) {
      jQuery(".titile-logo").css("opacity", "0");

      jQuery(".titile-logo").addClass("animated-nav");
    } else {
      jQuery(".titile-logo").css("opacity", "1");
      jQuery(".titile-logo").removeClass("animated-nav");
    }
  }
});

// if (jQuery(window).width() < 992) {
//   jQuery(".carousel").carousel({
//     keyboard: false,
//   });
// }
