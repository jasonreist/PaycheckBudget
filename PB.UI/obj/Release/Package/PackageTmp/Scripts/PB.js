toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-bottom-full-width",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

var URLBASE = GetUrlBase();

function GetUrlBase()
{
    var url = "http://svc.pb.com/api/";
    var current = window.location.href;
    if (current.indexOf('jasonreist') > -1)
    {
        url = "http://jasonreist-001-site2.htempurl.com/api/";
    }
    return url;
}

function CreateEditBillSetup(a,b,c)
{
    $(document).ready(function () {
        $('li.duedayli').click(function (e) {
            $('li.duedayli').each(function () {
                $(this).removeClass().addClass('duedayli');
            });
            $(this).addClass('duedayli active');
            var txt = $(this).find(">:first-child").html();
            $("#editbillsduedayddl").html(txt + ' <span class="caret"></span>');
            var chosenday = txt.replace('th', '').replace('st', '').replace('rd', '');
            $("#Bill_DueDay").val(chosenday);
            $("#DueDay").val(chosenday);
        });

        $('li.iconli').click(function (e) {
            $('li.iconli').each(function () {
                $(this).removeClass().addClass('iconli');
            });
            $(this).addClass('iconli active');
            var txt = $(this).find(">:first-child").html();
            $("#editbillsiconddl").html(txt + ' <span class="caret"></span>');
            var chosenIcon = txt.substr(txt.indexOf('>') + 9);
            $("#iconpreview").removeClass().addClass('fa ' + chosenIcon);
            $("#Bill_Icon").val(chosenIcon);
            $("#Icon").val(chosenIcon);
        });

        $("#iconpreview").removeClass().addClass('fa ' + c);
    });

    // ViewModel
    var AppViewModel = function () {
        this.Name = ko.observable("");
        this.Amount = ko.observable("");
        this.BGColor = ko.observable("#" + a);
        this.ForeColor = ko.observable("#" + b);
    }

    // ViewModel instance
    var app = new AppViewModel();

    function UpdateColor(bcolor, fcolor) {
        app.BGColor(bcolor);
        app.ForeColor(fcolor);
    }

    // Activates knockout.js
    ko.applyBindings(app);
}

function CustomBillsSetup(id, nadays)
{
    //var bills = "";

    //$.get("http://localhost:53253/api/getcustombills/" + id),
    //function (data) {
    //    for (var i = 0; i < data.Data.length; i++) {
    //        var d = yeardashmonthdashdaydash(data.Data.BillDate);
    //        alert(d);
    //        bills = bills + "'" + d + "': { 'class': 'na' },";
    //    };
    //};



    //bills = bills.length > 0 ? bills.subString(0, bills.length) : "";
    ////nadays = replaceQuote(nadays);
    //alert('bills:'+bills);

    //$(document).ready(function () {
    //    $(".responsive-calendar").responsiveCalendar({
    //        time: yeardashmonth(),
    //        events: bills
    //    });
    //});
}


function SaveBill() {
    var userid = $('#Bill_UserId').val();
    var billid = $('#Bill_Id').val();
    var billname = $('#Bill_Name').val();
    var billicon = $('#Bill_Icon').val();
    var amount = $('#Bill_Amount').val();
    var dueday = $('#Bill_DueDay').val();
    var bgcolor = $('#Bill_BackgroundColor').val().substr(1);
    var forecolor = $('#Bill_ForeColor').val().substr(1);
    

    var source = { 'UserId': userid, 'Id': billid, 'Name': billname, 'DueDay': dueday, 'Amount': amount, 'BackgroundColor': bgcolor, 'ForeColor': forecolor, 'Icon': billicon };

    var request = $.ajax({
        type: 'PUT',
        url: URLBASE + "UpdateBill",
        data: source
    });

    request.done(function (msg) {
        toastr.success('Your bill has been saved.');
    });

    request.fail(function (jqXHR, textStatus) {
        toastr.error("Failed to save bill.");
    });
}

function CreateBill() {
    var userid = $('#UserId').val();
    var billname = $('#Name').val();
    var billicon = $('#Icon').val();
    console.log(billicon);
    var amount = $('#Amount').val();
    var dueday = $('#DueDay').val();
    var bgcolor = $('#BackgroundColor').val().substr(1);
    var forecolor = $('#ForeColor').val().substr(1);

    var source = { 'UserId': userid, 'Name': billname, 'DueDay': dueday, 'Amount': amount, 'BackgroundColor': bgcolor, 'ForeColor': forecolor, 'Icon': billicon };

    var request = $.ajax({
        type: 'POST',
        url: URLBASE + "CreateBill",
        data: source
    });

    request.done(function (msg) {
        toastr.success('Your bill has been created.');
    });

    request.fail(function (jqXHR, textStatus) {
        toastr.error("Failed to create bill.");
    });
}

