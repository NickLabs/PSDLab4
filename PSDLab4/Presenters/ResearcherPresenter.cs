using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.ViewInterfaces;
using DomainModel.Infrastructure;

namespace PSDLab4.Presenters
{
    class ResearcherPresenter
    {
        private readonly IResearcherForm form;
        private readonly IMathModel model;
        private readonly IDataBaseModel dataBase;
        private int researcherId;
        private string researcherName;

        public ResearcherPresenter(IResearcherForm form, IMathModel model, IDataBaseModel dataBase)
        {
            this.form = form;
            this.model = model;
            this.dataBase = dataBase;
        }

        public void Start(string login)
        {
            this.researcherId=this.dataBase.getUserIdViaLogin(login);
            this.researcherName = this.dataBase.getUserNameViaId(this.researcherId);
            this.form.Start(this.researcherName, this.dataBase.getAllMaterials());
            this.form.calculate += Calculate;
            this.form.materialChanged += FetchMaterialCoefficientsAndProperties;
        }

        private void FetchMaterialCoefficientsAndProperties(object sender, EventArgs e)
        {
            this.form.Coefficients = this.dataBase.fetchAllCoefficients(this.form.ChosenMaterial);
            this.form.Properties = this.dataBase.fetchAllProperties(this.form.ChosenMaterial);
        }

        private void Calculate(object sender, EventArgs e)
        {
            
        }

        
    }
}
