Imports System.Web.Mvc

Namespace manga_register.Controllers
    Public Class HomeController
        Inherits Controller

        ' GET: Home/Welcome
        Public Function Welcome(userName As String) As ActionResult
            ViewBag.UserName = userName
            Return View()
        End Function
    End Class
End Namespace

