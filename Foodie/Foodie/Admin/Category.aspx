﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="Foodie.Admin.Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%#lblMsg.ClientID%>").style.display = "none";
        }, seconds * 1000);
        };
    </script>
     <script>
     function ImagePreview(input) {
         if (input.files && input.files[0]) {
             var reader = new FileReader();
             reader.onload = function (e) {
                 $('#<%=imagePreview.ClientID%>').prop('src', e.target.result)
                     .width(200)
                     .height(200);
             };
             reader.readAsDataURL(input.files[0]);
         }
     }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="pcoded-inner-content">
        <div class ="align-align-self-end">
            <asp:Label ID="lblMsg" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
        <div class="main-body">
            <div class="page-wrapper">

                <div class="page-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="card">
                                <div class="card-header">
                                </div>
                                <div class="card-block">
                                    <div class="row">
                                        <div class="col-sm-6 col-md-4 col-lg-4">
                                            <h4 class="sub-title">Category</h4>
                                            <div class="form-group">
                                                <label>Category Name</label>
                                                <div>


                                                    <asp:TextBox ID="txtCategoryName" CssClass="form-control" placeholder="Enter Category Name" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCategoryName" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Required" ControlToValidate="txtCategoryName"></asp:RequiredFieldValidator>
                                                    </div>
                                            </div>
                                            <div class="form-group">
                                                <label>Category Image</label>
                                                <div>
                                                    <asp:FileUpload ID="fuCategoryImage" CssClass="form-control" runat="server"
                                                        onchange="ImagePreview(this);" />
                                                    <asp:HiddenField ID="hfCategoryID" runat="server" Value="0" />
                                                </div>
                                            </div>


                                            <div class="form-check pl-4">
                                                <asp:CheckBox ID="cbIsActive" runat="server" Text="&nbsp; IsActive" CssClass="form-check-input" />
                                            </div>

                                            <div class="pb-5">
                                                <div class="text-left">
                                                    <asp:Button ID="btnAddOrUpdate" runat="server" CssClass="btn btn-info" Text="Add"  CausesValidation="True" OnClick="btnAddOrUpdate_Click" />
                                                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-dark" Text="Reset" CausesValidation="false" OnClick="btnClear_Click" />
                                                </div>

                                                <div>
                                                    <asp:Image ID="imagePreview" CssClass="img-thumbnail" runat="server" />

                                                </div>
                                            </div>
                                        </div>
                                    <div class="col-sm-6 col-md-8 col-lg-8 mobile-inputs">
     <h4 class="sub-title">Category List</h4>
                                        <div class="card-block table-border-style">
                                            <div class="table-responsive">
                                                <asp:Repeater ID="rCategory" runat="server">
                                                    <HeaderTemplate>
                                                        <table>
                                                            <tr>
                                                                <th>Name</th>
                                                                <th>Image</th>
                                                                <th>IsAcitve</th>
                                                                <th>CreateDate</th>
                                                                <th>Action</th>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%#Eval("CategoryName")%></td>
                                                            <td><%#Eval("CategoryImage")%></td>
                                                            <td><%#Eval("IsActive")%></td>
                                                            <td><%#Eval("CreateDate")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>

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
