﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImprimirNotasDePeso.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Reportes.ImprimirNotasDePeso" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            SelectMethod="GetNotasDePeso" 
            TypeName="COCASJOL.LOGIC.Reportes.ReporteLogic">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="0" Name="NOTAS_ID" 
                    QueryStringField="NOTAS_ID" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

        <div align="center">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" 
                ShowBackButton="False" ShowCredentialPrompts="False" 
                ShowDocumentMapButton="False" ShowFindControls="False" 
                ShowPageNavigationControls="False" ShowParameterPrompts="False" 
                ShowPromptAreaButton="False" SizeToReportContent="True">
                <LocalReport ReportPath="resources\rdlcs\notasDePeso\NotasDePeso.rdlc" OnSubreportProcessing="ReportViewer1_SubreportProcessing">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="NotasDePesoDataSet" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
    </div>
    </form>
</body>
</html>
