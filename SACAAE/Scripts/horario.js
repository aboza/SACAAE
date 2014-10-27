$(function () {
    /* Deshabilitar componentes que no tienen datos cargados */
    $("#sltCurso").prop("disabled", "disabled");
    $("#sltGrupo").prop("disabled", "disabled");
    /* Funcion llamada cuando se cambien los valores de las sedes o las modalidades */
    $("#sltBloque").change(function () {
        var plan = getCookie("SelPlanDeEstudio");
        var route = "/BloqueXPlanXCurso/Cursos/List/" + plan + "/" + $('select[name="sltBloque"]').val();
        $.getJSON(route, function (data) {
            var items = "";
            $.each(data, function (i, curso) {
                items += "<option value= " + curso.Value + ">" + curso.Text + "</option>";
            });


            $("#sltCurso").prop("disabled", false);
            $("#sltGrupo").html("");
            $("#sltGrupo").prop("disabled", "disabled");
            $("#sltCurso").html(items);
            $("#sltCurso").prepend("<option value='' selected='selected'>-- Seleccionar Curso --</option>");

        });
    });
    $("#sltCurso").change(function () {
        var plan = getCookie("SelPlanDeEstudio");
        var route = "/CursoProfesor/Grupos/List/" + $('select[name="sltCurso"]').val() + "/" + plan + "/" + $('select[name="sltBloque"]').val() + "/" + getCookie("PeriodoHorario");

        $.getJSON(route, function (data) {
            var items = "";
            $.each(data, function (i, grupo) {
                items += "<option value='" + grupo.Value + "'>" + grupo.Text + "</option>";
            });


            $("#sltGrupo").prop("disabled", false);
            $("#sltGrupo").html(items);
            $("#sltGrupo").prepend("<option value='' selected='selected'>-- Seleccionar Grupo --</option>");

        });
    });

    /*$("#sltGrupo").change(function () {
        var route = "/CursoProfesor/Horarios/Info/" + $('select[name="sltGrupo"]').val();
        var horas = 0;
        $.getJSON(route, function (data) {
            //alert(data.toSource());
            for (var k = 0; k < data.length; k++) {
                var Curso = $('#sltCurso option:selected').text();
                var GrupoText = $('#sltGrupo option:selected').text();
                var Dia = data[k]["Dia1"];
                var HoraInicioCookie = data[k]["Hora_Inicio"];
                var HoraFinCookie = data[k]["Hora_Fin"];

                var Inicio = parseInt(HoraInicioCookie)
                var Fin = parseInt(HoraFinCookie)
                var i = Inicio
                //Actualizo la tabla con el nuevo curso
                var CantCeldas = 6;
                while (i < Fin) {

                    var IdCelda = Dia + " " + i;
                    if (i < 100) { IdCelda = Dia + " 0" + i; } //repara el string en caso de que la hora fuera 0010 ya que el parse la deja como 10
                    if (i == 0) { IdCelda = Dia + " 000"; } 
                    if (i == Inicio) {
                        var primera = document.getElementById(IdCelda);
                        }
                    i += 10;
                    if (i % 100 == 60) { i += 40; }//cuando se llega al minuto 60 se le suman 40 al numero para que pase por ejemplo de 1060 a 1100
                    CantCeldas++;
                }
                primera.innerHTML+="<p>" + Curso + " " + GrupoText + "</p>";
                primera.style.backgroundColor = "#3276b1";
                primera.rowSpan = CantCeldas.toString();
                var route2 = "/CursoProfesor/Grupos/Info/" + $('select[name="sltGrupo"]').val();
                $.ajax({
                    url: route2,
                    datatype: 'json',
                    async: false,
                    success: function (data) {
                        var cookie = getCookie("i");//i es el nombre de la cookie, es un contador de cursos pero en toda la sesion no local
                        var cantidad = 0;
                        if (cookie != "" && cookie != null && !isNaN(cookie)) {
                            cantidad = parseInt(cookie);
                        }
                        cantidad++;
                        setCookie("i", cantidad.toString(), 1);
                        setCookie("Cookie" + cantidad.toString(), Curso + "|" + Dia + "|" + HoraInicioCookie + "|" + HoraFinCookie + "|" + $('select[name="sltBloque"]').val() + "|" + $('select[name="sltGrupo"]').val() + "|" + data[0]["Aula"], 1);
                    }
                });
            }


        });
    });*/
});

