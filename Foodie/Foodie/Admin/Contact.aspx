<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Foodie.Admin.Contact" %>
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
                                         <h4 class="sub-title">Contacts List</h4>
                                        <div class="card-block table-border-style">
                                            <div class="table-responsive">
                                                 <asp:Repeater ID="rContacts" runat="server" OnItemCommand="rContacts_ItemCommand">
     
     <HeaderTemplate>
         <table class="table datatable-export table-hover nowrap">
             <thead>
             <tr>
                 <th class="table-plus">SrNo</th>
                 <th>User Name</th>
                 <th>Email</th>
                 <th>Subject</th>
                 <th>Messsage</th>
                 <th>Contact Date</th>
                 <th class="dataTable-nosort">Delete</th>
             </tr>
         </thead>
             <tbody>
     </HeaderTemplate>
     <ItemTemplate>
         <tr>
             <td class="table-plus"><%#Eval("SrNo")%></td>
             <td><%#Eval("FullName")%></td>
             <td><%#Eval("Email")%></td>
             <td><%#Eval("Subject")%></td>
             <td><%#Eval("Message")%></td>
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
           CommandArgument='<%# Eval("ContactID") %>' CommandName="delete" CausesValidation="False"  onClientClick="return confirm('Do you want to delete this contact?');">
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
