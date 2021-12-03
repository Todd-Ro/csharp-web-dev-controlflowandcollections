using System;
using System.Collections.Generic;

namespace MyDictionaryGradebookAmendment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*  
             *  Plan:
             *  - Create a pair class with a string and a double
             *  - Get the names from the user and add them to a list of pairs, and to the dictionary
             *  with name -> [position, null]
             *  - Create a method for comparing entries in the list of pairs 
             *  (may involve called method for comparing doubles)
             *  - Sort the list of pairs 
             *  - Ask for each student's grade in order 
             *  - Use the string match between dictionary and string from pair to update dictionary with score
             *  - Iterate over the pair list again and use the dictionary match again to display the scores
             */

            Boolean isGreaterDoubleDouble (double []a, double[] b)
            {
                // Returns true if a is "greater than" or equal to b
                if (a[0] > b[0])
                {
                    return true;
                } else if ((a[0] == b[0]) && (a[1] >= b[1]))
                {
                    return true;
                } else
                {
                    return false;
                }
            }

            int scaleDownDiff(double a, double b)
            {
                //Returns a non-negative integer smaller in magnitude than 65536 that scales with Abs(a-b)
                //Returns 32768 if Abs(a-b)=1
                if (a == b)
                {
                    return 0;
                }
                double quartered = (Math.Pow((Math.Abs(a - b)),(1.0/4.0)));
                double loga = Math.Log(quartered, 2);
                int logScaledDiff = Convert.ToInt32(loga * 32766.9 / 256.01);
                int ret = logScaledDiff + 32768;
                if (ret > 65535)
                {
                    return 65535;
                }
                else if (ret >= 1)
                {
                    return ret;
                }
                else
                {
                    return 1;
                }
            }

            int compareDoubleDoubles (double[] a, double[] b)
            {
                if (a[0] > b[0])
                {
                    return Math.Abs(scaleDownDiff(a[0], b[0]) * 32767);
                }
                else if ((a[0] == b[0]) && (a[1] >= b[1]))
                {
                    return Math.Abs(scaleDownDiff(a[1], b[1]));
                }
                else if (b[0] > a[0])
                {
                    return -1 * Math.Abs(scaleDownDiff(b[0], a[0] * 32767));
                } 
                else if (a[0] == b[0] && a[1] == b[1])
                {
                    return 0;
                }
                else 
                {
                    return -1 * Math.Abs(scaleDownDiff(b[1], a[1]));
                }
            }

            double bigScale = Math.Log2(10) * 308 + Math.Log2(1.79769313486233 * 2);
            double smallScale = Math.Log2(10) * -324 + Math.Log2(4.9); 

            int compareDouble(double a, double b)
            {
                /*
                 * Should return 0 if doubles are equal, 1 073 741 824 (2^30) if difference is 1, 
                 * 2 147 483 647 if difference is largest possible, between 1 and 1 073 741 823 if 
                 * difference is between 0 and 1, and negative if b is larger than a.
                 */
                double diff = a - b;
                if ((a == b) || (diff == 0))
                {
                    return 0;
                }
                else if (diff == 1)
                {
                    return 1073741824;
                }
                /* 
                 * The largest magnitude positive and negative doubles are plus or minus 
                 * 1.79769313486232*10^308
                 */
                else if (diff > 1)
                {
                    return Convert.ToInt32(1 + 1073741824 + 1073741822 * Math.Log2(diff) / bigScale);
                }
                // The smallest positive double is 5.0 × 10^−324
                else if (diff > 0)
                {
                    return Convert.ToInt32(1 + 1073741822 * (-1 + Math.Log2(diff)) / (-1+smallScale));
                }
                else
                {
                    return -1 * compareDouble(b, a);
                }
            }

            int compareStringDoublePair(StringDoublePair a, StringDoublePair b)
            {
                double double1 = a.Dbl;
                double double2 = b.Dbl;
                if ((double1 != double2))
                {
                    if (double1 > double2)
                    {
                        return (1073741824 + (compareDouble(double1, double2) / 2));
                    }
                    else
                    {
                        return (-1073741824 + (compareDouble(double1, double2) / 2));
                    }
                } 
                else
                {
                    string string1 = a.Str;
                    string string2 = b.Str;
                    return (string1.CompareTo(string2) / 2);
                }

            }

            /*List<double[]> sortedColl (ICollection<double[]> dictValues)
            {
                int itemsPlaced = 0;
                List<double[]> ret = new List<double[]>();
                IEnumerator<double[]> enumer = dictValues.GetEnumerator();
                while (itemsPlaced < dictValues.Count)
                {
                    enumer.MoveNext();
                    double[] nextArr = enumer.Current;
                    if (itemsPlaced == 0)
                    {
                        ret.Add(nextArr);
                    } 
                    else if (itemsPlaced == 1)
                    {
                        if 
                    }
                }
                return ret;
            }*/

            Dictionary<string, double> students = new Dictionary<string, double>();
            string newStudent;

            Console.WriteLine("Enter your students (or ENTER to finish):");

            List<String> studentNames = new List<String>();
            List<StringDoublePair> studentPairs = new List<StringDoublePair>();

            string input;
            double studentIndex = 0;
            // Get student names
            do
            {
                Console.WriteLine("Student: ");
                input = Console.ReadLine();
                newStudent = input;

                if (!Equals(newStudent, ""))
                {

                    studentNames.Add(newStudent);
                    StringDoublePair thisPair = new StringDoublePair(newStudent, studentIndex);
                    studentPairs.Add(thisPair);
                    studentIndex++;
                    students.Add(newStudent, 0);
                    
                }

            } while (!Equals(newStudent, ""));

            studentPairs.Sort(compareStringDoublePair);

            // Get grades
            Console.WriteLine("\nClass roster:");
            double sum = 0.0;

            foreach(StringDoublePair student in studentPairs) {
                Console.WriteLine("Grade for "+student.Str+": ");
                input = Console.ReadLine();
                double newGrade = double.Parse(input);
                students[student.Str] = newGrade;
                Console.WriteLine();
                // Read in the newline before looping back
            }

            foreach (StringDoublePair student in studentPairs)
            {
                Console.WriteLine(student.Str + " (" + students[student.Str] + ")");
                sum += students[student.Str];
            }

            double avg = sum / students.Count;
            Console.WriteLine("Average grade: " + avg);
        }
    }
}
