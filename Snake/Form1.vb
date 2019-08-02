
Public Class Form1
    Enum Direction
        UP = 1
        Down = -1
        Left = 2
        Right = -2
    End Enum
    Structure FoodInfo
        Dim XPos As Integer
        Dim YPos As Integer
        Dim FoodCol As Color
    End Structure
    Structure SnakeBody
        Dim XPos As Integer
        Dim YPos As Integer
        Dim Snk_Color As Color
        Dim Direction As Direction
    End Structure
    Dim Snk_Body As Dictionary(Of Integer, SnakeBody) = New Dictionary(Of Integer, SnakeBody)
    Dim StartDraw As Boolean
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For ii = 0 To 2
            Dim Col As Color = Color.Green
            If ii = 0 Then
                Col = Color.Red
            End If
            Dim TempSnakeBod As New SnakeBody With {
                .XPos = 10 + ii,
                .YPos = 10,
                .Direction = Direction.Left,
                .Snk_Color = Col}
            Snk_Body.Add(ii, TempSnakeBod)
        Next
        StartDraw = True
    End Sub
    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
        Dim DimOf2dPlane As Integer = 20
        Dim Temp As Graphics = e.Graphics

        Dim rnd As New Random
        Dim XCount As Integer = PictureBox1.Size.Width / DimOf2dPlane
        Dim YCount As Integer = PictureBox1.Size.Height / DimOf2dPlane

        Dim Br As SolidBrush = New SolidBrush(Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)))
        For Each item In Snk_Body
            Dim Temps As SnakeBody = item.Value
            Temp.FillRectangle(New SolidBrush(Temps.Snk_Color), Temps.XPos * DimOf2dPlane, Temps.YPos * DimOf2dPlane, DimOf2dPlane, DimOf2dPlane)
        Next
        Temp.FillRectangle(Br, rnd.Next(0, XCount - 1) * DimOf2dPlane, rnd.Next(0, YCount - 1) * DimOf2dPlane, DimOf2dPlane, DimOf2dPlane)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Timer1.Enabled = Not Timer1.Enabled
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        PictureBox1.Refresh()
    End Sub
End Class
