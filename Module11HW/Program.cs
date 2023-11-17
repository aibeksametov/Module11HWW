using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
enum Gender
{
    Male,
    Female
}

enum EducationForm
{
    FullTime,
    PartTime
}
struct Student
{
    public string FullName;
    public string Group;
    public double AverageGrade;
    public double FamilyIncome;
    public int FamilyMembers;
    public Gender Gender;
    public EducationForm EducationForm;

    public override string ToString()
    {
        return $"{FullName} ({Group}) - Avg Grade: {AverageGrade}, Family Income: {FamilyIncome}, Family Members: {FamilyMembers}, Gender: {Gender}, Education Form: {EducationForm}";
    }
}

class Program
{
    static void Main()
    {
        List<Student> students = new List<Student>();
        students.Add(new Student { FullName = "Aiba Samet", Group = "A1", AverageGrade = 4.5, FamilyIncome = 150000, FamilyMembers = 4, Gender = Gender.Male, EducationForm = EducationForm.FullTime });
        students.Add(new Student { FullName = "Alua Saken", Group = "B2", AverageGrade = 5.0, FamilyIncome = 100000, FamilyMembers = 3, Gender = Gender.Female, EducationForm = EducationForm.FullTime });
        Console.WriteLine("Report 1: Priority provision of places in the hostel:");
        var priorityList = students
            .OrderByDescending(s => s.FamilyIncome / s.FamilyMembers < 2 * GetMinSalary())
            .ThenByDescending(s => s.AverageGrade);

        PrintStudents(priorityList);
        Console.WriteLine("\nReport 2: Priority of providing places in the hostel (5 green, 5 yellow, the rest red):");
        var coloredList = students
            .OrderByDescending(s => s.AverageGrade)
            .Select((s, index) => new { Student = s, Index = index })
            .GroupBy(x => x.Index < 10 ? "Green" : (x.Index < 20 ? "Yellow" : "Red"))
            .SelectMany(g => g.Select(x => x.Student));

        PrintStudents(coloredList);
        Console.WriteLine("\nReport 3: List of students with incomplete family:");
        var incompleteFamilyList = students.Where(s => s.FamilyMembers < 5);
        PrintStudents(incompleteFamilyList);

        Console.WriteLine("\nReport 4: 5 students with the highest score:");
        var topStudents = students.OrderByDescending(s => s.AverageGrade).Take(5);
        PrintStudents(topStudents);
        Console.WriteLine("\nReport 5: 5 students with the lowest score:");
        var bottomStudents = students.OrderBy(s => s.AverageGrade).Take(5);
        PrintStudents(bottomStudents);

        Console.WriteLine("\nReport 6: 3 students with the lowest family income and an inferior family:");
        var lowIncomeIncompleteFamily = students
            .Where(s => s.FamilyIncome < 2 * GetMinSalary() && s.FamilyMembers < 5)
            .OrderBy(s => s.FamilyIncome)
            .Take(3);
        PrintStudents(lowIncomeIncompleteFamily);
    }
    static double GetMinSalary()
    {
        return 50000;
    }
    static void PrintStudents(IEnumerable<Student> studentList)
    {
        foreach (var student in studentList)
        {
            Console.WriteLine(student);
        }
    }
}
