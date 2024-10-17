using System;
public abstract class Software
{
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public DateTime ReleaseDate { get; set; }

    public Software(string name, string manufacturer, DateTime releaseDate)
    {
        Name = name;
        Manufacturer = manufacturer;
        ReleaseDate = releaseDate;
    }

    public abstract void DisplayInfo();

    public abstract bool IsUsable();
}

public class Free : Software
{
    public Free(string name, string manufacturer, DateTime releaseDate)
        : base(name, manufacturer, releaseDate) { }

    public override void DisplayInfo()
    {
        Console.WriteLine($"[Free Software]\nName: {Name}\nManufacturer: {Manufacturer}\nRelease Date: {ReleaseDate.ToShortDateString()}");
    }

    public override bool IsUsable()
    {
        return true;
    }
}

public class Trial : Software
{
    public DateTime InstallationDate { get; set; }
    public int FreePeriodMonths { get; set; }

    public Trial(string name, string manufacturer, DateTime releaseDate, DateTime installationDate, int freePeriodMonths)
        : base(name, manufacturer, releaseDate)
    {
        InstallationDate = installationDate;
        FreePeriodMonths = freePeriodMonths;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"[Trial Software]\nName: {Name}\nManufacturer: {Manufacturer}\nRelease Date: {ReleaseDate.ToShortDateString()}\nInstallation Date: {InstallationDate.ToShortDateString()}\nFree Use Period: {FreePeriodMonths} months");
    }

    public override bool IsUsable()
    {
        DateTime expirationDate = InstallationDate.AddMonths(FreePeriodMonths);
        return DateTime.Now <= expirationDate;
    }
}

public class Commercial : Software
{
    public decimal Price { get; set; }
    public DateTime InstallationDate { get; set; }
    public DateTime ExpirationDate { get; set; }

    public Commercial(string name, string manufacturer, DateTime releaseDate, decimal price, DateTime installationDate, DateTime expirationDate)
        : base(name, manufacturer, releaseDate)
    {
        Price = price;
        InstallationDate = installationDate;
        ExpirationDate = expirationDate;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"[Commercial Software]\nName: {Name}\nManufacturer: {Manufacturer}\nRelease Date: {ReleaseDate.ToShortDateString()}\nPrice: {Price:C}\nInstallation Date: {InstallationDate.ToShortDateString()}\nExpiration Date: {ExpirationDate.ToShortDateString()}");
    }

    public override bool IsUsable()
    {
        return DateTime.Now <= ExpirationDate; 
    }
}
public class Program
{
    public static void Main(string[] args)
    {

        Software[] softwares = new Software[]
        {
            new Free("WDK", "GNU", new DateTime(2015, 7, 8)),
            new Trial("WinRAR", "IWNDO", new DateTime(2019, 5, 1), DateTime.Now.AddMonths(-1), 1), // 1 month trial
            new Commercial("Microsoft Office", "Microsoft", new DateTime(2021, 1, 1), 99.99m, DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(6)) // Commercial
        };


        Console.WriteLine("All Software Information:");
        foreach (var software in softwares)
        {
            software.DisplayInfo();
            Console.WriteLine();
        }

        Console.WriteLine("Usable Software Information:");
        foreach (var software in softwares)
        {
            if (software.IsUsable())
            {
                software.DisplayInfo();
                Console.WriteLine();
            }
        }
    }
}

/////////////////////////////////////////////////////////////////////////

// Defining an interface
public interface IShape
{
    double CalculateArea();
}

// Implementing the interface in a class
public class Circle : IShape
{
    public double Radius { get; set; }

    public Circle(double radius)
    {
        Radius = radius;
    }

    // Implementing the interface method
    public double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
}

// Another class implementing the same interface
public class Rectangle : IShape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    // Implementing the interface method
    public double CalculateArea()
    {
        return Width * Height;
    }
}

class Program
{
    static void Main(string[] args)
    {
        IShape circle = new Circle(5);
        IShape rectangle = new Rectangle(4, 5);

        Console.WriteLine($"Circle Area: {circle.CalculateArea()}");
        Console.WriteLine($"Rectangle Area: {rectangle.CalculateArea()}");
    }
}

