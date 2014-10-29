/*
* Fecha de creación:	07-Mayo-2012
* Autor:				Carlos Valladolid
*
* Descripción:			Métodos para la validación de los formularios
*/

function ValidarCasillas() {
    var intCount;
    var bolValidateCheckbox = false;
    var ctrForm;

    for (intCount = 0; intCount < document.forms[0].elements.length; intCount++) {
        ctrForm = document.forms[0].elements[intCount];

        if (ctrForm.type == 'checkbox')
            if (ctrForm.checked)
            bolValidateCheckbox = true;
    }

    if (!bolValidateCheckbox)
        alert("Debe seleccionar al menos un registro");

    return bolValidateCheckbox;
}


