<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Madtastisk.Master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<Iteration1.Models.RecipeDTO>>" %>
<%@ Import Namespace="Library" %>

<asp:Content runat="server" ContentPlaceHolderID="cphHead">
    <link href="<%= "../".Repeat(Page.RouteData.Values.Count - 1) %>Content/SearchRecipe.css" rel="stylesheet" type="text/css" />
    
    <script src="<%= "../".Repeat(Page.RouteData.Values.Count) %>Scripts/SearchRecipe.js" type="text/javascript"></script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="cphBody">
    <% Html.RenderPartial("~/Views/Shared/Header.ascx", new Iteration1.Models.HeaderConfiguration { SelectedMenuItem = Iteration1.Models.HeaderConfiguration.MenuItem.SearchRecipe }); %>
    
    <div id="searchrecipe">
        <% using (Html.BeginForm()) { %>
            <div id="tabs">
                <ul>
		            <li><a href="#tabs-0">Ingredienser</a></li>
		            <li><a href="#tabs-1">Opskrifter</a></li>
	            </ul>

                <div id="tabs-0">
                    <% Html.RenderPartial("~/Views/Shared/IngredientsSuggest.ascx"); %>
                </div>

                <div id="tabs-1">
                    <% Html.RenderPartial("~/Views/Shared/RecipesSuggest.ascx"); %>
                </div>
            </div>
        <% } %>

        <% if (Model != null) foreach (var r in Model) { %>
            <h1><%= Html.ActionLink(r.Name, "Vis", new { query = new Regex(Iteration1.Common.Auxiliary.REGEX_SPACE).Replace(r.Name, "-"), alternateOutputFormat = "html" })%></h1>

            <% if (r.Ingredients != null) { %>
                <ul>
                    <% foreach (var i in r.Ingredients) { %>
                        <li>
                            <%= Html.Literal(i.Name, i.MatchAccuracy == Iteration1.Models.IngredientDTO.Accuracy.PartialMatch || i.MatchAccuracy == Iteration1.Models.IngredientDTO.Accuracy.FullMatch)%>
                            <%= Html.Literal(i.Amount == Double.MinValue ? "" : i.Amount.ToString(), i.MatchAccuracy == Iteration1.Models.IngredientDTO.Accuracy.FullMatch)%>
                            <%= Html.Literal(i.Unit, i.MatchAccuracy == Iteration1.Models.IngredientDTO.Accuracy.FullMatch)%>
                        </li>
                    <% } %>
                </ul>
            <% } %>
        <% } %>
    </div>
</asp:Content>