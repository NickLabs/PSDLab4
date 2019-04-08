using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Infrastructure;

namespace DomainModel.Service
{
    class MathModel : IMathModel
    {
        public double[] temperatures { get; }
        public double[] viscosity { get; }
        public double performance { get; }
        public void Calculate()
        {

        }
    }
}
