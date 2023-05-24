using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace cursedCsharp
{
    
     
    internal class Program
    {
        public static void pressAnyKeyToContinue()
        {
            Console.WriteLine("Для продолжения игры нажмите любую клавишу...");
            Console.ReadKey();
        }
        static string[] days = {"Понедельник", "Вторник", "Среда", "Четверг", "Пятница"};

     public class WorkshopBuilder {

        
        private string name;
        private string workshopName;
        private int[] detailsCreated = new int[5];

        public WorkshopBuilder(string n, string wname, int[] d) {
            name = n;
            workshopName = wname;
            for (int i = 0; i < 5; i++) {

                detailsCreated[i] = d[i];
            }
        }

        public void setName(string _name) { name = _name; }
        public void setWorkshopName(string _wName) { workshopName = _wName; }
        public void setDetailsCreated(int[] _detailsCr) { for (int i = 0; i < 5; i++) detailsCreated[i] = _detailsCr[i]; }
        public string getName() { return name; }
        public string getWorkshopName() { return workshopName; }
        public int[] getDetailsCreated() { return detailsCreated; }

        public int getAllDetailsCreated() {
            int detailSum = 0;
            foreach (var x in detailsCreated) {
                detailSum += x;
            }
            
            return detailSum;
        }

        public void printBuilder() {

            Console.Write("ФИО: " + name + " \t " + "ЦЕХ: " + workshopName +
                    " \t " + "Изготовлено деталей: " + detailsCreated[0] + " "
                    + detailsCreated[1] + " " + detailsCreated[2] + " "
                    + detailsCreated[3] + " " + detailsCreated[4] + "\n");
        }
        public void printBuilderMaxDetail() {
            int day = -1, details = -999;
            for (int i = 0; i < 5; i++) {
                if (detailsCreated[i] > details) {
                    details = detailsCreated[i];
                    day = i;
                }
            }
            Console.Write("ФИО: " + name + " \t " + days[day] + " " + details + " д" + "\n");
        }

    };

     static void updateInputFile(List<WorkshopBuilder> workers) {

        //ofstream fout("input.txt");
        
        using (StreamWriter writer = new StreamWriter("input.txt")) {
            for (int i = 0; i < workers.Count; i++) {
                WorkshopBuilder x = workers[i];
                int[] dt = x.getDetailsCreated();
                string txt = x.getName() + " " + x.getWorkshopName() + " " + dt[0] + " " + dt[1]
                             + " " + dt[2] + " " + dt[3] + " " + dt[4];
                writer.Write(txt);
                if (i != workers.Count - 1) writer.Write("\n");
            }
            writer.Close();
        }

    }

     static void readWorkersFromConsole(List<WorkshopBuilder> workers) {
         
        int count = 0;
        string name, workshopName;
        int[] detailsCreated = new int[5];
        Console.Write("Введите количество рабочих: "); count = Convert.ToInt32(Console.ReadLine());



        for (int k = 0; k < count; k++) {

            Console.Write("Введите имя: "); name = Console.ReadLine();
            //console.nextLine();
            Console.Write("Введите цех: "); workshopName = Console.ReadLine();

            Console.Write("Введите количество деталей по дням(через пробел) : ");
            var numbers = Console.ReadLine().Split(' ').Select(token => int.Parse(token));
            detailsCreated = numbers.ToArray();

            /*for (int i = 0; i < 5; i++) {
                int x;
                x = Convert.ToInt32(Console.ReadLine()); Console.Write(" ");
                detailsCreated[i] = x;
            }*/
            Console.Write("\n");
            WorkshopBuilder WB = new WorkshopBuilder(name, workshopName, detailsCreated);
            workers.Add(WB);
        }
        updateInputFile(workers);
    }

     static void readWorkersFromFile(List<WorkshopBuilder> workers)  {
        
        try
        {
            using (StreamReader reader = new StreamReader("input.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    string name = "";
                    string wname = "";
                    string mynum = "";
                    int[] dArr = new int[5];
                    int arrPos = 0;
                    int spaceCounter = 0;

                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == ' ') spaceCounter += 1;
                        if (spaceCounter <= 2)name += line[i];
                        if (spaceCounter > 2 && spaceCounter < 4 && line[i] != ' ')wname += line[i];
                        if (spaceCounter >= 4) {
                            if (line[i] != ' ') {
                                mynum += line[i];

                            }
                            else {
                                if (spaceCounter != 4) {

                                    dArr[arrPos] = Convert.ToInt32(mynum);
                                    arrPos++;
                                    mynum = "";
                                }
                            }
                            if (i == line.Length-1)dArr[arrPos] = Convert.ToInt32(mynum);
                        }
                    }
                    WorkshopBuilder WB = new WorkshopBuilder(name, wname, dArr);
                    workers.Add(WB);
                }
            }
            
        }
        catch (IOException e)
        {
            Console.Write("Файл не найден. Создаем новый файл\n\n");
            pressAnyKeyToContinue();
            readWorkersFromConsole(workers);
            
        }


    }

    public static void writeWorkersFromFile(List<WorkshopBuilder> workers) {
        //ofstream fout("output.txt");
        
        try
        {
            using (StreamWriter writer = new StreamWriter("output.txt"))
            {
                for (int i = 0; i < workers.Count; i++) {
                    WorkshopBuilder x = workers[i];
                    string txt = x.getName() + " " + x.getAllDetailsCreated();
                    writer.Write(txt);
                    if (i != workers.Count - 1) writer.Write("\n");
                }
                writer.Close();
            }
            
        }
        catch (IOException e)
        {
            Console.Write(e.Message);
        }
        


    }
    public static void printAllBuilders(List<WorkshopBuilder> workers) {
        foreach (var x in workers)
        {
            x.printBuilder();
        }
        
    }
    //Вывести всех рабочих заданного цеха и день недели в который он собрал наибольшее кол-во деталей
    public static void printFromCurWS(List<WorkshopBuilder> workers) {
        string workshopName;
        Console.Write("Введите цех: "); workshopName = Console.ReadLine();
        foreach (var x in workers)
        {
            if (x.getWorkshopName().Equals(workshopName)) {
                x.printBuilderMaxDetail();
            }
        }
        
    }
    public static void printAllBuildersOutputFormat(List<WorkshopBuilder> workers) {
        foreach (var x in workers) {
            Console.Write(x.getName() + " " + x.getAllDetailsCreated() + "\n");
        }
        writeWorkersFromFile(workers);
    }
    public void printAllBuildersFromInputFile()  {
        List<WorkshopBuilder> localWorkers = new List<WorkshopBuilder>();
        readWorkersFromFile(localWorkers);
        printAllBuilders(localWorkers);
    }

    public static void addWorker(List<WorkshopBuilder> workers) {
        string name;
        string workshopName;
        int[] detailsCreated = new int[5];
        Console.Write("Введите имя: "); name = Console.ReadLine();
        Console.Write("Введите цех: "); workshopName = Console.ReadLine();
        Console.Write("Введите количество деталей по дням(через пробел): ");
        var numbers = Console.ReadLine().Split(' ').Select(token => int.Parse(token));
        detailsCreated = numbers.ToArray();
        WorkshopBuilder WB = new WorkshopBuilder(name, workshopName, detailsCreated);
        workers.Add(WB);
        try
        {
            StreamWriter writer = new StreamWriter("input.txt", true);
            String txt = "\n" + name + " " + workshopName + " " + detailsCreated[0] + " " + detailsCreated[1] + " "
                         + detailsCreated[2] + " " + detailsCreated[3] + " " + detailsCreated[4];
            writer.Write(txt);
            writer.Close();
            
        }catch(IOException ex){

            Console.Write(ex.Message);
        }

    }

    public static void deleteWorkerByFio(List<WorkshopBuilder> workers)  {
        string name;
        int flag = 0;
        Console.Write("Введите ФИО для удаления работника: "); name = Console.ReadLine();
        for (int i = 0; i < workers.Count; i++) {

            if (workers[i].getName().Equals(name)) {
                flag = 1;
                workers.RemoveAt(i);
                i--;
            }
        }
        if (flag == 1) {
            updateInputFile(workers);
        }
        else {
            Console.Write("Рабочий не найден\n");
            pressAnyKeyToContinue();
        }

    }
    public static void renameWorkerWorkshopByFio(List<WorkshopBuilder> workers)  {
        int flag = 0;
        string name, workshopName;
        Console.Write("Введите ФИО работника для смены цеха: "); name = Console.ReadLine();
        Console.Write("Введите новый цех: "); workshopName = Console.ReadLine();
        for (int i = 0; i < workers.Count; i++) {
            if (workers[i].getName().Equals(name)) {
                flag = 1;
                workers[i].setWorkshopName(workshopName);
            }
        }
        if (flag == 1) {
            updateInputFile(workers);
        }
        else {
            Console.Write("Рабочий не найден\n");
            pressAnyKeyToContinue();
        }
    }
    public static void showMenu(List<WorkshopBuilder> workers)  {
        bool isScanned = false;
        while (true) {
            if (!isScanned) {
                Console.Write("\n" +
                              "1. Считать содержимое из файла\n" +
                              "2. Заполнить рабочих через консоль\n");
            }

            else {
                Console.Write("\n" +
                              "3. Выдать на экран содержимое файла\n" +
                              "4. Выдать на экран список рабочих заданного цеха\n" +
                              "5. Распечатать файл упрощенной структуры\n" +
                              "6. Добавить данные нового рабочего\n" +
                              "7. Удалить все элементы записи определённого рабочего\n" +
                              "8. Изменить цех у определённого рабочего\n" +
                              "н. Назад\n" +
                              "в. Выход\n");

            }

            
            char choice = Console.ReadLine()[0]; //charAt() method returns the character at the specified index in a string.
            // The index of the first character is 0, the second character is 1, and so on.
            switch (choice) {
                case '1':
                    readWorkersFromFile(workers);
                    Console.Clear();
                    isScanned = true;
                    break;
                case '2':
                    Console.Clear();
                    readWorkersFromConsole(workers);
                    isScanned = true;
                    break;
                case '3':
                    Console.Clear();
                    if (isScanned) printAllBuilders(workers);
                    break;
                case '4':
                    Console.Clear();
                    if (isScanned) printFromCurWS(workers);
                    break;
                case '5':
                    Console.Clear();
                    if (isScanned) printAllBuildersOutputFormat(workers);
                    break;
                case '6':
                    if (isScanned) addWorker(workers);
                    Console.Clear();
                    break;
                case '7':
                    if (isScanned) deleteWorkerByFio(workers);
                    Console.Clear();
                    break;
                case '8':
                    if (isScanned) renameWorkerWorkshopByFio(workers);
                    Console.Clear();
                    break;
                case 'в':
                    Environment.Exit(0);
                    break;
                case 'н':
                    Console.Clear();
                    isScanned = false;
                    workers.Clear();
                    break;
                default:
                    Console.Write( "Недопустимое значение!\n" );
                    break;
            }
        }

    }
        
        
        
        public static void Main(string[] args)
        {
            List<WorkshopBuilder> workers = new List<WorkshopBuilder>();

            showMenu(workers);
        }
    }
}