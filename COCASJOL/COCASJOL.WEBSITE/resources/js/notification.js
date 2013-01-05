var viewClick = function (dv, e) {
    var group = e.getTarget("h2.notification-selector");

    if (group) {
        Ext.fly(group).up("div").toggleClass("collapsed");
        Ext.select("#notifications-ct tr.l-" + group.innerHTML).toggleClass("collapsed");
    }
};

var nodeClick = function (view, index, node, e) {
    var nd = Ext.fly(node).first("td");

    DataViewContextMenu.node = {
        NotificacionId: nd.getAttributeNS("", "notifID"),
        Usuario: nd.dom.innerHTML,
        Estado: nd.getAttributeNS("", "estado"),
        Titulo: nd.getAttributeNS("", "title"),
        Mensaje: nd.getAttributeNS("", "message")
    };

    DataViewContextMenu.showAt(e.getXY());
};