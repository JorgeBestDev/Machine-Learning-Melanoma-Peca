Imports System.IO
Imports System.Net.Http

Module Program
    Public Class ClasificadorImagen
        Public Async Function DetectarImagen(ByVal imagenPath As String) As Task(Of String)

            Using cliente As New HttpClient()


                Using fileStream As FileStream = New FileStream(imagenPath, FileMode.Open, FileAccess.Read)
                    Dim form As New MultipartFormDataContent()
                    form.Add(New StreamContent(fileStream), "image", Path.GetFileName(imagenPath))


                    Dim respuesta As HttpResponseMessage = Await cliente.PostAsync("http://localhost:5000/predict", form)

                    If respuesta.IsSuccessStatusCode Then
                        Dim resultado As String = Await respuesta.Content.ReadAsStringAsync()
                        Return resultado
                    Else
                        Return $"Error: {respuesta.StatusCode} - {respuesta.ReasonPhrase}"
                    End If
                End Using

            End Using
        End Function
    End Class

    Sub Main(args As String())
        Dim clasificador As New ClasificadorImagen()
        Dim imagenPath As String = "C:\Users\Jorge\source\repos\ConsoleApp2\peca3.jpg"


        Dim result As Task(Of String) = clasificador.DetectarImagen(imagenPath)
        result.Wait()

        Console.WriteLine(result.Result)
    End Sub
End Module


