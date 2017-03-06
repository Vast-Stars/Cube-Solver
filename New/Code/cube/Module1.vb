Module Module1
    Public Const WS_CHILD = &H40000000
    Public Const WS_VISIBLE = &H10000000
    Public Const WM_CAP_START = &H400
    Public Const WM_USER = &H400
    Public Const WM_CAP_DRIVER_CONNECT = WM_CAP_START + 10
    Public Const WM_CAP_SET_SCALE = WM_CAP_START + 53
    Public Const WM_CAP_SET_PREVIEWRATE = WM_CAP_START + 52
    Public Const WM_CAP_SET_PREVIEW = WM_CAP_START + 50
    Public Const WM_CAP_DRIVER_DISCONNECT As Integer = WM_USER + 11
    Public Const WM_CAP_SINGLE_FRAME_CLOSE = WM_CAP_START + 71
    Public Const WM_CAP_SET_OVERLAY = WM_CAP_START + 51
    Public Const WS_CAPTION = 12582912

    Public Const WM_CAP_GET_CAPSTREAMPTR = WM_CAP_START + 1

    Public Const WM_CAP_SET_CALLBACK_ERROR = WM_CAP_START + 2
    Public Const WM_CAP_SET_CALLBACK_STATUS = WM_CAP_START + 3
    Public Const WM_CAP_SET_CALLBACK_YIELD = WM_CAP_START + 4
    Public Const WM_CAP_SET_CALLBACK_FRAME = WM_CAP_START + 5
    Public Const WM_CAP_SET_CALLBACK_VIDEOSTREAM = WM_CAP_START + 6
    Public Const WM_CAP_SET_CALLBACK_WAVESTREAM = WM_CAP_START + 7
    Public Const WM_CAP_GET_USER_DATA = WM_CAP_START + 8
    Public Const WM_CAP_SET_USER_DATA = WM_CAP_START + 9

    'Public Const WM_CAP_DRIVER_CONNECT = WM_CAP_START + 10
    'Public Const WM_CAP_DRIVER_DISCONNECT = WM_CAP_START + 11
    Public Const WM_CAP_DRIVER_GET_NAME = WM_CAP_START + 12
    Public Const WM_CAP_DRIVER_GET_VERSION = WM_CAP_START + 13
    Public Const WM_CAP_DRIVER_GET_CAPS = WM_CAP_START + 14

    Public Const WM_CAP_FILE_SET_CAPTURE_FILE = WM_CAP_START + 20
    Public Const WM_CAP_FILE_GET_CAPTURE_FILE = WM_CAP_START + 21
    Public Const WM_CAP_FILE_ALLOCATE = WM_CAP_START + 22
    Public Const WM_CAP_FILE_SAVEAS = WM_CAP_START + 23
    Public Const WM_CAP_FILE_SET_INFOCHUNK = WM_CAP_START + 24
    Public Const WM_CAP_FILE_SAVEDIB = WM_CAP_START + 25

    Public Const WM_CAP_EDIT_COPY = WM_CAP_START + 30

    Public Const WM_CAP_SET_AUDIOFORMAT = WM_CAP_START + 35
    Public Const WM_CAP_GET_AUDIOFORMAT = WM_CAP_START + 36

    Public Const WM_CAP_DLG_VIDEOFORMAT = WM_CAP_START + 41
    Public Const WM_CAP_DLG_VIDEOSOURCE = WM_CAP_START + 42
    Public Const WM_CAP_DLG_VIDEODISPLAY = WM_CAP_START + 43
    Public Const WM_CAP_GET_VIDEOFORMAT = WM_CAP_START + 44
    Public Const WM_CAP_SET_VIDEOFORMAT = WM_CAP_START + 45
    Public Const WM_CAP_DLG_VIDEOCOMPRESSION = WM_CAP_START + 46

    Public Const WM_CAP_GET_STATUS = WM_CAP_START + 54
    Public Const WM_CAP_SET_SCROLL = WM_CAP_START + 55

    Public Const WM_CAP_GRAB_FRAME = WM_CAP_START + 60
    Public Const WM_CAP_GRAB_FRAME_NOSTOP = WM_CAP_START + 61

    Public Const WM_CAP_SEQUENCE = WM_CAP_START + 62
    Public Const WM_CAP_SEQUENCE_NOFILE = WM_CAP_START + 63
    Public Const WM_CAP_SET_SEQUENCE_SETUP = WM_CAP_START + 64
    Public Const WM_CAP_GET_SEQUENCE_SETUP = WM_CAP_START + 65
    Public Const WM_CAP_SET_MCI_DEVICE = WM_CAP_START + 66
    Public Const WM_CAP_GET_MCI_DEVICE = WM_CAP_START + 67
    Public Const WM_CAP_STOP = WM_CAP_START + 68
    Public Const WM_CAP_ABORT = WM_CAP_START + 69

    Public Const WM_CAP_SINGLE_FRAME_OPEN = WM_CAP_START + 70
    Public Const WM_CAP_SINGLE_FRAME = WM_CAP_START + 72

    Public Const WM_CAP_PAL_OPEN = WM_CAP_START + 80
    Public Const WM_CAP_PAL_SAVE = WM_CAP_START + 81
    Public Const WM_CAP_PAL_PASTE = WM_CAP_START + 82
    Public Const WM_CAP_PAL_AUTOCREATE = WM_CAP_START + 83
    Public Const WM_CAP_PAL_MANUALCREATE = WM_CAP_START + 84

    '// Following added post VFW 1.1
    Public Const WM_CAP_SET_CALLBACK_CAPCONTROL = WM_CAP_START + 85

    '// Defines end of the message range
    Public Const WM_CAP_END = WM_CAP_SET_CALLBACK_CAPCONTROL




    Public Declare Function capCreateCaptureWindow Lib "avicap32.dll" Alias "capCreateCaptureWindowA" (
    ByVal lpszWindowName As String,
ByVal dwStyle As Integer,
ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Short,
ByVal hWndParent As Integer, ByVal nID As Integer) As IntPtr

    Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (
        ByVal hWnd As Integer,
        ByVal wMsg As Integer,
        ByVal wParam As Integer,
        ByVal IParam As Integer) As Integer


End Module
