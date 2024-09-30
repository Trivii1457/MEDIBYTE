// namespaces
var dwv = dwv || {};
dwv.io = dwv.io || {};

/**
 * Raw image loader.
 * @constructor
 */
dwv.io.RawImageLoader = function ()
{
    // closure to self
    var self = this;

    /**
     * if abort is triggered, all image.onload callbacks have to be cancelled
     * @type {boolean}
     * @private
     */
    var aborted = false;

    /**
     * Set the loader options.
     * @param {Object} opt The input options.
     */
    this.setOptions = function () {
        // does nothing
    };

    /**
     * Is the load ongoing? TODO...
     * @return {Boolean} True if loading.
     */
    this.isLoading = function () {
        return true;
    };

    /**
     * Create a Data URI from an HTTP request response.
     * @param {Object} response The HTTP request response.
     * @param {String} dataType The data type.
     * @private
     */
    function createDataUri(response, dataType) {
        // image type
        var imageType = dataType;
        if (!imageType || imageType === "jpg") {
            imageType = "jpeg";
        }
        // create uri
        var file = new Blob([response], {type: 'image/' + imageType});
        return window.URL.createObjectURL(file);
    }

    /**
     * Load data.
     * @param {Object} buffer The read data.
     * @param {String} origin The data origin.
     * @param {Number} index The data index.
     */
    this.load = function (buffer, origin, index) {
        aborted = false;
        // create a DOM image
        var image = new Image();
        // triggered by ctx.drawImage
        image.onload = function (/*event*/) {
            try {
                if (!aborted) {
                    self.onprogress({
                        lengthComputable: true,
                        loaded: 100,
                        total: 100,
                        index: index,
                        source: origin
                    });
                    self.onload(dwv.image.getViewFromDOMImage(this, origin));
                }
            } catch (error) {
                self.onerror({
                    error: error,
                    source: origin
                });
            } finally {
                self.onloadend({
                    source: origin
                });
            }
        };
        // storing values to pass them on
        image.origin = origin;
        image.index = index;
        if (typeof origin === "string") {
            // url case
            var ext = origin.split('.').pop().toLowerCase();
            image.src = createDataUri(buffer, ext);
        } else {
            image.src = buffer;
        }
    };

    /**
     * Abort load.
     */
    this.abort = function () {
        aborted = true;
        self.onabort({});
        self.onloadend({});
    };

}; // class RawImageLoader

/**
 * Check if the loader can load the provided file.
 * @param {Object} file The file to check.
 * @return True if the file can be loaded.
 */
dwv.io.RawImageLoader.prototype.canLoadFile = function (file) {
    return file.type.match("image.*");
};

/**
 * Check if the loader can load the provided url.
 * @param {String} url The url to check.
 * @return True if the url can be loaded.
 */
dwv.io.RawImageLoader.prototype.canLoadUrl = function (url) {
    var urlObjext = dwv.utils.getUrlFromUri(url);
    // extension
    var ext = dwv.utils.getFileExtension(urlObjext.pathname);
    var hasImageExt = (ext === "jpeg") || (ext === "jpg") ||
            (ext === "png") || (ext === "gif");
    // content type (for wado url)
    var contentType = urlObjext.searchParams.get("contentType");
    var hasContentType = contentType !== null &&
        typeof contentType !== "undefined";
    var hasImageContentType = (contentType === "image/jpeg") ||
        (contentType === "image/png") ||
        (contentType === "image/gif");

    return hasContentType ? hasImageContentType : hasImageExt;
};

/**
 * Get the file content type needed by the loader.
 * @return One of the 'dwv.io.fileContentTypes'.
 */
dwv.io.RawImageLoader.prototype.loadFileAs = function () {
    return dwv.io.fileContentTypes.DataURL;
};

/**
 * Get the url content type needed by the loader.
 * @return One of the 'dwv.io.urlContentTypes'.
 */
dwv.io.RawImageLoader.prototype.loadUrlAs = function () {
    return dwv.io.urlContentTypes.ArrayBuffer;
};

/**
 * Handle a load start event.
 * @param {Object} event The load start event.
 * Default does nothing.
 */
dwv.io.RawImageLoader.prototype.onloadstart = function (/*event*/) {};
/**
 * Handle a progress event.
 * @param {Object} event The progress event.
 * Default does nothing.
 */
dwv.io.RawImageLoader.prototype.onprogress = function (/*event*/) {};
/**
 * Handle a load event.
 * @param {Object} event The load event fired
 *   when a file has been loaded successfully.
 * Default does nothing.
 */
dwv.io.RawImageLoader.prototype.onload = function (/*event*/) {};
/**
 * Handle an load end event.
 * @param {Object} event The load end event fired
 *  when a file load has completed, successfully or not.
 * Default does nothing.
 */
dwv.io.RawImageLoader.prototype.onloadend = function (/*event*/) {};
/**
 * Handle an error event.
 * @param {Object} event The error event.
 * Default does nothing.
 */
dwv.io.RawImageLoader.prototype.onerror = function (/*event*/) {};
/**
 * Handle an abort event.
 * @param {Object} event The abort event.
 * Default does nothing.
 */
dwv.io.RawImageLoader.prototype.onabort = function (/*event*/) {};

/**
 * Add to Loader list.
 */
dwv.io.loaderList = dwv.io.loaderList || [];
dwv.io.loaderList.push( "RawImageLoader" );
