Imports System.Web.Mvc
Imports manga_register.manga_register.Models
Imports System.Web.Security

Namespace manga_register.Controllers
    Public Class AccountController
        Inherits Controller

        Private userRepository As New UserRepository()

        ' GET: Account/Register
        Public Function Register() As ActionResult
            Return View()
        End Function

        ' POST: Account/Register
        <HttpPost>
        Public Function Register(user As User) As ActionResult
            If ModelState.IsValid Then
                ' Verificar se a senha tem pelo menos 6 caracteres
                If user.Password.Length < 6 Then
                    ModelState.AddModelError("Password", "A senha deve ter pelo menos 6 caracteres.")
                ElseIf userRepository.AddUser(user) Then
                    Return RedirectToAction("Login")
                Else
                    ModelState.AddModelError("Email", "Este e-mail já está registrado.")
                End If
            End If
            Return View(user)
        End Function

        ' GET: Account/Login
        Public Function Login() As ActionResult
            Return View()
        End Function

        ' POST: Account/Login
        <HttpPost>
        Public Function Login(user As User) As ActionResult
            If ModelState.IsValid AndAlso userRepository.ValidateUser(user.Email, user.Password) Then
                FormsAuthentication.SetAuthCookie(user.Email, False)
                Dim userName = userRepository.GetUserName(user.Email)
                Return RedirectToAction("Welcome", "Home", New With {.name = userName})
            Else
                ModelState.AddModelError("", "E-mail ou senha inválidos.")
            End If
            Return View(user)
        End Function

        ' GET: Account/ForgotPassword
        Public Function ForgotPassword() As ActionResult
            Return View()
        End Function

        ' POST: Account/ForgotPassword
        <HttpPost>
        Public Function ForgotPassword(email As String, password As String, confirmPassword As String) As ActionResult
            If password <> confirmPassword Then
                ModelState.AddModelError("", "As senhas não coincidem.")
                Return View()
            End If
            If password.Length < 6 Or confirmPassword.Length < 6 Then
                ModelState.AddModelError("Password", "A senha deve ter pelo menos 6 caracteres.")
                Return View()
            End If
            If userRepository.UpdatePassword(email, password) Then
                TempData("SuccessMessage") = "Senha alterada com sucesso."
                Return RedirectToAction("Login")
            Else
                ModelState.AddModelError("", "E-mail não encontrado.")
                Return View()
            End If
        End Function
    End Class
End Namespace
