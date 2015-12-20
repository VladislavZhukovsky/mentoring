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
            ulong lst;
            PerformOperation(
                PowerManagementFunctions.CallNtPowerInformation(
                    PowerInformationLevel.LastSleepTime,
                    IntPtr.Zero,
                    0,
                    out lst,
                    Marshal.SizeOf(typeof(ulong))
                ),
                false
            );
            return lst;
        }

        public ulong GetLastWakeTime()
        {
            ulong lst;
            PerformOperation(
                PowerManagementFunctions.CallNtPowerInformation(
                    PowerInformationLevel.LastWakeTime,
                    IntPtr.Zero,
                    0,
                    out lst,
                    Marshal.SizeOf(typeof(ulong))
                ),
                false
            );
            return lst;
        }

        public SystemBatteryState GetSystemBatteryState()
        {
            SystemBatteryState sbs;
            PerformOperation(
                PowerManagementFunctions.CallNtPowerInformation(
                    PowerInformationLevel.SystemBatteryState,
                    IntPtr.Zero,
                    0,
                    out sbs,
                    Marshal.SizeOf(typeof(SystemBatteryState))
                ),
                false
            );
            return sbs;
        }

        public SystemPowerInformation GetSystemPowerInformation()
        {
            SystemPowerInformation spi;
            PerformOperation(
                PowerManagementFunctions.CallNtPowerInformation(
                    PowerInformationLevel.SystemPowerInformation,
                    IntPtr.Zero,
                    0,
                    out spi,
                    Marshal.SizeOf(typeof(SystemPowerInformation))
                ),
                false
            );
            return spi;
        }

        public void ReserveHibernationFile()
        {
            byte lpInBuffer = 1;
            IntPtr lpInBufferPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)));
            Marshal.StructureToPtr(lpInBuffer, lpInBufferPtr, false);
            IntPtr lpOutBufferPtr;
            PerformOperation(
                PowerManagementFunctions.CallNtPowerInformation(
                    10,
                    lpInBufferPtr,
                    Marshal.SizeOf(typeof(byte)),
                    out lpOutBufferPtr,
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
            IntPtr lpOutBufferPtr;
            PerformOperation(
                PowerManagementFunctions.CallNtPowerInformation(
                    10,
                    lpInBufferPtr,
                    Marshal.SizeOf(typeof(byte)),
                    out lpOutBufferPtr,
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
