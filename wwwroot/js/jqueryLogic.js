document.ready = $(() => {
    $(".app-hamburger").click((e) => {
        $(".app-sidebar").toggleClass("full-width");
    });
});