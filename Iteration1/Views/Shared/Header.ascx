<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Iteration1.Models.HeaderConfiguration>" %>
<%@ Import Namespace="Library" %>

<script src="<%= "../".Repeat(Page.RouteData.Values.Count - 1) %>Scripts/Header.js" type="text/javascript"></script>

<div id="header">
    <ul>
        <li class="<%= Model.SelectedMenuItem == Iteration1.Models.HeaderConfiguration.MenuItem.SearchRecipe ? "selected" : "" %>"><%= Html.ActionLink("Find en opskrift", "Find")%></li>
        <li class="<%= Model.SelectedMenuItem == Iteration1.Models.HeaderConfiguration.MenuItem.AddRecipe ? "selected" : "" %>"><%= Html.ActionLink("Del en opskrift", "Del")%></li>
    </ul>
</div>