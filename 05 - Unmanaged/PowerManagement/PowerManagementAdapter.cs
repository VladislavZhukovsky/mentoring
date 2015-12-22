using System;
using System.Runtime.InteropServices;
using PowerManagement.Exceptions;
using PowerManagement.PowerManagementTypes;
using PowerManagement.Internal;

namespace PowerManagement
{
    public class PowerManagementAdapter
    {
        public ulong GetLastSleepTime()
        {
            IntPtr retVal = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(UInt64)));
            PerformOperation(
                PowerManagementFunctions.CallNtPowerInformation(
                    PowerInformationLevel.LastSleepTime,
                    IntPtr.Zero,
                    0,
                    retVal,
                    Marshal.SizeOf(typeof(ulong))
                ),
                false
            );
            ulong lst = (ulong)Marshal.PtrToStructure(retVal, typeof(UInt64));
            return lst;
        }

        public ulong GetLastWakeTime()
        {
            IntPtr retVal = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(UInt64)));
            PerformOperation(
                PowerManagementFunctions.CallNtPowerInformation(
                    PowerInformationLevel.LastWakeTime,
                    IntPtr.Zero,
                    0,
                    retVal,
                    Marshal.SizeOf(typeof(ulong))
                ),
                false
            );
            ulong lwt = (ulong)Marshal.PtrToStructure(retVal, typeof(UInt64));
            return lwt;
        }

        public SystemBatteryState GetSystemBatteryState()
        {
            IntPtr retVal = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(SystemBatteryState)));
            PerformOperation(
                PowerManagementFunctions.CallNtPowerInformation(
                    PowerInformationLevel.SystemBatteryState,
                    IntPtr.Zero,
                    0,
                    retVal,
                    Marshal.SizeOf(typeof(SystemBatteryState))
                ),
                false
            );
            SystemBatteryState sbs = (SystemBatteryState)Marshal.PtrToStructure(retVal, typeof(SystemBatteryState));
            return sbs;
        }

        public SystemPowerInformation GetSystemPowerInformation()
        {
            IntPtr retVal = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(SystemPowerInformation)));
            PerformOperation(
                PowerManagementFunctions.CallNtPowerInformation(
                    PowerInformationLevel.SystemPowerInformation,
                    IntPtr.Zero,
                    0,
                    retVal,
                    Marshal.SizeOf(typeof(SystemPowerInformation))
                ),
                false
            );
            SystemPowerInformation spi = (SystemPowerInformation)Marshal.PtrToStructure(retVal, typeof(SystemPowerInformation));
            return spi;
        }

        public void ReserveHibernationFile()
        {
            byte lpInBuffer = 1;
            IntPtr lpInBufferPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)));
            Marshal.StructureToPtr(lpInBuffer, lpInBufferPtr, false);
            IntPtr lpOutBufferPtr = IntPtr.Zero;
            PerformOperation(
                PowerManagementFunctions.CallNtPowerInformation(
                    10,
                    lpInBufferPtr,
                    Marshal.SizeOf(typeof(byte)),
                    lpOutBufferPtr,
                    0
                ),
                false
            );
        }

        public void RemoveHibernationFile()
        {
            byte lpInBuffer = 0;
            IntPtr lpInBufferPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)));
            Marshal.StructureToPtr(lpInBuffer, lpInBufferPtr, false);
            IntPtr lpOutBufferPtr = IntPtr.Zero;
            PerformOperation(
                PowerManagementFunctions.CallNtPowerInformation(
                    10,
                    lpInBufferPtr,
                    Marshal.SizeOf(typeof(byte)),
                    lpOutBufferPtr,
                    0
                ),
                false
            );
        }

        public void SetSuspendState()
        {
            PerformOperation(
                PowerManagementFunctions.SetSuspendState(
                    (byte)0,
                    (byte)0,
                    (byte)0
                ),
                true
            );
        }

        private void PerformOperation(uint errorCode, bool inverseError)
        {
            if (inverseError)
            {
                if (errorCode == 0)
                {
                    throw new PowerManagementException(string.Format("Function executed with error code {0}", errorCode), errorCode);
                }
                return;
            }
            else
            {
                if (errorCode == 0)
                {
                    return;
                }
                throw new PowerManagementException(string.Format("Function executed with error code {0}", errorCode), errorCode);
            }
        }
    }
}
