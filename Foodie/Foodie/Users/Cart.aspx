﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Users/User.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="Foodie.Users.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_container">
                <div class="align-self-end">
                    <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
                </div>
                <h2>Your Shopping Cart</h2>
            </div>
        </div>
        <div class="container">
            <asp:Repeater ID="rCartItem" runat="server" OnItemCommand="rCartItem_ItemCommand" OnItemDataBound="rCartItem_ItemDataBound">
                <HeaderTemplate>
                    <table class="table">
                        <thead>
                            <tr>
                                <th>Product Name</th>
                                <th>Image</th>
                                <th>Unit Price</th>
                                <th>Quantity</th>
                                <th>Total Price</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ProductName") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Image src='<%#Foodie.clsUtils.GetImageUrl(Eval("ProductImage")) %>' runat="server" Width="60" Alt="" />
                        </td>
                        <td>
                            <asp:Label ID="lblPrice" runat="server" Text='<%#Eval("Price") %>'></asp:Label>
                            <asp:HiddenField ID="hdnProductID" runat="server" Value='<%#Eval("ProductID") %>' />
                            <asp:HiddenField ID="hdnQuantity" runat="server" Value='<%#Eval("Quantity") %>' />
                            <asp:HiddenField ID="hdnPrdQuantity" runat="server" Value='<%#Eval("PrdQty") %>' />
                        </td>
                       
                        <td>
                            <div class="product__detail__option">
                                <div class="quantity">
                                    <div class="pro-qty">
                                        <asp:TextBox ID="txtQuantity" runat="server" TextMode="Number" Text='<%#Eval("Quantity")%>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revQuantity" runat="server" ErrorMessage="Must Be 10 digits"
                                            ControlToValidate="txtQuantity" EnableClientScript="true" Display="Dynamic" ValidationExpression="^[0-9]*" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                        </td>
                       
                        <td>
                            <asp:Label ID="lblTotalPrice" runat="server"></asp:Label></td>
                        <td>
                            <asp:LinkButton ID="lbDelete" runat="server" Text="Remove" CommandName="remove"
                                CommandArgument='<%#Eval("ProductID")%>' OnClientClick="return confirm('Do you want to remove this item from cart?');">
                                    <i class="fa fa-close"></i></asp:LinkButton>
                        </td>
                    </tr>

                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td colspan="3"></td>
                        <td class="pl-lg-5">
                            <b>Grand Total:</b>
                        </td>
                        <td><%Response.Write(Session["GrandTotalPrice"]); %></td>
                        <td></td>
                    </tr>
                    <tr>
                    <td colspan="2" class="continue__btn">
                        <a href="Menu.aspx" class="btn btn-info"><i class="fa fa-arrow-circle-left mr-2"></i>Continue Shopping</a>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbUpdateCart" runat="server" CommandName="updateCart" CssClass="btn btn-warning">
                            <i class="fa fa-refresh mr-2">Update Cart</i></asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbCheckout" runat="server" CommandName="checkout" CssClass="btn btn-success">Checkout
                      <i class="fa fa-arrow-circle-right mr-2"></i></asp:LinkButton>
                    </td>
                        </tr>
                    </tbody>
                       </table>
                </FooterTemplate>

            </asp:Repeater>
        </div>
    </section>
</asp:Content>
