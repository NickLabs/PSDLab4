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
        double[] temperatures { get; }
        double[] viscosity { get; }
        double performance { get; }
        event EventHandler calculationFinished;
        void Calculate(double[] Coefficients, double[] Properties, double[] canalGeometry, double[] varParametrs, int numberOfSteps);
    }
}
