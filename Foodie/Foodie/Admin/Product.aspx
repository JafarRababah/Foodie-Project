<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="Foodie.Admin.Product" %>

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
        <div class="align-align-self-end">
            <asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
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
                                            <h4 class="sub-title">Product</h4>
                                            <div class="form-group">
                                                <label>Product Name</label>
                                                <div>


                                                    <asp:TextBox ID="txtProductName" CssClass="form-control" placeholder="Enter Product Name" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvProductName" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Required" ControlToValidate="txtProductName"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label>Product Description</label>
                                                <div>
                                                    <asp:TextBox ID="txtDescription" CssClass="form-control" placeholder="Enter Description" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvDescription" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Required" ControlToValidate="txtDescription"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label>Product Price</label>
                                                <div>
                                                    <asp:TextBox ID="txtProductPrice" CssClass="form-control" placeholder="Enter Product Price" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvProductPrice" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Required" ControlToValidate="txtProductPrice"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator runat="server" ErrorMessage="Price must be decimal" ControlToValidate="txtProductPrice" ValidationExpression="^\d{0,8}(\.\d{1,4})?$"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label>Product Quantity</label>
                                                <div>
                                                    <asp:TextBox ID="txtQuantity" CssClass="form-control" placeholder="Enter Quantity" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvQuantity" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Required" ControlToValidate="txtQuantity"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator runat="server" ErrorMessage="Price must be decimal" ControlToValidate="txtQuantity" ValidationExpression="^([1-9]\d*|0)$"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label>Product Image</label>
                                                <div>
                                                    <asp:FileUpload ID="fuProductImage" CssClass="form-control" runat="server"
                                                        onchange="ImagePreview(this);" />
                                                    <asp:HiddenField ID="hfProductID" runat="server" Value="0" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label>Product Category</label>
                                                <div>
                                                    <asp:DropDownList ID="ddlCategories" CssClass="form-control" runat="server" 
                                                        DataSourceID="sqlCategories" DataTextField="CategoryName" 
                                                        DataValueField="CategoryID" AppendDataBoundItems="true">
                                                        <asp:ListItem value="0">Select Category</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvProductCategory" ForeColor="Red"
                                                        Font-Size="Small" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Required"
                                                        ControlToValidate="ddlCategories" initialValue="0"></asp:RequiredFieldValidator>
                                                    <asp:SqlDataSource ID="sqlCategories" runat="server" ConnectionString="<%$ ConnectionStrings:FoodieDBConnectionString %>" ProviderName="<%$ ConnectionStrings:FoodieDBConnectionString.ProviderName %>" SelectCommand="SELECT [CategoryID], [CategoryName] FROM [tblCategories]"></asp:SqlDataSource>
                                                    
                                                </div>
                                            </div>

                                            <div class="form-check pl-4">
                                                <asp:CheckBox ID="cbIsActive" runat="server" Text="&nbsp; IsActive" CssClass="form-check-input" />
                                            </div>

                                            <div class="pb-5">
                                                <div class="text-left">
                                                    <asp:Button ID="btnAddOrUpdate" runat="server" CssClass="btn btn-info" Text="Add" CausesValidation="True" OnClick="btnAddOrUpdate_Click" />
                                                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-dark" Text="Reset" CausesValidation="false" OnClick="btnClear_Click" />
                                                </div>

                                                <div>
                                                    <asp:Image ID="imagePreview" CssClass="img-thumbnail" runat="server" />

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-md-8 col-lg-8 mobile-inputs">
                                            <h4 class="sub-title">Product List</h4>
                                            <div class="card-block table-border-style">
                                                <div class="table-responsive">
                                                    <asp:Repeater ID="rProduct" runat="server" OnItemCommand="rProduct_ItemCommand">

                                                        <headertemplate>
                                                            <table class="table datatable-export table-hover nowrap">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="table-plus">Name</th>
                                                                        <th>Image</th>
                                                                        <th>Price</th>
                                                                        <th>Qty</th>
                                                                        <th>Category</th>
                                                                        <th>IsAcitve</th>
                                                                        <th>Description</th>
                                                                        <th>CreateDate</th>
                                                                        <th class="dataTable-nosort">Action</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                        </headertemplate>
                                                        <itemtemplate>
                                                            <tr>
                                                                <td class="table-plus"><%#Eval("ProductName")%></td>
                                                                
                                                                <td>
                                                                    <img width="40" src="<%# Foodie.clsUtils.GetImageUrl(Eval("ProductImage")) %>" alt="image" />
                                                                </td>
                                                                <%-- <td>
                                                                <asp:Label ID="lblIsActive" runat="server" Text="<%Eval("IsActive")%>"></asp:Label>
                                                            </td>--%>
                                                                <td><%#Eval("Price")%></td>
                                                                <td><%#Eval("Quantity")%></td>
                                                                <td><%#Eval("CategoryName")%></td>
                                                               <td><%#Eval("IsActive")%></td>
                                                              <td><%#Eval("Description")%></td>
                                                                <td><%#Eval("CreateDate")%></td>
                                                                <td>
                                                                    <asp:LinkButton ID="lbEdit" runat="server" CssClass="badge badge-primary"
                                                                        CommandArgument='<%# Eval("ProductID") %>' CommandName="edit" CausesValidation="False">
                                                                        <i class="ti-pencil"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton ID="lbDelete" runat="server" CssClass="badge badge-danger"
                                                                        CommandArgument='<%# Eval("ProductID") %>' CommandName="delete" CausesValidation="False" onClientClick="return confirm('Do you want to delete this product?');">
                                                                        <i class="ti-trash"></i>
                                                                    </asp:LinkButton>


                                                                </td>
                                                            </tr>
                                                        </itemtemplate>
                                                        <footertemplate>
                                                            </tbody>
                                                        </table>
                                                        </footertemplate>
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
