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
        [DllImport("user32.dll")]
        private static extern int MessageBox(
            [In] IntPtr hWnd,
            string text,
            string caption,
            int options);

        [DllImport("powrprof.dll")]
        internal static extern uint CallNtPowerInformation(
            [In]  UInt32 informationLevel,
            [In]  IntPtr lpInputBuffer,
            [In]  Int32 nInputBufferSize,
            [Out] IntPtr lpOutputBuffer,
            [In]  Int32 nOutputBufferSize
        );

        static void Main(string[] args)
        {
            //MessageBox(IntPtr.Zero, "абвгд", "Window1", 0);
            //var int32s = new Int32Struct();
            //int32s.Int = 5;
            //IntPtr sbsPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(SystemBatteryState)));
            //CallNtPowerInformation(
            //    5,
            //    IntPtr.Zero,
            //    0,
            //    sbsPtr,
            //    Marshal.SizeOf(typeof(SystemBatteryState))
            //);
            //SystemBatteryState sbs = (SystemBatteryState)Marshal.PtrToStructure(sbsPtr, typeof(SystemBatteryState));

            IntPtr spiPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(SystemPowerInformation)));
            CallNtPowerInformation(
                12,
                IntPtr.Zero,
                0,
                spiPtr,
                Marshal.SizeOf(typeof(SystemPowerInformation))
            );
            SystemPowerInformation spi = (SystemPowerInformation)Marshal.PtrToStructure(spiPtr, typeof(SystemPowerInformation));
            Console.WriteLine(spi.ToString());
            Console.ReadKey();
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

        [StructLayout(LayoutKind.Sequential)]
        public struct SystemBatteryState
        {
            public bool AcOnLine;
            public bool BatteryPresent;
            public bool Charging;
            public bool Discharging;
            public byte Spare1;
            public byte Spare2;
            public byte Spare3;
            public byte Spare4;
            public UInt32 MaxCapacity;
            public UInt32 RemainingCapacity;
            public UInt32 Rate;
            public UInt32 EstimatedTime;
            public UInt32 DefaultAlert1;
            public UInt32 DefaultAlert2;

            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.AppendLine(string.Format("AcOnLine :          {0}", AcOnLine));
                sb.AppendLine(string.Format("BatteryPresent :    {0}", BatteryPresent));
                sb.AppendLine(string.Format("Charging :          {0}", Charging));
                sb.AppendLine(string.Format("Discharging :       {0}", Discharging));
                sb.AppendLine(string.Format("MaxCapacity :       {0}", MaxCapacity));
                sb.AppendLine(string.Format("RemainingCapacity : {0}", RemainingCapacity));
                sb.AppendLine(string.Format("Rate :              {0}", Rate));
                sb.AppendLine(string.Format("EstimatedTime :     {0}", EstimatedTime));
                return sb.ToString();
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SystemPowerInformation
        {
            public UInt32 MaxIdlenessAllowed;
            public UInt32 Idleness;
            public UInt32 TimeRemaining;
            public byte CoolingMode;

            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.AppendLine(string.Format("Max Idleness Allowed: {0}%", MaxIdlenessAllowed));
                sb.AppendLine(string.Format("Idleness: {0}%", Idleness));
                sb.AppendLine(string.Format("Time Remaining : {0} sec", TimeRemaining));

                string coolingMode = string.Empty;
                switch (CoolingMode)
                {
                    case 0:
                        coolingMode = "Active";
                        break;
                    case 1:
                        coolingMode = "Passive";
                        break;
                    case 2:
                        coolingMode = "No CPU throttling or no thermal zone defined in the system";
                        break;
                    default:
                        break;
                }
                sb.AppendLine(string.Format("Cooling Mode : {0}", coolingMode));

                return sb.ToString();
            }
        }
    }
}
