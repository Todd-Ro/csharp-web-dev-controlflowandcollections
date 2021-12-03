using System;

namespace ArrayGradebook
{
    class Program
    {
        static void Main(string[] args)
        {
            // Allow for at most 30 students
            int maxStudents = 30;

            string[] students = new string[maxStudents];
            double[] grades = new double[maxStudents];

            string input;
            string newStudent;
            int numStudents = 0;

            Console.WriteLine("Enter your students (or ENTER to finish):");

            // Get student names
            do
            {
                input = Console.ReadLine();
                newStudent = input;
         
                if (!Equals(newStudent, ""))
                {
                    students[numStudents] = newStudent;
                    numStudents++;
                }

            } while (!Equals(newStudent, ""));

            // Get student grades
            for (int i = 0; i < numStudents; i++)
            {
                Console.WriteLine("Grade for " + students[i] + ": ");
                input = Console.ReadLine();
                double grade = double.Parse(input);
                grades[i] = grade;
            }

            // Print class roster
            Console.WriteLine("\nClass roster:");
            double sum = 0.0;

            for (int i = 0; i < numStudents; i++)
            {
                Console.WriteLine(students[i] + " (" + grades[i] + ")");
                sum += grades[i];
            }

            double avg = sum / numStudents;
            Console.WriteLine("Average grade: " + avg);
        }
    }
}
