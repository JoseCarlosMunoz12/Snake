
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
    Dim FoodLoc As FoodInfo
    Dim StartDraw As Boolean
    Dim XDir As Integer = -1
    Dim YDir As Integer = 0
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
        FoodLoc = New FoodInfo With {
            .FoodCol = Color.Red,
            .XPos = 1,
            .YPos = 1}

        StartDraw = True
    End Sub
    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
        Dim DimOf2dPlane As Integer = 20
        Dim Temp As Graphics = e.Graphics

        Dim rnd As New Random
        Dim XCount As Integer = PictureBox1.Size.Width / DimOf2dPlane
        Dim YCount As Integer = PictureBox1.Size.Height / DimOf2dPlane

        Dim Br As SolidBrush = New SolidBrush(FoodLoc.FoodCol)
        For Each item In Snk_Body
            Dim Temps As SnakeBody = item.Value
            Temp.FillRectangle(New SolidBrush(Temps.Snk_Color), Temps.XPos * DimOf2dPlane, Temps.YPos * DimOf2dPlane, DimOf2dPlane, DimOf2dPlane)
        Next
        Temp.FillRectangle(Br, FoodLoc.XPos * DimOf2dPlane, FoodLoc.YPos * DimOf2dPlane, DimOf2dPlane, DimOf2dPlane)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Timer1.Enabled = Not Timer1.Enabled
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        For ii = 0 To Snk_Body.Count - 1
            ChangeCal(Snk_Body.Count - 1 - ii)
        Next
        Dim Temp As SnakeBody = Snk_Body(0)
        For ii = 1 To Snk_Body.Count - 1
            If Temp.XPos = Snk_Body(ii).XPos And Temp.YPos = Snk_Body(ii).YPos Then
                Timer1.Enabled = False
                MsgBox("YOU LOOSE")
            End If
        Next
        If Temp.XPos < 0 Or Temp.XPos > PictureBox1.Size.Width / 20 Or Temp.YPos < 0 Or Temp.YPos > PictureBox1.Size.Height / 20 Then
            Timer1.Enabled = False
            MsgBox("YOU LOOSE")
        End If
        If FoodLoc.XPos = Snk_Body(0).XPos And FoodLoc.YPos = Snk_Body(0).YPos Then
            Dim rnd As New Random
            FoodLoc.XPos = rnd.Next(0, PictureBox1.Size.Width / 20)
            FoodLoc.YPos = rnd.Next(0, PictureBox1.Size.Height / 20)
        End If
        PictureBox1.Refresh()
    End Sub
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.W Then
            XDir = 0
            YDir = -1
        End If
        If e.KeyCode = Keys.S Then
            XDir = 0
            YDir = 1
        End If
        If e.KeyCode = Keys.A Then
            XDir = -1
            YDir = 0
        End If
        If e.KeyCode = Keys.D Then
            XDir = 1
            YDir = 0
        End If
    End Sub
    Private Sub ChangeCal(IndexKy As Integer)
        If IndexKy = 0 Then
            Dim Temp As New SnakeBody
            Temp = Snk_Body(IndexKy)
            Temp.XPos += XDir
            Temp.YPos += YDir
            Snk_Body(IndexKy) = Temp
        Else
            Dim Temps As New SnakeBody
            Temps = Snk_Body(IndexKy - 1)
            Temps.Snk_Color = Color.Green
            Snk_Body(IndexKy) = Temps
        End If

    End Sub
End Class
