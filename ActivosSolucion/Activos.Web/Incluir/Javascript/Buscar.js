/*
* Fecha de creación:	28-Marzo-2012
* Autor:				Carlos Valladolid
*
* Descripción:			Métodos para la funcionalidad de buscar de las pantalla de la aplicación
*/

function InicializarBusqueda()
{
    $('#DivBusqueda').click(function() {
        $('#DivBuscar').toggle();
        $('#ImagenMostrarDiv').toggle();
        $('#ImagenOcultarDiv').toggle();

        return false;
    });

    $('#BotonCancelar').click(function() {
        $('#DivBuscar').hide();
        $('#ImagenMostrarDiv').toggle();
        $('#ImagenOcultarDiv').toggle();
    });
}