function Cargar() {
    setCookie("i", 0, 1);
    setCookie("Cantidad", 0, 1);
    var route = "/getHorarios/List/" + getCookie("SelPlanDeEstudio") + "/" + getCookie("PeriodoHorario");
    $.getJSON(route, function (data) {
        $.each(data, function (i, Horario) {
            var Curso = Horario.Nombre;
            var GrupoText = Horario.Numero;
            var Dia = Horario.Dia1;
            var HoraInicioCookie = Horario.Hora_Inicio;
            var HoraFinCookie = Horario.Hora_Fin;

            var Inicio = parseInt(HoraInicioCookie)
            var Fin = parseInt(HoraFinCookie)
            var i = Inicio
            //Actualizo la tabla con el nuevo curso
            var CantCeldas = 0;
            var items = "";
            while (i < Fin + 60) {

                var IdCelda = Dia + " " + i;
                if (i < 100) { IdCelda = Dia + " 0" + i; } //repara el string en caso de que la hora fuera 0010 ya que el parse la deja como 10
                if (i == 0) { IdCelda = Dia + " 000"; }
                var celda = document.getElementById(IdCelda);
                if (i == Inicio) {
                    var primera = document.getElementById(IdCelda);
                }
                else if (celda != null) {
                    items += celda.innerHTML;
                    celda.parentNode.removeChild(celda);
                }
                i += 10;
                if (i % 100 == 60) { i += 40; }//cuando se llega al minuto 60 se le suman 40 al numero para que pase por ejemplo de 1060 a 1100
                CantCeldas++;
            }
            primera.innerHTML += items + "<p id=" + Dia + ">" + Curso + " " + GrupoText + "</p>";
            primera.style.backgroundColor = "#3276b1";
            primera.rowSpan = CantCeldas.toString();
            var cookie = getCookie("i");//i es el nombre de la cookie, es un contador de cursos pero en toda la sesion no local
            var cantidad = 0;
            if (cookie != "" && cookie != null && !isNaN(cookie)) {
                cantidad = parseInt(cookie);
            }
            cantidad++;
            setCookie("i", cantidad.toString(), 1);
            setCookie("Cookie" + cantidad.toString(), Curso + "|" + Dia + "|" + HoraInicioCookie + "|" + HoraFinCookie + "|" + "-" + "|" + Horario.ID + "|" + Horario.Aula, 1);
        });
    });
}

