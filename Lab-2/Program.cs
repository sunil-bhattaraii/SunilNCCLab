namespace Lab2{
  class College
  {
    public readonly string Name;
    public string CampusChief{get; set;}

    public College(string name, string chief = "")
    {
      Name = name;
      CampusChief = chief;
    }
  }
  class Program
  {
    static void Main()
    {
      College pmc = new College("Patan Multiple Campus");
      pmc.CampusChief = "Dr. Raghubir Bista";

      Console.WriteLine($"College: {pmc.Name}\nChief: {pmc.CampusChief}");

      Shared.Print.MyDetails(2);
    }
  }
}
