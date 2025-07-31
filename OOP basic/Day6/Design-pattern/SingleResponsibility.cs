using System.Drawing;

namespace SOLID
{
    #region Single Responsibility Principle (SRP)
    /*
     - A class should have only one reason to change, meaning it should have only one job or responsibility.
     - This principle helps in reducing the complexity of the code and makes it easier to maintain.
     */

    //Bad Example:
    public class  BadEmployee
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }

        //responsibility 1: Employee data management
        public void SaveToDatabase()
        {
           //save logic
        }

        //responsibility 2: Bonus calculation
        public decimal CalculateBonus()
        {
            //bonus calculation logic
            return 1; 
        }

        //responsibility 3: Report generation
        public string reportGeneration()
        {
            //report generation logic
            return "Report";
        }
    }

    //Good Example:
    public class Employee
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }
    }

    public class EmployeeRepository
    {
        public void SaveToDatabase(Employee employee)
        {
            //save logic
        }
    }

    public class BonusCalculator
    {
        public decimal CalculateBonus(Employee employee)
        {
            //bonus calculation logic
            return 1; 
        }
    }

    public class ReportGenerator
    {
        public string GenerateReport(Employee employee)
        {
            //report generation logic
            return "Report";
        }
    }
    #endregion


    
}
