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
                    <asp:label runat="server" ID="lblMsg"></asp:label>
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
                                        <itemtemplate>
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
                                        </itemtemplate>
                                    </asp:Repeater>
                                </div>
                                     <%-- End Basic User Info --%>
                                <%-- start History --%>
                                <div class="tab-pane fade" id="connectedServices" role="tabpanel" aria-labelledby="ConnectedServices-tab">
                                    <h3>Order History</h3>
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
