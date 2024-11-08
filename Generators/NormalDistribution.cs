﻿using SPPMiNPO.DataType;

namespace SPPMiNPO.Generators;

public class NormalDistribution : IDistribution
{
    private readonly Random _random = new();

    public IDistributionParameters Parameters { get; }

    public NormalDistribution(NormalParameters? normalParameters = null)
    {
        Parameters = normalParameters ?? new NormalParameters();
    }

    public double Density(double x)
    {
        return Math.Exp(-Math.Pow(x, 2) / 2) / Math.Sqrt(2 * Math.PI);
    }
    
    public double GenerateValue()
    {
        var firstRandomValue = GetRandomValue();
        var secondRandomValue = GetRandomValue();

        return Math.Sqrt(-2 * Math.Log(firstRandomValue)) * Math.Cos(2 * Math.PI * secondRandomValue);
    }

    private double GetRandomValue()
    {
        return _random.NextDouble();
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
}
