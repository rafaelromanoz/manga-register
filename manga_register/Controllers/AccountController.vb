Imports System.Web.Mvc
Imports manga_register.manga_register.Models

Namespace manga_register.Controllers
    Public Class AccountController
        Inherits Controller

        Private userRepository As New UserRepository()

        ' GET: Account/Login
        Public Function Login() As ActionResult
            Return View()
        End Function

        ' POST: Account/Login
        <HttpPost>
        Public Function Login(user As User) As ActionResult
            If ModelState.IsValid Then
                Dim isValidUser = userRepository.ValidateUser(user.Email, user.Password)
                If isValidUser Then
                    ' Obtendo o UserName do usuário logado
                    Dim loggedInUser = userRepository.GetUserByEmail(user.Email)
                    ' Redirecionar para a tela de Welcome com o UserName do usuário
                    Return RedirectToAction("Welcome", "Home", New With {.userName = loggedInUser.UserName})
                Else
                    ModelState.AddModelError("", "Email ou senha inválidos.")
                End If
            End If
            Return View(user)
        End Function

        ' GET: Account/Register
        Public Function Register() As ActionResult
            Return View()
        End Function

        ' POST: Account/Register
        <HttpPost>
        Public Function Register(user As User) As ActionResult
            If ModelState.IsValid Then
                If userRepository.AddUser(user) Then
                    Return RedirectToAction("Login")
                End If
            End If
            Return View(user)
        End Function

        ' GET: Account/ForgotPassword
        Public Function ForgotPassword() As ActionResult
            Return View()
        End Function

        ' POST: Account/ForgotPassword
        <HttpPost>
        Public Function ForgotPassword(email As String, newPassword As String, confirmPassword As String) As ActionResult
            If newPassword = confirmPassword AndAlso userRepository.UpdatePassword(email, newPassword) Then
                Return RedirectToAction("Login")
            End If
            ModelState.AddModelError("", "Falha ao atualizar a senha.")
            Return View()
        End Function
    End Class
End Namespace