////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;

// Custom collection implementing IEnumerable
public class MyCollection : IEnumerable<int>
{
    private int[] _items = new int[] { 1, 2, 3, 4, 5 };

    // Implementing GetEnumerator method
    public IEnumerator<int> GetEnumerator()
    {
        foreach (var item in _items)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class Program
{
    static void Main(string[] args)
    {
        MyCollection collection = new MyCollection();
        foreach (int item in collection)
        {
            Console.WriteLine(item);
        }
    }
}

////////////////////////////////////////////////////////////////////////////

using System;

// Implementing IComparable for sorting
public class Student : IComparable<Student>
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Student(string name, int age)
    {
        Name = name;
        Age = age;
    }

    // Implementing CompareTo method for sorting by age
    public int CompareTo(Student other)
    {
        return Age.CompareTo(other.Age);
    }

    public override string ToString()
    {
        return $"{Name}, Age: {Age}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Using an array of Student
        Student[] students = new Student[]
        {
            new Student("Alice", 20),
            new Student("Bob", 22),
            new Student("Charlie", 19)
        };

        // Sorting the array based on the CompareTo implementation
        Array.Sort(students);
        foreach (var student in students)
        {
            Console.WriteLine(student);
        }
    }
}

//////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

// Implementing IComparer for custom sorting by name
public class StudentNameComparer : IComparer<Student>
{
    public int Compare(Student x, Student y)
    {
        return string.Compare(x.Name, y.Name);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Using an array of Student
        Student[] students = new Student[]
        {
            new Student("Alice", 20),
            new Student("Bob", 22),
            new Student("Charlie", 19)
        };

        // Sorting the array using the custom comparer
        Array.Sort(students, new StudentNameComparer());
        foreach (var student in students)
        {
            Console.WriteLine(student);
        }
    }
}


////////////////////////////////////////////////////////////////////

using System;

// Implementing ICloneable for creating object clones
public class Person : ICloneable
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    // Implementing Clone method
    public object Clone()
    {
        return new Person(Name, Age);  // shallow copy
    }

    public override string ToString()
    {
        return $"{Name}, Age: {Age}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Creating an array of Person objects
        Person[] people = new Person[]
        {
            new Person("John", 30),
            new Person("Jane", 25)
        };

        // Cloning the array
        Person[] clonedPeople = new Person[people.Length];
        for (int i = 0; i < people.Length; i++)
        {
            clonedPeople[i] = (Person)people[i].Clone();
        }

        // Modifying the cloned object
        clonedPeople[0].Name = "Mike";

        Console.WriteLine("Original Array:");
        foreach (var person in people)
        {
            Console.WriteLine(person);
        }

        Console.WriteLine("Cloned Array:");
        foreach (var person in clonedPeople)
        {
            Console.WriteLine(person);
        }
    }
}

///////////////////////////////////////////////////////////////////////


using System;

// Implementing IEquatable for equality comparison
public class Car : IEquatable<Car>
{
    public string Model { get; set; }
    public int Year { get; set; }

    public Car(string model, int year)
    {
        Model = model;
        Year = year;
    }

    // Implementing Equals method
    public bool Equals(Car other)
    {
        return Model == other.Model && Year == other.Year;
    }

    public override string ToString()
    {
        return $"{Model}, Year: {Year}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Creating an array of Car objects
        Car[] cars = new Car[]
        {
            new Car("Toyota", 2020),
            new Car("Toyota", 2020),
            new Car("Ford", 2021)
        };

        // Comparing cars in the array
        Console.WriteLine(cars[0].Equals(cars[1])); // True
        Console.WriteLine(cars[0].Equals(cars[2])); // False
    }
}


////////////////////////////////////////////////////////////////////////
// GENERICS

public class GenericCollection<T>
{
    private T[] items = new T[10];
    private int count = 0;

    public void Add(T item)
    {
        if (count < items.Length)
        {
            items[count] = item;
            count++;
        }
    }

    public T Get(int index)
    {
        return items[index];
    }
}

