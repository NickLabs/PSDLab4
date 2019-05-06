using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.ViewInterfaces;
using DomainModel.Infrastructure;

namespace PSDLab4.Presenters
{
    public class ResearcherPresenter
    {
        private readonly IResearcherForm form;
        private readonly IMathModel model;
        private readonly IDataBaseModel dataBase;
        private int materialId;
        private int researcherId;
        private string researcherName;
        private double[] materialCoefficients;
        private double[] materialProperties;
        private double[] minLimitations;
        private double[] maxLimitations;

        public ResearcherPresenter(IResearcherForm form, IMathModel model, IDataBaseModel dataBase)
        {
            this.form = form;
            this.model = model;
            this.dataBase = dataBase;
            this.form.calculate += Calculate;
            this.form.materialChanged += FetchMaterialCoefficientsAndProperties;
            this.model.calculationFinished += ModelCalculationsFinished;
        }

        public void Start(string login)
        {
            this.researcherId = this.dataBase.GetUserIdViaLogin(login);
            this.researcherName = this.dataBase.GetUserNameViaId(this.researcherId);
            this.form.Start(this.researcherName, this.dataBase.GetAllMaterials());
        }

        private void ModelCalculationsFinished(object sender, EventArgs e)
        {
            double[] visc = this.model.GetViscosity();
            double[] temper = this.model.GetTemperatures();
            double perf = this.model.GetPerformance();
            int canalId = dataBase.CreateCanalRow(this.form.GetCanalGeometry());
            int experimentId = dataBase.CreateExperiment(visc[visc.Length - 1], temper[temper.Length - 1], perf,
                this.researcherId, this.materialId, canalId);
            this.dataBase.CreateVariablesValues(this.form.GetVariableParams(), experimentId);
            this.form.SetResults(temper, visc, this.form.GetCanalGeometry()[0],perf);
            //Дальше кидаем в базу результаты, и кидаем всё в форму для графиков и прочего
        }

        private void FetchMaterialCoefficientsAndProperties(object sender, EventArgs e)
        {
            this.materialId = this.dataBase.GetMaterialIdViaName(this.form.ChosenMaterial);
            this.materialCoefficients= this.dataBase.FetchAllCoefficients(this.materialId);
            this.materialProperties = this.dataBase.FetchAllProperties(this.materialId);
            this.form.SetData(this.materialCoefficients, this.materialProperties);
            this.minLimitations = this.dataBase.FetchLimitsMin(this.materialId);
            this.maxLimitations = this.dataBase.FetchLimitsMax(this.materialId);
        }

        private void Calculate(object sender, EventArgs e)
        {
            bool areInputParametrsCorrect = true;
            List<int> wrongInputParametrsIndexes = new List<int>();
            double[] CanalGeometry = this.form.GetCanalGeometry();
            double[] VariableParams = this.form.GetVariableParams();
            for(int i = 0; i < CanalGeometry.Length; i++)
            {
                if(CanalGeometry[i] < 0.00001 || 
                    CanalGeometry[i] > 100000)
                {
                    areInputParametrsCorrect = false;
                    wrongInputParametrsIndexes.Add(-1);
                    break;
                }
            }

            for(int i = 0; i < VariableParams.Length-1; i++)
            {
                if(VariableParams[i] < this.minLimitations[i] ||
                    VariableParams[i] > this.maxLimitations[i])
                {
                    areInputParametrsCorrect = false;
                    wrongInputParametrsIndexes.Add(i);
                }
            }

            if (areInputParametrsCorrect)
            {
                try
                {
                    this.model.Calculate(materialCoefficients, materialProperties,
                        CanalGeometry, VariableParams , this.form.NumberOfSteps);
                    
                }
                catch (DivideByZeroException)
                {
                    this.form.DivideByZeroError();
                }
            }
            else
            {
                this.form.VariableOutOfBounds(wrongInputParametrsIndexes, minLimitations, maxLimitations);
            }
        }
    }
}
