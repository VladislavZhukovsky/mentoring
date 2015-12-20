using System;
using System.Runtime.InteropServices;
using PowerManagement.PowerManagementTypes;

namespace PowerManagement.Internal
{
    internal static class PowerManagementFunctions
    {
        [DllImport("powrprof.dll")]
        internal static extern uint CallNtPowerInformation(
            [In]  UInt32 informationLevel,
            [In]  IntPtr lpInputBuffer,
            [In]  Int32 nInputBufferSize,
            [Out] out ulong lpOutputBuffer,
            [In]  Int32 nOutputBufferSize
        );

        [DllImport("powrprof.dll")]
        internal static extern uint CallNtPowerInformation(
            [In]  UInt32 informationLevel,
            [In]  IntPtr lpInputBuffer,
            [In]  Int32 nInputBufferSize,
            [Out] out SystemBatteryState lpOutputBuffer,
            [In]  Int32 nOutputBufferSize
        );

        [DllImport("powrprof.dll")]
        internal static extern uint CallNtPowerInformation(
            [In]  UInt32 informationLevel,
            [In]  IntPtr lpInputBuffer,
            [In]  Int32 nInputBufferSize,
            [Out] out SystemPowerInformation lpOutputBuffer,
            [In]  Int32 nOutputBufferSize
        );

        [DllImport("powrprof.dll")]
        internal static extern uint CallNtPowerInformation(
            [In]  UInt32 informationLevel,
            [In]  IntPtr lpInputBuffer,
            [In]  Int32 nInputBufferSize,
            [Out] out IntPtr lpOutputBuffer,
            [In]  Int32 nOutputBufferSize
        );

        [DllImport("powrprof.dll")]
        internal static extern byte SetSuspendState(
            [In] byte hibernate,
            [In] byte forceCritical,
            [In] byte disableWakeEvent
        );

    }
}
