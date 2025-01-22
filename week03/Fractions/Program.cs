using System;

public class Fraction
{
    // Private attributes
    private int _numerator;
    private int _denominator;

    // Default constructor (1/1)
    public Fraction()
    {
        _numerator = 1;
        _denominator = 1;
    }

    // Constructor with one parameter (top number)
    public Fraction(int numerator)
    {
        _numerator = numerator;
        _denominator = 1;
    }

    // Constructor with two parameters (top and bottom)
    public Fraction(int numerator, int denominator)
    {
        if (denominator == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.");
        }
        _numerator = numerator;
        _denominator = denominator;
    }

    // Getters and setters
    public int Numerator
    {
        get { return _numerator; }
        set { _numerator = value; }
    }

    public int Denominator
    {
        get { return _denominator; }
        set
        {
            if (value == 0)
            {
                throw new ArgumentException("Denominator cannot be zero.");
            }
            _denominator = value;
        }
    }

    // Method to return the fraction as a string
    public string GetFractionString()
    {
        return $"{_numerator}/{_denominator}";
    }

    // Method to return the fraction as a decimal
    public double GetDecimalValue()
    {
        return (double)_numerator / _denominator;
    }
}
class Program
{
    static void Main(string[] args)
    {
        // Test default constructor
        Fraction fraction1 = new Fraction();
        Console.WriteLine(fraction1.GetFractionString()); // Output: 1/1
        Console.WriteLine(fraction1.GetDecimalValue());  // Output: 1

        // Test constructor with one parameter
        Fraction fraction2 = new Fraction(5);
        Console.WriteLine(fraction2.GetFractionString()); // Output: 5/1
        Console.WriteLine(fraction2.GetDecimalValue());  // Output: 5

        // Test constructor with two parameters
        Fraction fraction3 = new Fraction(3, 4);
        Console.WriteLine(fraction3.GetFractionString()); // Output: 3/4
        Console.WriteLine(fraction3.GetDecimalValue());  // Output: 0.75

        // Test changing values using setters
        fraction3.Numerator = 1;
        fraction3.Denominator = 3;
        Console.WriteLine(fraction3.GetFractionString()); // Output: 1/3
        Console.WriteLine(fraction3.GetDecimalValue());  // Output: 0.3333333333333333
    }
}