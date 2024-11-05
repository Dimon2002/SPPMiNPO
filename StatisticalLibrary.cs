namespace SPPMiNPO;

public static class StatisticalLibrary
{
    public static double Mean(double[] values)
    {
        return values.Sum() / values.Length;
    }

    public static double Median(double[] values)
    {
        var sortedValues = values.OrderBy(x => x).ToList();
        var countValues = sortedValues.Count;

        if (countValues % 2 == 0)
        {
            var value1 = sortedValues[countValues / 2];
            var value2 = sortedValues[countValues / 2 - 1];

            return (value1 + value2) / 2;
        }

        return sortedValues[countValues / 2];
    }

    public static double Variance(double[] values)
    {
        var countValues = values.Length;
        var mean = Mean(values);

        return values.Select(x => double.Pow(x - mean, 2)).Sum() / countValues;
    }

    public static double CoefficientAsymmetry(double[] values)
    {
        var countValues = values.Length;
        var mean = Mean(values);
        var variance = Variance(values);

        return values.Select(x => double.Pow(x - mean, 3)).Sum()
               / (countValues * double.Pow(variance, 1.5));
    }

    public static double CoefficientExcess(double[] values)
    {
        var countValues = values.Length;
        var mean = Mean(values);
        var varianceSquare = double.Pow(Variance(values), 2);

        return values.Select(x => double.Pow(x - mean, 4)).Sum()
               / (countValues * varianceSquare) - 3;
    }

    public static double MeanCut(double[] values, double alpha)
    {
        if (alpha < 0 || alpha >= 0.5d)
        {
            throw new ArgumentOutOfRangeException(nameof(alpha));
        }

        var countValues = values.Length;
        var sortedValues = values.OrderBy(x => x).ToList();

        var k = (int)(countValues * alpha);

        double sum = 0d;

        for (int i = k; i < countValues - k; ++i)
        {
            sum += sortedValues[i];
        }

        return sum / (countValues - 2 * k);
    }

}
