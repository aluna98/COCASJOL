<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SolicitudPrestamo.aspx.cs" Inherits="COCASJOL.Website.Source.Prestamos.SolicitudPrestamo" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="RM1" runat="server">
            <Listeners>
                <DocumentReady />
            </Listeners>
        </ext:ResourceManager>
        <ext:Hidden ID="LoggedUserHdn" runat="server"/>
        <ext:Viewport ID="view1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="panel1" runat="server" Frame="false" Header="false" Title="Solicitudes" Icon="Money" Layout="FitLayout">
                    <Items>
                        <ext:GridPanel ID="SolicitudesGriP" runat="server" Height="300" Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
                            <Store>
                                <ext:Store ID="SolicitudesSt" runat="server" SkipIdForNewRecords="false" AutoSave="false" WarningOnDirty="false" OnRefreshData="SolicitudesSt_Reload">
                                    <Reader>
                                        <ext:JsonReader IDProperty="SOLICITUDES_ID">
                                            <Fields>
                                                <ext:RecordField Name="SOCIOS_ID"/>
                                                <ext:RecordField Name="SOLICITUDES_ID" />
                                                <ext:RecordField Name="SOLICITUDES_MONTO" />
                                                <ext:RecordField Name="SOLICITUDES_INTERES" />
                                                <ext:RecordField Name="SOLICITUDES_PLAZO" Type="Date" />
                                                <ext:RecordField Name="SOLICITUDES_PAGO" />
                                                <ext:RecordField Name="SOLICITUDES_DESTINO" />
                                                <ext:RecordField Name="PRESTAMOS_ID" />
                                                <ext:RecordField Name="SOLICITUDES_CARGO" />
                                                <ext:RecordField Name="SOLICITUDES_PROMEDIO3" />
                                                <ext:RecordField Name="SOLICITUDES_PRODUCCIONACT" />
                                                <ext:RecordField Name="SOLICITUDES_NORTE" />
                                                <ext:RecordField Name="SOLICITUDES_SUR" />
                                                <ext:RecordField Name="SOLICITUDES_ESTE" />
                                                <ext:RecordField Name="SOLICITUDES_OESTE" />
                                                <ext:RecordField Name="SOLICITUDES_VEHICULO" />
                                                <ext:RecordField Name="SOLICITUDES_AGUA" />
                                                <ext:RecordField Name="SOLICITUDES_ENEE" />
                                                <ext:RecordField Name="SOLICITUDES_CASA" />
                                                <ext:RecordField Name="SOLICITUDES_BENEFICIO" />
                                                <ext:RecordField Name="SOLICITUD_OTROSCULTIVOS" />
                                                <ext:RecordField Name="SOLICITUD_CALIFICACION" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </ext:Store>
                            </Store>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
