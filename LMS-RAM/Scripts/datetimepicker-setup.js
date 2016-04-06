$(function () {
    // # Går på id't hos en EditorFor kontroll som är samma som propertynamnet i modellen
    //$('#StartTime').datetimepicker({
    //    daysOfWeekDisabled: [0, 6]
    //});
    //$('#EndTime').datetimepicker({
    //    daysOfWeekDisabled: [0, 6]
    //});
    //$('#StartDate').datetimepicker({
    //    daysOfWeekDisabled: [0, 6]
    //});
    //$('#EndDate').datetimepicker({
    //    daysOfWeekDisabled: [0, 6]
    //});

    // Generisk, lägg til CSS-Klassnamn datetimepicker i en EditorFor helper
    $('.datetimepicker').datetimepicker({
        daysOfWeekDisabled: [0, 6]
    });
});
