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
        }

        public void Start(string login)
        {
            this.researcherId=this.dataBase.GetUserIdViaLogin(login);
            this.researcherName = this.dataBase.GetUserNameViaId(this.researcherId);
            this.form.Start(this.researcherName, this.dataBase.GetAllMaterials());
            this.form.calculate += Calculate;
            this.form.materialChanged += FetchMaterialCoefficientsAndProperties;
            this.model.calculationFinished += ModelCalculationsFinished;
        }

        private void ModelCalculationsFinished(object sender, EventArgs e)
        {
            //Дальше кидаем в базу результаты, и кидаем всё в форму для графиков и прочего
        }

        private void FetchMaterialCoefficientsAndProperties(object sender, EventArgs e)
        {
            this.materialId = this.dataBase.GetMaterialIdViaName(this.form.ChosenMaterial);
            this.materialCoefficients= this.dataBase.FetchAllCoefficients(this.form.ChosenMaterial);
            this.materialProperties = this.dataBase.FetchAllProperties(this.form.ChosenMaterial);
            this.form.Coefficients = this.materialCoefficients;
            this.form.Properties = this.materialProperties;
            this.minLimitations = this.dataBase.FetchLimitsMin(this.materialId);
            this.maxLimitations = this.dataBase.FetchLimitsMax(this.materialId);
        }

        private void Calculate(object sender, EventArgs e)
        {
            int temp = 0;
            bool areInputParametrsCorrect = true;
            List<int> wrongInputParametrsIndexes = new List<int>();
            for(int i = 0; i < this.form.CanalGeometry.Length; i++)
            {
                if(this.form.CanalGeometry[i] < this.minLimitations[i] || 
                    this.form.CanalGeometry[i] > this.maxLimitations[i])
                {
                    areInputParametrsCorrect = false;
                    wrongInputParametrsIndexes.Add(i);
                }
                temp++;
            }
            for(int i = 0; i < this.form.VariableParams.Length-1; i++)
            {
                if(this.form.VariableParams[i] < this.minLimitations[i+temp] ||
                    this.form.VariableParams[i] > this.maxLimitations[i + temp])
                {
                    areInputParametrsCorrect = false;
                    wrongInputParametrsIndexes.Add(i+temp);
                }
            }

            if (areInputParametrsCorrect)
            {
                try
                {
                    this.model.Calculate(materialCoefficients, materialProperties,
                        this.form.CanalGeometry, this.form.VariableParams , this.form.NumberOfSteps);
                    
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
