using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.ViewInterfaces
{
    public interface IResearcherForm
    {
        string ChosenMaterial { get; }
        double[] Properties { set; }
        double[] Coefficients { set; }
        double[] CanalGeometry { get; }
        double[] VariableParams { get; }
        int NumberOfSteps { get; }
        event EventHandler calculate;
        event EventHandler materialChanged;
        void Start(string name, string[] materialNames);
        void VariableOutOfBounds(List<int> variablesWithErrors, double[] minLimits,
            double[] maxLimits);
        void DivideByZeroError();
    }
}
