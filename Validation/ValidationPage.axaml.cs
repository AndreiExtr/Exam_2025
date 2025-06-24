using Avalonia.Controls;
using Avalonia.Interactivity;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Math;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace Validation
{
    public partial class ValidationPage : Window
    {
        public ValidationPage()
        {
            InitializeComponent();
        }

        private async void GetOnClick(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            var response = client.GetStringAsync("http://localhost:4444/TransferSimulator/email").Result;
            var json = JObject.Parse(response);
            FullNameText.Text = json["value"].ToString();
        }

        private async void ValidationOnClick(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameText.Text;
            bool isValid = Regex.IsMatch(fullName, @"^[\w._%+-]+@[\w.-]+\.[a-zA-Z]{2,4}$");
            string result = isValid ? "Успешно" : "Не корректная электронная почта";
            ResultExcel(fullName, result);
            ValidationNameText.Text = result;
        }

        private async void ResultExcel(string input, string actualResult)
        {
            XLWorkbook workbook;
            IXLWorksheet sheet;
            string projectFolder = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
            string filePath = Path.Combine(projectFolder, "Результаты.xlsx");

            if (File.Exists(filePath)) {
                workbook = new XLWorkbook(filePath);
                sheet = workbook.Worksheet("Лист_1");
            }
            else
            {
                workbook = new XLWorkbook();
                sheet = workbook.Worksheets.Add("Лист_1");
            }

            sheet.Cell(1, 1).Value = "Email";
            sheet.Cell(1, 2).Value = "Результаты";

            int nextRow = sheet.LastRowUsed()?.RowNumber() + 1 ?? 2;

            sheet.Cell(nextRow, 1).Value = input;
            sheet.Cell(nextRow, 2).Value = actualResult;

            workbook.SaveAs(filePath); 

        }
    }
}