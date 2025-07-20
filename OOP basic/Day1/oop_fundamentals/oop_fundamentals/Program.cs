
class Car
{
    public string brand;
    public string model;
    public int year;
    private double milage;
    private bool isRunning;

    public Car(string brand, string model, int year)
    {
        this.brand = brand;
        this.model = model;
        this.year = year;
        this.milage = 0.0;
        this.isRunning = false;
    }

    public void StartEngine()
    {
        isRunning = true;
    }
    public void StopEngine()
    {
        isRunning = false;
    }

    public void Drive(double miles)
    {
        if(isRunning && miles>0)
        {
            milage += miles;
            Console.WriteLine($"Driving {miles} miles. Total milage is now {milage} miles.");
        }
        else if(!isRunning)
        {
            Console.WriteLine("Cannot drive. The engine is not running.");
        }
        else
        {
           Console.WriteLine("Miles driven must be greater than zero.");
        }
    }

    public double GetMilage()
    {
        return milage;
    }

    public void GetCarInfo()
    {
        Console.WriteLine($"Brand: {brand}, Model: {model}, Year: {year}, Milage: {milage} miles, Engine Running: {isRunning}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Car myCar = new Car("Toyota", "Corolla", 2020);
        myCar.StartEngine();
        myCar.Drive(50);
        myCar.StopEngine();

        //day 2
        myCar.StartEngine();
        myCar.Drive(100);
        myCar.StopEngine();

        //day 3
        myCar.StartEngine();
        myCar.Drive(50);
        myCar.StopEngine();
        myCar.GetCarInfo();

        //day 4
        myCar.StartEngine();
        myCar.Drive(20);

        //day5 
        myCar.Drive(20);
        myCar.StopEngine();
    }
}