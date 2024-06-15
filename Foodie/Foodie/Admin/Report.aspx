<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="Foodie.Admin.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pcoded-inner-content pt-0">
        <div class="align-align-self-end">
            <asp:Label ID="lblMsg" runat="server" Text=">>" Visible="false"></asp:Label>
        </div>
        <div class="main-body">
            <div class="page-wrapper">

                <div class="page-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="card">
                                <div class="card-header">
                                    <div class="container">
                                        <div class="form-row">
                                            <div class="form-group col-md-4">
                                                <label>From Date:</label><br />
                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" SetFocusOnError="true" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date" CssClass="from-control" style="width:300px"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label>To Date:</label><br />
                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" SetFocusOnError="true" ControlToValidate="txtToDate"
                                                    ErrorMessage="Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtToDate" runat="server" TextMode="Date" CssClass="from-control" style="width:300px"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary mt-md-4" OnClick="btnSearch_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-block">
                                    <div class="row">

                                        <div class="col-12 mobile-inputs">
                                            <h4 class="sub-title">Selling Report</h4>
                                            <div class="card-block table-border-style">
                                                <div class="table-responsive">
                                                    <asp:Repeater ID="rReport" runat="server">

                                                        <HeaderTemplate>
                                                            <table class="table datatable-export table-hover nowrap">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="table-plus">SrNo</th>
                                                                        <th>User Name</th>
                                                                        <th>Email</th>
                                                                        <th>Item Order</th>
                                                                        <th>Total Cost</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="table-plus"><%#Eval("SrNo")%></td>
                                                                <td><%#Eval("FullName")%></td>
                                                                <td><%#Eval("Email")%></td>
                                                                <td><%#Eval("TotalOrders")%></td>
                                                                <td><%#Eval("TotalPrice")%></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody>
                                                             </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row pl-4">
                                        <asp:Label ID="lblTotal" runat="server" Font-Bold="true" Font-Size="Small"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
