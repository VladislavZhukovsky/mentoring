using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Unmanaged
{
    class Program
    {
        [DllImport("user32.dll", ThrowOnUnmappableChar = true)]
        private static extern int MessageBox(
            [In] IntPtr hWnd,
            [MarshalAs(UnmanagedType.LPStr)] string text,
            [MarshalAs(UnmanagedType.LPStr)] string caption,
            int options);

        static void Main(string[] args)
        {
            MessageBox(IntPtr.Zero, "Simple message box", "Window1", 0);
        }
    }
}
