<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Madtastisk.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="Library" %>

<asp:Content runat="server" ContentPlaceHolderID="cphHead">
    <link href="<%= "../".Repeat(Page.RouteData.Values.Count - 1) %>Content/AddRecipe.css" rel="stylesheet" type="text/css" />

    <script src="<%= "../".Repeat(Page.RouteData.Values.Count) %>Scripts/AddRecipe.js" type="text/javascript"></script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="cphBody">
    <% Html.RenderPartial("~/Views/Shared/Header.ascx", new Iteration2.Models.HeaderConfiguration { SelectedMenuItem = Iteration2.Models.HeaderConfiguration.MenuItem.AddRecipe }); %>

    <% using(Html.BeginForm()) { %>
        <div id="addrecipe">
            <div id="recipe">
                <dl>
                    <dt>Navn</dt>
                    <dd><%= Html.TextBox("RecipeName") %></dd
                </dl>

                <dl>
                    <dt>Tilberedning</dt>
                    <dd><%= Html.TextArea("RecipePreperation") %></dd>
                </dl>
            </div>

            <div id="ingredients">
                <div id="search">
                    <dl>
                        <dt>Ingredienser</dt>
                        <dd><% Html.RenderPartial("~/Views/Shared/IngredientsSuggest.ascx"); %> <input type="button" value="Tilføj" /></dd>
                    </dl>
                </div>

                <ul>
                    <li></li>
                </ul>
            </div>

            <div id="submitbutton" class="clear">
                <%= Html.SubmitButton("RecipeAdd", "Gem") %>
            </div>
        </div>
    <% } %>
</asp:Content>