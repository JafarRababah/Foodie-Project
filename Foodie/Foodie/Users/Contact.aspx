﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Users/User.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Foodie.Users.Contact" %>
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
     <!-- book section -->
  <section class="book_section layout_padding">
    <div class="container">
      <div class="heading_container">
          <div class="align-self-lg-end">
             <asp:Label ID="lblMsg" runat="server"></asp:Label>
          </div>
        <h2>
          Send Your Query
        </h2>
      </div>
      <div class="row">
        <div class="col-md-6">
          <div class="form_container">
            
              <div>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="Your Name"></asp:TextBox>
             <asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic"
                 ErrorMessage="Required" SetFocusOnError="true" ControlToValidate="txtName" ForeColor="red"></asp:RequiredFieldValidator>
              </div>
              <div>
                   <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Your Email" TextMode="Email"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="Dynamic" 
    ErrorMessage="Required" SetFocusOnError="true" ControlToValidate="txtEmail" ForeColor="red"></asp:RequiredFieldValidator>
              </div>
              <div>
                   <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="Subject"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvSubject" runat="server" Display="Dynamic"
    ErrorMessage="Required" SetFocusOnError="true" ControlToValidate="txtSubject" ForeColor="red"></asp:RequiredFieldValidator>
              </div>
                            <div>
                   <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Enter Your Feedback"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvMessage" runat="server" Display="Dynamic"
    ErrorMessage="Required" SetFocusOnError="true" ControlToValidate="txtMessage" ForeColor="red"></asp:RequiredFieldValidator>
              </div>
              
             
              <div class="btn_box">
         <asp:Button ID="btnSubmit" runat="server" Text="Submit"
             CssClass="btn btn-warning rounded-pill pl-4 pr-4 text-white" OnClick="btnSubmit_Click" />
                
              </div>
            
          </div>
        </div>
        <div class="col-md-6">
          <div class="map_container ">
            <div id="googleMap"></div>
          </div>
        </div>
      </div>
    </div>
  </section>
  <!-- end book section -->
</asp:Content>
