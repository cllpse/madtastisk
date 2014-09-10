<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Iteration3.Models.HeaderConfiguration>" %>
<%@ Import Namespace="Library" %>

<script src="<%= "../".Repeat(Page.RouteData.Values.Count - 1) %>Scripts/Header.js" type="text/javascript"></script>

<div id="logo">
    <h1>Madtastisk</h1>
    <h2>Find og del opskrifter</h2>
</div>

<div id="header">
    <ul>
        <li class="<%= Model.SelectedMenuItem == Iteration3.Models.HeaderConfiguration.MenuItem.SearchRecipe ? "selected" : "" %>"><%= Html.ActionLink("Find en opskrift", "Find", null, new { title = "Find en opskrift. Søg på navn eller udfra ingredienser" })%></li>
        <li class="<%= Model.SelectedMenuItem == Iteration3.Models.HeaderConfiguration.MenuItem.AddRecipe ? "selected" : "" %>"><%= Html.ActionLink("Del en opskrift", "Del", null, new { title = "Del en opskrift. Tilføj din egen opskrift til siden" })%></li>
    </ul>
</div>