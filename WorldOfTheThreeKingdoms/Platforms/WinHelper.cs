using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Linq;
using System.IO;
using System.Security;
using System.Security.Cryptography;

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

    public class FileEncryptor
    {

        public static void EncryptFile(string sInputFilename,
         string sOutputFilename,
         string sKey)
        {
            FileStream fsInput = new FileStream(sInputFilename,
               FileMode.Open,
               FileAccess.Read);

            FileStream fsEncrypted = new FileStream(sOutputFilename,
               FileMode.Create,
               FileAccess.Write);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            CryptoStream cryptostream = new CryptoStream(fsEncrypted,
               desencrypt,
               CryptoStreamMode.Write);

            byte[] bytearrayinput = new byte[fsInput.Length];
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Close();
            fsInput.Close();
            fsEncrypted.Close();
        }

        public static void DecryptFile(string sInputFilename,
           string sOutputFilename,
           string sKey)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();

            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            FileStream fsread = new FileStream(sInputFilename,
               FileMode.Open,
               FileAccess.Read);
            ICryptoTransform desdecrypt = DES.CreateDecryptor();
            CryptoStream cryptostreamDecr = new CryptoStream(fsread,
               desdecrypt,
               CryptoStreamMode.Read);

            FileStream fswrite = new FileStream(sOutputFilename,
                FileMode.Create,
                FileAccess.Write);

            int b;
            while ((b = cryptostreamDecr.ReadByte()) >= 0)
            {
                fswrite.WriteByte((byte)b);
            }

            fswrite.Close();
            cryptostreamDecr.Close();
            fsread.Close();
        }
    }
}
