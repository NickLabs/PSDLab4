using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;
using View.ViewInterfaces;
using DomainModel.Infrastructure;
using System.Diagnostics;
using System.IO;
using System.Data;

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

        public event EventHandler changeUser;

        public ResearcherPresenter(IResearcherForm form, IMathModel model, IDataBaseModel dataBase)
        {
            this.form = form;
            this.model = model;
            this.dataBase = dataBase;
            this.form.calculate += Calculate;
            this.form.changeUser += ChangeUserLogics;
            this.form.materialChanged += FetchMaterialCoefficientsAndProperties;
            this.model.calculationFinished += ModelCalculationsFinished;
            this.form.generateReport += GenerateReport;
        }

        private void ChangeUserLogics(object sender, EventArgs e)
        {
            this.form.Stop();
            this.changeUser?.Invoke(this, null);
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
            this.form.SetResults(temper, visc, this.form.GetCanalGeometry()[0], perf, this.model.GetTime());
        }

        private void FetchMaterialCoefficientsAndProperties(object sender, EventArgs e)
        {
            this.materialId = this.dataBase.GetMaterialIdViaName(this.form.ChosenMaterial);
            this.materialCoefficients = this.dataBase.FetchAllCoefficients(this.materialId);
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
            for (int i = 0; i < CanalGeometry.Length; i++)
            {
                if (CanalGeometry[i] <= 0)
                {
                    areInputParametrsCorrect = false;
                    wrongInputParametrsIndexes.Add(-1);
                    break;
                }
            }

            for (int i = 0; i < VariableParams.Length - 1; i++)
            {
                if (VariableParams[i] < this.minLimitations[i] ||
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
                    this.model.Calculate(this.form.GetCoefs(), this.form.GetParams(),
                        CanalGeometry, VariableParams, this.form.NumberOfSteps);

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
        public void Stop()
        {
            this.form.Stop();
        }

        public void GenerateReport(object sender, EventArgs e)
        {
            DataTable dt = this.form.GetDataForReport();
            string fileName = this.form.GetFileName();
            var doc = DocX.Create(fileName);
            string text = "Отчёт о моделлировании";
            Formatting f = new Formatting();
            f.FontFamily = new Font("Times New Roman");
            f.Size = 32;
            Paragraph paragraphTitle = doc.InsertParagraph(text, false, f);
            paragraphTitle.Alignment = Alignment.center;

            int rows = dt.Rows.Count;
            int columns = dt.Columns.Count+1;

            string[] ParamStr = { "Длина, м", "Ширина, м", "Глубина, м", "Скорость крышки, м/с", "Темература крышки, °C", "Количество шагов по длине канала",
            "Плотность, кг/м³", "Удельная теплоёмкость, Дж/( кг*°C )", "Температура плавления, °C ", "Коэффициент консистенции, Па*сⁿ", "Коэффициент теплоотдачи от крышки, Вт/(м²*с)",
            "Температурный коэффициент вязкости, 1/°C","Индекс течения материала","Температура приведения,  °C" };

            double[][] tmp = new double[4][];
            tmp[0] = this.form.GetCanalGeometry();
            tmp[1] = this.form.GetVariableParams();
            tmp[2] = this.dataBase.FetchAllProperties(materialId);
            tmp[3] = this.dataBase.FetchAllCoefficients(materialId);
            List<string> ParamsVal = new List<string>();
            foreach(double[] s in tmp)
            {
                foreach(double ss in s)
                {
                    ParamsVal.Add(ss.ToString());
                }
            }
            text = "";
            for(int i = 0; i < ParamStr.Length; i++)
            {
                text += (ParamStr[i] + ": " + ParamsVal[i] + "; ");
            }
            text += '\n';
            f.Position = 2;
            f.Size = 14;
            Paragraph paragraph = doc.InsertParagraph(text, false, f);
            paragraphTitle.Alignment = Alignment.both;

            text = "Таблица 1, Значения вязкости и температуры";
            f.Size = 12;
            Paragraph pars = doc.InsertParagraph(text, false, f);
            pars.Alignment = Alignment.left;

            Table t = doc.AddTable(rows, columns);
            t.Alignment = Alignment.center;
            t.Rows[0].Cells[0].Paragraphs.First().Append("Номер");
            t.Rows[0].Cells[1].Paragraphs.First().Append("Координата по длине канала, м");
            t.Rows[0].Cells[2].Paragraphs.First().Append("Температура, °C");
            t.Rows[0].Cells[3].Paragraphs.First().Append("Вязкость, Па*с");
            for (int i = 1; i < rows; i++)
            {
                t.Rows[i].Cells[0].Paragraphs.First().Append(i.ToString());
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    t.Rows[i].Cells[j+1].Paragraphs.First().Append(dt.Rows[i-1].ItemArray[j].ToString());
                }
            }
            doc.InsertTable(t);
            doc.InsertParagraph();

            f.Size = 11;
            string[] ttmp = { "вязкости", "температуры" };
            for (int i = 1; i < 3; i++)
            {
                Image img = doc.AddImage(Directory.GetCurrentDirectory() + String.Format(@"/tmp/{0}.png", i));
                Picture p = img.CreatePicture();
                Paragraph par = doc.InsertParagraph();
                par.AppendPicture(p);
                par.Alignment = Alignment.center;

                
                text = String.Format("Рисунок {0}, зависимость {1} от длины", i, ttmp[i-1]);
                Paragraph paragraphForUnderLine = doc.InsertParagraph(text, false, f);
                paragraphForUnderLine.Alignment = Alignment.center;
            }

            double[] results = this.form.GetResults();
            string final = String.Format("Значения выходных параметров:\nПроизводительность, кг/с: {0}\nТемпература, °C: {1}\nВязкость, Па*с: {2}", results[0], results[1], results[2]);
            f.Size = 14;
            Paragraph fin = doc.InsertParagraph(final, false, f);
            fin.Alignment = Alignment.left;

            doc.Save();
        }
    }
}
