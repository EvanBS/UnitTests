using System;
using System.Collections.Generic;

namespace UnitTests
{
    public abstract class Employee
    {
        public Manager manager;

        public Employee(Manager manager = null)
        {
            this.manager = manager;
        }
        
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        protected double Salary { get; set; }

        public int Experience { get; set; }

        public abstract void GetSalary(double salary);

        protected void CheckExperience()
        {
            if (Experience > 2 && Experience < 6)
            {
                Salary += 200;
            }
            else if (Experience > 5)
            {
                Salary += Salary * 1.2 + 500;
            }
        }

        public override string ToString()
        {
            return string.Format($"{FirstName} {SecondName}, manager:{manager.SecondName}, experiance:{Experience}");
        }

        public void ShowSalary()
        {
            Console.WriteLine($"{FirstName}, {SecondName}: got salary: {Salary}");
        }

    }

    public class Developer : Employee
    {
        public Developer(Manager manager) : base(manager) { }

        public override void GetSalary(double salary)
        {
            Salary = salary;

            CheckExperience();

            ShowSalary();
        }


    }

    public class Designer : Employee
    {
        public Designer(Manager manager, double coeff = 0.1) : base(manager)
        {
            this.coeff = coeff;
        }

        private double coeff;

        public override void GetSalary(double salary)
        {
            Salary = salary + (salary * coeff);

            CheckExperience();

            ShowSalary();
        }
    }

    public class Manager : Employee
    {
        public Manager()
        {
            manager = this;
        }

        public List<Designer> designers = new List<Designer>();

        public List<Developer> developers = new List<Developer>();

        public override void GetSalary(double salary)
        {
            Salary = salary;

            int Members = designers.Count + developers.Count;

            if (Members > 5 && Members < 11)
            {
                Salary += 200;
            }
            else if (Members > 10)
            {
                Salary += 300;
            }

            if (developers.Count > designers.Count)
            {
                Salary *= 1.1;
            }

            CheckExperience();

            ShowSalary();
        }
    }


    public class Department
    {
        public List<Manager> managers = new List<Manager>();

        public void GiveSalary()
        {
            foreach (var manager in managers)
            {
                foreach (var designer in manager.designers)
                {
                    designer.GetSalary(400);
                }

                foreach (var dev in manager.developers)
                {
                    dev.GetSalary(500);
                }

                manager.GetSalary(700);
            }
        }
    }


    public class Program
    {
        public static Department department = new Department();

        public static void Main(string[] args)
        {

            Manager manager1 = new Manager() { FirstName = "Pots1", SecondName = "Pots1" };
            manager1.designers = new List<Designer>() // ez init
            {
                new Designer(manager1, 0.9) { FirstName = "Vasyan", SecondName = "Jopin", Experience = 0 },
                new Designer(manager1) { FirstName = "Artemy", SecondName = "Lebedev", Experience = 15 },
            };
            // more than half of team members are developers.
            manager1.developers = new List<Developer>()
            {
                new Developer(manager1) {FirstName = "Pavel", SecondName = "Durov", Experience = 5 },
                new Developer(manager1) {FirstName = "Extremecode", SecondName = "Extremecode", Experience = 7 },
                new Developer(manager1) {FirstName = "Alexey", SecondName = "Shertsov", Experience = 0 },
            };



            Manager manager2 = new Manager() { FirstName = "Pots2", SecondName = "Pots2" };
            manager2.designers = new List<Designer>()
            {
                new Designer(manager2) { FirstName = "HZ", SecondName = "HZ", Experience = 5 },
                new Designer(manager2) { FirstName = "Asap", SecondName = "Rocky", Experience = 9 },
                new Designer(manager2) { FirstName = "Yuriy", SecondName = "Hoy", Experience = 30 },
            };

            manager2.developers = new List<Developer>()
            {
                new Developer(manager2) {FirstName = "Ivan", SecondName = "Kolomoets", Experience = 0 },
                new Developer(manager2) {FirstName = "Igor", SecondName = "Link", Experience = 8 },
            };


            department.managers.Add(manager1);
            department.managers.Add(manager2);

            department.GiveSalary();
        }
    }
}
