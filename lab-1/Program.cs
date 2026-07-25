namespace Lab1;

  class Product
  {
    string name;
    double price;

    static Product()
    {
      Console.WriteLine("This is static constructor\n");
    }

    //default constructor
    public Product()
    {
      Console.WriteLine("Using default constructor");

      name = "";
      price = 0;
    }

    public Product(string name, double price)
    {
      Console.WriteLine("Using parameterized constructor");

      this.name = name;
      this.price = price;
    }

    //copy constructor
    public Product(Product p)
    {
      Console.WriteLine("Using copy constructor");

      this.name = p.name;
      this.price = p.price;
    }

    public void Display()
    {
      Console.WriteLine($"Name : {name}\nPrice: {price}\n");
    }
  }

  class Admin
  {
    readonly int id;
    readonly string name;
    private readonly static Admin instance = new Admin();

    private Admin()
    {
      Console.WriteLine("Using private constructor");
      id = 0;
      name = "Mr. Maalik";
    }

    public static Admin GetAdmin()
    {
      return instance;
    }

    public void DisplayName()
    {
      Console.WriteLine($"id: {id}\nName: {name}\n");
    }
  }
  class Program
  {
    static void Main()
    {
      Product d = new Product();
      d.Display();

      Product p = new Product("wai wai", 25);
      p.Display();

      Product pcpy = new Product(p);
      pcpy.Display();

      Admin superUser = Admin.GetAdmin();
      superUser.DisplayName();

      Shared.Print.MyDetails(1);
    }
  }
