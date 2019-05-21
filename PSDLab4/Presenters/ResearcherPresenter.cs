using DomainModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using View.ViewInterfaces;
using Xceed.Words.NET;

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
            form.Stop();
            changeUser?.Invoke(this, null);
        }

        public void Start(string login)
        {
            researcherId = dataBase.GetUserIdViaLogin(login);
            researcherName = dataBase.GetUserNameViaId(researcherId);
            form.Start(researcherName, dataBase.GetAllMaterials());
        }

        private void ModelCalculationsFinished(object sender, EventArgs e)
        {
            double[] visc = model.GetViscosity();
            double[] temper = model.GetTemperatures();
            double perf = model.GetPerformance();
            int canalId = dataBase.CreateCanalRow(form.GetCanalGeometry());
            int experimentId = dataBase.CreateExperiment(visc[visc.Length - 1], temper[temper.Length - 1], perf,

            researcherId, materialId, canalId);
            dataBase.CreateVariablesValues(form.GetVariableParams(), experimentId);
            form.SetResults(temper, visc, form.GetCanalGeometry()[0], perf, model.GetTime());
        }

        private void FetchMaterialCoefficientsAndProperties(object sender, EventArgs e)
        {
            materialId = dataBase.GetMaterialIdViaName(form.ChosenMaterial);
            materialCoefficients = dataBase.FetchAllCoefficients(materialId);
            materialProperties = dataBase.FetchAllProperties(materialId);
            form.SetData(materialCoefficients, materialProperties);
            minLimitations = dataBase.FetchLimitsMin(materialId);
            maxLimitations = dataBase.FetchLimitsMax(materialId);
        }

        private void Calculate(object sender, EventArgs e)
        {
            bool areInputParametrsCorrect = true;
            List<int> wrongInputParametrsIndexes = new List<int>();
            double[] CanalGeometry = form.GetCanalGeometry();
            double[] VariableParams = form.GetVariableParams();
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
                if (VariableParams[i] < minLimitations[i] ||
                    VariableParams[i] > maxLimitations[i])
                {
                    areInputParametrsCorrect = false;
                    wrongInputParametrsIndexes.Add(i);
                }
            }

            if (areInputParametrsCorrect)
            {
                try
                {

                    model.Calculate(form.GetCoefs(), form.GetParams(),
                        CanalGeometry, VariableParams, form.NumberOfSteps);


                }
                catch (DivideByZeroException)
                {
                    form.DivideByZeroError();
                }
            }
            else
            {
                form.VariableOutOfBounds(wrongInputParametrsIndexes, minLimitations, maxLimitations);
            }
        }
        public void Stop()
        {
            form.Stop();
        }

        public void GenerateReport(object sender, EventArgs e)
        {
            DataTable dt = form.GetDataForReport();
            string fileName = form.GetFileName();
            var doc = DocX.Create(fileName);

            string text = "Отчёт о моделировании";
            Formatting f = new Formatting();
            f.FontFamily = new Font("Times New Roman");
            f.Size = 32;
            Paragraph paragraphTitle = doc.InsertParagraph(text, false, f);
            paragraphTitle.Alignment = Alignment.center;

            int rows = dt.Rows.Count;
            int columns = dt.Columns.Count+1;

            text = $"Материал: {this.form.ChosenMaterial}\n";
            f.Position = 2;
            f.Size = 14;
            Paragraph paragraph1 = doc.InsertParagraph(text, false, f);
            paragraph1.Alignment = Alignment.left;

            string[] ParamStr = { "Длина, м", "Ширина, м", "Глубина, м", "Скорость крышки, м/с", "Температура крышки, °C", "Количество шагов по длине канала",
            "Плотность, кг/м³", "Удельная теплоёмкость, Дж/( кг*°C )", "Температура плавления, °C ", "Коэффициент консистенции, Па*сⁿ", "Коэффициент теплоотдачи от крышки, Вт/(м²*с)",
            "Температурный коэффициент вязкости, 1/°C","Индекс течения материала","Температура приведения,  °C" };

            double[][] tmp = new double[4][];

            tmp[0] = form.GetCanalGeometry();
            tmp[1] = form.GetVariableParams();
            tmp[2] = dataBase.FetchAllProperties(materialId);
            tmp[3] = dataBase.FetchAllCoefficients(materialId);
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
            paragraph.Alignment = Alignment.both;

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

            double[] results = form.GetResults();
            string final = String.Format("Значения выходных параметров:\nПроизводительность, кг/с: {0}\nТемпература, °C: {1}\nВязкость, Па*с: {2}", results[0], results[1], results[2]);
            f.Size = 14;
            Paragraph fin = doc.InsertParagraph(final, false, f);
            fin.Alignment = Alignment.left;

            doc.Save();
        }
    }
}
