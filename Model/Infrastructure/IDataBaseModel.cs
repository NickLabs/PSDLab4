﻿using System.Collections.Generic;
using System.Data;
using System.Collections;

namespace DomainModel.Infrastructure
{
    public interface IDataBaseModel
    {
        bool GetRule(string tableName);
        bool[] DoesUserExist(string login, string password);
        int GetUserIdViaLogin(string login);
        string GetUserLoginPassViaId(int id);
        string GetUserNameViaId(int id);
        string[] GetAllMaterials();
        string[] GetAllTables();
        int GetMaterialIdViaName(string name);
        double[] FetchAllProperties(int idMaterial);
        double[] FetchAllCoefficients(int idMaterial);
        double[] FetchLimitsMin(int idMaterial);
        double[] FetchLimitsMax(int idMaterial);
        int CreateExperiment(double viscosity, double temperature, double performance, int userId,  int materialId, int canalId);
        int CreateCanalRow(double[] dimensionsLWD);
        void CreateVariablesValues(double[] variablesST, int experimentId);
        void InsertRow(string table, ArrayList values);
        void UpdateRow(string table, ArrayList values,string[] columnNames);
        void DeleteRow(string table, int id);
        void DeleteRow(string table, int id1, int id2, string column1, string column2);
        Dictionary<string, int> IdAndRelevantNames(string tableName, string[] columnNames);
        Dictionary<int, string> IdAndRelevantNameReverse(string tableName, string[] columnNames);
        DataTable GetTableData(string tableName);
        string GetTranslationToRus(string str);
        string GetTranslationToEng(string str);
    }
}
