
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASEANPhonebook
{
    public class Country
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public Country(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }

    public class Student
    {
        public string StudentNumber { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Occupation { get; set; }
        public string Gender { get; set; }
        public string CountryCode { get; set; }
        public string AreaCode { get; set; }
        public string PhoneNumber { get; set; }

        public Student(
            string studentNumber,
            string surname,
            string firstName,
            string occupation,
            string gender,
            string countryCode,
            string areaCode,
            string phoneNumber)
        {
            StudentNumber = studentNumber;
            Surname = surname;
            FirstName = firstName;
            Occupation = occupation;
            Gender = gender;
            CountryCode = countryCode;
            AreaCode = areaCode;
            PhoneNumber = phoneNumber;
        }

        public void DisplayInformation()
        {
            string genderText = Gender == "M" ? "Male" : "Female";

            Console.WriteLine(
                $"{Surname}, {FirstName}, with student number " +
                $"{StudentNumber}, is a {Occupation}. " +
                $"{genderText}. His/Her phone number is " +
                $"{CountryCode}-{AreaCode}-{PhoneNumber}");
        }
    }

    public class Phonebook
    {
        private List<Student> students = new List<Student>();

        private List<Country> countries = new List<Country>()
        {
            new Country("Malaysia", "60"),
            new Country("Indonesia", "62"),
            new Country("Philippines", "63"),
            new Country("Singapore", "65"),
            new Country("Thailand", "66")
        };

        public void MainMenu()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n====================");
                Console.WriteLine("    ASEAN PHONEBOOK");
                Console.WriteLine("====================");
                Console.WriteLine("[1] Store to ASEAN phonebook");
                Console.WriteLine("[2] Edit entry in ASEAN phonebook");
                Console.WriteLine("[3] Search ASEAN phonebook by country");
                Console.WriteLine("[4] Exit");
                Console.WriteLine("====================");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        StoreStudent();
                        break;

                    case "2":
                        EditStudent();
                        break;

                    case "3":
                        SearchByCountry();
                        break;

                    case "4":
                        Console.WriteLine(
                            "\nThank you for using ASEAN Phonebook!");
                        running = false;
                        break;

                    default:
                        Console.WriteLine(
                            "\nInvalid option! Please try again.");
                        break;
                }
            }
        }


        private void StoreStudent()
        {
            bool addAnother = true;

            while (addAnother)
            {
                Console.WriteLine("\n====================");
                Console.WriteLine("     STORE STUDENT");
                Console.WriteLine("====================");

                string studentNumber;

                while (true)
                {
                    studentNumber =
                        ReadRequired("Enter student number: ");

                    if (students.Any(s =>
                        s.StudentNumber.Equals(
                            studentNumber,
                            StringComparison.OrdinalIgnoreCase)))
                    {
                        Console.WriteLine(
                            "Student number already exists!");
                    }
                    else
                    {
                        break;
                    }
                }

                string surname =
                    ReadRequired("Enter surname: ");

                string firstName =
                    ReadRequired("Enter first name: ");

                string occupation =
                    ReadRequired("Enter occupation: ");

                string gender = ReadGender();

                Console.WriteLine("\nAvailable Country Codes:");
                DisplayCountries();

                string countryCode = ReadCountryCode();

                string areaCode =
                    ReadDigits("Enter area code: ");

                string phoneNumber =
                    ReadDigits("Enter number: ");

                Student newStudent = new Student(
                    studentNumber,
                    surname,
                    firstName,
                    occupation,
                    gender,
                    countryCode,
                    areaCode,
                    phoneNumber
                );

                students.Add(newStudent);

                Console.WriteLine(
                    "\nStudent successfully added!");

                Console.Write(
                    "\nDo you want to enter another entry [Y/N]? ");

                string answer = (Console.ReadLine() ?? "")
                    .Trim()
                    .ToUpper();

                if (answer != "Y")
                {
                    addAnother = false;
                }
            }
        }

        private void EditStudent()
        {
            Console.WriteLine("\n====================");
            Console.WriteLine("     EDIT STUDENT");
            Console.WriteLine("====================");

            if (students.Count == 0)
            {
                Console.WriteLine(
                    "The phonebook is currently empty.");
                return;
            }

            string studentNumber =
                ReadRequired("Enter student number: ");

            Student student = students.FirstOrDefault(s =>
                s.StudentNumber.Equals(
                    studentNumber,
                    StringComparison.OrdinalIgnoreCase));

            if (student == null)
            {
                Console.WriteLine(
                    "\nERROR: Student number does not exist!");
                return;
            }

            bool editing = true;

            while (editing)
            {
                Console.WriteLine(
                    "\nHere is the existing information:");

                student.DisplayInformation();

                Console.WriteLine(
                    "\nWhich information do you wish to change?");

                Console.WriteLine("[1] Student number");
                Console.WriteLine("[2] Surname");
                Console.WriteLine("[3] First name");
                Console.WriteLine("[4] Occupation");
                Console.WriteLine("[5] Country code");
                Console.WriteLine("[6] Area code");
                Console.WriteLine("[7] Phone number");
                Console.WriteLine(
                    "[8] None - Go back to main menu");

                Console.Write("\nEnter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":

                        string newStudentNumber =
                            ReadRequired(
                                "Enter new student number: ");

                        bool duplicate = students.Any(s =>
                            s != student &&
                            s.StudentNumber.Equals(
                                newStudentNumber,
                                StringComparison.OrdinalIgnoreCase));

                        if (duplicate)
                        {
                            Console.WriteLine(
                                "Student number already exists!");
                        }
                        else
                        {
                            student.StudentNumber =
                                newStudentNumber;

                            Console.WriteLine(
                                "Student number updated!");
                        }

                        break;

                    case "2":

                        student.Surname =
                            ReadRequired("Enter new surname: ");

                        Console.WriteLine("Surname updated!");
                        break;

                    case "3":

                        student.FirstName =
                            ReadRequired("Enter new first name: ");

                        Console.WriteLine("First name updated!");
                        break;

                    case "4":

                        student.Occupation =
                            ReadRequired("Enter new occupation: ");

                        Console.WriteLine("Occupation updated!");
                        break;

                    case "5":

                        Console.WriteLine("\nAvailable Country Codes:");

                        DisplayCountries();

                        student.CountryCode = ReadCountryCode();

                        Console.WriteLine("Country code updated!");
                        break;

                    case "6":

                        student.AreaCode =
                            ReadDigits("Enter new area code: ");

                        Console.WriteLine("Area code updated!");
                        break;

                    case "7":

                        student.PhoneNumber =
                            ReadDigits("Enter new phone number: ");

                        Console.WriteLine("Phone number updated!");
                        break;

                    case "8":

                        editing = false;
                        break;

                    default:

                        Console.WriteLine(
                            "Invalid option! Please try again.");
                        break;
                }
            }
        }


        private void SearchByCountry()
        {
            Console.WriteLine("\n====================");
            Console.WriteLine("   SEARCH BY COUNTRY");
            Console.WriteLine("====================");

            if (students.Count == 0)
            {
                Console.WriteLine(
                    "The phonebook is currently empty.");
                return;
            }

            List<string> selectedCountries =
                new List<string>();

            bool selecting = true;
            bool allCountries = false;

            while (selecting)
            {
                Console.WriteLine("\nFrom which country?");

                Console.WriteLine("[1] Philippines");
                Console.WriteLine("[2] Thailand");
                Console.WriteLine("[3] Singapore");
                Console.WriteLine("[4] Indonesia");
                Console.WriteLine("[5] Malaysia");
                Console.WriteLine("[6] ALL");
                Console.WriteLine("[0] No More");

                Console.Write("\nEnter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":

                        AddCountry(selectedCountries, "63");
                        break;

                    case "2":

                        AddCountry(selectedCountries, "66");
                        break;

                    case "3":

                        AddCountry(selectedCountries, "65");
                        break;

                    case "4":

                        AddCountry(selectedCountries, "62");
                        break;

                    case "5":

                        AddCountry(selectedCountries, "60");
                        break;

                    case "6":

                        allCountries = true;
                        selecting = false;
                        break;

                    case "0":

                        selecting = false;
                        break;

                    default:

                        Console.WriteLine(
                            "Invalid option. Please try again.");
                        break;
                }
            }

            if (!allCountries &&
                selectedCountries.Count == 0)
            {
                Console.WriteLine("\nNo countries selected.");
                return;
            }

            List<Student> results;

            if (allCountries)
            {
                results = students.ToList();
            }
            else
            {
                results = students
                    .Where(s =>
                        selectedCountries.Contains(s.CountryCode))
                    .ToList();
            }

            results = results
                .OrderBy(
                    s => s.Surname,
                    StringComparer.OrdinalIgnoreCase)
                .ThenBy(
                    s => s.FirstName,
                    StringComparer.OrdinalIgnoreCase)
                .ToList();

            Console.WriteLine("\n====================");
            Console.WriteLine("    SEARCH RESULTS");
            Console.WriteLine("====================");

            if (results.Count == 0)
            {
                Console.WriteLine(
                    "No students found in the selected countries.");
            }
            else
            {
                foreach (Student student in results)
                {
                    student.DisplayInformation();
                    Console.WriteLine("------------");
                }

                Console.WriteLine(
                    "Total students found: " + results.Count);
            }
        }


        private void AddCountry(
            List<string> selectedCountries,
            string code)
        {
            if (selectedCountries.Contains(code))
            {
                Console.WriteLine(
                    "Country already selected!");
            }
            else
            {
                selectedCountries.Add(code);

                Console.WriteLine(
                    "Country added successfully!");
            }
        }


        private void DisplayCountries()
        {
            foreach (Country country in countries)
            {
                Console.WriteLine(
                    country.Name + " - " + country.Code);
            }
        }

        private string ReadCountryCode()
        {
            while (true)
            {
                Console.Write("Enter country code: ");

                string code = (Console.ReadLine() ?? "").Trim();

                if (countries.Any(c => c.Code == code))
                {
                    return code;
                }

                Console.WriteLine(
                    "Invalid country code! Please try again.");
            }
        }


        private string ReadGender()
        {
            while (true)
            {
                Console.Write(
                    "Enter gender (M for male, F for female): ");

                string gender = (Console.ReadLine() ?? "")
                    .Trim()
                    .ToUpper();

                if (gender == "M" || gender == "F")
                {
                    return gender;
                }

                Console.WriteLine(
                    "Invalid gender! Enter M or F.");
            }
        }


        private string ReadRequired(string message)
        {
            while (true)
            {
                Console.Write(message);

                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }

                Console.WriteLine(
                    "Input cannot be empty!");
            }
        }


        private string ReadDigits(string message)
        {
            while (true)
            {
                Console.Write(message);

                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input) &&
                    input.All(char.IsDigit))
                {
                    return input;
                }

                Console.WriteLine(
                    "Please enter numbers only!");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Phonebook phonebook = new Phonebook();

            phonebook.MainMenu();
        }
    }
}
