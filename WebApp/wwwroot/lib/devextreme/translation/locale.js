
"use strict";

class Translation {

    constructor(indicator, languages) {

        this.locales = languages;

        this.locale = setIndicator();

        $(document).ready(labelChange());

        function setIndicator() {

            var locale = getLocale(indicator);
            DevExpress.localization.locale(locale);
            return locale;
        }

        function labelChange() {

            var label;

            switch (getLocale(indicator)) {

                case "es":
                    label = "Spanish to English";
                    break;

                case "en":
                    label = "English to Spanish";
                    break;
            }

            $(".lblLang").html(label);

        }

        function getLocale(indicator) {
            var locale = sessionStorage.getItem("locale");
            return locale !== null ? locale : indicator;
        }

        function setLocale(locale) {
            sessionStorage.setItem("locale", locale);
        }

        this.changeLocale = function (data) {
            console.log(data);
            setLocale(data.value);
            document.location.reload();
            //document.location.replace("/");
        }

        this.setShiftLocale = function () {

            var shift;

            switch (getLocale(indicator)) {
                case "es":
                    shift = "en";
                    break;

                case "en":
                    shift = "es";
                    break;
            }

            this.changeLocale({ value: shift });
        }
    }

}