using Machines.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Machines.App
{
    
    public class ServiceMachines
    {
        public ServiceMachines()
        {

        }

        public static LiteDbEntity<T> LiteDb<T>()
        {
            LiteDbEntity<T> db = new LiteDbEntity<T>(@"C:\Users\АбироваА\Desktop\mydb");

            return db;
        }


        //private Worker worker= null;
        public Car car = new Car();
        public Worker worker = new Worker();
        public Project project = new Project();

        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Регистрация");
            Console.WriteLine("2. Вход в систему");
            Console.WriteLine("3. Выход");

            Console.Write(": ");
            int choice = 0;
            bool tryChoice = Int32.TryParse(Console.ReadLine(), out choice);
            if (tryChoice)
            {
                if (choice == 1)
                {
                    Console.Clear();
                    RegistrationMenu();
                }
                else if (choice == 2)
                {
                    Console.Clear();
                    LogOnMenu();
                }
                else return;
            }
            else
            {
                //Console.WriteLine("Неправильно введены данные");
                return;
            }
        }

        private void RegistrationMenu()
        {
            //Worker worker1 = new Worker();
            Console.WriteLine("Введите логин: ");
            worker.login = Console.ReadLine();
            Console.WriteLine("Введите пароль: ");
            worker.password = Console.ReadLine();
            Console.WriteLine("Укажите позицию: 1- менеджер, 2- работник");

            int choice = 0;
            bool test = Int32.TryParse(Console.ReadLine(), out choice);
            if (test == false || choice > 2 || choice < 1)
            {
                while (test == false)
                {
                    Console.WriteLine("Неверно введены данные. Попробуйте еще раз:");
                    test = Int32.TryParse(Console.ReadLine(), out choice);
                }
            }
            if (choice == 1)
                worker.access = access.менеджер;
            if (choice == 2)
                worker.access = access.работник;
            //else
            //    worker.access = access.работник; //default
               
            string message = "";
           
            if (Registration(worker, out message))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(3000);
                MainMenu();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1000);
                MainMenu();
            }

            worker = null;

        }


        public bool Registration(Worker worker, out string message)
        {
            try
            {
                LiteDb<Worker>().Add(worker);
                message = "Регистрация прошла успешно";
               
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }


        public Worker LogOn(string login, string password, out string message)
        {
            Worker wr = null;
            List<Worker> workers = LiteDb<Worker>().GetCollection().ToList();
            if (workers.Any(n => n.login == login && n.password == password))
            {
                wr = workers.FirstOrDefault(n => n.login == login && n.password == password);
                message = "Ок";
            }
            else
            {
                message = "Неправильные логин или пароль";
            }
            return wr;
        }

        private void LogOnMenu()
        {
            
            string login = "";
            string password = "";
            string message = "";
            Console.Write("Введите логин: ");
            login = Console.ReadLine();
            Console.Write("Введите пароль: ");
            password = Console.ReadLine();
            worker = LogOn(login, password, out message);
            if (worker != null)
            {
                AuthoriseMenu();
            }
            else
            {
                Console.WriteLine(message);
                Thread.Sleep(1000);
                MainMenu();
            }

        }

        public void AuthoriseMenu() //this on is full, restricted is to be done
        {
            Console.Clear();  

            Console.WriteLine("1. Создать проект");
            Console.WriteLine("2. Обслужить машину");
            Console.WriteLine("3. Список машин по проектам");
            Console.WriteLine("4. Список работников по проектам");                   
            Console.WriteLine("5. Выход");
            int choice = 0;
            bool tryChoice = Int32.TryParse(Console.ReadLine(), out choice);
            if (tryChoice)
            {
                if (choice == 5)
                {
                    Console.Clear();
                    worker = null;
                    MainMenu();
                }

                else if (choice == 1)
                {
                    Console.Clear();
                    CreateProjectMenu();
                }

                else if (choice == 2)
                {
                    Console.Clear();
                    ServiceCarMenu();
                }

                else if (choice == 3)
                {
                    Console.Clear();
                    ShowCarsByProject();
                    Console.ReadKey();
                    AuthoriseMenu();
                }
                else if (choice == 4)
                {
                    Console.Clear();
                    ShowWorkersMenu();
                    Console.ReadKey();
                    AuthoriseMenu();
                }
            }
            else
            {
                worker = null;
                MainMenu();
            }

        }

        public static Car createCar()
        {

            Console.WriteLine("Введите марку:");
            string brand = Console.ReadLine();
            
            Console.WriteLine("Введите модель:");
            string model = Console.ReadLine();
            Console.WriteLine("Введите гаражный номер:");
            string garageNumber = Console.ReadLine();

            Console.WriteLine("Укажите тип: 1 - дробилка, 2 - погрузчик, 3 - самосвал");
            int choice2 = 0;
            type type = type.дробилка;
            bool ischoice2 = Int32.TryParse(Console.ReadLine(), out choice2);
            if (ischoice2)
            {
                switch (choice2)
                {
                    case 1:
                        type = type.дробилка;
                        break;
                    case 2:
                        type = type.погрузчик;
                        break;
                    case 3:
                        type = type.самосвал;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine("Неправильный ввод данных");
                //CreateProjectMenu();
            }
            Car cr = new Car(brand, model, garageNumber, type);
            return cr;
        }

        public void CreateProjectMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Создать проект");
            Console.WriteLine("2. Назад");
            int choice = 0;
            bool tryChoice = Int32.TryParse(Console.ReadLine(), out choice);
            if (tryChoice)
            {
                if (choice == 2){
                    Console.Clear();
                    AuthoriseMenu();
                }

                else if (choice == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Введите название проекта:");
                    project.name = Console.ReadLine();
                   
                    //Project project = new Project(name);                    
                    Console.Clear();
                    Console.WriteLine("1. Добавить машину");
                    Console.WriteLine("2. Назад");
                    int option = 0;
                    bool tryChoice2 = Int32.TryParse(Console.ReadLine(), out option);
                    if (tryChoice2)
                    {
                        if (option == 2)
                        {
                            Console.Clear();
                            CreateProjectMenu();
                        }
                        else if (option == 1)
                        {
                            Console.Clear();

                            Console.WriteLine("Введите марку:");
                            car.brand = Console.ReadLine();

                            Console.WriteLine("Введите модель:");
                            car.model = Console.ReadLine();
                            Console.WriteLine("Введите гаражный номер:");
                            car.garageNumber = Console.ReadLine();

                            Console.WriteLine("Укажите тип: 1 - дробилка, 2 - погрузчик, 3 - самосвал");
                            int choice2 = 0;
                            //type type = type.дробилка;
                            bool ischoice2 = Int32.TryParse(Console.ReadLine(), out choice2);
                            if (ischoice2)
                            {
                                switch (choice2)
                                {
                                    case 1:
                                        car.type = type.дробилка;
                                        break;
                                    case 2:
                                        car.type = type.погрузчик;
                                        break;
                                    case 3:
                                        car.type = type.самосвал;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Неправильный ввод данных");
                                CreateProjectMenu();
                            }
                            //Car c = new Car(brand, model, garageNumber, type);                            
                            //project.addCar(car);
                            //project.cars.Add(car);
                            //LiteDb<Car>().Add(car);
                            //LiteDb<Project>().Add(project);
                        }                      
                    }
                    else
                    {
                        Console.Clear();
                        CreateProjectMenu();
                    }
                    project.cars.Add(car);
                    LiteDb<Car>().Add(car);
                    LiteDb<Project>().Add(project);
                    Console.Clear();
                    AuthoriseMenu();
                }               
            }
            else
            {
                Console.Clear();
                AuthoriseMenu();
            }
            
        }

        private Stop createStop(Car car, Worker worker)
        {
            Console.WriteLine("Введите описание проблемы:");
            string description = Console.ReadLine();
            Console.WriteLine("Введите рекоммендации:");
            string recommendation = Console.ReadLine();
            Stop stop = new Stop(car, description, recommendation, worker);
            return stop;
        }

        private Component createComponent()
        {
        
            Console.WriteLine("Введите наименование компонента:");
            string name = Console.ReadLine();
            Console.WriteLine("Введите код компонента:");
            string code = Console.ReadLine();
            Component comp = new Component(name, code);
            return comp;
        }
       


        public void ServiceCarMenu()
        {
            Console.Clear();           

            List<Project> projects = LiteDb<Project>().GetCollection();


            if (projects == null)
            {
                Console.WriteLine("Пока нет доступных машин");
                Thread.Sleep(500);
                AuthoriseMenu();
            }
            else
            {
                foreach (Project item in projects)
                {
                    Console.WriteLine(item.name);
                    item.printAll();
                }
               
                
                Console.WriteLine("Укажите проект:");
                string name = Console.ReadLine();
                Project p = projects.FirstOrDefault(n => n.name == name);
                if (p == null)
                {
                    Console.WriteLine("Нет  проекта с таким названием");
                    Thread.Sleep(500);
                    AuthoriseMenu();
                }
                if(p!=null)
                {
                    p.printAll();
                    foreach (Car item in p.cars)
                    {
                        item.printInfo();
                    }
                }

                Console.WriteLine("Введите номер машины");
                string garageNumber = Console.ReadLine();
                List<Car> cars = LiteDb<Car>().GetCollection();
                Car c = cars.FirstOrDefault(n => n.garageNumber == garageNumber);
                if (c == null)
                {
                    Console.WriteLine("Нет машины с таким номером");
                    Thread.Sleep(200);
                    AuthoriseMenu();
                }
                else
                {
                    Console.WriteLine("1. Создать остановку");
                    Console.WriteLine("2. Добавить компонент");
                    Console.WriteLine("3. Назад");
                    string message = "";
                    int choice3 = 0;
                    bool tryChoice3 = Int32.TryParse(Console.ReadLine(), out choice3);
                    if (tryChoice3)
                    {
                        switch (choice3)
                        {
                            case 1:
                                Console.Clear();
                                Stop s = createStop(c, worker);
                                p.createStop(c, s, out message);
                                LiteDb<Stop>().Add(s);
                                LiteDb<Car>().Edit(c);
                                break;
                            case 2:
                                Console.Clear();
                                //Component comp = createComponent();
                                p.addNewComponent(c, out message);

                                LiteDb<Car>().Edit(c);
                                break;
                            case 3:
                                Console.Clear();
                                AuthoriseMenu();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

        }

        public void ShowCarsByProject()
        {
            List<Project> projects = LiteDb<Project>().GetCollection();
            if (projects==null)
            {
                Console.WriteLine("На данный момент ни одного проекта не создано");
            }

            else
            {
                foreach (Project item in projects)
                {                   
                    if (item.cars != null)
                        item.printInfo();
                    else
                        Console.WriteLine("Нет машин на проекте");

                }
            }
        }

        public void ShowWorkersMenu()
        {
            List<Worker> workers = LiteDb<Worker>().GetCollection();           
            if (workers == null)
                Console.WriteLine("На данный момент ни одного работника не создано");
            else
            {
                foreach (Worker item in workers)
                {
                    item.printInfo();
                    
                }
            }
        }
    }
}
