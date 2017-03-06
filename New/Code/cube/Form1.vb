Imports System
Imports System.ComponentModel
Imports System.IO
Imports System.Text




Public Class Form1


    'Base option 1

    Dim dj%(4, 8) '’轴编号 臂编号  中顺逆开闭 现行位置
    Dim Time1, Time2 As Integer
    Dim Capwhnd As Integer




    Private Sub Form1_Load(sender As Object, e As EventArgs)
        SerialPort1.BaudRate = 9600
        TextBox1.Text = 3
        '’ Debug.Print()
    End Sub

    '打开端口
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        On Error Resume Next
        If Button1.Text = "打开端口" Then
            SerialPort1.PortName = “COM” & TextBox1.Text
            SerialPort1.Open()
            If (Not SerialPort1.IsOpen) Then
                MsgBox("打开串口失败！", 0)
                Return
            End If
            Button1.Text = "关闭端口"
        Else
            SerialPort1.Close()
            If (SerialPort1.IsOpen) Then
                MsgBox("关闭串口失败！", 0)
                Return
            End If
            Button1.Text = "打开端口"
        End If
    End Sub

    Private Sub HScrollBar1_ValueChanged(sender As Object, e As EventArgs)
        Label1.Text = 500 + HScrollBar1.Value
        '' Debug.Print(500 + HScrollBar1.Value)
        If SerialPort1.IsOpen Then
            SerialPort1.Write("#1P" + CStr(500 + HScrollBar1.Value) & "T100" & Chr(13) & Chr(10))
        End If

    End Sub

    '载入数据
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ComboBox1.Items.Clear()
        'Debug.Print(Application.StartupPath & "\config.txt")
        Dim text1 As String = IO.File.ReadAllText(Application.StartupPath & "\config.txt", Encoding.Default)
        Dim _text As String
        Dim _a1%, _a2%， _a3%
        Label2.Text = "轴      爪       中      顺      逆       开      闭" & Chr(13)
        _a2 = 1
        ''Debug.Print(twotext(1, text1, "舵机1" & Chr(13), Chr(13),))
        For _a3 = 1 To 4
            Label2.Text = Label2.Text & "舵机" & _a3 & Chr(13)
            _a2 = InStr(text1, "舵机" & _a3) + 3
            ' Debug.Print(_a3 & ":" & _a2)
            For _a1 = 1 To 7
                _text = twotext(_a2, text1, Chr(10), Chr(13))   ''TXT中 先13 后10 （先回车后换行）
                'Debug.Print(" " & _a1 & ":" & _text)
                _a2 = InStr(_a2, text1, _text) + _text.Length
                dj(_a3, _a1) = Val(_text)
                Label2.Text += _text.PadRight(8)

            Next _a1
            'Debug.Print(Label2.Text.Length, _a3 * 4 + _a1 * 2 - 2)
            Label2.Text += Chr(13)
            ComboBox1.Items.Add("舵机" & dj(_a3, 1))
            ComboBox1.Items.Add("舵机" & dj(_a3, 2))
            ' dj(_a3, 8) = dj(_a3, 5)
        Next _a3

        Time1 = Val(twotext(1, text1, "轴转动时间=", Chr(13)))
        Time2 = Val(Mid(text1, InStr(text1, "爪开闭时间=",) + 6， text1.Length - InStr(text1, "爪开闭时间=",)))

        Label2.Text += "轴转动时间=" & Time1 & Chr(13) & "爪开闭时间=" & Time2
    End Sub

    ' 测试专用
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Button11_Click(sender, e)
    End Sub


    Private Function twotext(start As Integer, text1 As String, ByVal text2 As String, Optional ByVal text3 As String = "", Optional compare As CompareMethod = CompareMethod.Binary) As String  ''默认text2在text3前面且只出现一次  默认区分大小写
        Dim _a1%, _a2%
        Dim _retext As String
        _a1 = InStr(start, text1， text2, compare)
        If (_a1 = 0) Then
            Return ""
        End If
        _a1 = _a1 + Len(text2)
        _a2 = InStr(_a1, text1, text3, compare)
        _retext = Mid(text1, _a1, _a2 - _a1)
        Return (_retext)
    End Function

    '调试子程序
    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Debug.Print("——————————————————" & Now)
    End Sub

    '保存数据按钮
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

    End Sub

    '复位按钮
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        SerialPort1.Write("#" & dj(1, 2) & "P" & dj(1, 6) & "#" & dj(2, 2) & "P" & dj(2, 6) & "#" & dj(3, 2) & "P" & dj(3, 6) & "#" & dj(4, 2) & "P" & dj(4, 6) & "T" & Time2 & Chr(13) & Chr(10))
        djsendmessage(1, 1, dj(1, 4), Time1, 2, 1, dj(2, 4))
        djsendmessage(1, 1, dj(1, 3), Time1, 2, 1, dj(2, 3))
        djsendmessage(3, 1, dj(3, 4), Time1, 4, 1, dj(4, 4))
        djsendmessage(3, 1, dj(3, 3), Time1, 4, 1, dj(4, 3))
    End Sub

    '执行按钮
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        TextBox2.Text = TextBox2.Text.ToUpper()
        If (TextBox2.Text(0) = "F" Or TextBox2.Text(0) = "B" Or TextBox2.Text(0) = "L" Or TextBox2.Text(0) = "R") Then
            move1(TextBox2.Text)
        Else
            move2(TextBox2.Text)
        End If
    End Sub
    '停止按钮
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        SerialPort1.Write("#STOP" & Chr(13) & Chr(10))
    End Sub
    '爪子开按钮
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        SerialPort1.Write("#" & dj(1, 2) & "P" & dj(1, 6) & "#" & dj(2, 2) & "P" & dj(2, 6) & "#" & dj(3, 2) & "P" & dj(3, 6) & "#" & dj(4, 2) & "P" & dj(4, 6) & "T1000" & Chr(13) & Chr(10))
    End Sub
    '爪子关按钮
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        SerialPort1.Write("#" & dj(1, 2) & "P" & dj(1, 7) & "#" & dj(2, 2) & "P" & dj(2, 7) & "#" & dj(3, 2) & "P" & dj(3, 7) & "#" & dj(4, 2) & "P" & dj(4, 7) & "T1000" & Chr(13) & Chr(10))
    End Sub


    '
    Private Sub djsendmessage(Num1 As Integer, num2 As Integer, postion As Integer, Optional time As Integer = -1， Optional Num3 As Integer = -1, Optional num4 As Integer = -1, Optional postion2 As Integer = -1)
        If (time = -1) Then
            If num2 = 1 Then
                time = Time1
            Else time = Time2
            End If
        End If
        If (Num3 = -1) Then
            SerialPort1.Write("#" & dj(Num1, num2) & "P" & postion & "T" & time & Chr(13) & Chr(10))

        Else
            SerialPort1.Write("#" & dj(Num1, num2) & "P" & postion & "#" & dj(Num3, num4) & "P" & postion2 & "T" & time & Chr(13) & Chr(10))
        End If
    End Sub

    'FBLR  
    Private Sub move1(method As String) 'LRFB   U D     //左转右转 翻转等
        Dim _ms = 1, _bh As Integer
        ' 模式1 ：L R F B  
        '模式2：
        If Len(method) = 1 Then
            method = method & "1"
        End If
        If method(0) = "F" Then
            _bh = 1
        ElseIf method(0) = "B" Then
            _bh = 2
        ElseIf method(0) = "L" Then
            _bh = 3
        Else
            _bh = 4
        End If

        If method(1) = "'" Then
            _ms = 4
        ElseIf method(1) = "1" Then
            _ms = 3
        End If
        If _ms = 1 Then
            djsendmessage(_bh, 2, dj(_bh, 5), Time2) '爪子开
            djsendmessage(_bh, 1, dj(_bh, 4), Time1)   '逆时针
            djsendmessage(_bh, 2, dj(_bh, 6), Time2) '爪子闭
            djsendmessage(_bh, 1, dj(_bh, 3), Time1)   '顺时针
            djsendmessage(_bh, 2, dj(_bh, 5), Time2) '爪子开
            djsendmessage(_bh, 1, dj(_bh, 2), Time1)     '归中
            djsendmessage(_bh, 2, dj(_bh, 6), Time2) '爪子闭
        Else
            djsendmessage(_bh, 1, dj(_bh, _ms), Time1)   '顺逆时针
            djsendmessage(_bh, 2, dj(_bh, 5), Time2) ' 爪子开
            djsendmessage(_bh, 1, dj(_bh, 2), Time1)     '归中
            djsendmessage(_bh, 2, dj(_bh, 6), Time2) '爪子闭
        End If
    End Sub
    'z y  u d
    Private Sub move2(method As String)
        If (Asc(method(0)) > 88) Then
            djsendmessage(3, 2, dj(3, 6), Time2, 4, 2, dj(4, 6)) '左右爪子松开

            If method = "y" Or method = "Y" Then
                djsendmessage(1, 1, dj(1, 4),, 2, 1, dj(2, 5))
            Else
                djsendmessage(1, 1, dj(1, 5),, 2, 1, dj(2, 4))
            End If
            djsendmessage(3, 2, dj(3, 7),, 4, 2, dj(4, 7)) '左右爪子关闭
            djsendmessage(1, 2, dj(1, 6),, 1, 2, dj(1, 6)) '前后爪子松开
            djsendmessage(1, 1, dj(1, 3),, 2, 1, dj(2, 3)) '前后归位
            djsendmessage(1, 2, dj(1, 7),, 1, 2, dj(1, 7)) '前后爪子关闭

        Else

            move2("y")
            If (method(0) = "U") Then
                method.Replace("U", "R")
            Else method.Replace("D", "L")
            End If
            move1(method)
            move2("z")
        End If
    End Sub

    Private Sub HScrollBar1_Scroll(sender As Object, e As ScrollEventArgs) Handles HScrollBar1.Scroll
        Debug.Print(HScrollBar1.Value + 500)
        djsendmessage(ComboBox1.SelectedIndex \ 2 + 1, 2 - (ComboBox1.SelectedIndex + 1) Mod 2, HScrollBar1.Value + 500)
    End Sub


    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        move1("F1")
        move1("F'")
        move1("F2")
        move1("B1")
        move1("B'")
        move1("B2")
        move1("L1")
        move1("L'")
        move1("L2")
        move1("R1")
        move1("R'")
        move1("R2")
        move2("U1")
        move2("U'")
        move2("U2")
        move2("D")
        move2("D'")
        move2("D2")
    End Sub


    '打开摄像头！
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim back As Integer
        Capwhnd = capCreateCaptureWindow("视频窗口"， WS_VISIBLE Or WS_CHILD, 0, 30, 480, 320, TabControl1.SelectedTab.Handle, 0)
        Debug.Print(Capwhnd)
        If (Capwhnd <= 0) Then
            MsgBox("创建视频窗口失败！")
            Exit Sub
        End If

        back = SendMessage(Capwhnd, WM_CAP_DRIVER_CONNECT, 0, 0)
        While (back = 0)
            back = SendMessage(Capwhnd, WM_CAP_DRIVER_CONNECT, 0, 0)
        End While
        Debug.Print(back)

        back = SendMessage(Capwhnd, WM_CAP_SET_SCALE, True, 0)
        Debug.Print(back)
        If (back <= 0) Then
            MsgBox("SCALE失败！")
            Exit Sub
        End If

        back = SendMessage(Capwhnd, WM_CAP_SET_PREVIEWRATE, 30, 0)
        Debug.Print(back)
        If (back <= 0) Then
            MsgBox("设置帧率失败！")
            Exit Sub
        End If

        back = SendMessage(Capwhnd, WM_CAP_SET_PREVIEW, True, 0)
        Debug.Print(back)
        If (back <= 0) Then
            MsgBox("开始预览失败！")
            Exit Sub
        End If

        Button11.Enabled = False
        Debug.Print("开启预览")
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        HScrollBar1.Value = 1000
        Debug.Print(" :" & 2 - (ComboBox1.SelectedIndex + 1) Mod 2)

    End Sub

    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Click
        Dim x As Color

    End Sub

    '关闭摄像头
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        SendMessage(Capwhnd, WM_CAP_DRIVER_DISCONNECT, 0, 0)
        SendMessage(Capwhnd, WM_CAP_SINGLE_FRAME_CLOSE, 0, 0)
        SendMessage(Capwhnd, &H10, 0, 0)
        Debug.Print("-----------结束")
        Button11.Enabled = True
    End Sub

    Private Sub Form1_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Call Button12_Click(Me, e)
    End Sub
End Class