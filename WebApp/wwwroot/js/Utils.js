
/* Funcionalidad para calcular el digito de verificacion desde un nit*/
function CalcularDigitoVerificacion(myNit) {
    try {
        var vpri,
            x,
            y,
            z;

        // Se limpia el Nit
        myNit = myNit.replace(/\s/g, ""); // Espacios
        myNit = myNit.replace(/,/g, ""); // Comas
        myNit = myNit.replace(/\./g, ""); // Puntos
        myNit = myNit.replace(/-/g, ""); // Guiones

        // Se valida el nit
        if (isNaN(myNit)) {
            console.log("El nit/cédula '" + myNit + "' no es válido(a).");
            return "0";
        };

        // Procedimiento
        vpri = new Array(16);
        z = myNit.length;

        vpri[1] = 3;
        vpri[2] = 7;
        vpri[3] = 13;
        vpri[4] = 17;
        vpri[5] = 19;
        vpri[6] = 23;
        vpri[7] = 29;
        vpri[8] = 37;
        vpri[9] = 41;
        vpri[10] = 43;
        vpri[11] = 47;
        vpri[12] = 53;
        vpri[13] = 59;
        vpri[14] = 67;
        vpri[15] = 71;

        x = 0;
        y = 0;
        for (var i = 0; i < z; i++) {
            y = (myNit.substr(i, 1));
            // console.log ( y + "x" + vpri[z-i] + ":" ) ;

            x += (y * vpri[z - i]);
            // console.log ( x ) ;
        }

        y = x % 11;
        // console.log ( y ) ;

        return (y > 1) ? 11 - y : y;
    } catch (e) {
        return "0";
    }
    
}

function DiasEntreFechas(f1, f2) {

    var day_as_milliseconds = 86400000;
    var diff_in_millisenconds = f2 - f1;
    var diff_in_days = diff_in_millisenconds / day_as_milliseconds;
    return Math.trunc(diff_in_days);
}

/* Funcionalidad para calcular la edad desde una fecha*/
function ObtenerEdadCompleta(fromDate, toDate) {

    if (fromDate == null || fromDate == undefined)
        return { edad: 0, meses: 0, dias: 0, textoEdad: '' };

    if (!(fromDate instanceof Date)) {
        fromDate = new Date(fromDate);
    }

    var fecha_hoy = new Date();
    if (toDate != undefined) {
        if (!(toDate instanceof Date)) {
            toDate = new Date(toDate);
        }
        fecha_hoy = toDate;
    }

    var a = moment(fecha_hoy);
    var b = moment(fromDate);

    var years = a.diff(b, 'year');
    b.add(years, 'years');

    var months = a.diff(b, 'months');
    b.add(months, 'months');

    var days = a.diff(b, 'days');


    var textEdad = "";
    if (years <= 0)
        textEdad = "";
    else if (years == 1)
        textEdad = years + " año";
    else
        textEdad = years + " años";

    var textMes = "";
    if (months <= 0)
        textMes = "";
    else if (months == 1)
        textMes = months + " mes";
    else
        textMes = months + " meses";

    var textDias = "";
    if (days <= 0)
        textDias = "";
    else if (days == 1)
        textDias = days + " día";
    else
        textDias = days + " días";

    var textoTotal = textEdad + " " + textMes + " " + textDias;

    var result = { edad: years, meses: months, dias: days, textoEdad: textoTotal };
    return result;

}


/* Funcionalidad para realizar pantalla completa del navegador*/
var isFullscreen = false;
function CambiarPantallaCompleta() {
    var elem = document.documentElement;

    if (!isFullscreen) {
        if (elem.requestFullscreen) {
            elem.requestFullscreen();
        } else if (elem.mozRequestFullScreen) { /* Firefox */
            elem.mozRequestFullScreen();
        } else if (elem.webkitRequestFullscreen) { /* Chrome, Safari & Opera */
            elem.webkitRequestFullscreen();
        } else if (elem.msRequestFullscreen) { /* IE/Edge */
            elem.msRequestFullscreen();
        }
        isFullscreen = !isFullscreen;
    } else {
        if (document.exitFullscreen) {
            document.exitFullscreen();
        } else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen();
        } else if (document.webkitExitFullscreen) {
            document.webkitExitFullscreen();
        } else if (document.msExitFullscreen) {
            document.msExitFullscreen();
        }
        isFullscreen = !isFullscreen;
    }
}

function BorrarStorageGrids() {
    var objstorage = Object.keys(localStorage).filter(key => key.startsWith('SiisoGridStorage'));
    for (var i = 0; i < objstorage.length; i++) {
        localStorage.removeItem(objstorage[i]);
    }
}

function AgregarDiasAFecha(fecha, nroDias) {
    var result = new Date(fecha);
    result.setDate(result.getDate() + (nroDias - 1));
    return result;
}
