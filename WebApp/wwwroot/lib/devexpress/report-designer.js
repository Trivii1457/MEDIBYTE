(function() {
    function goToViewer() {
        var redirectUrl = $("#redirectValue").val();
        if(redirectUrl.length > 0)
            window.location.replace(redirectUrl);
    }
    window.goToViewer = goToViewer;
})();
