<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Madtastisk.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="Library" %>

<asp:Content runat="server" ContentPlaceHolderID="cphHead">
    <link href="<%= "../".Repeat(Page.RouteData.Values.Count - 1) %>Content/AddRecipe.css" rel="stylesheet" type="text/css" />

    <script src="<%= "../".Repeat(Page.RouteData.Values.Count) %>Scripts/AddRecipe.js" type="text/javascript"></script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="cphBody">
    <% Html.RenderPartial("~/Views/Shared/Header.ascx", new Iteration3.Models.HeaderConfiguration { SelectedMenuItem = Iteration3.Models.HeaderConfiguration.MenuItem.AddRecipe }); %>

    <% using(Html.BeginForm()) { %>
        <div id="addrecipe">
            <div id="recipe">
                <dl>
                    <dt><h1>Navn</h1></dt>
                    <dd><%= Html.TextBox("RecipeName") %></dd
                </dl>

                <dl>
                    <dt><h1>Tilberedning</h1></dt>
                    <dd><%= Html.TextArea("RecipePreperation") %></dd>
                </dl>
            </div>

            <div id="ingredients">
                <div id="search">
                    <dl>
                        <dt><h1>Ingredienser</h1></dt>
                        <dd>
                            <div class="info">
                                <p class="icon">?</p>

                                <p>
                                    Tilføj ingredienser til din opskrift herunder<br />
                                    For eksempel <strong>"tomat 4 stk, ost 200 g, osv."</strong>
                                </p>
                            </div>
                            
                            <% Html.RenderPartial("~/Views/Shared/IngredientsSuggest.ascx"); %> <input type="button" value="Tilføj" />
                        </dd>
                    </dl>
                </div>

                <ul>
                    <li></li>
                </ul>
            </div>

            <div id="submitbutton" class="clear">
                <%= Html.SubmitButton("RecipeAdd", "Del min opskrift") %>
            </div>
        </div>
    <% } %>
</asp:Content>