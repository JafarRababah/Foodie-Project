<%@ Page Title="" Language="C#" MasterPageFile="~/Users/User.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Foodie.Users.Login" %>
<%@ Import Namespace="Foodie" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%#lblMsg.ClientID%>").style.display = "none";
         }, seconds * 1000);
        };
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_container">
                <div class="align-self-end">
                    <asp:label runat="server" ID="lblMsg"></asp:label>
                </div>
                <h2>Login</h2>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div>
                        <div class="form-container">
                            <img ID="UserLogin" src="../Images/Login.png" alt="" class="img-thumbnail" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div>
                            <div class="form-container">
                                <div>
                                    <asp:RequiredFieldValidator ID="rfvUsername" ControlToValidate="txtUsername" runat="server"
                                        ErrorMessage="Required" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small"></asp:RequiredFieldValidator>
                                  <asp:TextBox ID="txtUsername" runat="server" Cssclass="form-control" placeholder="Enter Username"></asp:TextBox>

                                </div>
                                <div>
                                    <asp:RequiredFieldValidator ID="rfvPassword" ControlToValidate="txtPassword" runat="server"
                                        ErrorMessage="Required" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small"></asp:RequiredFieldValidator>
                                 <asp:TextBox ID="txtPassword" runat="server" Cssclass="form-control" placeholder="Enter Password" TextMode="Password"></asp:TextBox>

                                </div>
                                <div class="btn_box">
                                    <asp:Button ID="btnLogin" runat="server" Text="Login" Cssclass="btn btn-success rounded-pill pl-4 pr-4 text-ahite"
                                        OnClick="btnLogin_Click"/>
                                </div>
                                <span class="pl-3 text-info">New User?<a href="Registiration.aspx" class="badge badge-info">Register Here</a></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        
    </section>
</asp:Content>
