Imports System.Data.SqlClient
Imports manga_register.manga_register.Models

Public Class UserRepository
    Private connectionString As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

    Public Function AddUser(user As User) As Boolean
        If IsEmailRegistered(user.Email) Then
            Return False
        End If

        Using con As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand("INSERT INTO Users (UserName, Password, Email) VALUES (@UserName, @Password, @Email)", con)
            cmd.Parameters.AddWithValue("@UserName", user.UserName)
            cmd.Parameters.AddWithValue("@Password", user.Password)
            cmd.Parameters.AddWithValue("@Email", user.Email)
            con.Open()
            Return cmd.ExecuteNonQuery() > 0
        End Using
    End Function

    Private Function IsEmailRegistered(email As String) As Boolean
        Using con As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand("SELECT COUNT(*) FROM Users WHERE Email = @Email", con)
            cmd.Parameters.AddWithValue("@Email", email)
            con.Open()
            Dim count = Convert.ToInt32(cmd.ExecuteScalar())
            Return count > 0
        End Using
    End Function

    Public Function ValidateUser(email As String, password As String) As Boolean
        Using con As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand("SELECT COUNT(*) FROM Users WHERE Email = @email AND Password = @Password", con)
            cmd.Parameters.AddWithValue("@Email", email)
            cmd.Parameters.AddWithValue("@Password", password)
            con.Open()
            Dim count = Convert.ToInt32(cmd.ExecuteScalar())
            Return count > 0
        End Using
    End Function

    Public Function UpdatePassword(email As String, newPassword As String) As Boolean
        Using con As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand("UPDATE Users SET Password = @Password WHERE Email = @Email", con)
            cmd.Parameters.AddWithValue("@Email", email)
            cmd.Parameters.AddWithValue("@Password", newPassword)
            con.Open()
            Return cmd.ExecuteNonQuery() > 0
        End Using
    End Function

    Public Function GetUserName(email As String) As String
        Using con As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand("SELECT UserName FROM Users WHERE Email = @Email", con)
            cmd.Parameters.AddWithValue("@Email", email)
            con.Open()
            Return Convert.ToString(cmd.ExecuteScalar())
        End Using
    End Function
End Class
