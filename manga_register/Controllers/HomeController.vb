Imports System.Web.Mvc

Namespace manga_register.Controllers
    Public Class HomeController
        Inherits Controller

        ' GET: Home/Welcome
        Public Function Welcome(name As String) As ActionResult
            ViewBag.Name = name
            Return View()
        End Function
    End Class
End Namespace


