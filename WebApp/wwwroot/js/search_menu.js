var dataMenuModules = [];

var inputSearch = $("#search-menu-input").keyup(filterMenu);
function filterMenu(event) {
    if (!(event.which == 13 || event.keyCode == 13)) {
        return;
    }

    var textSearch = inputSearch.val().toLowerCase();
    var resultado = document.querySelector("#sidebar-menu-container-options");
    var resultMenu = '';
    resultado.innerHTML = '';
    
    for (var module of dataMenuModules) {

        var optionsFinded = module.Options.findIndex(function (e) {
            return e.Resource.toLowerCase().indexOf(textSearch) !== -1;
        }); 
        if (optionsFinded !== -1) {
            resultMenu += `<li><a><i class="${module.Icon}"></i> ${module.Module} <span class="fa fa-chevron-down"></span></a><ul class="nav child_menu">`;
            for (var option of module.Options) {
                var textOption = option.Resource.toLowerCase();
                if (textOption.indexOf(textSearch) !== -1) {
                    resultMenu += `<li><a onclick="GetViewOnContainer('/${option.Name}/ListPartial')">${option.Resource}</a></li>`;
                }
            }
            resultMenu += '</ul></li>';
        }
    }
    resultado.innerHTML = resultMenu;
    setEventsMenu();

    if (textSearch !== "") {
        var $containerOptions = $('#sidebar-menu-container-options');
        var $firstModule = $('li:first', $containerOptions);
        $firstModule.addClass('active');
        $('ul:first', $firstModule).slideDown(function () {
            setContentHeight();
        });
    }

}


function setEventsMenu() {
    $SIDEBAR_MENU.find('a').on('click', function (ev) {

        var $li = $(this).parent();
        if ($li.is('.active')) {
            $li.removeClass('active active-sm');
            $('ul:first', $li).slideUp(function () {
                setContentHeight();
            });
        } else {
            // prevent closing menu if we are on child menu
            if (!$li.parent().is('.child_menu')) {
                $SIDEBAR_MENU.find('li').removeClass('active active-sm');
                $SIDEBAR_MENU.find('li ul').slideUp();
            } else {
                if ($BODY.is(".nav-sm")) {
                    $li.parent().find("li").removeClass("active active-sm");
                    $li.parent().find("li ul").slideUp();
                }
            }
            $li.addClass('active');

            $('ul:first', $li).slideDown(function () {
                setContentHeight();
            });
        }
    });
}

var setContentHeight = function () {
    // reset height
    $RIGHT_COL.css('min-height', $(window).height());

    var bodyHeight = $BODY.outerHeight(),
        footerHeight = $BODY.hasClass('footer_fixed') ? -10 : $FOOTER.height(),
        leftColHeight = $LEFT_COL.eq(1).height() + $SIDEBAR_FOOTER.height(),
        contentHeight = bodyHeight < leftColHeight ? leftColHeight : bodyHeight;

    // normalize content
    contentHeight -= $NAV_MENU.height() + footerHeight;

    $RIGHT_COL.css('min-height', contentHeight);
};

/*Agrega al Jquery funcion de contextmenu*/
(function ($, window) {

    $.fn.contextMenu = function (settings) {

        return this.each(function () {

            // Open context menu
            $(this).on("contextmenu", function (e) {
                // return native menu if pressing control
                if (e.ctrlKey) return;

                //open menu
                var $menu = $(settings.menuSelector)
                    .data("invokedOn", $(e.target))
                    .show()
                    .css({
                        position: "absolute",
                        left: getMenuPosition(e.clientX, 'width', 'scrollLeft'),
                        top: getMenuPosition(e.clientY, 'height', 'scrollTop')
                    })
                    .off('click')
                    .on('click', 'a', function (e) {
                        $menu.hide();

                        var $invokedOn = $menu.data("invokedOn");
                        var $selectedMenu = $(e.target);

                        settings.menuSelected.call(this, $invokedOn, $selectedMenu);
                    });

                return false;
            });

            //make sure menu closes on any click
            $('body').click(function () {
                $(settings.menuSelector).hide();
            });
        });

        function getMenuPosition(mouse, direction, scrollDir) {
            var win = $(window)[direction](),
                scroll = $(window)[scrollDir](),
                menu = $(settings.menuSelector)[direction](),
                position = mouse + scroll;

            // opening menu would pass the side of the page
            if (mouse + menu > win && menu < mouse)
                position -= menu;

            return position;
        }

    };
})(jQuery, window);

//Le agrega contextMenu a las opciones del menu
$(".menu-option").contextMenu({
    menuSelector: "#contextMenu",
    menuSelected: function (invokedOn, selectedMenu) {
        if (selectedMenu.attr("optionMenu") == "newTab")
            window.open("/" + invokedOn.attr("dataTargetOption") + "/List", '_blank');
    }
});
