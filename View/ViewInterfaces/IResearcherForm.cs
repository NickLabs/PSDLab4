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
        double[] Properties { set; get; }
        double[] Coefficients { set; get; }
        double[] CanalGeometry { get; }
        double[] VariableParams { get; }
        event EventHandler calculate;
        event EventHandler materialChanged;
        void Start(string name, string[] materialNames);
    }
}
