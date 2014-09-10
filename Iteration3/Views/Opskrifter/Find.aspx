<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Madtastisk.Master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<Iteration3.Models.RecipeDTO>>" %>
<%@ Import Namespace="Library" %>

<asp:Content runat="server" ContentPlaceHolderID="cphHead">
    <link href="<%= "../".Repeat(Page.RouteData.Values.Count - 1) %>Content/SearchRecipe.css" rel="stylesheet" type="text/css" />
    
    <script src="<%= "../".Repeat(Page.RouteData.Values.Count) %>Scripts/SearchRecipe.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(function ()
        {
            if (window.location.toString().lastIndexOf("Find.html") < 0) $("#tabs").tabs("select", "tabs-1");
        });

    </script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="cphBody">
    <% Html.RenderPartial("~/Views/Shared/Header.ascx", new Iteration3.Models.HeaderConfiguration { SelectedMenuItem = Iteration3.Models.HeaderConfiguration.MenuItem.SearchRecipe }); %>
    
    <div id="searchrecipe">
        <% using (Html.BeginForm()) { %>
            <div id="tabs">
                <ul>
                    <li id="introtext">Søg efter en opskrift udfra</li>
		            <li><a href="#tabs-0" title="Søg efter opskrifter udfra navn">Navn</a></li>
                    <li><a href="#tabs-1" title="Søg efter opskrifter udfra hvilke ingredienser du har til rådighed">Ingredienser</a></li>
	            </ul>

                <div id="tabs-0">
                    <div class="info">
                        <p class="icon">?</p>

                        <p>
                            Søg på opskrifter udfra navn. For eksempel <strong>"pizza"</strong> eller <strong>"suppe"</strong><br />
                            Fundne opskrifter sorteres efter navn
                        </p>
                    </div>

                    <% Html.RenderPartial("~/Views/Shared/RecipesSuggest.ascx"); %>
                </div>

                <div id="tabs-1">
                    <div class="info">
                        <p class="icon">?</p>
                        
                        <p>
                            Søg på opskrifter udfra ingredienser du har til rådighed. For eksempel <strong>"tomat 4 stk, ost 200 g, mel, vand"</strong><br />
                            Fundne opskrifter sorteres efter mindste antal ingredienser, du mangler, for at kunne lave opskriften 
                        </p>
                    </div>

                    <% Html.RenderPartial("~/Views/Shared/IngredientsSuggest.ascx"); %>
                </div>
            </div>
        <% } %>

        <% if (Model != null) foreach (var r in Model) { %>
            <h1><%= Html.ActionLink(r.Name, "Vis", new { query = new Regex(Iteration3.Common.Auxiliary.REGEX_SPACE).Replace(r.Name, "-"), alternateOutputFormat = "html" })%></h1>

            <% if (r.Ingredients != null) { %>
                <ul>
                    <% foreach (var i in r.Ingredients) { %>
                        <li>
                            <%= Html.Literal(i.Name, i.MatchAccuracy == Iteration3.Models.IngredientDTO.Accuracy.PartialMatch || i.MatchAccuracy == Iteration3.Models.IngredientDTO.Accuracy.FullMatch)%>
                            <%= Html.Literal(i.Amount == Double.MinValue ? "" : i.Amount.ToString(), i.MatchAccuracy == Iteration3.Models.IngredientDTO.Accuracy.FullMatch)%>
                            <%= Html.Literal(i.Unit, i.MatchAccuracy == Iteration3.Models.IngredientDTO.Accuracy.FullMatch)%>
                        </li>
                    <% } %>
                </ul>
            <% } %>
        <% } %>
    </div>
</asp:Content>