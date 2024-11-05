using SPPMiNPO.DataType;

namespace SPPMiNPO.Generators;

public class HuberDistribution : IDistribution
{
    private HuberParameters _parameters;

    private readonly Random _random = new();
    private readonly NormalDistribution _normalDistribution = new();

    public IDistributionParameters Parameters => _parameters;

    public HuberDistribution(HuberParameters parameters)
    {
        _parameters = parameters;

         //Initialize(); // why man 
    }

    private void Initialize()
    {
        _parameters.P = 2 * Density(_parameters.K, _parameters.V) * (1 - _parameters.V) / _parameters.K;
    }

    public double GenerateValue()
    {
        double r1 = GetRandomValue();

        if (r1 <= _parameters.P)
        {
            double x1;
            do
            {
                x1 = _normalDistribution.GenerateValue();
            } while (double.Abs(x1) > _parameters.K);

            return x1;
        }

        double r2 = GetRandomValue();
        double x2 = _parameters.K - (double.Log(r2)) / _parameters.K;

        return r1 < (1 + _parameters.P) / 2 ? x2 : -x2;
    }

    public double[] GenerateValues(int N)
    {
        IList<double> values = [];

        for (int i = 0; i < N; ++i)
        {
            var value = GenerateValue();
            values.Add(value);
        }

        return [.. values];
    }

    private double Density(double x, double v)
    {
        double coefficient = (1 - v) / double.Sqrt(2 * double.Pi);

        if (double.Abs(x) <= _parameters.K)
        {
            return coefficient * double.Exp(-double.Pow(x, 2) / 2);
        }

        return coefficient * double.Exp(double.Pow(_parameters.K, 2) / 2 - _parameters.K * double.Abs(x));
    }

    private double GetRandomValue()
    {
        return _random.NextDouble();
    }


}
