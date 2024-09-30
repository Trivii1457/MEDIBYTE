(function() {

    function addButton(text, hint, url) {
        if(parent && parent.Site) {
            var func = parent.Site.addButtonToNavLinksArea;
            if(func)
                func(text, hint, url);
            return;
        }

        $(function() {
            var demoWrapper = $(".demo-wrapper");
            if(!demoWrapper.length)
                return;

            $("<div id=demoshell-report-designer-button></div>")
                .appendTo(demoWrapper)
                .dxButton({
                    text: text,
                    hint: hint,
                    type: "default",
                    onClick: function() {
                        location = url;
                    }
                });
        });
    }

    function addDesignerButton(url) {
        addButton("REPORT DESIGNER", "Customize the report on the client side using the in-browser Report Designer", url);
    }

    function addViewerButton(url) {
        addButton("VIEWER", "Show WebDocument Viewer", url);
    }

    function appendQueryString(url, bag) {
        return url + (url.indexOf("?") > -1 ? "&" : "?") + $.param(bag);
    }

    window.addReportDesignerButton = function(reportID, designerUrl) {
        addDesignerButton(appendQueryString(designerUrl, {
            reportID: reportID,
            redirectUrl: location.href
        }));
    };

    window.addReportDesignerViewerToggleButton = function(demoUrl) {
        var isDesigner = location.search.indexOf("ShowDesigner") > -1;

        if(isDesigner) {
            addViewerButton(demoUrl);
        } else {
            addDesignerButton(appendQueryString(demoUrl, { ShowDesigner: 1 }));
        }
    };

})();
