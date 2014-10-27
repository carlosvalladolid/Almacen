/*
* Fecha de creación:	22-Septiembre-2011
* Autor:				Carlos Valladolid
*
* Descripción:			Métodos para que la funcionalidad de los calendarios corra con los updatepanel
*/

function SetNewCalendar(strNewStartDateName, strNewEndDateName)
{
    $(function() {
		    $(strNewStartDateName).datepicker($.datepicker.regional["es"]);
		    $(strNewEndDateName).datepicker($.datepicker.regional["es"]);
	    });
}

function SetControlNewCalendar(strControlNewStartDateName, strControlNewEndDateName)
{
    var txtStartDate;
    var txtEndDate;

    txtStartDate = document.getElementById(strControlNewStartDateName);
    txtEndDate = document.getElementById(strControlNewEndDateName);

    //alert(strControlNewStartDateName);
    if(!(txtStartDate == null))
    {
        $(function() {
		    $("#" + strControlNewStartDateName).datepicker($.datepicker.regional["es"]);
		    $("#" + strControlNewEndDateName).datepicker($.datepicker.regional["es"]);
	    });
    }
}
