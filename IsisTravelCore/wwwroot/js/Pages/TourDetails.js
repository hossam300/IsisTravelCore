$('#Description').summernote('disable');
$('#Intro').summernote('disable');
$('#CategoryTour-Form').submit(function (e) {
    e.preventDefault();
    if ($('#CategoryId').val() == "" || $("#Days").val() == "" || $("#Cities").val() == "" || $("#AdultPrice").val() == "" || $("#BabyPrice").val() == "" || $("#ChildPrice").val() == "" || $("#SingleRoomExtrafees").val() == "") {

        if ($('#CategoryId').val() == "") {
            $('#CategoryVaildation').show();
            $('#CategoryVaildation').text("Categoría requerida");
        }
        else if ($("#Days").val() == "") {
            $('#CategoryVaildation').show();
            $('#CategoryVaildation').text("Dias requerida");
        }
        else if ($("#Cities").val() == "") {
            $('#AdultPriceVaildation').show();
            $('#AdultPriceVaildation').text("ciudad requerida");
        }
        else if ($("#AdultPrice").val() == "") {
            $('#AdultPriceVaildation').show();
            $('#AdultPriceVaildation').text("Precio adulto requerida");
        }
        else if ($("#BabyPrice").val() == "") {
            $('#BabyPriceVaildation').show();
            $('#BabyPriceVaildation').text("Precio de bebe requerida");
        }
        else if ($("#ChildPrice").val() == "") {
            $('#BabyPriceVaildation').show();
            $('#BabyPriceVaildation').text("Precio del niño requerida");
        }
        else if ($("#SingleRoomExtrafees").val() == "") {
            $('#SingleRoomExtrafeesVaildation').show();
            $('#SingleRoomExtrafeesVaildation').text("Habitación individual Tarifas extra requerida");
        }
    }
    else {
        mApp.blockPage({ message: 'Just a moment..' });
        var Days = [];
        $.each($("#Days option:selected"), function () {
            Days.push($(this).val());
        });
        
        var tourCategoryVM = {
            Id: $("#TourCategoryId").val(),
            CategoryId: $("#CategoryId").val(),
            Category: $("#Category").val(),
            TourId: $('#TourId').val(),
            Days: Days.toString(),
            CityId: $("#Cities").val(),
            AdultPrice: $("#AdultPrice").val(),
            BabyPrice: $("#BabyPrice").val(),
            ChildPrice: $("#ChildPrice").val(),
            SingleRoomExtrafees: $("#SingleRoomExtrafees").val(),
        }
        $.ajax({
            type: 'POST',
            url: "/Tours/CreateTourCategory/",
            data: tourCategoryVM,
            success: function (result) {
                $(document).ajaxStop($.unblockUI);
                swal({
                    title: "Categoría !",
                    text: "Categoría agregada para recorrer con éxito",
                    type: "success",
                });
                //var html = '<tr> <td style="text-align:center"> <a href="/Tours/CategoryDayTour/' + result.id + '">' + result.categoryName + '</a></td>' + '<td style="text-align:center">' + result.cityName + '</td>'
                //    + '<td style="text-align:center">' + result.adultPrice + '</td>' +
                //    '<td style="text-align:center">' + result.childPrice + '</td>' +
                //    '<td style="text-align:center">' + result.babyPrice + '</td>' + '<td style="text-align:center">' + result.singleRoomExtrafees + '</td> <td style="text-align:center"> <a href="/Tours/CategoriesActivation/' + result.id + '">Inactivo</a></td></tr>';
                //$('#CategoryId').val('');
                //$("#Days").val('').trigger('change');
                //$("#AdultPrice").val('');
                //$("#BabyPrice").val('');
                //$("#ChildPrice").val('');
                //$("#SingleRoomExtrafees").val('');
                //$("#ToursCategory tbody").append(html);
                  location.reload();
            },
            error: function () {
                $(document).ajaxStop($.unblockUI);
                swal({
                    title: "Categoría !",
                    text: "Algo mal por favor intente de nuevo",
                    type: "error",
                });
            }
        });
    }
});
$('#ActivityTour-Form').submit(function (e) {
    e.preventDefault();
    if ($('#ActivityId').val() == "") {
        $('#ActivityVaildation').show();
        $('#ActivityVaildation').text("Actividad requerida");
    }
    if ($('#ActivityDescription').val() == "") {
        $('#DescriptionVaildation').show();
        $('#ActivityVaildation').text("Description requerida");
    }
    else {
        mApp.blockPage({ message: 'Just a moment..' });
        $('#ActivityVaildation').hide();
        $('#DescriptionVaildation').hide();
        var tourActivityVM = {
            TourId: $("#TourId").val(),
            ActivityId: $('#ActivityId').val(),
            Description: $("#ActivityDescription").val(),
            TourActivityId: $("#tourActivityId").val(),
            Activity: $("#activity").val(),
        };
        $.ajax({
            type: 'POST',
            url: "/Tours/CreateTourActivity/",
            data: tourActivityVM,
            success: function (result) {
                $(document).ajaxStop($.unblockUI);
                swal({
                    title: "Actividad !",
                    text: "Actividad agregada para recorrer con éxito",
                    type: "success",
                });
                //var activegtml = '<tr> <td style="text-align:center">' + $('#ActivityId').val() + '</td>' +
                //    '<td style="text-align:center"><a href="/Tours/ActivationActivity/' + result + '">Inactivo</a></td></tr>';
                //$('#ActivityId').val('');
                //$("#ActivityDescription").summernote("reset");
                //$("#TourActivities tbody").append(activegtml);
                location.reload();
            },
            error: function () {
                $(document).ajaxStop($.unblockUI);
                swal({
                    title: "Actividad !",
                    text: "Algo mal por favor intente de nuevo",
                    type: "error",
                });
            }
        });
    }
});
$('#IncludeTour-Form').submit(function (e) {
    e.preventDefault();
    if ($('#IncludeId').val() == "") {
        $('#IncludeVaildation').show();
        $('#IncludeVaildation').text("Incluir requerida");
    }
    if ($('#IncludeDescription').val() == "") {
        $('#IncludeDescriptionVaildation').show();
        $('#IncludeDescriptionVaildation').text("Incluir requerida");
    }
    else {
        mApp.blockPage({ message: 'Just a moment..' });
        var tourIncludeVM = {
            TourId: $("#TourId").val(),
            IncludeId: $('#IncludeId').val(),
            IncludeDescription: $("#IncludeDescription").val(),
            TourIncludeId: $("#tourIncludeId").val(),
            Include: $("#include").val(),
        };
        $.ajax({
            type: 'POST',
            url: "/Tours/CreateTourInclude/",
            data: tourIncludeVM,
            success: function (result) {
                $(document).ajaxStop($.unblockUI);
                swal({
                    title: "Incluir !",
                    text: "Incluir agregada para recorrer con éxito",
                    type: "success",
                });
                //var Includegtml = '<tr> <td style="text-align:center">' + $('#IncludeId').val() + '</td>' +
                //    '<td style="text-align:center"><a href="/Tours/ActivationActivity/' + result + '">Inactivo</a></td></tr>';
                //$('#IncludeId').val('');
                //$("#IncludeDescription").summernote("reset");;
                //$("#IncludeTours tbody").append(Includegtml);
                  location.reload();
            },
            error: function () {
                $(document).ajaxStop($.unblockUI);
                swal({
                    title: "Incluir !",
                    text: "Algo mal por favor intente de nuevo",
                    type: "error",
                });
            }
        });
    }
});
$('#Quote-Form').submit(function (e) {
    e.preventDefault();
    if ($('#QuoteName').val() == "") {
        $('#QuoteVaildation').show();
        $('#QuoteVaildation').text("cotización Nombre requerida");
    }
    else if ($('#QuotePrice').val() == "") {
        $('#QuoteVaildation').show();
        $('#QuoteVaildation').text("cotización Precio requerida");
    }
    else {
        mApp.blockPage({ message: 'Just a moment..' });
        var tourQuoteVM = {
            TourId: $("#TourId").val(),
            QuoteName: $('#QuoteName').val(),
            QuotePrice: $("#QuotePrice").val(),
            TourQuoteId: $("#TourQuoteId").val(),
        };
        $.ajax({
          
            type: 'POST',
            url: "/Tours/CreateTourQuote",
            data: tourQuoteVM,
            success: function (result) {
                $(document).ajaxStop($.unblockUI);
                swal({
                    title: "Servicio !",
                    text: "Servicio agregada para recorrer con éxito",
                    type: "success",
                });
                var Quotesgtml = '<tr> <td style="text-align:center">' + $('#QuoteName').val() + '</td>' +
                    '<td style="text-align:center">' + $("#QuotePrice").val() + '</td></tr>' +
                    $('#QuoteName').val('');
                $('#QuotePrice').val('');
                $("#TourQuotes tbody").append(Quotesgtml);

                // location.reload();
            },
            error: function () {
                $(document).ajaxStop($.unblockUI);
                swal({
                    title: "Servicio !",
                    text: "Algo mal por favor intente de nuevo",
                    type: "error",
                });
            }
        });
    }
});
$(".CategoriesEdit").on('click', function (e) {
    var id = $(this).attr("data-id");
    mApp.blockPage({ message: 'Just a moment..' });
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'Get',
        url: "/Tours/GetTourCategory/?id=" + id,
        success: function (result) {
            $(document).ajaxStop($.unblockUI);

            console.log(result);
            FillCategortForm(result);
            // location.reload();
        },
        error: function () {
            $(document).ajaxStop($.unblockUI);
            swal({
                title: "Servicio !",
                text: "Algo mal por favor intente de nuevo",
                type: "error",
            });
        }
    });
});

