<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>@ViewBag.Title - My ASP.NET Application</title>
    <link href="~/Content/bootstrap.css" rel="stylesheet" />
    <link href="~/Content/site.css" rel="stylesheet" />
    <script src="~/Scripts/modernizr-2.8.3.js"></script>
</head>
<body>
    <div class="container body-content">
        @RenderBody()
        <hr />
        <footer>
            <p>&copy; @DateTime.Now.Year - My ASP.NET Application</p>
        </footer>
    </div>

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required:=False)
</body>
</html>