function DeleteBill(id) {
    var source = { 'Id': id };
    var billname = $('#BillName_' + id).text();

    var request = $.ajax({
        type: 'DELETE',
        url: URLBASE + 'DeleteBill/',
        data: source
    });

    request.done(function (msg) {
        toastr.success('<b>' + billname + '</b> and any custom bills have been deleted.');
        $('#BillRow_' + id).hide(1000);
    });

    request.fail(function (jqXHR, textStatus) {
        toastr.error("Failed to delete bill. " + textStatus);
    });

}

function AddUpdateCustomBill(index)
{
    var custombillid = $('#CustomBills_' + index + '__Id').val();
    var billid = $('#Bill_Id').val();
    var amount = $('#CustomBills_' + index + '__Amount').val();
    var date = $('#date_' + index).text();
    var action = $('#btnAddUpdateCustomBill_' + index).val();
    var actionurl = (action == 'Create') ? 'CreateCustomBill/' : 'UpdateCustomBill/';
    var verb = (action == 'Create') ? 'POST' : 'PUT';

    if (amount == '')
    {
        action = 'delete';
        actionurl = 'DeleteCustomBill';
        verb = 'DELETE';
        amount = 0;
    }

    var source = { 'Id': custombillid, 'BillId': billid, 'BillDate': date, 'Amount': amount };

    var request = $.ajax({
        type: verb,
        url: URLBASE + actionurl,
        data: source
    });

    request.done(function (msg) {
        toastr.success('Custom bill ' + action.toLowerCase() + 'd - $' + amount + ' set for ' + date + '.');
    });

    request.fail(function (jqXHR, textStatus) {
        toastr.error("Failed to " + action.toLowerCase() + " custom bill.");
    });

    if (verb == 'POST') {
        $('#btnAddUpdateCustomBill_' + index).val('Save');
    }
    if (verb == 'DELETE') {
        $('#btnAddUpdateCustomBill_' + index).val('Create');
        $('#CustomBills_' + index + '__Amount').val('0');
    }
}

function IncomeSetup() { }

function CalculateTithe()
{
    var ca = $("#PaycheckAmount").val();
    var m = $("#TitheMultiplier").val();
    var total = m != '.' ? numeral(m * ca).format('$0,0.00') : '';
    $("#spanAmount").html(total);
}

function EditSettingsSetup(a, b) {
    $(document).ready(function () {
        $('li.sdli').click(function (e) {
            $('li.sdli').each(function () {
                $(this).removeClass().addClass('sdli');
            });
            $(this).addClass('sdli active');
            var txt = $(this).find(">:first-child").html();
            $("#editsdddl").html(txt + ' <span class="caret"></span>');
            $("#WeekStartDay").val(DOWToInt(txt));
        });

        CalculateTithe();
        $('#TitheMultiplier').keyup(function (e) {
            CalculateTithe();
        });

        $('li.dipli').click(function (e) {
            $('li.dipli').each(function () {
                $(this).removeClass().addClass('dipli');
            });
            $(this).addClass('dipli active');
            var txt = $(this).find(">:first-child").html();
            $("#editdipddl").html(txt + ' <span class="caret"></span>');
            $("#DaysInPeriod").val(txt.replace(' days', ''));
        });

        $('li.csli').click(function (e) {
            $('li.csli').each(function () {
                $(this).removeClass().addClass('csli');
            });
            $(this).addClass('csli active');
            var txt = $(this).find(">:first-child").html();
            $("#editcsddl").html(txt + ' <span class="caret"></span>');
            $("#ChecksToShow").val(txt);
        });

        $('li.ppli').click(function (e) {
            $('li.ppli').each(function () {
                $(this).removeClass().addClass('ppli');
            });
            $(this).addClass('ppli active');
            var txt = $(this).find(">:first-child").html();
            $("#editppddl").html(txt + ' <span class="caret"></span>');
            $("#PreviousPeriodsToShow").val(txt);
        });
    });

    $('#cbAddTithe').change(function () {
        $('#AddTithe').val($(this).is(':checked'));
    });

    // ViewModel
    var AppViewModel = function () {
        //this.Amount = ko.observable('');
        this.BGColor = ko.observable('#' + a);
        this.ForeColor = ko.observable('#' + b);
    }

    // ViewModel instance
    var app = new AppViewModel();

    function UpdateColor(bcolor, fcolor) {
        app.BGColor(bcolor);
        app.ForeColor(fcolor);
        //ko.applyBindings(app);
    }

    // Activates knockout.js
    ko.applyBindings(app);
}

function DOWToInt(dayofweek)
{
    if (dayofweek == 'Sunday') return 0;
    if (dayofweek == 'Monday') return 1;
    if (dayofweek == 'Tuesday') return 2;
    if (dayofweek == 'Wednesday') return 3;
    if (dayofweek == 'Thursday') return 4;
    if (dayofweek == 'Friday') return 5;
    if (dayofweek == 'Saturday') return 6;
}