$(".ActivityEdit").on('click', function (e) {
    var id = $(this).attr("data-id");
    mApp.blockPage({ message: 'Just a moment..' });
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'Get',
        url: "/Tours/GetTourActivity/?id=" + id,
        success: function (result) {
            $(document).ajaxStop($.unblockUI);

            console.log(result);
            FillActivityForm(result);
            // location.reload();
        },
        error: function () {
            $(document).ajaxStop($.unblockUI);
            swal({
                title: "Servicio !",
                text: "Algo mal por favor intente de nuevo",
                type: "error",
            });
        }
    });
});

$(".IncludeEdit").on('click', function (e) {
    var id = $(this).attr("data-id");
    mApp.blockPage({ message: 'Just a moment..' });
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'Get',
        url: "/Tours/GetTourInclude/?id=" + id,
        success: function (result) {
            $(document).ajaxStop($.unblockUI);

            console.log(result);
            FillIncludeForm(result);
            // location.reload();
        },
        error: function () {
            $(document).ajaxStop($.unblockUI);
            swal({
                title: "Servicio !",
                text: "Algo mal por favor intente de nuevo",
                type: "error",
            });
        }
    });
});
$(".QuoteEdit").on('click', function (e) {
    var id = $(this).attr("data-id");
    mApp.blockPage({ message: 'Just a moment..' });
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'Get',
        url: "/Tours/GetTourQuote/?id=" + id,
        success: function (result) {
            $(document).ajaxStop($.unblockUI);

            console.log(result);
            FillQuoteForm(result);
            // location.reload();
        },
        error: function () {
            $(document).ajaxStop($.unblockUI);
            swal({
                title: "Servicio !",
                text: "Algo mal por favor intente de nuevo",
                type: "error",
            });
        }
    });
});
function FillQuoteForm(result)
{
    $("#QuoteName").val(result["quoteName"])
    $("#QuotePrice").val(result["quotePrice"]);
    $("#IncludeId").val(result["includeId"]);
    $("#TourQuoteId").val(result["tourQuoteId"]);
}
function FillIncludeForm(result)
{
    $("#tourIncludeId").val(result["tourIncludeId"])
    $("#include").val(result["include"]);
    $("#IncludeId").val(result["includeId"]);
    $("#IncludeDescription").summernote('editor.pasteHTML', (result["includeDescription"]));

}
function FillActivityForm(result) {
    $("#tourActivityId").val(result["tourActivityId"])
    $("#activity").val(result["activity"]);
    $("#ActivityId").val(result["activityId"]); 
    $("#ActivityDescription").summernote('editor.pasteHTML',(result["description"]));
}
function FillCategortForm(result) {
    var days = result["days"].split(',');
    console.log(days)
    $("#TourCategoryId").val(result["id"])
    $("#CategoryId").val(result["categoryId"]);
    $("#Category").val(result["category"]);
    $("#Days").val(days).trigger('change');
    $("#Cities").val(result["cityId"]).trigger('change');
    $("#AdultPrice").val(result["adultPrice"]);
    $("#ChildPrice").val(result["childPrice"]);
    $("#BabyPrice").val(result["babyPrice"]);
    $("#SingleRoomExtrafees").val(result["singleRoomExtrafees"]);
    
}