let url = "https://apicorecruddepartamentosdamt.azurewebsites.net/";
function getDepartamentosAsync(callBack) {
    //console.log("Dentro de JS")
    let request = "api/departamentos";
    $.ajax({
        url: url + request,
        type: "GET",
        dataType: "json",
        success: function (data) {
            //console.log("Aqui en Ajax");
            //console.log(data);
            callBack(data);
        }
    })
}

//  PARA GENERAR EL JSON DE DEPARTAMENTO
function convertDeptJson(id, nombre, localidad) {
    let dept = new Object();
    dept.idDepartamento = id;
    dept.nombre = nombre;
    dept.localidad = localidad;

    var json = JSON.stringify(dept);
    return json;
}

//  EN LA FUNCION PARA QUE SEA DE FORMA ASINCRONA SE DEBE INCLUIR callBack
function insertDepartamentoAsync(id, nombre, localidad, callBack) {
    let json = convertDeptJson(id, nombre, localidad);
    console.log(json)
    var request = "api/departamentos";
    $.ajax({
        url: url + request,
        type: "POST",
        data: json,
        contentType: "application/json",
        success: function () {
            callBack();
        }
    })
}

function updateDepartamentoAsync(id, nombre, localidad, callBack) {
    let json = convertDeptJson(id, nombre, localidad);
    var request = "api/departamentos";
    $.ajax({
        url: url + request,
        type: "PUT",
        data: json,
        contentType: "application/json",
        success: function () {
            callBack();
        }
    })
}

function deleteDepartamentoAsync(id, callBack) {
    var request = "api/departamentos/"+id;
    $.ajax({
        url: url + request,
        type: "DELETE",
        success: function () {
            callBack();
        }
    })
}