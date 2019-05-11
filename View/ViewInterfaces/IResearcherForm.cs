using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.ViewInterfaces
{
    public interface IResearcherForm
    {
        string ChosenMaterial { get; }
        double[] GetCanalGeometry();
        double[] GetVariableParams();
        int NumberOfSteps { get; }
        event EventHandler calculate;
        event EventHandler materialChanged;
        event EventHandler generateReport;
        event EventHandler changeUser;
        void Start(string name, string[] materialNames);
        void Stop();
        void VariableOutOfBounds(List<int> variablesWithErrors, double[] minLimits,
            double[] maxLimits);
        void DivideByZeroError();
        void SetData(double[] coefficients, double[] properties);
        void SetResults(double[] temperature, double[] viscosity, double length, double output);
        DataTable GetDataForReport();
        string GetFileName();
        double[] GetResults();
    }
}
