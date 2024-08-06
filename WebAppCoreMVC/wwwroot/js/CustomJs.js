//for search
$('#txtSearch').keyup(function () {
    var typeValue = $(this).val();
    $('tbody tr').each(function () {
        if ($(this).text().search(new RegExp(typeValue, "i")) < 0) {
            $(this).fadeOut();
        }
        else {
            $(this).show();
        }
    })
});


// for Delete All
$('#Select_All').on('click', function () {
    let checkboxes = document.getElementsByTagName('input');
    let val = null;
    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].type == 'checkbox') {
            if (val == null) {
                val = checkboxes[i].checked;
            } 
            else {
                checkboxes[i].checked = val;
            }
        }
    }
});

$('#btnDelete').on('click', function () {
    let val = [];
    console.log(val);

    ////process1: if checked then count the fill in into val
    //$(':checkbox:checked').each(function (i) {
    //    val[i] = $(this).val();
    //})

    //process 2
    $("input[name='Select_One']:checked").each(function () {
        val.push($(this).val());
    });

    $.ajax({
        type: 'POST',
        url: '/Employee/DeleteSingleOrMultiple',
        data: { 'ids': val },
        success: function (response) {
            if (response == 'success') {
                location.reload();
            }
        },
        error: function () {

        }
    })
});