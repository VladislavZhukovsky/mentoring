using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrackMeDump
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            var res = eval_a(new string[] { "123", "456" });
        }

        private static bool eval_a(string[] A_0)
        {
            byte[] evalAa;
            int num1;
            // ISSUE: variable of a compiler-generated type
            NetworkInterface networkInterface;
            switch (0)
            {
                case 0:
                label_2:
                    // ISSUE: object of a compiler-generated type is created
                    //evalA = new Form1.eval_a();
                    networkInterface = Enumerable.FirstOrDefault<NetworkInterface>((IEnumerable<NetworkInterface>)NetworkInterface.GetAllNetworkInterfaces());
                    num1 = 0;
                    goto default;
                default:
                    while (true)
                    {
                        switch (num1)
                        {
                            case 0:
                                switch (-22053 == -22053 ? 1 : 0)
                                {
                                    case 0:
                                    case 2:
                                        goto label_8;
                                    default:
                                        if (1 == 0)
                                            ;
                                        if (0 == 0)
                                            ;
                                        num1 = networkInterface != null ? 1 : 2;
                                        continue;
                                }
                            case 1:
                                goto label_7;
                            case 2:
                            label_8:
                                num1 = 3;
                                continue;
                            case 3:
                                goto label_9;
                            default:
                                goto label_2;
                        }
                    }
                label_7:
                    byte[] addressBytes = networkInterface.GetPhysicalAddress().GetAddressBytes();
                    // ISSUE: reference to a compiler-generated field
                    evalAa = BitConverter.GetBytes(DateTime.Now.Date.ToBinary());
                    // ISSUE: reference to a compiler-generated method
                    //Func<byte, int, int> selector1 = new Func<byte, int, int>(evalA.eval_a);
                    int[] numArray = Enumerable.ToArray<int>(Enumerable.Select<int, int>(Enumerable.Select<byte, int>((IEnumerable<byte>)addressBytes, x => x), (Func<int, int>)(A_0_2 =>
                    {
                        if (A_0_2 <= 999)
                        {
                            int num2 = 17361;
                            int num3 = num2;
                            num2 = 17361;
                            int num4 = num2;
                            switch (num3 == num4 ? 1 : 0)
                            {
                                case 0:
                                case 2:
                                    break;
                                default:
                                    num2 = 0;
                                    if (num2 == 0)
                                        ;
                                    num2 = 0;
                                    num2 = 1;
                                    if (num2 == 0)
                                        ;
                                    return A_0_2 * 10;
                            }
                        }
                        return A_0_2;
                    })));
                    // ISSUE: reference to a compiler-generated field
                    //evalA.b = Enumerable.ToArray<int>(Enumerable.Select<string, int>((IEnumerable<string>)A_0_1, new Func<string, int>(int.Parse)));
                    // ISSUE: reference to a compiler-generated method
                    //Func<int, int, int> selector2 = new Func<int, int, int>(evalA.eval_a);
                    return Enumerable.All<int>(Enumerable.Select<int, int>((IEnumerable<int>)numArray, x => x), A_0_2 =>
                    {
                        int num2 = 862;
                        int num3 = num2;
                        num2 = 862;
                        int num4 = num2;
                        switch (num3 == num4)
                        {
                            case true:
                                int num5 = 1;
                                if (num5 == 0)
                                    ;
                                num5 = 0;
                                if (num5 == 0)
                                    ;
                                num5 = 0;
                                return A_0_2 == 0;
                            default:
                                throw new Exception();
                        }
                    });
                label_9:
                    return false;
            }
        }
    }
}
