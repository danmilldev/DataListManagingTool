//TODO
//
// Delete Adjustments: making it so each line can be deleted by its first number it has
// 

using System.Collections.Generic;
using System.Reflection.PortableExecutable;

string FileName = "ItemList.txt";
int input = 0;

Menu();

void Menu()
{
    while (input <= 5)
    {
        if (!File.Exists(FileName))
        {
            FileStream F = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            F.Close();
        }

        Console.WriteLine("Menu");
        Console.WriteLine("1. Create New item.");
        Console.WriteLine("2. Show List.");
        Console.WriteLine("3. Delete item");
        Console.WriteLine("6 or higher to exit");

        Console.Write("Choose: ");
        input = Convert.ToInt32(Console.ReadLine());

        switch (input)
        {
            case 1: CreateItem(); break;
            case 2: ShowList(); break;
            case 3: DeleteItem(); break;
            default:
                break;
        }
    }
}

long CountLinesReader(FileInfo file)
{
    var lineCounter = 0L;
    using (StreamReader reader = new(file.FullName))
    {
        while (reader.ReadLine() != null)
        {
            lineCounter++;
        }
        return lineCounter;
    }
}

void CreateItem()
{
    long FileLinesCount;

    using (StreamReader sr = new StreamReader(FileName))
    {
        FileInfo fileInfo = new FileInfo(FileName);
        FileLinesCount = CountLinesReader(fileInfo);
    }

    using (StreamWriter sw = new StreamWriter(FileName, true))
    {
        Console.WriteLine("What do you want to input: ");
        string NewItem = FileLinesCount++ + ". " + Console.ReadLine();

        sw.WriteLine(NewItem);
    }

    Console.Clear();

    Console.WriteLine("TextFileOutput:");

    string line = "";
    using (StreamReader sr = new StreamReader(FileName))
    {
        while ((line = sr.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }
    }
}

void ShowList()
{
    string line = "";
    using (StreamReader sr = new StreamReader(FileName))
    {
        while ((line = sr.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }
    }
}

void DeleteItem()
{
    bool wasFound = false;

    Console.WriteLine("Wich line to delete just the first number: ");
    string LineNumber = Console.ReadLine().TrimEnd();
    Console.WriteLine("Line number is " + LineNumber);

    List<string> content = File.ReadAllLines(FileName).ToList();

    foreach (var item in content.ToArray())
    {
        if (String.Equals(item.Substring(0, 1), LineNumber.Substring(0, 1)))
        {
            content.Remove(item);
            wasFound = true;
            continue;
        }
    }

    File.WriteAllLines(FileName, content.ToArray());

    if (!wasFound)
        Console.WriteLine("Nothing was deleted: Line not Found.");
    else
        Console.WriteLine("Line found and Deleted.");
}