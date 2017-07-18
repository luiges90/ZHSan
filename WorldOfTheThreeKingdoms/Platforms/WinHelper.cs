using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace WorldOfTheThreeKingdoms
{

    public class WinHelper
    {
        public static void FullScreen(IntPtr hWnd)
        {
            //Form frm = MainProcessManager.mainGame.GameForm;
            //IntPtr hWnd = frm.Handle;
            int oldStyle = GetWindowLong(hWnd, GWL_STYLE);
            oldStyle &= (~WS_CAPTION);
            oldStyle &= (~WS_THICKFRAME);
            SetWindowLong(hWnd, GWL_STYLE, oldStyle);
            MoveWindow(hWnd, 0, 0, GetSystemMetrics(SM_CXSCREEN), GetSystemMetrics(SM_CYSCREEN), true);
            SendMessage(hWnd, WM_SYSCOMMAND, (IntPtr)SC_MAXIMIZE, IntPtr.Zero);
            UpdateWindow(hWnd);
        }
        public static void RestoreFullScreen(IntPtr hWnd)
        {
            //Form frm = MainProcessManager.mainGame.GameForm;
            //IntPtr hWnd = frm.Handle;
            int oldStyle = GetWindowLong(hWnd, GWL_STYLE);
            oldStyle |= WS_CAPTION;
            oldStyle |= WS_THICKFRAME;
            SetWindowLong(hWnd, GWL_STYLE, oldStyle);
            MoveWindow(hWnd, 0, 0, GetSystemMetrics(SM_CXSCREEN), GetSystemMetrics(SM_CYSCREEN), true);
            SendMessage(hWnd, WM_SYSCOMMAND, (IntPtr)SC_MAXIMIZE, IntPtr.Zero);
            UpdateWindow(hWnd);
        }
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool repaint);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetSystemMetrics(int smIndex);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int UpdateWindow(IntPtr hWnd);
        public const int GWL_STYLE = -16;
        public const int WS_CAPTION = 0x00C00000;
        public const int WS_THICKFRAME = 0x00040000;
        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;
        public const uint WM_SYSCOMMAND = 0x0112;
        public const int SC_MAXIMIZE = 0xF030;
    }
}
