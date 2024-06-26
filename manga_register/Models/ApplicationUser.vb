Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports System.Security.Claims
Imports System.Threading.Tasks

Public Class ApplicationUser
    Inherits IdentityUser

    ' Adicione propriedades personalizadas aqui, se necessário.

    Public Function GenerateUserIdentityAsync(manager As UserManager(Of ApplicationUser)) As Task(Of ClaimsIdentity)
        ' Observe que o authenticationType deve corresponder a um definido em CookieAuthenticationOptions.AuthenticationType
        Dim userIdentity = manager.CreateIdentityAsync(Me, DefaultAuthenticationTypes.ApplicationCookie)
        ' Adicione reivindicações do usuário personalizado aqui
        Return userIdentity
    End Function
End Class