class Program
{
    static void Main(string[] args)
    {
        GenericCollection<int> intCollection = new GenericCollection<int>();
        intCollection.Add(10);

        GenericCollection<string> stringCollection = new GenericCollection<string>();
        stringCollection.Add("Hello");

        int number = intCollection.Get(0); // No casting required
        string text = stringCollection.Get(0); // No casting required
        Console.WriteLine(number);
        Console.WriteLine(text);
    }
}

/////////////////////////////////////////////////////////////////

class Program
{
    static void Main(string[] args)
    {
        int value = 10;

        // Boxing: Converting a value type (int) to a reference type (object)
        object boxedValue = value;

        // Unboxing: Converting back from reference type (object) to value type (int)
        int unboxedValue = (int)boxedValue;

        Console.WriteLine($"Boxed Value: {boxedValue}");
        Console.WriteLine($"Unboxed Value: {unboxedValue}");
    }
}

////////////////////////////////////////////////////////////////////

public class GenericBox<T>
{
    private T _value;

    public GenericBox(T value)
    {
        _value = value;
    }

    public T GetValue()
    {
        return _value;
    }

    public void SetValue(T value)
    {
        _value = value;
    }
}

class Program
{
    static void Main(string[] args)
    {
        GenericBox<int> intBox = new GenericBox<int>(10);
        Console.WriteLine($"Value in intBox: {intBox.GetValue()}");

        GenericBox<string> strBox = new GenericBox<string>("Hello");
        Console.WriteLine($"Value in strBox: {strBox.GetValue()}");
    }
}


////////////////////////////////////////////////////////////////////////


public class Utilities
{
    // A generic method that swaps two elements of any type
    public static void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }
}

class Program
{
    static void Main(string[] args)
    {
        int x = 5, y = 10;
        Console.WriteLine($"Before swap: x = {x}, y = {y}");
        Utilities.Swap(ref x, ref y);
        Console.WriteLine($"After swap: x = {x}, y = {y}");

        string str1 = "Hello", str2 = "World";
        Console.WriteLine($"Before swap: str1 = {str1}, str2 = {str2}");
        Utilities.Swap(ref str1, ref str2);
        Console.WriteLine($"After swap: str1 = {str1}, str2 = {str2}");
    }
}


///////////////////////////////////////////////////////////////////////
///

