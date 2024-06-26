Imports Microsoft.Owin
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Owin

Partial Public Class Startup
    Public Sub ConfigureAuth(app As IAppBuilder)
        ' Configure o contexto db, gerenciador de usuários e gerenciador de login para usar uma única instância por solicitação
        app.CreatePerOwinContext(AddressOf ApplicationDbContext.Create)
        app.CreatePerOwinContext(Of ApplicationUserManager)(AddressOf ApplicationUserManager.Create)
        app.CreatePerOwinContext(Of ApplicationSignInManager)(AddressOf ApplicationSignInManager.Create)

        ' Habilitar o aplicativo para usar um cookie para armazenar informações para o usuário logado
        ' E usar um cookie para armazenar temporariamente informações sobre um usuário que faz login com um provedor de login de terceiros
        ' Configurar o login de cookie
        app.UseCookieAuthentication(New CookieAuthenticationOptions() With {
            .AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            .LoginPath = New PathString("/Account/Login"),
            .Provider = New CookieAuthenticationProvider() With {
                .OnValidateIdentity = SecurityStampValidator.OnValidateIdentity(Of ApplicationUserManager, ApplicationUser)(
                    validateInterval:=TimeSpan.FromMinutes(30),
                    regenerateIdentity:=Function(manager, user) user.GenerateUserIdentityAsync(manager))
            }
        })
        app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie)

        ' Permite que o aplicativo armazene temporariamente informações do usuário quando ele estiver verificando o segundo fator no processo de autenticação de dois fatores.
        app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5))

        ' Permite que o aplicativo lembre o segundo fator de verificação de login, como telefone ou e-mail.
        ' Uma vez marcada esta opção, o segundo passo de verificação durante o processo de login será lembrado no dispositivo no qual você fez login.
        ' Isto é semelhante à opção RememberMe quando você faz login.
        app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie)
    End Sub
End Class
