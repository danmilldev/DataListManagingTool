using System.Collections.Generic;
using System.Reflection.PortableExecutable;

string FileName = "ItemList.txt";
int input = 0;

Menu();

//Menu to select what to change or to create or to delete.
//And checking first if file exists if not it will be created.
void Menu()
{
    if (!File.Exists(FileName))
    {
        FileStream F = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        F.Close();
    }

    while (input <= 5)
    {

        ShowList();

        Console.WriteLine("----------");

        Console.WriteLine("Menu");
        Console.WriteLine("1. Create New item.");
        Console.WriteLine("2. Show List.");
        Console.WriteLine("3. Update item");
        Console.WriteLine("4. Delete Item");
        Console.WriteLine("6 or higher to exit");

        Console.Write("Choose: ");
        input = Convert.ToInt32(Console.ReadLine());

        switch (input)
        {
            case 1: CreateItem(); break;
            case 2: ShowList(); break;
            case 3: UpdateItem(); break;
            case 4: DeleteItem(); break;
            default:
                break;
        }
    }
}

//Reading all lines to count them and to set the correct number for the next item.
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

//Creating an item by getting an input from the user and writing it to the file with the CountLinesReader method
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
}

//Just printing out the whole TXT file.
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

//Update an item in the TXT file by reading all content finding that item to update
//and then just getting input from the user and change it in the List and write the new
//List to the FIle.
void UpdateItem()
{
    Console.WriteLine("Wich line to edit just the first number: ");
    string LineNumber = Console.ReadLine().TrimEnd();
    Console.WriteLine("Line number is " + LineNumber);

    List<string> content = File.ReadAllLines(FileName).ToList();

    int index = content.FindIndex(i => i.Substring(0,1).Contains(LineNumber));

    Console.WriteLine("found item: " + content[index]);
    Console.Write("NewText: ");

    string newItemText = Console.ReadLine();

    content[index] = newItemText;

    File.WriteAllLines(FileName, content);
    Console.Clear();
}

//deleting an item found by the foreach loop and the given user input
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

    File.WriteAllLines(FileName, content);

    if (!wasFound)
        Console.WriteLine("Nothing was deleted: Line not Found.");
    else
        Console.WriteLine("Line found and Deleted.");

    Thread.Sleep(1000);
    Console.Clear();
}