var Invoice = function (params) {
    var self = this;
    self.statementID = ko.observable(params && params.statementID);
    self.statementDate = ko.observable(GetFormattedDate(params && params.statementDate));
    self.preBalance = ko.observable(GetFormattedPrice(params && params.preBalance));
    self.curChanges = ko.observable(GetFormattedPrice(params && params.curChanges));
    self.total = ko.observable(GetFormattedPrice(params && params.total));
    self.lateFees = ko.observable(GetFormattedPrice(params && params.lateFees));
}

function GetFormattedDate(date) {
    var d = new Date(date)
    var formattedDate = (d.getMonth() + 1) + "/" + d.getDate() + "/" + d.getFullYear();
    return formattedDate;
}

function GetFormattedPrice(price) {
    return '¥' + numberWithCommas(price);
}

function numberWithCommas(x) {
    x = x.toString();
    var pattern = /(-?\d+)(\d{3})/;
    while (pattern.test(x))
        x = x.replace(pattern, "$1,$2");
    return x;
}

ko.components.register("invoice", {
    viewModel: Invoice,
    template:
        '<td data-bind="text: statementID"></td>' +
        '<td data-bind="text: statementDate"></td>' +
        '<td data-bind="text: preBalance"></td>' +
        '<td data-bind="text: curChanges"></td>' +
        '<td data-bind="text: total"></td>' +
        '<td data-bind="text: lateFees"></td>'
});

function InvoiceViewModel() {
    var self = this;

    self.invoiceList = ko.observableArray();
    self.totalRow = ko.observable();

    self.username = ko.observable(localStorage.getItem("username"));
    var countRow = 0;
    var token = localStorage.getItem("token");

    self.selectORM = ko.observable("");
    self.ormList = ko.observableArray ([
        'Entity Framework','Dapper'
    ]);
    var url = "https://localhost:44330/api/invoice/getAllOwnInvoice";
    getAllOwnInvoice(url);
    self.selectORM.subscribe(function(orm) {
        console.log(orm);
        if (orm === "Entity Framework") {
            getAllOwnInvoice(url);
        }
        else if (orm === "Dapper") {
            url = "https://localhost:44330/api/invoiceDapper/getAllOwnInvoice"; 
            getAllOwnInvoice(url);
        }
    }, this);
    function getAllOwnInvoice(url) {
        $.ajax({
            url: url,
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            headers: {
                authorization: 'Bearer ' + token
            },
            success: function (result) {
                $.each(result.invoiceList, function (key, item) {
                    var invoice = item;
                    self.invoiceList.push(invoice);
                    countRow++;
                });
                self.totalRow(countRow);
            },
            error: function (err) {
                alert(err);
            }
        });
    }
}

ko.components.register("invoice-list", {
    viewModel: InvoiceViewModel,
    template:
        '<div class="table-responsive table-responsive-sm">' +
        '<table class="table table-bordered">' +
        '<thead class="fixedHeader">' +
        '<tr style="background-color: #fff;">' +
        '<th scope="col" class="text-color-primary">Statement ID</th>' +
        '<th scope="col" class="text-color-primary">Statement Date</th>' +
        '<th scope="col" class="text-color-primary">Previous Balance</th>' +
        '<th scope="col" class="text-color-primary">Current Charges</th>' +
        '<th scope="col" class="text-color-primary">Total</th>' +
        '<th scope="col" class="text-color-primary">Late Fees</th>' +
        '</tr>' +
        '</thead>' +
        '<tbody data-bind="foreach: invoiceList">' +
        '<tr data-bind="component: {' +
        'name: \'invoice\',' +
        'params: { statementID: $data.statementID,' +
        'statementDate: $data.statementDate,' +
        'preBalance: $data.preBalance, ' +
        'curChanges: $data.curCharges,' +
        'total: $data.total,' +
        'lateFees: $data.lateFees}' +
        '}"></tr>' +
        '</tbody>' +
        '<tfoot align= "right" >' +
        '<tr style="background-color: #fff;" class= "table-borderless" > ' +
        '<td colspan="6">' +
        '<span ></span>' +
        '</td> ' +
        '</tr>' +
        '</tfoot >' +
        '</table></div>'
        + '<div style="float: right" data-bind="text: totalRow() > 0 ? \'1 - \' + totalRow() + \' of \' + totalRow() + \' items\' : \'0 item\'"></div>'

});
ko.applyBindings();