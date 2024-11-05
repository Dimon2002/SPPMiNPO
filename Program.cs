using SPPMiNPO.DataType;
using SPPMiNPO.Generators;
using System.Globalization;

namespace SPPMiNPO;

public class Program
{
    private static readonly string OutPath = "../../../out.txt";

    static void Main()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        Console.WriteLine("Hello, World!");

        var distribution = new HuberDistribution(new HuberParameters()
        {
            V = 0.05,
            K = 1.398,
            P = 0.796
        });

        using var writer = new StreamWriter(OutPath);

        int i = 1;
        foreach (var item in distribution.GenerateValues(500))
        {
            writer.WriteLine($"{i} {item}");
            ++i;
        };
    }
}

