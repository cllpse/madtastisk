<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Madtastisk.Master" Inherits="System.Web.Mvc.ViewPage<Iteration3.Models.RecipeDTO>" %>
<%@ Import Namespace="Library" %>

<asp:Content runat="server" ContentPlaceHolderID="cphHead">
    <link href="<%= "../".Repeat(Page.RouteData.Values.Count - 1) %>Content/ShowRecipe.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="cphBody">
    <% Html.RenderPartial("~/Views/Shared/Header.ascx", new Iteration3.Models.HeaderConfiguration { SelectedMenuItem = Iteration3.Models.HeaderConfiguration.MenuItem.None }); %>
    
    <div id="showrecipe">
        <% if (Model != null) { %>
            <h1><%: Model.Name %></h1>

            <% if (Model.Ingredients.Count > 0) { %>
                <ul>
                <% foreach (var i in Model.Ingredients) { %>
                    <li><%: i.Name %> <%: i.Amount == Double.MinValue ? "" : i.Amount.ToString() %> <%: i.Unit %></li>
                <% } %>
                </ul>
            <% } %>

            <h2>Tilberedning</h2>
            
            <div>
                <%= Model.Description.Replace(Environment.NewLine, "<br />") %>
            </div>
        <% } %>
    </div>
</asp:Content>