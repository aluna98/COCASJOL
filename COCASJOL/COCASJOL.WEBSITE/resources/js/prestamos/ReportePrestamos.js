
var PageX = {
    getNombreDeSocio: function (sociosIdTxt, nombreTxt) {
        var comboBox = sociosIdTxt, value = comboBox.getValue();
        record = comboBox.findRecord(comboBox.valueField, value), index = comboBox.getStore().indexOf(record);
        var nombreCompleto = record.data.SOCIOS_PRIMER_NOMBRE +
                                         (record.data.SOCIOS_PRIMER_APELLIDO !== '' ? (' ' + record.data.SOCIOS_PRIMER_APELLIDO) : '');
        nombreTxt.setValue(nombreCompleto);
    }
};