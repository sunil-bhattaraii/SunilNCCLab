namespace Lab4;

class Student
{
    Dictionary<int, string> NameList = new Dictionary<int, string>();

    public string this[int roll]
    {
        get
        {
            return NameList[roll];
        }
        set
        {
            NameList[roll] = value;
        }
    }

    public int this[string name]
    {
        get
        {
            foreach (KeyValuePair<int, string> pair in NameList)
            {
                if (pair.Value == name) return pair.Key;
            }
            return -1;
        }

        set
        {
            NameList[value] = name;
        }
    }
}

class Program
{
    static void Main()
    {
        Student s = new Student();

        s[10] = "Ram";
        s[11] = "Shyam";
        s["Rita"] = 12;
        s["sita"] = 13;

        Console.WriteLine($"Ram's Roll No. is {s["Ram"]}.");
        Console.WriteLine($"The name of Roll No. 13 is {s[13]}.");

        Shared.Print.MyDetails(4);
    }
}
