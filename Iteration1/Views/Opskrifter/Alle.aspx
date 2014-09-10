<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Iteration1.Models.RecipeDTO>>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Alle</title>
</head>
<body>
    <% if (Model != null) foreach (var r in Model) { %>
            <h1><%: r.Name %></h1>

            <% if (r.Ingredients != null) { %>
                <ul>
                    <% foreach (var i in r.Ingredients) { %>
                        <li>
                            <%: i.Name %>
                            <%: i.Amount == Double.MinValue ? "" : i.Amount.ToString() %>
                            <%: i.Unit %>
                        </li>
                    <% } %>
                </ul>
            <% } %>

            <%= r.Description.Replace(Environment.NewLine, "<br />") %>
            
            <hr />
        <% } %>
</body>
</html>