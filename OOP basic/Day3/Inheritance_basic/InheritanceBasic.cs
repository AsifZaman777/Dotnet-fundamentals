namespace InheritanceBasic
{
    class Rectangle
    {
        protected double length;
        protected double width;

        public Rectangle(double length, double width)
        {
            this.length = length;
            this.width = width;
        }

        public double getArea()
        {
            return length * width;
        }
        public void display()
        {
            Console.WriteLine($"Length is {length}");
            Console.WriteLine($"Width is {width}");
            Console.WriteLine($"Area is {getArea()}");
        }
    }

    //conventional way
    class Table : Rectangle
    {

        //initializing base class member variables using constructor chaining
        public Table(double length, double width) : base(length, width) { }

        public double getCost()
        {
            return getArea() * 70;
        }

        public void display()
        {
            base.display(); //call base class display method
            Console.WriteLine($"Table cost is {getCost()}");
        }

    }

    //primary constructor c#12.0 feature modern approach
    class Chair(double length, double width) : Rectangle(length, width)
    {
        public double getCost()
        {
            return getArea() * 50;
        }
        public void display()
        {
            base.display(); //call base class display method
            Console.WriteLine($"Chair cost is {getCost()}");
        }
    }


    class InheritanceBasic
    {
        static void Main(string[] args)
        {
            Table table = new Table(5, 3);
            table.display();
            Chair chair = new Chair(2, 2);
            chair.display();
        }
    }

}