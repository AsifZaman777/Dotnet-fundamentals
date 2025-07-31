
/*
- Classes should be open for extension but closed for modification. That means you should be able to add new 
functionality without changing existing code.
- This principle encourages the use of interfaces or abstract classes to allow for new implementations without altering existing code.
 */

//Bad Example: Must modify existing code to add new shape types
public class BadAreaCalculator
{
    public static double CalculateArea(object shape)
    {
        if (shape is Rectangle rect)
        {
            return rect.Width * rect.Height;
        }
        else if (shape is Circle circle)
        {
            return Math.PI * Math.Pow(circle.Radius, 2);
        }
        //To add triangle , we must modify this method
        else if (shape is Triangle triangle)
        {
            return 0.5 * triangle.Base * triangle.Height;
        }

        return 0;
    }
}

//Good Example: New shapes can be added without modifying existing code
public abstract class Shape
{
    public abstract double CalculateArea();
}

public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }
    public override double CalculateArea() => Width * Height;
}

public class Circle : Shape
{
    public double Radius { get; set; }
    public override double CalculateArea() => Math.PI * Math.Pow(Radius, 2);
}

// Lets add a new shape without modifying existing code
public class Triangle : Shape
{
    public double Base { get; set; }
    public double Height { get; set; }
    public override double CalculateArea() => 0.5 * Base * Height;
}

public class AreaCalculator
{
    public static double CalculateArea(Shape shape)
    {
        return shape.CalculateArea();
    }
}


