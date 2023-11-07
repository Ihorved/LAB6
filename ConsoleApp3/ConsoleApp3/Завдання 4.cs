using System;
using System.Collections.Generic;

public abstract class GraphicPrimitive
{
    public int X { get; set; }
    public int Y { get; set; }

    public abstract void Draw();
    public abstract void Move(int x, int y);
    public abstract void Scale(float factor);
}

public class Circle : GraphicPrimitive
{
    public int Radius { get; set; }

    public override void Draw()
    {
        Console.WriteLine($"Drawing Circle at ({X},{Y}) with radius {Radius}");
    }

    public override void Move(int x, int y)
    {
        X += x;
        Y += y;
    }

    public override void Scale(float factor)
    {
        Radius = (int)(Radius * factor);
    }
}

public class Rectangle : GraphicPrimitive
{
    public int Width { get; set; }
    public int Height { get; set; }

    public override void Draw()
    {
        Console.WriteLine($"Drawing Rectangle at ({X},{Y}) with width {Width} and height {Height}");
    }

    public override void Move(int x, int y)
    {
        X += x;
        Y += y;
    }

    public override void Scale(float factor)
    {
        Width = (int)(Width * factor);
        Height = (int)(Height * factor);
    }
}

public class Triangle : GraphicPrimitive
{
}

public class Group : GraphicPrimitive
{
    private List<GraphicPrimitive> elements = new List<GraphicPrimitive>();

    public void Add(GraphicPrimitive element)
    {
        elements.Add(element);
    }

    public override void Draw()
    {
        foreach (var element in elements)
        {
            element.Draw();
        }
    }

    public override void Move(int x, int y)
    {
        foreach (var element in elements)
        {
            element.Move(x, y);
        }
    }

    public override void Scale(float factor)
    {
        foreach (var element in elements)
        {
            element.Scale(factor);
        }
    }
}

public class GraphicsEditor
{
    private List<GraphicPrimitive> graphics = new List<GraphicPrimitive>();

    public void AddGraphicPrimitive(GraphicPrimitive primitive)
    {
        graphics.Add(primitive);
    }

    public void DrawAll()
    {
        foreach (var primitive in graphics)
        {
            primitive.Draw();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {

        GraphicsEditor editor = new GraphicsEditor();

        Circle circle = new Circle { X = 10, Y = 20, Radius = 5 };
        Rectangle rectangle = new Rectangle { X = 30, Y = 40, Width = 8, Height = 12 };

        Group group = new Group();
        group.Add(circle);
        group.Add(rectangle);

        editor.AddGraphicPrimitive(circle);
        editor.AddGraphicPrimitive(rectangle);
        editor.AddGraphicPrimitive(group);

        editor.DrawAll();

        circle.Move(5, 5);
        rectangle.Scale(1.5f);
        group.Move(10, 10);

        editor.DrawAll();
    }
}
