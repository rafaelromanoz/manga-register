@ModelType manga_register.manga_register.Models.User
@Code
    ViewBag.Title = "Login"
End Code

<h2>@ViewBag.Title</h2>

@Using Html.BeginForm("Login", "Account", FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
    @Html.AntiForgeryToken()

    @<text>
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
        <div class="form-group">
            @Html.LabelFor(Function(m) m.Email, New With {.class = "col-md-2 control-label"})
            <div class="col-md-10">
                @Html.TextBoxFor(Function(m) m.Email, New With {.class = "form-control"})
                @Html.ValidationMessageFor(Function(m) m.Email, "", New With {.class = "text-danger"})
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(m) m.Password, "Senha", New With {.class = "col-md-2 control-label"})
            <div class="col-md-10">
                @Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control"})
                @Html.ValidationMessageFor(Function(m) m.Password, "", New With {.class = "text-danger"})
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" class="btn btn-default" value="Entrar" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <a href="@Url.Action("Register", "Account")" class="btn btn-link">Cadastrar</a>
                <a href="@Url.Action("ForgotPassword", "Account")" class="btn btn-link">Esqueci a Senha</a>
            </div>
        </div>
    </text> End Using

@section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
