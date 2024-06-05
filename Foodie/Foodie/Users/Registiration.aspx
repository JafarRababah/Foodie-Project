<%@ Page Title="" Language="C#" MasterPageFile="~/Users/User.Master" AutoEventWireup="true" CodeBehind="Registiration.aspx.cs" Inherits="Foodie.Users.Registiration" %>

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
    <section class="book_section layout_Padding">
        <div class="container">
            <div class="heading_container">
                <div class="align-self-end">
                    <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
                </div>
                <asp:Label ID="lblHeaderMsg" runat="server" Text="<h2>User Registration</h2>"></asp:Label>

            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="form_container">
                        <div>

                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Required"
                                ControlToValidate="txtName" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revName" runat="server" ErrorMessage="Must Be Characters only"
                                ControlToValidate="txtName" Display="Dynamic" ValidationExpression="^[a-zA-Z\s]+$" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtName" CssClass="form-control" placeholder="Enter Full Name"
                                ToolTip="Full Name" runat="server"></asp:TextBox>
                        </div>
                        <div>

                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ErrorMessage="Required"
                                ControlToValidate="txtUsername" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtUsername" CssClass="form-control" placeholder="Enter UserName"
                                ToolTip="UserName" runat="server"></asp:TextBox>
                        </div>
                        <div>

                            <asp:RequiredFieldValidator ID="rvfEmail" runat="server" ErrorMessage="Required"
                                ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtEmail" CssClass="form-control" placeholder="Enter Email"
                                ToolTip="Email" runat="server" TextMode="Email"></asp:TextBox>
                        </div>
                        <div>

                            <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ErrorMessage="Required"
                                ControlToValidate="txtMobile" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revMobile" runat="server" ErrorMessage="Must Be 10 digits"
                                ControlToValidate="txtMobile" Display="Dynamic" ValidationExpression="^[0-9]{10}$" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtMobile" CssClass="form-control" placeholder="Enter Mobile Number"
                                ToolTip="UserName" runat="server"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="col-md-6">
                    <div class="form_container">
                        <div>

                            <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ErrorMessage="Required"
                                ControlToValidate="txtAddress" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtAddress" CssClass="form-control" placeholder="Enter Address"
                                ToolTip="Address" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div>

                            <asp:RegularExpressionValidator ID="revPostCode" runat="server" ErrorMessage="Must Be 6 digits"
                                ControlToValidate="txtPostCode" Display="Dynamic" ValidationExpression="^[0-9]{6}$" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtPostCode" CssClass="form-control" placeholder="Enter Post Code"
                                ToolTip="PostCode" runat="server" TextMode="Number"></asp:TextBox>

                        </div>
                        <div>
                            <asp:FileUpload ID="fuUserImage" CssClass="form-control" ToolTip="User Image" onOmage="imagePreview(this);" runat="server" />

                        </div>
                        <div>

                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Required"
                                ControlToValidate="txtPassword" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtPassword" CssClass="form-control" placeholder="Enter Password"
                                ToolTip="Password" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row pl-4">
                    <div class="btn_box">
                        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-success rounded-pill pl-4 text-ahite" OnClick="btnRegister_Click" />
                        <asp:Label ID="lblAlreadyUser" runat="server" Text="Already registered?<a href='Login.aspx' class='badge badge-info'>Login Here..</a>"></asp:Label>
                    </div>
                </div>
                <div class="row p-5">
                    <div style="align-items: center">
                        <asp:Image ID="imagePreview" runat="server" CssClass="img-thumbnail" />
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
