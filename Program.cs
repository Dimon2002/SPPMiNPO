using SPPMiNPO.DataType;
using SPPMiNPO.Generators;
using System.Globalization;

namespace SPPMiNPO;

public class Program
{
    private static readonly string OutPath = "../../../out.txt";
    private static int N = 500;

    private const double variableAlpha = 0.20d;
    private const double alpha1 = 0.05d;
    private const double alpha2 = 0.10d;
    private const double alpha3 = 0.15d;

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

        var values = distribution.GenerateValues(N);

        var quantiles = CalcQuantiles(values);

        Console.WriteLine($"Квантиль 5% {quantiles.Item1}\n" +
                          $"Квантиль 10% {quantiles.Item2}\n" +
                          $"Квантиль 15% {quantiles.Item3}\n");


        // PrintArithmeticParameters(values);
        // WriteDataToFile(values);
    }

    private static Tuple<double, double, double> CalcQuantiles(double[] values)
    {
        var sortedValues = values.OrderBy(x => x).ToArray();

        var q1 = sortedValues[(int)((1 - alpha1) * N) + 1];
        var q2 = sortedValues[(int)((1 - alpha2) * N) + 1];
        var q3 = sortedValues[(int)((1 - alpha3) * N) + 1];

        return (q1, q2, q3).ToTuple();
    }

    private static void PrintArithmeticParameters(double[] values)
    {
        var mean = StatisticalLibrary.Mean(values);
        var median = StatisticalLibrary.Median(values);
        var variance = StatisticalLibrary.Variance(values);
        var coefficientAsymmetry = StatisticalLibrary.CoefficientAsymmetry(values);
        var coefficientExcess = StatisticalLibrary.CoefficientExcess(values);
        var meanCut = StatisticalLibrary.MeanCut(values, variableAlpha);

        var meanCut1 = StatisticalLibrary.MeanCut(values, alpha1);
        var meanCut2 = StatisticalLibrary.MeanCut(values, alpha2);
        var meanCut3 = StatisticalLibrary.MeanCut(values, alpha3);

        Console.WriteLine(
            $"Среднее арифметическое {mean:F7}\n" +
            $"Медиана {median:F7}\n" +
            $"Дисперсия {variance:F7}\n" +
            $"Коэффициент асимметрии {coefficientAsymmetry:F7}\n" +
            $"Коэффициент эксцесса {coefficientExcess:F7}\n" +
            $"Усечённое среднее 20% {meanCut:F7}\n" +
            $"Усечённое среднее 5% {meanCut1:F7}\n" +
            $"Усечённое среднее 10% {meanCut2:F7}\n" +
            $"Усечённое среднее 15% {meanCut3:F7}\n"
            );
    }

    private static void WriteDataToFile(double[] values)
    {
        using var writer = new StreamWriter(OutPath);

        int i = 1;
        foreach (var item in values)
        {
            writer.WriteLine($"{i} {item}");
            ++i;
        };
    }
}

