using System;
using System.Collections.Generic;
using System.Diagnostics;
using DomainModel.Infrastructure;

namespace DomainModel.Service
{
    public class MathModel : IMathModel
    {
        public double performance;

        private double formCoefficient;
        private double inputStreamSpeed;
        private double streamVolumeConsumptionRate;
        private double frictionHeatGain;
        private double capHeatGain;
        private List<double> temperaturesT = new List<double>();
        private List<double> viscosityT = new List<double>();
        private string t = "";

        public event EventHandler calculationFinished;

        public double GetPerformance()
        {
            return performance;
        }

        public double[] GetTemperatures()
        {
            return temperaturesT.ToArray();
        }

        public double[] GetViscosity()
        {
            return viscosityT.ToArray();
        }

        public string GetTime()
        {
            return t;
        }

        public void Calculate(double[] coefficients, double[] properties, double[] canalGeometry, double[] varParametrs, int numberOfSteps)
        {
            temperaturesT.Clear();
            viscosityT.Clear();
            double width, depth, length, capSpeed, capTemperature, step, density, meltingTemperature, heatCapacity;
            double consistencyCoefficientAllign, viscosityTemperatureCoefficient, temperatureAlign, flowIndex, capHeatTransmissionCoefficient;
            length = canalGeometry[0]; width = canalGeometry[1]; depth = canalGeometry[2];
            capSpeed = varParametrs[0]; capTemperature = varParametrs[1]; step = length / numberOfSteps;
            density = properties[0]; heatCapacity = properties[1]; meltingTemperature = properties[2];
            consistencyCoefficientAllign = coefficients[0]; viscosityTemperatureCoefficient = coefficients[1];
            temperatureAlign = coefficients[2]; flowIndex = coefficients[3]; capHeatTransmissionCoefficient = coefficients[4];

            Stopwatch stop = new Stopwatch();
            stop.Start();
            FormCoefficientCalculation( depth,  width);
            InputStreamSpeedCalculation(depth, capSpeed);
            StreamVolumeConsumptionRateCalculation(depth, width, capSpeed);
            CapHeatGainCalculation(width, viscosityTemperatureCoefficient, capHeatTransmissionCoefficient, capTemperature, temperatureAlign);
            FrictionHeatGainCalculation(width, depth, consistencyCoefficientAllign, flowIndex);
            TemperatureCalculation(viscosityTemperatureCoefficient, temperatureAlign, width,step, density, heatCapacity, meltingTemperature, length, capHeatTransmissionCoefficient);
            ViscosityCalculation(temperatureAlign, viscosityTemperatureCoefficient, consistencyCoefficientAllign, flowIndex);
            productionCalculation(density);
            stop.Stop();
            t = stop.Elapsed.ToString().Split(':')[2];

            calculationFinished?.Invoke(this, null);
        }
        private void FormCoefficientCalculation(double depth, double width)
        {
            formCoefficient = 0.125 * Math.Pow(depth / width, 2) - 0.625 * depth / width + 1;
        }
        private void InputStreamSpeedCalculation(double depth, double capSpeed)
        {
            inputStreamSpeed = capSpeed / depth;
        }
        private void StreamVolumeConsumptionRateCalculation(double depth, double width, double capSpeed)
        {
            streamVolumeConsumptionRate = ((depth * width * capSpeed) / 2) * formCoefficient;
        }
        private void CapHeatGainCalculation(double width, double viscosityTemperatureCoefficient,
            double capHeatTransmissionCoefficient, double capTemperature, double temperatureAlign)
        {
            capHeatGain = width * capHeatTransmissionCoefficient * (1 / viscosityTemperatureCoefficient -
                 capTemperature + temperatureAlign);
        }
        private void FrictionHeatGainCalculation(double width, double depth, double consistencyCoefficient, double streamFollowIndex)
        {
            frictionHeatGain = width * depth * consistencyCoefficient * Math.Pow(inputStreamSpeed, streamFollowIndex+1);
        }
        private void TemperatureCalculation(double viscosityTemperatureCoefficient, double temperatureAlign, double width,
            double step, double density, double heatCapacity, double meltingTemperature, double length, double capHeatTransmissionCoefficient)
        {
            for(double i = 0; i < length+step; i += step)
            {
                double logPart;
                double firstLog;
                double thirdLog;
                double secondLog;
                firstLog = (viscosityTemperatureCoefficient*frictionHeatGain+width*capHeatTransmissionCoefficient)/
                   (viscosityTemperatureCoefficient*capHeatGain);
                secondLog = 1 - Math.Exp(-1 * (i * viscosityTemperatureCoefficient * capHeatGain / (density * heatCapacity * streamVolumeConsumptionRate)));
                thirdLog = Math.Exp(viscosityTemperatureCoefficient * (meltingTemperature - temperatureAlign - (i * capHeatGain) / (density * heatCapacity * streamVolumeConsumptionRate)));
                logPart = firstLog * secondLog + thirdLog;
                double temperatureOnThisStep = temperatureAlign + 1 / viscosityTemperatureCoefficient * Math.Log(logPart);
                temperaturesT.Add(temperatureOnThisStep);
            }
        }
        private void ViscosityCalculation(double temperatureAllign, double viscosityTemperatureCoefficient,double consistencyCoefficientAllign, double flowIndex)
        {
            foreach(double T in temperaturesT)
            {
                double consistencyCoefficient = consistencyCoefficientAllign*Math.Exp(-1*viscosityTemperatureCoefficient*(T-temperatureAllign));
                double viscosityOnThisStep = consistencyCoefficient * Math.Pow(inputStreamSpeed, flowIndex-1);
                viscosityT.Add(viscosityOnThisStep);
            }
        }
        private void productionCalculation(double density)
        {
            performance = density * streamVolumeConsumptionRate;
        }
    }
}
