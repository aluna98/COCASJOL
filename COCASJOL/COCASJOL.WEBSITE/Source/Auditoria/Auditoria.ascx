<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Auditoria.ascx.cs" Inherits="COCASJOL.WEBSITE.Source.Auditoria.Auditoria" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<ext:XScript runat="server">
    <script type="text/javascript">
       
      
       Ext.onReady(function(){
            /// <summary>Inicializacion para páginas que contienen PageX, crea la funcion  PageX.showAudit()</summary>
            if ( PageX != undefined )
            {
                PageX.showAudit = function () {
                    var record = Grid.getSelectionModel().getSelected();
                    var index = Grid.store.indexOf(record);
                    this.setIndex(index);
                    var record = this.getRecord();

                    if ( record != undefined )
                        showAudit(record.data);
                };
            }
            if ( Grid != undefined ) {
                Grid.getSelectionModel().addListener('rowselect', function (t, i, r) { updagteAudit(r.data) });
            }
         });

         var initAudit = function(pageController){
         /// <summary>Inicializacion para páginas que continen no contienen PageX, crea la funcion  pageController.showAudit()</summary>
         if ( pageController != undefined )
            {
                pageController.showAudit = function () {
                var record = this.getRecord();
                if ( record != undefined )
                    showAudit(record.data);

                };
            }
           
         }
        
        var showAudit = function (record) {
            /// <summary>Muestra la auditoria del registro</summary>
            /// <param name="record">data del registro</param>
            if ( record.data != undefined )
                record = record.data;
            #{AuditoriaWin}.show();
            #{AuditFormP}.getForm().findField("CREADO_POR").setValue(record.CREADO_POR);
            #{AuditFormP}.getForm().findField("FECHA_CREACION").setValue(record.FECHA_CREACION);
            #{AuditFormP}.getForm().findField("MODIFICADO_POR").setValue(record.MODIFICADO_POR);
            #{AuditFormP}.getForm().findField("FECHA_MODIFICACION").setValue(record.FECHA_MODIFICACION);
            
        };

        var updagteAudit = function (record) {
            if ( record.data != undefined )
                record = record.data;
            #{AuditFormP}.getForm().findField("CREADO_POR").setValue(record.CREADO_POR);
            #{AuditFormP}.getForm().findField("FECHA_CREACION").setValue(record.FECHA_CREACION);
            #{AuditFormP}.getForm().findField("MODIFICADO_POR").setValue(record.MODIFICADO_POR);
            #{AuditFormP}.getForm().findField("FECHA_MODIFICACION").setValue(record.FECHA_MODIFICACION);
            
        };

    </script>
    
</ext:XScript>
<ext:Window ID="AuditoriaWin" runat="server" Hidden="true" Icon="CogGo" Title="Auditoria"
    Width="500" Layout="FormLayout" AutoHeight="True" Resizable="false" Shadow="None" Modal="true"
    LabelWidth="200" X="50" Y="70">
    <Listeners>
        <show handler="#{AuditFormP}.getForm().reset();" />
    </Listeners>
    <Items>
        <ext:FormPanel ID="AuditFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right">
            <Items>
                <ext:Panel ID="Panel5" runat="server" Layout="AnchorLayout" AutoHeight="True" Resizable="false"
                    Header="false" Title="Title">
                    <Items>
                        <ext:Panel ID="Panel21" runat="server" Frame="false" Padding="5" Layout="AnchorLayout"
                            LabelWidth="130" Border="false">
                            <Items>
                                <ext:TextField runat="server" ID="CreatedByTxt" DataIndex="CREADO_POR" LabelAlign="Right"
                                    FieldLabel="Creado Por" AnchorHorizontal="90%" ReadOnly="true">
                                </ext:TextField>
                                <ext:DateField runat="server" ID="CreatedDateTxt" DataIndex="FECHA_CREACION" LabelAlign="Right"
                                    FieldLabel="Fecha de Creación" AnchorHorizontal="90%" ReadOnly="true">
                                </ext:DateField>
                                <ext:TextField runat="server" ID="UpdatedByTxt" DataIndex="MODIFICADO_POR" LabelAlign="Right"
                                    FieldLabel="Modificado Por" AnchorHorizontal="90%" ReadOnly="true">
                                </ext:TextField>
                                <ext:DateField runat="server" ID="UpdatedDateTxt" DataIndex="FECHA_MODIFICACION"
                                    LabelAlign="Right" FieldLabel="Fecha de Modificación" AnchorHorizontal="90%"
                                    ReadOnly="true">
                                </ext:DateField>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:FormPanel>
    </Items>
</ext:Window>
