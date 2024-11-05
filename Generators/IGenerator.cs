namespace SPPMiNPO.Generators;

public interface IGenerator
{
    double GenerateValue();

    double[] GenerateValues(int N);
}