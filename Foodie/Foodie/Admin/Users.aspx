<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Foodie.Admin.Users" %>
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
    <%--<script>
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
     </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pcoded-inner-content">
        <div class ="align-align-self-end">
            <asp:Label ID="lblMsg" runat="server" Text=">>" visible="false"></asp:Label>
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
                                  
                                    <div class="col-12 mobile-inputs">
                                         <h4 class="sub-title">Users List</h4>
                                        <div class="card-block table-border-style">
                                            <div class="table-responsive">
                                                <asp:Repeater ID="rUser" runat="server" OnItemCommand="rUser_ItemCommand">
                                                    
                                                    <HeaderTemplate>
                                                        <table class="table datatable-export table-hover nowrap">
                                                            <thead>
                                                            <tr>
                                                                <th class="table-plus">SrNo</th>
                                                                <th>Full Name</th>
                                                                <th>Username</th>
                                                                <th>Email</th>
                                                                <th>Joined Date</th>
                                                                <th class="dataTable-nosort">Delete</th>
                                                            </tr>
                                                        </thead>
                                                            <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="table-plus"><%#Eval("SrNo")%></td>
                                                            <td><%#Eval("FullName")%></td>
                                                            <td><%#Eval("Username")%></td>
                                                            <td><%#Eval("Email")%></td>
                                                            <td><%#Eval("CreatedDate")%></td>
                                                           <%-- <td>
                                                              <img width="40" src="<%# Foodie.clsUtils.GetImageUrl(Eval("CategoryImage")) %>" alt="image" />
                                                            </td>--%>
                                                           <%-- <td>
                                                                <asp:Label ID="lblIsActive" runat="server" Text="<%Eval("IsActive")%>"></asp:Label>
                                                            </td>--%>
                                                            <%--<td><%#Eval("IsActive")%></td>--%>
                                                            
                                                            <td>   
                                                         <asp:LinkButton ID="lbDelete" runat="server" CssClass="badge badge-danger" 
                                                          CommandArgument='<%# Eval("UserID") %>' CommandName="delete" CausesValidation="False"  onClientClick="return confirm('Do you want to delete this user?');">
                                                          <i class="ti-trash"></i></asp:LinkButton>

      
                                                        </td>
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
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
