<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Library" %>

<script src="<%= "../".Repeat(Page.RouteData.Values.Count - 1) %>Scripts/RecipesSuggest.js" type="text/javascript"></script>

<%= Html.TextBox("recipessuggest") %>