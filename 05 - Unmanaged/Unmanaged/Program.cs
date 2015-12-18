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
            var int32s = new Int32Struct();
            int32s.Int = 5;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Int32Struct
        {
            [FieldOffset(0)]
            public int Int;

            [FieldOffset(0)]
            public byte byte1;
            [FieldOffset(1)]
            public byte byte2;
            [FieldOffset(2)]
            public byte byte3;
            [FieldOffset(3)]
            public byte byte4;
        }
    }
}
