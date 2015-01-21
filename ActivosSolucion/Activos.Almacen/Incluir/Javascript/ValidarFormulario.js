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
                
            case "SimpleAlerta":
                alert(Mensaje);
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

jQuery.fn.SoloNumeros =
function()
{
    return this.each(function()
    {
        $(this).keydown(function(e)
        {
            var key = e.charCode || e.keyCode || 0;
            // Permitir backspace, tab, delete, enter, arrows, numbers and keypad numbers
            // home, end, period, and numpad decimal
            return (
                key == 8 || 
                key == 9 ||
                key == 13 ||
                //key == 46 ||
                key == 110 ||
                //key == 190 ||
                (key >= 35 && key <= 40) ||
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105));
        });
    });
};

jQuery.fn.Confirmar =
function(Mensaje)
{
    return this.each(function()
    {
        $(this).click(function()
        {
            if(confirm(Mensaje)) return true;
            return false;
        });
    });
};