function AgregarCurso() {
    try {
        var table = document.getElementById("Resultado");
        var Curso = document.getElementById("sltCurso").options[document.getElementById("sltCurso").selectedIndex].text;
        var Bloque = document.getElementById("sltBloque").options[document.getElementById("sltBloque").selectedIndex].value;
        var Grupo = document.getElementById("sltGrupo").options[document.getElementById("sltGrupo").selectedIndex].value;
        var GrupoText = document.getElementById("sltGrupo").options[document.getElementById("sltGrupo").selectedIndex].text;
        var Aula = document.getElementById("sltAula").options[document.getElementById("sltAula").selectedIndex].text;
        var Dia = document.getElementById("ComboDia").options[document.getElementById("ComboDia").selectedIndex].value;
        var HoraInicio = document.getElementById("ComboHoraInicio").options[document.getElementById("ComboHoraInicio").selectedIndex].value;
        var HoraFin = document.getElementById("ComboHoraFin").options[document.getElementById("ComboHoraFin").selectedIndex].value;
        var MinInicio = document.getElementById("ComboMinutoInicio").options[document.getElementById("ComboMinutoInicio").selectedIndex].value;
        var MinFin = document.getElementById("ComboMinutoFin").options[document.getElementById("ComboMinutoFin").selectedIndex].value;

        var Inicio = parseInt(HoraInicio + MinInicio)
        var Fin = parseInt(HoraFin + MinFin)
        var i = Inicio
    }
    catch (err) {
        alert("ERROR: Debe seleccionar el grupo de un curso en una aula");
        return;
    }
    if (Inicio == Fin) {
        alert("ERROR: La hora de inicio y fin son iguales");
        return;
    }
    if (Inicio > Fin) {
        alert("ERROR: La hora de inicio es posterior a la de fin");
        return;
    }
    if (Grupo == "")
    {
        alert("ERROR: Seleccione un grupo");
        return;
    }
    else if (Aula == "-- Seleccionar Aula --") {
        alert("ERROR: Seleccione una aula");
        return;
    }
    //Valido que no existan choques para la hora seleccionada
    /*while (i < Fin) {

        var IdCelda = Dia + " " + i;
        if (i < 100) { IdCelda = Dia + " 0" + i; } //repara el string en caso de que la hora fuera 010 ya que el parse la deja como 10
        if (i == 0) { IdCelda = Dia + " 000"; }    //repara el string en caso de que la hora fuera 000 ya que el parse la deja como 0

        var celda = document.getElementById(IdCelda);
        if (celda.style.backgroundColor != "") {
            alert("ERROR: Existe un choque de horario,\n No se puede agregar el curso");
            return;
        }

        i += 10;
        if (i % 100 == 60) { i += 40; }
    }*/
    var route = "/ExisteHorario/" + Dia + "/" + Inicio + "/" + Fin + "/" + Aula + "/" + Grupo;
    var choque = 0;
    $.ajax({
        url: route,
        datatype: 'json',
        async: false,
        success: function (data) {
            if (data == 1) {
                alert("Ya existe un curso a esa hora y aula");
                choque = 1;
                return;
            }
        }

    });
    if (choque == 0) {
        i = Inicio;
        var CantCeldas = 0;
        var items = "";
        while (i < Fin + 60) {

            var IdCelda = Dia + " " + i;
            if (i < 100) { IdCelda = Dia + " 0" + i; } //repara el string en caso de que la hora fuera 0010 ya que el parse la deja como 10
            if (i == 0) { IdCelda = Dia + " 000"; }
            var celda = document.getElementById(IdCelda);
            if (i == Inicio) {
                var primera = document.getElementById(IdCelda);
            }
            else if (celda != null) {
                items += celda.innerHTML;
                celda.parentNode.removeChild(celda);
            }
            i += 10;
            if (i % 100 == 60) { i += 40; }//cuando se llega al minuto 60 se le suman 40 al numero para que pase por ejemplo de 1060 a 1100
            CantCeldas++;
        }
        primera.innerHTML += items + "<p>" + Curso + " " + GrupoText + "</p>";
        primera.style.backgroundColor = "#3276b1";
        primera.rowSpan = CantCeldas;

        //Actualizo el contador
        var cookie = getCookie("i");//i es el nombre de la cookie, es un contador de cursos pero en toda la sesion no local
        var cantidad = 0;
        if (cookie != "" && cookie != null && !isNaN(cookie)) {
            cantidad = parseInt(cookie);
        }
        cantidad++;
        setCookie("i", cantidad.toString(), 1);
        setCookie("Cookie" + cantidad.toString(), Curso + "|" + Dia + "|" + HoraInicio + MinInicio + "|" + HoraFin + MinFin + "|" + Bloque + "|" + Grupo + "|" + Aula, 1);
    }

}