public interface IRepository<T>
{
    void Add(T item);
    T Get(int id);
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class ProductRepository : IRepository<Product>
{
    private Product[] _products = new Product[10];
    private int _index = 0;

    public void Add(Product item)
    {
        _products[_index++] = item;
    }

    public Product Get(int id)
    {
        foreach (var product in _products)
        {
            if (product != null && product.Id == id)
                return product;
        }
        return null;
    }
}

class Program
{
    static void Main(string[] args)
    {
        ProductRepository repository = new ProductRepository();

        repository.Add(new Product { Id = 1, Name = "Laptop" });
        repository.Add(new Product { Id = 2, Name = "Phone" });

        Product product = repository.Get(1);
        Console.WriteLine($"Product retrieved: {product.Name}");
    }
}


///////////////////////////////////////////////////////////////////
///

using System;

class Program
{
    static void Main(string[] args)
    {
        // Initialize an array
        int[] numbers = new int[] { 1, 2, 3, 4, 5 };

        // Access an element (Search by index)
        Console.WriteLine($"Element at index 2: {numbers[2]}"); // Output: 3

        // Insert an element by creating a new array (simulated insert at position 2)
        int[] newNumbers = new int[numbers.Length + 1];
        for (int i = 0, j = 0; i < newNumbers.Length; i++)
        {
            if (i == 2) // Insert at index 2
                newNumbers[i] = 99;
            else
                newNumbers[i] = numbers[j++];
        }

        Console.WriteLine("Array after insertion:");
        foreach (int num in newNumbers)
            Console.Write(num + " "); // Output: 1 2 99 3 4 5

        // Remove an element (simulated remove at index 3)
        int[] afterRemove = new int[newNumbers.Length - 1];
        for (int i = 0, j = 0; i < newNumbers.Length; i++)
        {
            if (i != 3) // Skip index 3 (removal)
                afterRemove[j++] = newNumbers[i];
        }

        Console.WriteLine("\nArray after removal:");
        foreach (int num in afterRemove)
            Console.Write(num + " "); // Output: 1 2 99 4 5
    }
}


/////////////////////////////////////////////////////////////
///

using System;
using System.Collections;

class Program
{
    static void Main(string[] args)
    {
        ArrayList list = new ArrayList();

        // Adding elements
        list.Add(10);
        list.Add(20);
        list.Add(30);

        // Inserting an element at index 1
        list.Insert(1, 15); // Inserting 15 at index 1

        // Searching for an element
        bool containsElement = list.Contains(20); // Check if list contains 20
        Console.WriteLine("Contains 20: " + containsElement); // Output: True

        // Removing an element
        list.Remove(20); // Remove element with value 20

        // Printing final list
        Console.WriteLine("List after removal:");
        foreach (var item in list)
        {
            Console.Write(item + " "); // Output: 10 15 30
        }
    }
}

////////////////////////////////////////////////////////////////
///

using System;

class Program
{
    static void Main(string[] args)
    {
        int[] numbers = new int[] { 5, 2, 8, 1, 3 };

        // Sorting the array
        Array.Sort(numbers);

        Console.WriteLine("Sorted array:");
        foreach (int number in numbers)
        {
            Console.Write(number + " "); // Output: 1 2 3 5 8
        }
    }
}

///////////////////////////////////////////////////////////////////////
///


using System;
using System.Collections;

class Program
{
    static void Main(string[] args)
    {
        ArrayList list = new ArrayList() { 30, 10, 20, 40, 5 };

        // Sorting the ArrayList
        list.Sort();

        Console.WriteLine("Sorted ArrayList:");
        foreach (var item in list)
        {
            Console.Write(item + " "); // Output: 5 10 20 30 40
        }
    }
}


/////////////////////////////////////////////////////////////////
///

class Program
{
    static void Main(string[] args)
    {
        int[] numbers = new int[5] { 1, 2, 3, 4, 5 };

        foreach (int number in numbers)
        {
            Console.Write(number + " "); // Output: 1 2 3 4 5
        }
    }
}


////////////////////////////////////////////////////
///

class Program
{
    static void Main(string[] args)
    {
        int[,] matrix = new int[3, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 } };

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + " "); // Output: 1 2 3 4 5 6
            }
            Console.WriteLine();
        }
    }
}


///////////////////////////////////////////////////////
///

int[] arr = new int[5] { 10, 20, 30, 40, 50 };
Console.WriteLine(arr[2]); // Output: 30


ArrayList arrayList = new ArrayList();
arrayList.Add(1);
arrayList.Add("Hello");
arrayList.Add(3.14);
Console.WriteLine(arrayList[1]); // Output: Hello

///////////////////////////////////////////////////
///
using System.Collections;

BitArray bitArray = new BitArray(5);
bitArray[0] = true;
bitArray[1] = false;
bitArray[2] = true;
foreach (bool bit in bitArray)
{
    Console.WriteLine(bit); // Output: True False True False False
}

////////////////////////////////////////////////////
///
Hashtable hashtable = new Hashtable();
hashtable.Add(1, "One");
hashtable.Add(2, "Two");
Console.WriteLine(hashtable[1]); // Output: One


///////////////////////////////////////////
///
Queue queue = new Queue();
queue.Enqueue(10);
queue.Enqueue(20);
Console.WriteLine(queue.Dequeue()); // Output: 10

//////////////////////////////////////
///
SortedList sortedList = new SortedList();
sortedList.Add(2, "Two");
sortedList.Add(1, "One");
foreach (DictionaryEntry entry in sortedList)
{
    Console.WriteLine($"{entry.Key}: {entry.Value}"); // Output: 1: One, 2: Two
}

///////////////////////////////////////////
///

Stack stack = new Stack();
stack.Push(10);
stack.Push(20);
Console.WriteLine(stack.Pop()); // Output: 20

//////////////////////////////////////////////////////
///

using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] numbers = { 5, 10, 15, 20, 25 };

        // LINQ query syntax
        var query = from number in numbers
                    where number > 10
                    select number;

        Console.WriteLine("Numbers greater than 10:");
        foreach (var num in query)
        {
            Console.WriteLine(num);  // Output: 15, 20, 25
        }
    }
}

