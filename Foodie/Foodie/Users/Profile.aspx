<%@ Page Title="" Language="C#" MasterPageFile="~/Users/User.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Foodie.Users.Profile" %>

<%@ Import Namespace="Foodie" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--  <%
        string UserImage = Session["UserImage"].ToString();
    %>--%>
    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_container">
                <div class="align-self-end">
                    <asp:Label runat="server" ID="lblMsg"></asp:Label>
                </div>
                <h2>User Information</h2>
            </div>
            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="card-title ab-4">
                                <div class="d-flex justify-content-start">
                                    <div class="image-container">
                                        <img src="<%# Foodie.clsUtils.GetImageUrl(Session["UserImage"].ToString())%>" class="img-thumbnail" id="imgProfile" style="width: 150px; height: 150px;" />
                                    </div>
                                    <div class="middle pt-2">
                                        <a href="Registiration.aspx?UserID=<%Response.Write(Session["UserID"]);%>" class="btn btn-warning">
                                            <i class="fa fa-pencil"></i>Edit Details
                                        </a>
                                    </div>
                                </div>
                                <div class="userData ml-3">
                                    <h2 class="d-block" style="font-size: 1.5rem; font-weight: bold">
                                        <a href="javascript:void(0);"><%Response.Write(Session["name"]);%></a>
                                    </h2>
                                    <h6 class="d-block">
                                        <a href="javascript:void(0)">
                                            <asp:Label ID="lblUsername" ToolTip="Unique Username"
                                                runat="server"><%Response.Write(Session["Username"]); %></asp:Label>
                                        </a>
                                    </h6>
                                    <h6 class="d-block">
                                        <a href="javascript:void(0)">
                                            <asp:Label ID="lblEmail" ToolTip="User Email"
                                                runat="server"><%Response.Write(Session["Email"]); %></asp:Label>
                                        </a>
                                    </h6>
                                    <h6 class="d-block">
                                        <a href="javascript:void(0)">
                                            <asp:Label ID="lblCreatedDate" ToolTip="Account Created on"
                                                runat="server"><%Response.Write(Session["CreatedDate"]); %></asp:Label>
                                        </a>
                                    </h6>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <ul class="nav nav-tabs ab-4" id="#yTab" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active text-info" id="basicinfo-tab" data-toggle="tab"
                                            href="#basicinfo" role="tab" aria-controls="basicInfo" aria-selected="true">
                                            <i class="fa da-id-basge mr-2"></i>Basic Info
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link text-info" id="connectedServices-tab" data-toggle="tab" href="#connectedServices"
                                            role="tab" aria-controls="connectedServices" aria-selected="false">
                                            <i class="fa fa-clock-o mr-2>"></i>Purchased History
                                        </a>
                                    </li>
                                </ul>
                                <div class="tab-content #l-1" id="myTabContent"></div>
                                <%-- Basic User Info --%>
                                <div class="tab-pane fade show active" id="basicInfo" role="tabpanel" aria-labelledby="basicInfo-tab">
                                    <asp:Repeater ID="rUserProfile" runat="server">
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="col-sm-3 col-md-2 col-5">
                                                    <label style="font-weight: bold;">Full Name</label>
                                                </div>
                                                <div class="col-md-8 col-6">
                                                    <%#Eval("FullName")%>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <div class="col-sm-3 col-md-2 col-5">
                                                    <label style="font-weight: bold;">Username</label>
                                                </div>
                                                <div class="col-md-8 col-6">
                                                    <%#Eval("Username")%>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <div class="col-sm-3 col-md-2 col-5">
                                                    <label style="font-weight: bold;">Mobile No.</label>
                                                </div>
                                                <div class="col-md-8 col-6">
                                                    <%#Eval("Mobile")%>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <div class="col-sm-3 col-md-2 col-5">
                                                    <label style="font-weight: bold;">Email</label>
                                                </div>
                                                <div class="col-md-8 col-6">
                                                    <%#Eval("Email")%>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <div class="col-sm-3 col-md-2 col-5">
                                                    <label style="font-weight: bold;">Post Code</label>
                                                </div>
                                                <div class="col-md-8 col-6">
                                                    <%#Eval("PostCode")%>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <div class="col-sm-3 col-md-2 col-5">
                                                    <label style="font-weight: bold;">Address</label>
                                                </div>
                                                <div class="col-md-8 col-6">
                                                    <%#Eval("Address")%>
                                                </div>
                                            </div>
                                            <hr />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <%-- End Basic User Info --%>
                                <%-- start History --%>
                                <div class="tab-pane fade" id="connectedServices" role="tabpanel" aria-labelledby="ConnectedServices-tab">
                                    <asp:Repeater ID="rPurchaseHistory" runat="server" OnItemDataBound="rPurchaseHistory_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="container">
                                                <div class="row pt-1 pb-1" style="background-color: lightgray">
                                                    <div class="col-4">
                                                        <span class="badge badge-pill badge-danger text-white">
                                                            <%#Eval("SrNo") %>
                                                        </span>
                                                        Payment Mode:<%#Eval("PaymentMode").ToString()=="cod"?"Cash on Delivery":Eval("PaymentMode").ToString().ToUpper() %></div>
                                                    <div class="col-6">
                                                        <%#string.IsNullOrEmpty( Eval("CardNo").ToString())?"":"CardNo: "+ Eval("CardNo")%>
                                                    </div>
                                                    <div class="col-2" style="text-align:end">
                                                        <a href="Invoice.aspx?PaymentID=<%#Eval("PaymentID")%>"><i class="fa fa-download mr-2"></i>Invoice</a>
                                                    </div>
                                                </div>
                                                <asp:HiddenField ID="hdnPaymentID" runat="server" Value='<%#Eval("PaymentID")%>'/>
                                                <asp:Repeater ID="rOrders" runat="server">
                                                    <HeaderTemplate>
                                                        <table>
                                                            <thead>
                                                                <tr>
                                                                    <th>ProductName</th>
                                                                    <th>Unit Price</th>
                                                                    <th>Qty</th>
                                                                    <th>Total Price</th>
                                                                    <th>OrderId</th>
                                                                    <th>Status</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ProductName")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%#string.IsNullOrEmpty( Eval("Price").ToString())?"":"$"+ Eval("Price")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("TotalPrice") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("OrderNo") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text='<%#Eval("Status") %>'
                                                                    Cssclass='<%#Eval("Status").ToString()=="Delivered"?"badge badge-success":"badge badge-warning"%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody>
                                                         </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <%-- End History --%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
