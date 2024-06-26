Imports System.Web.Mvc
Imports manga_register.manga_register.Models
Imports System.Web.Security
Imports System.Text.RegularExpressions

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
                ' Validação do email
                If String.IsNullOrEmpty(user.Email) Then
                    ModelState.AddModelError("", "Email é necessário.")
                    Return View(user)
                End If
                If Not IsValidEmail(user.Email) Then
                    ModelState.AddModelError("", "Formato de email inválido.")
                    Return View(user)
                End If

                ' Validação do nome de usuário
                If String.IsNullOrEmpty(user.UserName) Then
                    ModelState.AddModelError("", "O nome de usuário não pode estar em branco.")
                    Return View(user)
                End If

                If user.Password.Length < 6 Then
                    ModelState.AddModelError("", "A senha deve ter pelo menos 6 caracteres.")
                    Return View(user)
                End If

                If userRepository.AddUser(user) Then
                    Return RedirectToAction("Login")
                End If
                ModelState.AddModelError("", "Erro ao registrar o usuário.")
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
        Private Function IsValidEmail(email As String) As Boolean
            If String.IsNullOrEmpty(email) Then
                Return False
            End If

            Dim emailRegex As New Regex("^[^\s@]+@[^\s@]+\.[^\s@]+$")
            Return emailRegex.IsMatch(email)
        End Function
    End Class
End Namespace
