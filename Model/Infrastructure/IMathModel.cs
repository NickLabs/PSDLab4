using System;

namespace DomainModel.Infrastructure
{
    public interface IMathModel
    {
        double[] GetTemperatures();
        double[] GetViscosity();
        double GetPerformance();
        event EventHandler calculationFinished;
        void Calculate(double[] Coefficients, double[] Properties, double[] canalGeometry, double[] varParametrs, int numberOfSteps);
        string GetTime();
    }
}
