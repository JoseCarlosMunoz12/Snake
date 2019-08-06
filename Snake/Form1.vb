
Public Class Form1
    Enum Direction
        UP = 0
        Down
        Left
        Right
    End Enum
    Structure Bridge

    End Structure
    Structure TeleportLoc
        Dim XPos As Integer
        Dim YPos As Integer
        Dim TeleCol As Color
        Dim Direction As Direction
        Dim TeleportId As Integer
    End Structure
    Structure FoodInfo
        Dim XPos As Integer
        Dim YPos As Integer
        Dim FoodCol As Color
    End Structure
    Structure SnakeBody
        Dim XPos As Integer
        Dim YPos As Integer
        Dim ZPos As Integer
        Dim Snk_Color As Color
        Dim Direction As Direction
    End Structure
    Dim Snk_Body As Dictionary(Of Integer, SnakeBody) = New Dictionary(Of Integer, SnakeBody)
    Dim FoodLoc As FoodInfo
    Dim Teleport(1) As TeleportLoc
    Dim StartDraw As Boolean
    Dim XDir As Integer = -1
    Dim YDir As Integer = 0
    Dim rnd As New Random
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ResetGame()
        PictureBox1.BackColor = Color.PaleGoldenrod
        StartDraw = True
    End Sub
    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
        Dim DimOf2dPlane As Integer = 20
        Dim Temp As Graphics = e.Graphics
        Dim XCount As Integer = PictureBox1.Size.Width / DimOf2dPlane
        Dim YCount As Integer = PictureBox1.Size.Height / DimOf2dPlane
        Dim Br As SolidBrush = New SolidBrush(FoodLoc.FoodCol)
        Dim TL As SolidBrush = New SolidBrush(Teleport(0).TeleCol)
        Dim TS As SolidBrush = New SolidBrush(Teleport(1).TeleCol)
        For Each item In Snk_Body
            Dim Temps As SnakeBody = item.Value
            Temp.FillRectangle(New SolidBrush(Temps.Snk_Color), Temps.XPos * DimOf2dPlane, Temps.YPos * DimOf2dPlane, DimOf2dPlane, DimOf2dPlane)
        Next
        Temp.FillRectangle(Br, FoodLoc.XPos * DimOf2dPlane, FoodLoc.YPos * DimOf2dPlane, DimOf2dPlane, DimOf2dPlane)
        Temp.FillRectangle(TL, Teleport(0).XPos * DimOf2dPlane, Teleport(0).YPos * DimOf2dPlane, DimOf2dPlane, DimOf2dPlane)
        Temp.FillRectangle(TS, Teleport(1).XPos * DimOf2dPlane, Teleport(1).YPos * DimOf2dPlane, DimOf2dPlane, DimOf2dPlane)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ResetGame()
        Timer1.Enabled = Not Timer1.Enabled
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ''moves vody
        For ii = 0 To Snk_Body.Count - 1
            ChangeCal(Snk_Body.Count - 1 - ii)
        Next
        Dim Temp As SnakeBody = Snk_Body(0)
        ''check collision with self
        For ii = 1 To Snk_Body.Count - 1
            If Temp.XPos = Snk_Body(ii).XPos And Temp.YPos = Snk_Body(ii).YPos Then
                Timer1.Enabled = False
                MsgBox("YOU LOOSE")
            End If
        Next
        ''check collision with walls
        If Temp.XPos < 0 Or Temp.XPos > PictureBox1.Size.Width / 20 Or Temp.YPos < 0 Or Temp.YPos > PictureBox1.Size.Height / 20 Then
            Timer1.Enabled = False
            MsgBox("YOU LOOSE")
        End If
        ''checks collision with food
        If FoodLoc.XPos = Snk_Body(0).XPos And FoodLoc.YPos = Snk_Body(0).YPos Then
            FoodLoc.XPos = rnd.Next(0, PictureBox1.Size.Width / 20)
            FoodLoc.YPos = rnd.Next(0, PictureBox1.Size.Height / 20)
            Dim NewBod As New SnakeBody With {
                .Snk_Color = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)),
                .Direction = Snk_Body(Snk_Body.Count - 1).Direction
                }
            Select Case Snk_Body(Snk_Body.Count - 1).Direction
                Case Direction.Down
                    NewBod.XPos = Snk_Body(Snk_Body.Count - 1).XPos
                    NewBod.YPos = Snk_Body(Snk_Body.Count - 1).YPos - 1
                Case Direction.Left
                    NewBod.XPos = Snk_Body(Snk_Body.Count - 1).XPos - 1
                    NewBod.YPos = Snk_Body(Snk_Body.Count - 1).YPos
                Case Direction.Right
                    NewBod.XPos = Snk_Body(Snk_Body.Count - 1).XPos + 1
                    NewBod.YPos = Snk_Body(Snk_Body.Count - 1).YPos
                Case Direction.UP
                    NewBod.XPos = Snk_Body(Snk_Body.Count - 1).XPos
                    NewBod.YPos = Snk_Body(Snk_Body.Count - 1).YPos + 1
            End Select
            Snk_Body.Add(Snk_Body.Count, NewBod)
        End If
        '' Checks if it is on the teleporters
        For Each item In Teleport
            If item.XPos = Snk_Body(0).XPos And item.YPos = Snk_Body(0).YPos Then
                ChangeHeadPos(Snk_Body(0), item)
                Exit For
            End If
        Next
        ''refreshes the screen
        PictureBox1.Refresh()
    End Sub
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.W Then
            Dim Temp As New SnakeBody
            Temp = Snk_Body(0)
            Temp.Direction = Direction.UP
            Snk_Body(0) = Temp
            XDir = 0
            YDir = -1
        End If
        If e.KeyCode = Keys.S Then
            Dim Temp As New SnakeBody
            Temp = Snk_Body(0)
            Temp.Direction = Direction.Down
            Snk_Body(0) = Temp
            XDir = 0
            YDir = 1
        End If
        If e.KeyCode = Keys.A Then
            Dim Temp As New SnakeBody
            Temp = Snk_Body(0)
            Temp.Direction = Direction.Left
            Snk_Body(0) = Temp
            XDir = -1
            YDir = 0
        End If
        If e.KeyCode = Keys.D Then
            Dim Temp As New SnakeBody
            Temp = Snk_Body(0)
            Temp.Direction = Direction.Right
            Snk_Body(0) = Temp
            XDir = 1
            YDir = 0
        End If
    End Sub
    Private Sub ChangeCal(IndexKy As Integer)
        If IndexKy = 0 Then
            Dim Temp As New SnakeBody
            Temp = Snk_Body(0)
            Temp.XPos += XDir
            Temp.YPos += YDir
            Snk_Body(0) = Temp
        Else
            Dim Temps As New SnakeBody
            Temps = Snk_Body(IndexKy - 1)
            Temps.Snk_Color = Snk_Body(IndexKy).Snk_Color
            Snk_Body(IndexKy) = Temps
        End If

    End Sub
    Private Sub ChangeHeadPos(ByRef SnakeHead As SnakeBody, TeleLoc As TeleportLoc)
        Dim Temp As New SnakeBody With {
            .XPos = Teleport(TeleLoc.TeleportId).XPos,
            .YPos = Teleport(TeleLoc.TeleportId).YPos,
            .Direction = SnakeHead.Direction,
            .Snk_Color = SnakeHead.Snk_Color}
        SnakeHead = Temp
    End Sub
    Private Sub ResetGame()
        If StartDraw Then
            Snk_Body.Clear()
        End If
        YDir = 0
        XDir = -1
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
                .XPos = rnd.Next(0, PictureBox1.Size.Width / 20),
                .YPos = rnd.Next(0, PictureBox1.Size.Height / 20)}
        Teleport(0) = New TeleportLoc With {
            .TeleCol = Color.Blue,
            .XPos = rnd.Next(0, PictureBox1.Size.Width / 20),
            .YPos = rnd.Next(0, PictureBox1.Size.Height / 20),
            .TeleportId = 1}
        Teleport(1) = New TeleportLoc With {
            .TeleCol = Color.Green,
            .XPos = rnd.Next(0, PictureBox1.Size.Width / 20),
            .YPos = rnd.Next(0, PictureBox1.Size.Height / 20),
            .TeleportId = 0}
    End Sub
End Class