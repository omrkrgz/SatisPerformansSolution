
    function kendoGridSecilileriAl(gridAdi) {
        var ilgiliGridData = null;
        if (typeof gridAdi === 'string') {
            ilgiliGridData = $('#' + gridAdi).data('kendoGrid');
        } else {
            ilgiliGridData = gridAdi;

        }
        var satirVerileri = new Array();
        if (ilgiliGridData == undefined || ilgiliGridData == null) {
            return satirVerileri;
        }
        ilgiliGridData.select().each(function () {
            satirVerileri.push(ilgiliGridData.dataItem($(this)));
        });
        if (satirVerileri.length == 0) {
            return null;
        }
        if (satirVerileri.length == 1) {
            return satirVerileri[0];
        }
        return satirVerileri;
}

function ResizeGrid(selector, extra) {
    var body = document.body,
        html = document.documentElement;
    var yukseklik = Math.max(body.scrollHeight, body.offsetHeight,
        html.clientHeight, html.scrollHeight, html.offsetHeight);
    var extraYukseklik = 0;
    if (extra != undefined && extra > 0) {
        extraYukseklik = extra;
    }

    yukseklik = yukseklik - $(".main-header").height() - extraYukseklik - 200;
    $(selector).css("height", yukseklik);
}