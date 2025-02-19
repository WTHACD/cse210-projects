using System;
using System.Collections.Generic;

namespace FitnessTrackerApp
{
    abstract class Activity
    {
        private DateTime _date;
        private double _minutes;

        public Activity(DateTime date, double minutes)
        {
            _date = date;
            _minutes = minutes;
        }

        public DateTime Date => _date;
        public double Minutes => _minutes;

        public abstract double GetDistance();
        public abstract double GetSpeed();
        public abstract double GetPace();

        public virtual string GetSummary()
        {
            return $"{Date:dd MMM yyyy} {this.GetType().Name} ({Minutes} min) - " +
                   $"Distance: {GetDistance():0.0} miles, " +
                   $"Speed: {GetSpeed():0.0} mph, " +
                   $"Pace: {GetPace():0.0} min per mile";
        }
    }

   
    class Running : Activity
    {
        private double _distance; 

        public Running(DateTime date, double minutes, double distance)
            : base(date, minutes)
        {
            _distance = distance;
        }

        public override double GetDistance() => _distance;

        public override double GetSpeed() => _distance * 60 / Minutes;  

        public override double GetPace() => Minutes / _distance;         
    }

    
    class Cycling : Activity
    {
        private double _speed; 

        public Cycling(DateTime date, double minutes, double speed)
            : base(date, minutes)
        {
            _speed = speed;
        }

        public override double GetSpeed() => _speed;

        
        public override double GetDistance() => _speed * Minutes / 60;

        public override double GetPace() => Minutes / GetDistance();
    }

    
    class Swimming : Activity
    {
        private int _laps;

        public Swimming(DateTime date, double minutes, int laps)
            : base(date, minutes)
        {
            _laps = laps;
        }

        public override double GetDistance()
        {
            
            return _laps * 50.0 / 1000.0 * 0.62;
        }

        public override double GetSpeed() => GetDistance() * 60 / Minutes; // mph

        public override double GetPace() => Minutes / GetDistance();         // minutes per mile
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            
            List<Activity> activities = new List<Activity>();

           
            activities.Add(new Running(new DateTime(2022, 11, 03), 30, 3.0));

            
            activities.Add(new Cycling(new DateTime(2022, 11, 03), 45, 10));

            
            activities.Add(new Swimming(new DateTime(2022, 11, 03), 30, 20));

            
            foreach (var activity in activities)
            {
                Console.WriteLine(activity.GetSummary());
            }
        }
    }
}
