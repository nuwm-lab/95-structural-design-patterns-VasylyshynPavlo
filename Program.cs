using System;
using System.Collections.Generic;

namespace LabWork
{
    public interface IEuropeanStudyProgram
    {
        string GetTitle();
        double GetEctsCredits();
        string GetGradeLetter();
    }
    
    public class LocalCourse
    {
        public string Name { get; private set; }
        public int AcademicHours { get; private set; }
        public int GradeScore { get; private set; }

        public LocalCourse(string name, int hours, int grade)
        {
            Name = name;
            AcademicHours = hours;
            GradeScore = grade;
        }

        public int GetHours() => AcademicHours;
        public int GetLocalGrade() => GradeScore;
    }
    
    public class LocalToEuropeanAdapter : IEuropeanStudyProgram
    {
        private readonly LocalCourse _localCourse;
    
        private const int HoursPerCredit = 30;

        public LocalToEuropeanAdapter(LocalCourse localCourse)
        {
            _localCourse = localCourse;
        }

        public string GetTitle()
        {
            return _localCourse.Name;
        }

        public double GetEctsCredits()
        {
            return (double)_localCourse.GetHours() / HoursPerCredit;
        }

        public string GetGradeLetter()
        {
            var score = _localCourse.GetLocalGrade();

            return score switch
            {
                >= 90 => "A",
                >= 82 => "B",
                >= 74 => "C",
                >= 64 => "D",
                >= 60 => "E",
                _ => "F"
            };
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var semesterPlan = new List<IEuropeanStudyProgram>();

            var localMath = new LocalCourse("Advanced mathematics", 120, 95);
            var localHistory = new LocalCourse("History", 60, 85);

            semesterPlan.Add(new LocalToEuropeanAdapter(localMath));
            semesterPlan.Add(new LocalToEuropeanAdapter(localHistory));

            Console.WriteLine("--- European Diploma Supplement ---");
            foreach (var course in semesterPlan)
            {
                Console.WriteLine($"Course: {course.GetTitle()}");
                Console.WriteLine($"\tECTS: {course.GetEctsCredits():F1}");
                Console.WriteLine($"\tGrade: {course.GetGradeLetter()}");
            }
        }
    }
}
