﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Geometry
{ 
    internal class Program
    {
        static void Main(string[] args)
        {
            string version = "v1.0";
            List<Circle> circles = new List<Circle>() { new Circle(0, 0, 1, 1), new Circle(1, 1, 2, 1) };
            List<Rectangle> rectangles = new List<Rectangle>() { new Rectangle(1, 1, 1, 1, 2), new Rectangle(1, 1, 2, 2, 1), Rectangle.GetRectangleFromConsole(3)};
            List<Square> squares = new List<Square>() { new Square(1, 1, 1, 1), new Square(0, 0, 2, 1) };
            List<GeometryObject> geometryObject = new List<GeometryObject>();
            //Console.WriteLine(GetStringOfAllGeoObjects(geometryObject.Concat(circles).Concat(rectangles).Concat(squares).ToList()));
            Console.WriteLine(AllIntersects(rectangles[2], geometryObject.Concat(circles).Concat(rectangles).Concat(squares).ToList()));
            Console.ReadLine();
        }
        public static string GetStartString(string version)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Commandline tool for 2D geometry\n");
            sb.Append($"Version: {version}");
            sb.Append("Write \"Geometry -h\" or \"Geometry --help\" to get help with commands\n");
            return sb.ToString();
        }
        public static string GetHelpString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Comandline tool for 2D geometry\n\n");
            sb.Append("Usage:   Geometry [command] [command-aruments]\n");
            sb.Append("Example: Geometry Add Square\n\n");
            sb.Append("Global options:\n");
            sb.Append("-h, --help     \tPrints usage information\n");
            sb.Append("    --version  \tPrints the current version\n\n");
            sb.Append("Available commands:\n");
            sb.Append("  Add         \tCreates new object\n");
            sb.Append("  AllIntersets\tReturns all object that intersects with given object\n");
            sb.Append("  Area        \tCalculates the area of given object\n");
            sb.Append("  Get         \tPrints all geometry objets and it's properties\n");
            sb.Append("  Intersects  \tChecks if 2 objects intersects in 2D base on their properties\n");
            sb.Append("  Perimeter   \tCalculates the perimeter of given object\n");
            sb.Append("  Remove      \tRemoves object you chose\n");
            sb.Append("  RemoveAll   \tRemoves all instances of given type\n");
            return sb.ToString();
        }
        public static string GetHelpWithCommandString(string command) => $"For help with command {command} type \"{command} -h\" or \"{command} --help\"";
        public static string GetAddHelpString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Create new object\n\n");
            sb.Append("Usage:   Geometry Add [object-type]\n");
            sb.Append("Example: Geometry Add Square");
            sb.Append("-h, --help     \tPrints this usage information.\n\n");
            sb.Append("  [Circle]   \tCreates circle then asks you to enter it's properties\n");
            sb.Append("  [Rectangle]\tCreates rectangle then asks you to enter it's properties\n");
            sb.Append("  [Square]   \tCreates square then asks you to enter it's properties\n");
            return sb.ToString();
        }
        public static string GetAllIntersectsHelpString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Return every object that intersects with given object\n\n");
            sb.Append("Usage:   Geometry AllIntersects [object-type][object-number]\n");
            sb.Append("Example: Geometry All Intersects Rectangle2\n\n");
            sb.Append("-h, --help     \tPrints this usage information.\n\n");
            sb.Append("  [Circle]   \tObject of type Circle\n");
            sb.Append("  [Square]   \tObject of type Square\n");
            sb.Append("  [Rectangle]\tObject of type Rectangle\n");
            return sb.ToString(); 
        }
        public static string GetAreaHelpString() 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Calculates the area of given object\n\n");
            sb.Append("Usage:   Geometry Area [object-type][object-number]\n");
            sb.Append("Example: Geometry Area Circle2\n\n");
            sb.Append("-h, --help     \tPrints this usage information.\n\n");
            sb.Append("  [Circle]   \tObject of type Circle\n");
            sb.Append("  [Square]   \tObject of type Square\n");
            sb.Append("  [Rectangle]\tObject of type Rectangle\n");
            return sb.ToString();
        }
        public static string GetGetHelpString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Get properties of every existing objects or certain object type\n\n");
            sb.Append("Usage:    Geometry Get (optional)[object-type]\n");
            sb.Append("Example1: Geometry Get\n");
            sb.Append("Example2: Geometry Get Square\n\n");
            sb.Append("-h, --help     \tPrints this usage information.\n\n");
            sb.Append("  [Circle]   \tGet properties of every existing circle\n");
            sb.Append("  [Rectangle]\tGet properties of every existing rectangle\n");
            sb.Append("  [Square]   \tGet properties of every existing square\n");
            return sb.ToString();
        }
        public static string GetIntersectHelpString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Check if 2 objects intersects in 2D\n\n");
            sb.Append("Usage:   Geometry Intersect [first-object-type][first-object-number] [second-object-type][second-object-number]\n");
            sb.Append("Example: Geometry Intersect Circle1 Square3\n\n");
            sb.Append("-h, --help     \tPrints this usage information.\n\n");
            sb.Append("  [Circle]   \tObject type of Circle + Circle number\n");
            sb.Append("  [Rectangle]\tObject type of Rectagle + Rectangle number\n");
            sb.Append("  [Square]   \tObject type of Square + Square number\n");
            return sb.ToString();
        }
        public static string GetPerimeterHelpString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Calculates the perimeter of given object\n\n");
            sb.Append("Usage:   Geometry Perimeter [object-type][object-number]\n");
            sb.Append("Example: Geometry Perimeter Square1\n\n");
            sb.Append("-h, --help     \tPrints this usage information.\n\n");
            sb.Append("  [Circle]   \tObject of type Circle\n");
            sb.Append("  [Square]   \tObject of type Square\n");
            sb.Append("  [Rectangle]\tObject of type Rectangle\n");
            return sb.ToString();
        }
        public static string GetRemoveHelpString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Remove certain instance of object\n\n");
            sb.Append("Usage:   Geometry Remove [object-type][object-number]\n");
            sb.Append("Example: Geometry Remove Square2\n\n");
            sb.Append("-h, --help     \tPrints this usage information.\n\n");
            sb.Append("  [Circle]   \tRemoves the determined Circle\n");
            sb.Append("  [Rectangle]\tRemoves the determined Rectangle\n");
            sb.Append("  [Square]   \tRemoves the determined Square\n");
            return sb.ToString();
        }
        public static string GetRemoveAllHelpString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Remove certain instance object\n\n");
            sb.Append("Usage:   Geometry RemoveAll [object-type][object-number]\n");
            sb.Append("Example: Geometry RemoveAll Squares\n");
            sb.Append("-h, --help     \tPrints this usage information.\n\n");
            sb.Append("  [Circles]   \tRemoves every existing cirlce\n");
            sb.Append("  [Objects]   \tRemoves every existing geometry object\n");
            sb.Append("  [Rectangles]\tRemoves every existing rectangle\n");
            sb.Append("  [Squares]   \tRemoves every existing square\n");
            return sb.ToString();
        }
        public static GeometryObject AddGeometryObject(string go)
        {
            if (go.ToLower() == "circle")
            {
                //return Circle.GetCircleFromConsole();
            }
            else if (go.ToLower() == "square")
            {
                //return Square.GetSquareFromConsole();
            }
            else if (go.ToLower() == "rectangle")
            {
                //return Rectangle.GetRectangleFromConsole();
            }
            throw new ArgumentException();
        }
        public static string AllIntersects(GeometryObject go, List<GeometryObject> objects)
        {
            List<GeometryObject> intersects = new List<GeometryObject>();
            StringBuilder sb = new StringBuilder();
            sb.Append("+--------------+-------------------------------------+\n");
            sb.Append("| Object       | Objects that Intersects with Object |\n");
            sb.Append("+--------------+-------------------------------------+\n");
            sb.Append($"| {go.GetTypeString()}{go.ID}{new String(' ', 13 - go.GetTypeString().Length - go.ID.ToString().Length)}| ");
            foreach (var item in objects)
            {
                if (go.GetTypeString()+go.ID.ToString() == item.GetTypeString() + item.ID.ToString())
                {
                    continue;
                }
                bool intersect = go.IntersectsWith(item);
                if (intersect)
                {
                    intersects.Add(item);
                }
            }
            if (intersects.Count > 0)
            {
                sb.Append($"{intersects[0].GetTypeString()}{intersects[0].ID}{new String(' ', 36 - intersects[0].GetTypeString().Length - intersects[0].ID.ToString().Length)}|\n");
                sb.Append("+--------------+-------------------------------------+\n");
                for (int i = 1; i < intersects.Count; i++)
                {
                    sb.Append($"               | {intersects[i].GetTypeString()}{intersects[i].ID}{new String(' ', 36 - intersects[i].GetTypeString().Length - intersects[i].ID.ToString().Length)}|\n");
                    sb.Append("               +-------------------------------------+\n");
                }
            }
            else
            {
                sb.Append($"None{new String(' ', 32)}|\n");
                sb.Append("+--------------+-------------------------------------+\n");
            }
            return sb.ToString();
        }
        public static string GetStringOfAllGeoObjects(List<GeometryObject> objects)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            for (int i = 0; i < objects.Count; i++)
            {
                string behindOC = new String(' ', 5 - objects[i].ID.ToString().Length);
                if (i == 0)
                {
                    sb.Append("+-----------+--------+-----------------------+\n");
                    sb.Append("| Object    | Object |  Object               |\n");
                    sb.Append("| Type      | Number |  Properties           |\n");
                    sb.Append("+-----------+--------+-----------------------+\n");
                    if (objects[i] is Circle)
                    {
                        sb.Append($"| {objects[i].GetTypeString()}    |   {objects[i].ID}{behindOC}| {objects[i].ToString()}   |\n");
                        sb.Append("+-----------+--------+-----------------------+\n");
                    }
                    else if (objects[i] is Square)
                    {
                        sb.Append($"| {objects[i].GetTypeString()}    |   {objects[i].ID}{behindOC}| {objects[i].ToString()}    r\n");
                        sb.Append("+-----------+--------+-----------------------+\n");
                    }
                    else if (objects[i] is Rectangle)
                    {
                        sb.Append($"| {objects[i].GetTypeString()} |   {objects[i].ID}{behindOC}| {objects[i].ToString()}  e\n");
                        sb.Append("+-----------+--------+-----------------------+\n");
                    }
                }
                else
                {
                    if (objects[i] is Circle)
                    {
                        sb.Append($"| {objects[i].GetTypeString()}    |   {objects[i].ID}{behindOC}| {objects[i].ToString()}   |\n");
                        sb.Append("+-----------+--------+-----------------------+\n");
                    }
                    else if (objects[i] is Square)
                    {
                        sb.Append($"| {objects[i].GetTypeString()}    |   {objects[i].ID}{behindOC}| {objects[i].ToString()}     |\n");
                        sb.Append("+-----------+--------+-----------------------+\n");
                    }
                    else if (objects[i] is Rectangle)
                    {
                        sb.Append($"| {objects[i].GetTypeString()} |   {objects[i].ID}{behindOC}| {objects[i].ToString()}   |\n");
                        sb.Append("+-----------+--------+-----------------------+\n");
                    }
                }
            }
            return sb.ToString();

        }
        public static List<Circle> RemoveCircle(List<Circle> circles, int circleNum)
        {
            circles.RemoveAt(circleNum - 1);
            return circles;
        }
        public static List<Square> RemoveSquare(List<Square> squares, int squareNum)
        {
            squares.RemoveAt(squareNum - 1);
            return squares;
        }
        public static List<Rectangle> RemoveRectangle(List<Rectangle> rectangles, int rectangleNum)
        {
            rectangles.RemoveAt(rectangleNum - 1);
            return rectangles;
        }
    }
    class GeometryObject
    {
        private double centerOfGravityX;
        public double CenterOfGravityX { get => this.centerOfGravityX; set => this.centerOfGravityX = value; }
        private double centerOfGravityY;
        public double CenterOfGravityY { get => this.centerOfGravityY; set => this.centerOfGravityY = value; }
        private int id;
        public int ID
        {
            get => this.id;
            set => this.id = value;
        }
        public GeometryObject() : this(0, 0, 1) { }
        public GeometryObject(double centerGravityX, double centerGravityY, int objectID)
        {
            this.CenterOfGravityX = centerGravityX;
            this.CenterOfGravityY = centerGravityY;
            this.ID = objectID;
        }
        public virtual double Perimeter() => 0;
        public virtual double Area() => 0;
        public virtual bool IntersectsWith(GeometryObject go) => false;
        public string GetTypeString() => this.GetType().ToString().Split('.')[1].ToString();
        public override string ToString() => $"X: {CenterOfGravityX} Y: {CenterOfGravityY}";
        public override bool Equals(object other)
        {
            GeometryObject go = other as GeometryObject;
            return go.CenterOfGravityX == CenterOfGravityX && go.CenterOfGravityY == CenterOfGravityY;
        }
        public override int GetHashCode() => CenterOfGravityX.GetHashCode() ^ CenterOfGravityY.GetHashCode();
        public static bool operator ==(GeometryObject g1, GeometryObject g2) => g1.Equals(g2);
        public static bool operator !=(GeometryObject g1, GeometryObject g2) => !g1.Equals(g2);
    }
    class Circle : GeometryObject
    {
        private double radius;
        public double Radius
        {
            get => this.radius;
            set
            {
                if (value > 0)
                {
                    this.radius = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
        public Circle() : this(0, 0, 1, 1) { }
        public Circle(double centerOfGravitX, double centerOfGravitY, int circleID, double rad) : base(centerOfGravitX, centerOfGravitY, circleID) { this.Radius = rad; }
        public static Circle GetCircleFromConsole(int circleID)
        {
            double? x = null;
            double? y = null;
            double? r = null;
            while (true)
            {
                try
                {
                    if (x is null)
                    {
                        Console.Write("Enter the X cordinate of the circle's center: ");
                        x = double.Parse(Console.ReadLine());
                    }
                    if (y is null)
                    {
                        Console.Write("Enter the Y cordinate of the circle's center: ");
                        y = double.Parse(Console.ReadLine());
                    }
                    if (r is null)
                    {
                        Console.Write("Enter the length of radius of the circle: ");
                        r = double.Parse(Console.ReadLine());
                        return new Circle(x.Value, y.Value, circleID, r.Value);
                    }
                }
                catch (Exception e)
                {
                    if (e is ArgumentException)
                    {
                        Console.WriteLine("A positive number was expected!");
                        r = null;
                    }
                    else if (e is FormatException)
                    {
                        Console.WriteLine("A number was expected!");
                    }
                    else
                    {
                          throw new NullReferenceException();
                    }
                }
            }
        }
        public override double Perimeter() => 2 * Math.PI * Radius;
        public override double Area() => Math.PI * Math.Pow(Radius, 2);
        public override bool IntersectsWith(GeometryObject go)
        {
            if (go is Circle c)
            {
                return Math.Sqrt(Math.Pow(Math.Abs(CenterOfGravityX - c.CenterOfGravityX), 2) + Math.Pow(Math.Abs(CenterOfGravityY - c.CenterOfGravityY), 2)) <= this.Radius + c.Radius;
            }
            else if (go is Square s)
            {
                Point A = new Point(s.CenterOfGravityX - s.Side / 2, s.CenterOfGravityY - s.Side / 2);
                Point B = new Point(s.CenterOfGravityX + s.Side / 2, s.CenterOfGravityY + s.Side / 2);
                Point C = new Point(s.CenterOfGravityX + s.Side / 2, s.CenterOfGravityY + s.Side / 2);
                Point D = new Point(s.CenterOfGravityX - s.Side / 2, s.CenterOfGravityY + s.Side / 2);

                bool aPointInCircle = Math.Sqrt(Math.Pow(Math.Abs(A.X - CenterOfGravityX), 2) + Math.Pow(Math.Abs(A.Y - CenterOfGravityY), 2)) >= Radius;
                bool bPointInCircle = Math.Sqrt(Math.Pow(Math.Abs(B.X - CenterOfGravityX), 2) + Math.Pow(Math.Abs(B.Y - CenterOfGravityY), 2)) >= Radius;
                bool cPointInCircle = Math.Sqrt(Math.Pow(Math.Abs(C.X - CenterOfGravityX), 2) + Math.Pow(Math.Abs(C.Y - CenterOfGravityY), 2)) >= Radius;
                bool dPointInCircle = Math.Sqrt(Math.Pow(Math.Abs(D.X - CenterOfGravityX), 2) + Math.Pow(Math.Abs(D.Y - CenterOfGravityY), 2)) >= Radius;

                bool aLineInCircle = A.Y < CenterOfGravityY + Radius && A.Y > CenterOfGravityY - Radius && Math.Abs(A.Y - CenterOfGravityY) >= Radius;
                bool bLineInCircle = B.X < CenterOfGravityX + Radius && B.X > CenterOfGravityX - Radius && Math.Abs(B.X - CenterOfGravityX) >= Radius;
                bool cLineInCircle = C.Y < CenterOfGravityY + Radius && C.Y > CenterOfGravityY - Radius && Math.Abs(C.Y - CenterOfGravityY) >= Radius;
                bool dLineInCircle = D.X < CenterOfGravityX + Radius && D.X > CenterOfGravityX - Radius && Math.Abs(D.X - CenterOfGravityY) >= Radius;

                return aPointInCircle || bPointInCircle || cPointInCircle || dPointInCircle || aLineInCircle || bLineInCircle || cLineInCircle || dLineInCircle;
            }
            else if (go is Rectangle r)
            {
                Point A = new Point(r.CenterOfGravityX - r.ASide / 2, r.CenterOfGravityY - r.BSide / 2);
                Point B = new Point(r.CenterOfGravityX + r.ASide / 2, r.CenterOfGravityY + r.BSide / 2);
                Point C = new Point(r.CenterOfGravityX + r.ASide / 2, r.CenterOfGravityY + r.BSide / 2);
                Point D = new Point(r.CenterOfGravityX - r.ASide / 2, r.CenterOfGravityY + r.BSide / 2);

                bool aPointInCircle = Math.Sqrt(Math.Pow(Math.Abs(A.X - CenterOfGravityX), 2) + Math.Pow(Math.Abs(A.Y - CenterOfGravityY), 2)) >= Radius;
                bool bPointInCircle = Math.Sqrt(Math.Pow(Math.Abs(B.X - CenterOfGravityX), 2) + Math.Pow(Math.Abs(B.Y - CenterOfGravityY), 2)) >= Radius;
                bool cPointInCircle = Math.Sqrt(Math.Pow(Math.Abs(C.X - CenterOfGravityX), 2) + Math.Pow(Math.Abs(C.Y - CenterOfGravityY), 2)) >= Radius;
                bool dPointInCircle = Math.Sqrt(Math.Pow(Math.Abs(D.X - CenterOfGravityX), 2) + Math.Pow(Math.Abs(D.Y - CenterOfGravityY), 2)) >= Radius;

                bool aLineInCircle = A.Y < CenterOfGravityY + Radius && A.Y > CenterOfGravityY - Radius && Math.Abs(A.Y - CenterOfGravityY) >= Radius;
                bool bLineInCircle = B.X < CenterOfGravityX + Radius && B.X > CenterOfGravityX - Radius && Math.Abs(B.X - CenterOfGravityX) >= Radius;
                bool cLineInCircle = C.Y < CenterOfGravityY + Radius && C.Y > CenterOfGravityY - Radius && Math.Abs(C.Y - CenterOfGravityY) >= Radius;
                bool dLineInCircle = D.X < CenterOfGravityX + Radius && D.X > CenterOfGravityX - Radius && Math.Abs(D.X - CenterOfGravityY) >= Radius;

                return aPointInCircle || bPointInCircle || cPointInCircle || dPointInCircle || aLineInCircle || bLineInCircle || cLineInCircle || dLineInCircle;
            }
            else
            {
                return false;
            }
        }
        public override string ToString() => base.ToString() + $" Radius: {Radius}";
        public override bool Equals(object other)
        {
            Circle c = other as Circle;
            return base.Equals(c) && c.Radius == Radius;
        }
        public override int GetHashCode() => CenterOfGravityX.GetHashCode() ^ CenterOfGravityY.GetHashCode() ^ Radius.GetHashCode();
        public static bool operator ==(Circle c1, Circle c2) => c1.Equals(c2);
        public static bool operator !=(Circle c1, Circle c2) => !c1.Equals(c2);
    }
    class Square : GeometryObject
    {
        private double side;
        public double Side
        {
            get => this.side;
            set
            {
                if (value > 0)
                {
                    this.side = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
        public Square() : this(0, 0, 1, 1) { }
        public Square(double centerOfGravitX, double centerOfGravitY, int squareID, double sid) : base(centerOfGravitX, centerOfGravitY, squareID) { this.Side = sid; this.ID = squareID; }
        public static Square GetSquareFromConsole(int squareID)
        {
            double? x = null;
            double? y = null;
            double? a = null;
            while (true)
            {
                try
                {
                    if (x is null)
                    {
                        Console.Write("Enter the X cordinate of the square's center: ");
                        x = double.Parse(Console.ReadLine());
                    }
                    if (y is null)
                    {
                        Console.Write("Enter the Y cordinate of the square's center: ");
                        y = double.Parse(Console.ReadLine());
                    }
                    if (a is null)
                    {
                        Console.Write("Enter length of the square's side: ");
                        a = double.Parse(Console.ReadLine());
                        return new Square(x.Value, y.Value, squareID, a.Value);
                    }
                }
                catch (Exception e)
                {
                    if (e is ArgumentException)
                    {
                        Console.WriteLine("A positive number was expected!");
                        a = null;
                    }
                    else if (e is FormatException)
                    {
                        Console.WriteLine("A number was expected!");
                    }
                    else
                    {
                        throw new NullReferenceException();
                    }
                }
            }
        }
        public override double Perimeter() => 4 * Side;
        public override double Area() => Math.Pow(Side, 2);
        public override bool IntersectsWith(GeometryObject go)
        {
            if (go is Square s)
            {
                double a1 = CenterOfGravityX - Side / 2;
                double a2 = CenterOfGravityY + Side / 2;
                double a3 = CenterOfGravityX + Side / 2;
                double a4 = CenterOfGravityY - Side / 2;

                double b1 = s.CenterOfGravityX - s.Side / 2;
                double b2 = s.CenterOfGravityY + s.Side / 2;
                double b3 = s.CenterOfGravityX + s.Side / 2;
                double b4 = s.CenterOfGravityY - s.Side / 2;

                return (a1 < b3 && a1 > b1) || (a2 < b2 && a2 > b4) || (a3 < b3 && a3 > b1) || (a4 < b4 && a4 > b2);
            }
            else if (go is Circle c)
            {
                Point A = new Point(CenterOfGravityX - Side / 2, CenterOfGravityY - Side / 2);
                Point B = new Point(CenterOfGravityX + Side / 2, CenterOfGravityY - Side / 2);
                Point C = new Point(CenterOfGravityX + Side / 2, CenterOfGravityY + Side / 2);
                Point D = new Point(CenterOfGravityX - Side / 2, CenterOfGravityY + Side / 2);

                bool aPointInCircle = Math.Sqrt(Math.Pow(Math.Abs(A.X - c.CenterOfGravityX), 2) + Math.Pow(Math.Abs(A.Y - c.CenterOfGravityY), 2)) >= c.Radius;
                bool bPointInCircle = Math.Sqrt(Math.Pow(Math.Abs(B.X - c.CenterOfGravityX), 2) + Math.Pow(Math.Abs(B.Y - c.CenterOfGravityY), 2)) >= c.Radius;
                bool cPointInCircle = Math.Sqrt(Math.Pow(Math.Abs(C.X - c.CenterOfGravityX), 2) + Math.Pow(Math.Abs(C.Y - c.CenterOfGravityY), 2)) >= c.Radius;
                bool dPointInCircle = Math.Sqrt(Math.Pow(Math.Abs(D.X - c.CenterOfGravityX), 2) + Math.Pow(Math.Abs(D.Y - c.CenterOfGravityY), 2)) >= c.Radius;

                bool aLineInCircle = A.Y < c.CenterOfGravityY + c.Radius && A.Y > c.CenterOfGravityY - c.Radius && Math.Abs(A.Y - c.CenterOfGravityY) >= c.Radius;
                bool bLineInCircle = B.X < c.CenterOfGravityX + c.Radius && B.X > c.CenterOfGravityX - c.Radius && Math.Abs(B.X - c.CenterOfGravityX) >= c.Radius;
                bool cLineInCircle = C.Y < c.CenterOfGravityY + c.Radius && C.Y > c.CenterOfGravityY - c.Radius && Math.Abs(C.Y - c.CenterOfGravityY) >= c.Radius;
                bool dLineInCircle = D.X < c.CenterOfGravityX + c.Radius && D.X > c.CenterOfGravityX - c.Radius && Math.Abs(D.X - c.CenterOfGravityX) >= c.Radius;

                return aPointInCircle || bPointInCircle || cPointInCircle || dPointInCircle || aLineInCircle || bLineInCircle || cLineInCircle || dLineInCircle;
            }
            else if (go is Rectangle r)
            {
                double a1 = CenterOfGravityX - Side / 2;
                double a2 = CenterOfGravityY + Side / 2;
                double a3 = CenterOfGravityX + Side / 2;
                double a4 = CenterOfGravityY - Side / 2;

                double b1 = r.CenterOfGravityX - r.ASide / 2;
                double b2 = r.CenterOfGravityY + r.BSide / 2;
                double b3 = r.CenterOfGravityX + r.ASide / 2;
                double b4 = r.CenterOfGravityY - r.BSide / 2;

                return (a1 < b3 && a1 > b1) || (a2 < b2 && a2 > b4) || (a3 < b3 && a3 > b1) || (a4 < b4 && a4 > b2);
            }
            else
            {
                return false;
            }
        }
        public override string ToString() => base.ToString() + $" Side: {this.Side}";
        public override bool Equals(object other)
        {
            Square s = other as Square;
            return base.Equals(s) && s.Side == Side;
        }
        public override int GetHashCode() => CenterOfGravityX.GetHashCode() ^ CenterOfGravityY.GetHashCode() ^ Side.GetHashCode();
        public static bool operator ==(Square s1, Square s2) => s1.Equals(s2);
        public static bool operator !=(Square s1, Square s2) => !s1.Equals(s2);
    }
    class Point
    {
        private double x;
        public double X
        {
            get => this.x;
            set { this.x = value; }
        }
        private double y;
        public double Y
        {
            get => this.y;
            set { this.y = value; }
        }

        public Point() : this(0, 0) { }
        public Point(double xValue, double yValue)
        {
            this.X = xValue;
            this.Y = yValue;
        }
    }
    class Rectangle : GeometryObject
    {
        private double aSide;
        public double ASide
        {
            get => this.aSide;
            set
            {
                if (value > 0)
                {
                    this.aSide = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
        private double bSide;
        public double BSide
        {
            get => this.bSide;
            set
            {
                if (value > 0)
                {
                    this.bSide = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
        public Rectangle() : this(0, 0, 1, 1, 2) { }
        public Rectangle(double centerOfGravitX, double centerOfGravitY, int rectangleID, double aSid, double bSid) : base(centerOfGravitX, centerOfGravitY, rectangleID)
        {
            this.ASide = aSid;
            this.BSide = bSid;
            this.ID = rectangleID;
        }
        public static Rectangle GetRectangleFromConsole(int rectangleID)
        {
            double? x = null;
            double? y = null;
            double? a = null;
            double? b = null;
            while (true)
            {
                try
                {
                    if (x is null)
                    {
                        Console.Write("Enter the X cordinate of the rectangle's center: ");
                        x = double.Parse(Console.ReadLine());
                    }
                    if (y is null)
                    {
                        Console.Write("Enter the Y cordinate of the rectangle's center: ");
                        y = double.Parse(Console.ReadLine());
                    }
                    if (a is null)
                    {
                        Console.Write("Enter length of the rectangle's a side: ");
                        a = double.Parse(Console.ReadLine());
                    }
                    if (b is null && a > 0)
                    {
                        Console.Write("Enter length of the rectangle's b side: ");
                        b = double.Parse(Console.ReadLine());
                    }
                    return new Rectangle(x.Value, y.Value, rectangleID, a.Value, b.Value);
                }
                catch (Exception e)
                {
                    if (e is ArgumentException)
                    {
                        Console.WriteLine("A positive number was expected!");
                        if (a <= 0)
                        {
                            a = null;
                        }
                        if (b <= 0)
                        {
                            b = null;
                        }
                    }
                    else if (e is InvalidOperationException)
                    {
                        Console.WriteLine("A positive number was expected!");
                        a = null;
                    }
                    else if (e is FormatException)
                    {
                        Console.WriteLine("A number was expected!");
                    }
                    else
                    {
                        Console.WriteLine("Unknown error!");
                    }
                }
            }
        }
        public override double Perimeter() => 2 * ASide + 2 * BSide;
        public override double Area() => ASide * BSide;
        public override bool IntersectsWith(GeometryObject go)
        {
            if (go is Square s)
            {
                double a1 = CenterOfGravityX - BSide / 2;
                double a2 = CenterOfGravityY + ASide / 2;
                double a3 = CenterOfGravityX + BSide / 2;
                double a4 = CenterOfGravityY - ASide / 2;

                double b1 = s.CenterOfGravityX - s.Side / 2;
                double b2 = s.CenterOfGravityY + s.Side / 2;
                double b3 = s.CenterOfGravityX + s.Side / 2;
                double b4 = s.CenterOfGravityY - s.Side / 2;

                return (a1 < b3 && a1 > b1) || (a2 < b2 && a2 > b4) || (a3 < b3 && a3 > b1) || (a4 < b4 && a4 > b2);
            }
            else if (go is Circle c)
            {
                Point A = new Point(CenterOfGravityX - ASide / 2, CenterOfGravityY - BSide / 2);
                Point B = new Point(CenterOfGravityX + ASide / 2, CenterOfGravityY - BSide / 2);
                Point C = new Point(CenterOfGravityX + ASide / 2, CenterOfGravityY + BSide / 2);
                Point D = new Point(CenterOfGravityX - ASide / 2, CenterOfGravityY + BSide / 2);

                bool aPointInCircle = Math.Sqrt(Math.Pow(Math.Abs(A.X - c.CenterOfGravityX), 2) + Math.Pow(Math.Abs(A.Y - c.CenterOfGravityY), 2)) >= c.Radius;
                bool bPointInCircle = Math.Sqrt(Math.Pow(Math.Abs(B.X - c.CenterOfGravityX), 2) + Math.Pow(Math.Abs(B.Y - c.CenterOfGravityY), 2)) >= c.Radius;
                bool cPointInCircle = Math.Sqrt(Math.Pow(Math.Abs(C.X - c.CenterOfGravityX), 2) + Math.Pow(Math.Abs(C.Y - c.CenterOfGravityY), 2)) >= c.Radius;
                bool dPointInCircle = Math.Sqrt(Math.Pow(Math.Abs(D.X - c.CenterOfGravityX), 2) + Math.Pow(Math.Abs(D.Y - c.CenterOfGravityY), 2)) >= c.Radius;

                bool aLineInCircle = A.Y < c.CenterOfGravityY + c.Radius && A.Y > c.CenterOfGravityY - c.Radius && Math.Abs(A.Y - c.CenterOfGravityY) >= c.Radius;
                bool bLineInCircle = B.X < c.CenterOfGravityX + c.Radius && B.X > c.CenterOfGravityX - c.Radius && Math.Abs(B.X - c.CenterOfGravityX) >= c.Radius;
                bool cLineInCircle = C.Y < c.CenterOfGravityY + c.Radius && C.Y > c.CenterOfGravityY - c.Radius && Math.Abs(C.Y - c.CenterOfGravityY) >= c.Radius;
                bool dLineInCircle = D.X < c.CenterOfGravityX + c.Radius && D.X > c.CenterOfGravityX - c.Radius && Math.Abs(D.X - c.CenterOfGravityX) >= c.Radius;

                return aPointInCircle || bPointInCircle || cPointInCircle || dPointInCircle || aLineInCircle || bLineInCircle || cLineInCircle || dLineInCircle;
            }
            else if (go is Rectangle r)
            {
                double a1 = CenterOfGravityX - ASide / 2;
                double a2 = CenterOfGravityY + BSide / 2;
                double a3 = CenterOfGravityX + ASide / 2;
                double a4 = CenterOfGravityY - BSide / 2;

                double b1 = r.CenterOfGravityX - r.ASide / 2;
                double b2 = r.CenterOfGravityY + r.BSide / 2;
                double b3 = r.CenterOfGravityX + r.ASide / 2;
                double b4 = r.CenterOfGravityY - r.BSide / 2;

                return (a1 < b3 && a1 > b1) || (a2 < b2 && a2 > b4) || (a3 < b3 && a3 > b1) || (a4 < b4 && a4 > b2);
            }
            else
            {
                return false;
            }
        }
        public override string ToString() => base.ToString() + $" A: {this.ASide} B: {this.BSide}";
        public override bool Equals(object other)
        {
            Rectangle r = other as Rectangle;
            return base.Equals(r) && r.ASide == ASide && r.BSide == BSide;
        }
        public override int GetHashCode() => CenterOfGravityX.GetHashCode() ^ CenterOfGravityY.GetHashCode() ^ ASide.GetHashCode() ^ BSide.GetHashCode();
        public static bool operator ==(Rectangle r1, Rectangle r2) => r1.Equals(r2);
        public static bool operator !=(Rectangle r1, Rectangle r2) => !r1.Equals(r2);
    }
}