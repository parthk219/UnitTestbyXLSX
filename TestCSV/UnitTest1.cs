//// CalculatorTests.cs
//using NUnit.Framework;
//using UnitTestXML;

//[TestFixture]
//public class CalculatorTests
//{
//    [Test]
//    public void Multiply_ValidInputs_ReturnsCorrectResult()
//    {
//        // Arrange
//        Calculator calculator = new Calculator();

//        // Act
//        int result = calculator.Multiply(2, 3);

//        // Assert
//        Assert.AreEqual(6, result);
//    }

//    [Test]
//    public void Multiply_ZeroAsInput_ReturnsZero()
//    {
//        // Arrange
//        Calculator calculator = new Calculator();

//        // Act
//        int result = calculator.Multiply(0, 5);

//        // Assert
//        Assert.AreEqual(0, result);
//    }
//}














using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;
using UnitTestXML;
using System.Text;

public class MultiplicationTests
{
    public static IEnumerable<TestCaseData> TestDataFromExcel()
    {
        var testCases = new List<TestCaseData>();
        string excelFilePath = @"C:\Users\parth\source\repos\UnitTestXML\TestCSV\Excel\UT.xlsx";

        using (var stream = File.Open(excelFilePath, FileMode.Open, FileAccess.Read))
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                });

                var table = result.Tables[0];

                foreach (DataRow row in table.Rows)
                {
                    int operand1 = Convert.ToInt32(row["Operand1"]);
                    int operand2 = Convert.ToInt32(row["Operand2"]);
                    int expectedResult = Convert.ToInt32(row["ExpectedResult"]);

                    testCases.Add(new TestCaseData(operand1, operand2, expectedResult));   //to add a new test case to the testCases list. 
                }
            }
        }

        return testCases;
    }

    [Test]
    [TestCaseSource(nameof(TestDataFromExcel))]
    public void Multiply_WhenCalled_ReturnsCorrectResult(int operand1, int operand2, int expectedResult)
    {
        // Arrange
        var service = new Calculator();

        // Act
        int result = service.Multiply(operand1, operand2);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}