function UpdateSettings() {
    var userid = $('#UserID').val();
    var PreviousPeriodsToShow = $('#PreviousPeriodsToShow').val();
    var ChecksToShow = $('#ChecksToShow').val();
    var DaysInPeriod = $('#DaysInPeriod').val();
    var WeekStartDay = $('#WeekStartDay').val();
    var TitheMultiplier = $('#TitheMultiplier').val(); 
    var AddTithe = $('#AddTithe').val();
    var TitheBGColor = $('#TitheBGColor').val().substr(1);
    var TitheForeColor = $('#TitheForeColor').val().substr(1);

    console.log(TitheMultiplier);

    var source = {
        'UserId': userid,
        'PreviousPeriodsToShow': PreviousPeriodsToShow, 'ChecksToShow': ChecksToShow, 'DaysInPeriod': DaysInPeriod, 'WeekStartDay': WeekStartDay,
        'AddTithe': AddTithe, 'TitheMultiplier': TitheMultiplier, 'TitheBGColor': TitheBGColor, 'TitheForeColor': TitheForeColor
    };

    var request = $.ajax({
        type: "PUT",
        url: URLBASE + "UpdateSettings/",
        data: source
    });

    request.done(function (msg) {
        toastr.success('Your settings have been saved.');
    });

    request.fail(function (jqXHR, textStatus) {
        toastr.error('Failed to save settings. ' + textStatus);
    });

    //show message
}

function AddUpdatePaycheck(index) {
    var paycheckid = $('#Paychecks_' + index + '__ID').val();
    var userid = $('#Paychecks_0__UserID').val();
    var amount = $('#Paychecks_' + index + '__Amount').val();
    var type = $('#Type_' + index).text();

    var action = $('#btnAddUpdatePaycheck_' + index).val();
    var actionurl = (action == 'Create') ? 'CreatePaycheck/' : 'UpdatePaycheck/';
    var verb = (action == 'Create') ? 'POST' : 'PUT';

    if (amount == '') {
        action = 'delete';
        actionurl = 'DeletePaycheck';
        verb = 'DELETE';
        amount = 0;
    }

    var source = { 'Id': paycheckid, 'UserId': userid, 'Type': type, 'Amount': amount };

    var request = $.ajax({
        type: verb,
        url: URLBASE + actionurl,
        data: source
    });

    request.done(function (msg) {
        toastr.success('Paycheck ' + type + ' has been ' + action.toLowerCase() + 'd.');
    });

    request.fail(function (jqXHR, textStatus) {
        toastr.error('Failed to ' + action.toLowerCase() + ' Paycheck [' + type + ']. ' + textStatus);
    });

    if (verb == 'POST') {
        $('#btnAddUpdatePaycheck_' + index).val('Save');
    }
    if (verb == 'DELETE') {
        $('#btnAddUpdatePaycheck_' + index).val('Create');
        $('#Paychecks_' + index + '__Amount').val('0');
    }
}


function AddUpdatePayday(index) {
    var paydayid = $('#PayDay_Id').val();
    var userid = $('#Paychecks_0__UserID').val();
    var pdate = $('#PayDay_Paydate').val();
    var source = { 'Id': paydayid, 'UserId': userid, 'Paydate': pdate };

    var request = $.ajax({
        type: 'PUT',
        url: URLBASE + 'UpdatePayday/',
        data: source
    });

    request.done(function (msg) {
        toastr.success('Your payday seed has been updated.');
    });

    request.fail(function (jqXHR, textStatus) {
        toastr.error("Failed to save payday seed.");
    });
    
}

function replaceQuote(txt)
{
    txt = txt.replace("&quot;", "'");
    if (txt.indexOf('&') > -1) txt = replaceQuote(txt);
    return txt;
}

function yeardashmonth()
{
    var d = new Date();
    return d.getFullYear() + "-" + addLeadingZero(d.getMonth()+1);
}
function yeardashmonthdashdaydash(d) {
    return d.getFullYear() + "-" + addLeadingZero(d.getMonth()+1) + "-" + addLeadingZero(d.getDay());
}

function addLeadingZero(num) {
    if (num < 10) {
        return "0" + num;
    } else {
        return "" + num;
    }
}

/* KNOCKOUT HANDLERS */

ko.bindingHandlers.currency = {
    symbol: ko.observable('$'),
    update: function (element, valueAccessor, allBindingsAccessor) {
        return ko.bindingHandlers.text.update(element, function () {
            var value = +(ko.utils.unwrapObservable(valueAccessor()) || 0),
                symbol = ko.utils.unwrapObservable(allBindingsAccessor().symbol === undefined
                            ? allBindingsAccessor().symbol
                            : ko.bindingHandlers.currency.symbol);
            return symbol + value.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,");
        });
    }
};

ko.bindingHandlers.instantValue = {
    init: function (element, valueAccessor, allBindings) {
        var newAllBindings = function () {
            // for backwards compatibility w/ knockout  < 3.0
            return ko.utils.extend(allBindings(), { valueUpdate: 'afterkeydown' });
        };
        newAllBindings.get = function (a) {
            return a === 'valueupdate' ? 'afterkeydown' : allBindings.get(a);
        };
        newAllBindings.has = function (a) {
            return a === 'valueupdate' || allBindings.has(a);
        };
        ko.bindingHandlers.value.init(element, valueAccessor, newAllBindings);
    },
    update: ko.bindingHandlers.value.update
};