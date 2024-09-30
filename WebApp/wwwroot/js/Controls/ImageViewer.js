(function (window) {

    // dependencias: jquery
    var $ = jQuery; // de manera explicita.

    function imageChanged(item, PrefixId) {
        var reader = new FileReader();
        reader.onload = function (e) {

            $('#' + PrefixId + 'ImageViewerImg').attr('src', e.target.result);
            //$('#' + PrefixId + 'ImageViewerInput').dxTextBox("instance").option("value", e.target.result.split("base64,")[1]);
            $('input#' + PrefixId + 'ImageViewerInput').val(e.target.result.split("base64,")[1]);

            $('#' + PrefixId + 'ImageViewerNombre').dxTextBox("instance").option("value", item.value.name);
            $('#' + PrefixId + 'ImageViewerTipoContenido').dxTextBox("instance").option("value", item.value.type);
        }
        reader.readAsDataURL(item.value);
    }

    function imageClick(PrefixId) {
        $("#" + PrefixId + "ImageViewer .dx-fileuploader-button").trigger("click");
    }

    function imageDelete(PrefixId, defaultImage) {
        $('#' + PrefixId + 'ImageViewerImg').attr('src', defaultImage);
        //$('#' + PrefixId + 'ImageViewerInput').dxTextBox("instance").option("value", "");
        $('input#' + PrefixId + 'ImageViewerInput').val("");

        $('#' + PrefixId + 'ImageViewerNombre').dxTextBox("instance").option("value", "");
        $('#' + PrefixId + 'ImageViewerTipoContenido').dxTextBox("instance").option("value", "");
    }

    function imageInitialized(item, PrefixId, name, value) {
        $("#" + item.element[0].id + " .dx-fileuploader-button").addClass("btn btn-sm btn-primary");
        $("#" + item.element[0].id + " .dx-fileuploader-button").html('<i class="dx-icon dx-icon-upload text-white" style="margin:0;"></i>');
        $("#" + PrefixId + "ImageViewer").append("<input id='" + PrefixId + "ImageViewerInput' type='hidden' name='" + name + "' value='" + value +"'>");
    }

    window.SE = Object.assign(window.SE || {}, {
        ImageViewer: {
            imageChanged: imageChanged,
            imageClick: imageClick,
            imageDelete: imageDelete,
            imageInitialized: imageInitialized


        }
    });

}(window));