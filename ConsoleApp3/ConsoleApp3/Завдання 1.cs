using System;
using System.Collections.Generic;

public abstract class Vehicle
{
    public int Speed { get; set; }
    public int Capacity { get; set; }

    public abstract void Move();
}

public class Human
{
    public int Speed { get; set; }

    public void Move()
    {
        Console.WriteLine("The human is moving.");
    }
}

public class Car : Vehicle
{
    public Car()
    {
        Speed = 60; 
        Capacity = 5; 
    }

    public override void Move()
    {
        Console.WriteLine("The car is moving on the road.");
    }
}

public class Bus : Vehicle
{
    public Bus()
    {
        Speed = 40; 
        Capacity = 40; 
    }

    public override void Move()
    {
        Console.WriteLine("The bus is moving on the route.");
    }
}

public class Train : Vehicle
{
    public Train()
    {
        Speed = 80; 
        Capacity = 200; 
    }

    public override void Move()
    {
        Console.WriteLine("The train is moving on the tracks.");
    }
}

public class Route
{
    public string StartPoint { get; set; }
    public string EndPoint { get; set; }

    public void CalculateOptimalRoute(Vehicle vehicle)
    {
        if (vehicle is Car)
        {
            Console.WriteLine("Calculating the optimal route for a car...");
        }
        else if (vehicle is Bus)
        {
            Console.WriteLine("Calculating the optimal route for a bus...");          
        }
        else if (vehicle is Train)
        {
            Console.WriteLine("Calculating the optimal route for a train...");
        }
    }
}

public class TransportNetwork
{
    private List<Vehicle> vehicles = new List<Vehicle>();

    public void AddVehicle(Vehicle vehicle)
    {
        vehicles.Add(vehicle);
    }

    public void ControlMovement()
    {
        foreach (var vehicle in vehicles)
        {
            vehicle.Move();
        }
    }

    public void PassengerHandling(Route route)
    {
        Console.WriteLine("Handling passengers on the route from " + route.StartPoint + " to " + route.EndPoint);
    }
}

class Program
{
    static void Main(string[] args)
    {

        Car car = new Car();
        Bus bus = new Bus();
        Train train = new Train();

        Route route = new Route { StartPoint = "City A", EndPoint = "City B" };

        TransportNetwork network = new TransportNetwork();
        network.AddVehicle(car);
        network.AddVehicle(bus);
        network.AddVehicle(train);

        network.ControlMovement();
        route.CalculateOptimalRoute(car);
        network.PassengerHandling(route);
    }
}
