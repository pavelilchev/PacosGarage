$(function () {
    if ($("#appointmentDate").datepicker) {
        $("#appointmentDate").datepicker({
            minDate: new Date()
        });
    }
});