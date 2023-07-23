<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="MarksDetail.aspx.cs" Inherits="WebApplication1.Admin.MarksDetail" %>
<%@ Register  Src="~/MarksDetailUserControl.ascx" TagPrefix="uc" TagName="MarksDetail" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <uc:MarksDetail  runat="server" ID="MarksDetail1"/>


</asp:Content>
