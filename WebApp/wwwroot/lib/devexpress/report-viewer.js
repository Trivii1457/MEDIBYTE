(function() {
    function disposeOnUnload(sender, arsg) {
        $(window).on('beforeunload', function(e) {
            var viewer = sender.GetPreviewModel();
            setTimeout(function() {
                viewer && viewer.Close && viewer.Close();
            }, 1);
        });
    }

    function registerNationalityEditor(contentPath) {
        DevExpress.Reporting.Editing.EditingFieldExtensions.registerImageEditor({
            name: "Nationality",
            displayName: "Nationality",
            searchEnabled: true,
            images: [
                { url: contentPath + "Flags/Australia.png", text: "Australia" },
                { url: contentPath + "Flags/China.png", text: "China" },
                { url: contentPath + "Flags/France.png", text: "France" },
                { url: contentPath + "Flags/Germany.png", text: "Germany" },
                { url: contentPath + "Flags/India.png", text: "India" },
                { url: contentPath + "Flags/Italy.png", text: "Italy" },
                { url: contentPath + "Flags/Japan.png", text: "Japan" },
                { url: contentPath + "Flags/Russia.png", text: "Russia" },
                { url: contentPath + "Flags/United_Kingdom.png", text: "United Kingdom" },
                { url: contentPath + "Flags/United_States_of_America.png", text: "United States of America" }
            ]
        });
    }
    window.disposeOnUnload = disposeOnUnload;
    window.registerNationalityEditor = registerNationalityEditor;
})();



