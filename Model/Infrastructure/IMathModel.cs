using System.Configuration;
using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

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
