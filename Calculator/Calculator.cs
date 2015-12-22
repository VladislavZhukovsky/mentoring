using System;
using System.Runtime.InteropServices;

namespace Calculator
{
    [ComVisible(true)]
    [Guid("7AE386B0-B2CB-4E4C-A486-687A34E99FA5")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface ICalculator
    {
        int Add(int x, int y);
    }

    [ComVisible(true)]
    [Guid("5FAA2A04-AC0F-4DB3-8DA2-EB46B0624E5B")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Calculator: ICalculator
    {
        public Calculator() { }

        int ICalculator.Add(int x, int y)
        {
            return x + y;
        }
    }
}
