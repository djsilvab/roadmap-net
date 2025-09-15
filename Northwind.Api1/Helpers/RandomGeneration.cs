
using System.Security.Cryptography;

namespace Northwind.Api1.Helpers;

public class RandomGeneration
{
    private static RandomNumberGenerator _global = RandomNumberGenerator.Create();

    [ThreadStatic]
    private static Random _local;

    public static double NextDouble()
    {
        Random inst = _local;
        if (inst == null)
        {
            byte[] buffer = new byte[4];
            _global.GetBytes(buffer);
            _local = inst = new Random(BitConverter.ToInt32(buffer, 0));
            
        }
        return inst.NextDouble();
    }
}
