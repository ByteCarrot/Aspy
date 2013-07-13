<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Aspy.Web.Tests.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Aspy DEMO</title>
    <style type="text/css">
        .added {
            margin-left: 10px;
            font-weight: bold;
            color: #00b300;
        }
    </style>
</head>
<body>
    <form id="form" runat="server">
    <div>
        <h1>
            Aspy - ASP.NET Session and Cache viewer</h1>
        <div>
            Download it: <a href="http://nuget.org/packages/Aspy">http://nuget.org/packages/Aspy</a></div>
        <div>
            Go to Aspy handler: <a href="Aspy.ashx">Aspy.ashx</a></div>
        <div>
            <h3>
                ASP.NET Session</h3>
            <div>
                <asp:Button ID="_sessionSimpleValue" runat="server" Text="Add simple value" 
                    onclick="_sessionSimpleValue_Click" /> 
                <asp:Label runat="server" ID="_sessionSimpleValueLabel" Visible="False" EnableViewState="False" CssClass="added">Added</asp:Label></div>
            <div>
                <asp:Button ID="_sessionDictionary" runat="server" Text="Add dictionary" 
                    onclick="_sessionDictionary_Click" /> 
                <asp:Label runat="server" ID="_sessionDictionaryLabel" Visible="False" EnableViewState="False" CssClass="added">Added</asp:Label></div>
            <div>
                <asp:Button ID="_sessionComplexObject" runat="server" Text="Add complex object" 
                    onclick="_sessionComplexObject_Click" /> 
                <asp:Label runat="server" ID="_sessionComplexObjectLabel" Visible="False" EnableViewState="False" CssClass="added">Added</asp:Label></div>
        </div>
        <div>
            <h3>
                ASP.NET Cache</h3>
            <div>
                <asp:Button ID="_cacheSimpleValue" runat="server" Text="Add simple value" 
                    onclick="_cacheSimpleValue_Click" /> 
                <asp:Label runat="server" ID="_cacheSimpleValueLabel" Visible="False" EnableViewState="False" CssClass="added">Added</asp:Label></div>
            <div>
                <asp:Button ID="_cacheDictionary" runat="server" Text="Add dictionary" 
                    onclick="_cacheDictionary_Click" /> 
                <asp:Label runat="server" ID="_cacheDictionaryLabel" Visible="False" EnableViewState="False" CssClass="added">Added</asp:Label></div>
            <div>
                <asp:Button ID="_cacheComplexObject" runat="server" Text="Add complex object" 
                    onclick="_cacheComplexObject_Click" /> 
                <asp:Label runat="server" ID="_cacheComplexObjectLabel" Visible="False" EnableViewState="False" CssClass="added">Added</asp:Label></div>
        </div>
    </div>
    </form>
</body>
</html>