function EliminarCurso() {
    try {
        var table = document.getElementById("Resultado");
        var Curso = document.getElementById("sltCurso").options[document.getElementById("sltCurso").selectedIndex].text;
        var Grupo = document.getElementById("sltGrupo").options[document.getElementById("sltGrupo").selectedIndex].value;
        var GrupoText = document.getElementById("sltGrupo").options[document.getElementById("sltGrupo").selectedIndex].text;
        var Dia = document.getElementById("ComboDia").options[document.getElementById("ComboDia").selectedIndex].value;
        var HoraInicio = document.getElementById("ComboHoraInicio").options[document.getElementById("ComboHoraInicio").selectedIndex].value;
        var HoraFin;
        var MinInicio = document.getElementById("ComboMinutoInicio").options[document.getElementById("ComboMinutoInicio").selectedIndex].value;
        var MinFin;
    }
    catch (err) {
        alert("ERROR: Debe seleccionar el grupo de un curso y el dia");
        return;
    }

    if (Grupo == "")
    {
        alert("ERROR: Debe seleccionar el grupo")
        return;
    }

    var Inicio = parseInt(HoraInicio + MinInicio)
    var Cookie = getCookie("i");
    if (Cookie == "" || Cookie == null || isNaN(Cookie)) {
        alert("ERROR: No hay cursos que eliminar");
        return;
    }
    //Busco la cookie que se va a eliminar
    var objetivo;
    var Cantidad = parseInt(Cookie);
    for (var j = 1; j <= Cantidad; j++) {
        var ArrayCookie = getCookie("Cookie" + j.toString()).split("|");
        var CursoCookie = ArrayCookie[0];
        var DiaCookie = ArrayCookie[1];
        var HoraInicioCookie = ArrayCookie[2];
        var HoraFinCookie = ArrayCookie[3];
        var CursoCookieGrupo = ArrayCookie[5];
        if (CursoCookie == Curso && DiaCookie == Dia && CursoCookieGrupo==Grupo) {
            objetivo = j;
            //Elimino la cookie
            ArrayCookie[0] = "d";
            setCookie("Cookie" + j.toString(), ArrayCookie.join("|"), 1);
            $("p[id=" + Dia + "]").remove(":contains('" + Curso + " " + GrupoText + "')");
            break;
        }
    }
    
    /*

    var Fin = parseInt(HoraFinCookie)
    //Actualizo la tabla
    var i = Inicio
    while (i < Fin) {
        var IdCelda = Dia + " " + i;
        if (i < 100) { IdCelda = Dia + " 0" + i; } //repara el string en caso de que la hora fuera 0010 ya que el parse la deja como 10
        if (i == 0) { IdCelda = Dia + " 000"; }

        if (i == Inicio) {
            var objetivo = document.getElementById(IdCelda);
            objetivo.innerHTML = "";
            objetivo.style.backgroundColor = "";
        }
        else {
            var columna;
            switch (Dia) {
                case "Lunes":
                    columna = 1;
                    break;
                case "Martes":
                    columna = 2;
                    break;
                case "Miercoles":
                    columna = 3;
                    break;
                case "Jueves":
                    columna = 4;
                    break;
                case "Viernes":
                    columna = 5;
                    break;
                case "Sabado":
                    columna = 6;
                    break;
                case "Domingo":
                    columna = 7;
                    break;
            }
            var fila = document.getElementById("Guia " + i).parentNode;
            var CeldaNueva = fila.insertCell(columna);
            CeldaNueva.id = IdCelda;
        }
        i += 10;
        if (i % 100 == 60) { i += 40; }//cuando se llega al minuto 60 se le suman 40 al numero para que pase por ejemplo de 1060 a 1100
    }
    objetivo.rowSpan = "1";*/
}

function setCookie(cname, cvalue, exdays) {

    document.cookie = cname + "=" + cvalue;
}

function ActualizarContadores() {
    setCookie("Cantidad", getCookie("i"), 1);
    setCookie("i", "0", 1);
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i].trim();
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}