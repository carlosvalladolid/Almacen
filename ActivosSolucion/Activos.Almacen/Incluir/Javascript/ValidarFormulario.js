/*
* Fecha de creación:	07-Mayo-2012
* Autor:				Carlos Valladolid
*
* Descripción:			Métodos para la validación de los formularios
*/

function ConfirmarBorrado() {
    if (confirm("¿Desea eliminar los registros seleccionados?"))
        return true;
    else
        return false;
}

function MostrarMensaje(Mensaje, TipoMensaje) {
    $(document).ready(function() {
        switch(TipoMensaje)
        {
            case "Error":
                $.growlUIError('Error', Mensaje);
                break;

            case "Mensaje":
                $.growlUIMessage('Mensaje', Mensaje);
                break;
        }
    });
}

function ValidarCasillas() {
    var Contador;
    var ValidateCheckbox = false;
    var Form;

    for (Contador = 0; Contador < document.forms[0].elements.length; Contador++) {
        Form = document.forms[0].elements[Contador];

        if (Form.type == 'checkbox')
            if (Form.checked)
                ValidateCheckbox = true;
    }

    if (ValidateCheckbox)
        ValidateCheckbox = ConfirmarBorrado();
    else
        alert("Debe seleccionar al menos un registro");

    return ValidateCheckbox;
}