///////////////////////////////////////////////
///

using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] numbers = { 5, 10, 15, 20, 25 };

        // LINQ method syntax
        var query = numbers.Where(number => number > 10);

        Console.WriteLine("Numbers greater than 10:");
        foreach (var num in query)
        {
            Console.WriteLine(num);  // Output: 15, 20, 25
        }
    }
}


////////////////////////////////////////////////////
///

using System;
using System.Linq;

class Program
{
    static void Main()
    {
        string[] names = { "Alice", "Bob", "Charlie", "David", "Eve" };

        // LINQ method syntax with Where, Select, and OrderBy
        var filteredNames = names.Where(name => name.Length > 3)
                                 .Select(name => name.ToUpper())
                                 .OrderBy(name => name);

        Console.WriteLine("Filtered and sorted names:");
        foreach (var name in filteredNames)
        {
            Console.WriteLine(name);  // Output: ALICE, CHARLIE, DAVID
        }
    }
}


/////////////////////////////////////////////////////
///

using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] numbers = { 5, 10, 15, 20, 25 };

        int count = numbers.Count();
        int sum = numbers.Sum();
        double average = numbers.Average();
        int min = numbers.Min();
        int max = numbers.Max();

        Console.WriteLine($"Count: {count}");      // Output: Count: 5
        Console.WriteLine($"Sum: {sum}");          // Output: Sum: 75
        Console.WriteLine($"Average: {average}");  // Output: Average: 15
        Console.WriteLine($"Min: {min}");          // Output: Min: 5
        Console.WriteLine($"Max: {max}");          // Output: Max: 25
    }
}

/////////////////////////////////////////////////////
///
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] numbers = { 2, 3, 4 };

        // Multiply all numbers in the array
        int product = numbers.Aggregate((total, next) => total * next);

        Console.WriteLine($"Product: {product}");  // Output: Product: 24
    }
}

///////////////////////////////////////////////////////////////
///

using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] numbers = { 2, 4, 6, 8, 10 };

        bool anyGreaterThan5 = numbers.Any(n => n > 5);
        bool allEven = numbers.All(n => n % 2 == 0);
        int firstEven = numbers.First(n => n % 2 == 0);
        int lastEven = numbers.Last(n => n % 2 == 0);

        Console.WriteLine($"Any greater than 5: {anyGreaterThan5}");  // Output: True
        Console.WriteLine($"All are even: {allEven}");                // Output: True
        Console.WriteLine($"First even number: {firstEven}");         // Output: 2
        Console.WriteLine($"Last even number: {lastEven}");           // Output: 10
    }
}


//////////////////////////////////////////////////////////////////
///

using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] numbers = Enumerable.Range(1, 1000000).ToArray();

        // Sequential LINQ
        var sequentialQuery = numbers.Where(n => n % 2 == 0).ToArray();
        Console.WriteLine($"Sequential query result count: {sequentialQuery.Length}");

        // Parallel LINQ (PLINQ)
        var parallelQuery = numbers.AsParallel().Where(n => n % 2 == 0).ToArray();
        Console.WriteLine($"Parallel query result count: {parallelQuery.Length}");
    }
}


//////////////////////////////////////////////////////////////
///


using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Before async call");

        // Calling the async method
        string result = await GetDataAsync();

        Console.WriteLine("After async call");
        Console.WriteLine(result);
    }

    // Asynchronous method
    static async Task<string> GetDataAsync()
    {
        await Task.Delay(2000); // Simulates an I/O operation (2 seconds delay)
        return "Data received";
    }
}


///////////////////////////////////////////////////////////////
///

using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        // Start two async tasks concurrently
        Task<int> task1 = ComputeSumAsync(10);
        Task<int> task2 = ComputeSumAsync(20);

        // Await both tasks
        int result1 = await task1;
        int result2 = await task2;

        Console.WriteLine($"Result 1: {result1}, Result 2: {result2}");
    }

    // Async method that returns a Task<int>
    static async Task<int> ComputeSumAsync(int number)
    {
        await Task.Delay(1000); // Simulates a delay (e.g., I/O operation)
        return number * 2;
    }
}

//////////////////////////////////////////////////////