using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
    class GeometryObject
    {
        private double centerOfGravityX;
        public double CenterOfGravityX { get => this.centerOfGravityX; set => this.centerOfGravityX = value; }
        private double centerOfGravityY;
        public double CenterOfGravityY { get => this.centerOfGravityY; set => this.centerOfGravityY = value; }
        public GeometryObject() : this(0, 0) { }
        public GeometryObject(double centerGravityX, double centerGravityY)
        {
            this.CenterOfGravityX = centerGravityX;
            this.CenterOfGravityY = centerGravityY;
        }
        public virtual double Perimeter() => 0;
        public virtual double Area() => 0;
        public virtual bool IntersectsWith(GeometryObject go) => false;
        public override string ToString() => $"X: {this.CenterOfGravityX} Y: {this.CenterOfGravityY}";
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
        public Circle() : this(0, 0, 1) { }
        public Circle(double centerOfGravitX, double centerOfGravitY, double rad) : base(centerOfGravitX, centerOfGravitY) { this.Radius = rad; }
        public static Circle GetCircleFromConsole()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter the X cordinate of the circle center: ");
                    double x = double.Parse(Console.ReadLine());
                    Console.Write("Enter the Y cordinate of the circle center: ");
                    double y = double.Parse(Console.ReadLine());
                    Console.Write("Enter the length of radius of the circle: ");
                    double r = double.Parse(Console.ReadLine());
                    return new Circle(x, y, r);
                }
                catch (Exception) { }
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
        public Square() : this(0, 0, 1) { }
        public Square(double centerOfGravitX, double centerOfGravitY, double sid) : base(centerOfGravitX, centerOfGravitY) { this.Side = sid; }
        public static Square GetSquareFromConsole()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter the X cordinate of the square center: ");
                    double x = double.Parse(Console.ReadLine());
                    Console.Write("Enter the Y cordinate of the square center: ");
                    double y = double.Parse(Console.ReadLine());
                    Console.Write("Enter the length of side of the square: ");
                    double s = double.Parse(Console.ReadLine());
                    return new Square(x, y, s);
                }
                catch (Exception) { }
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
        public Rectangle() : this(0, 0, 1, 2) { }
        public Rectangle(double centerOfGravitX, double centerOfGravitY, double aSid, double bSid) : base(centerOfGravitX, centerOfGravitY)
        {
            this.ASide = aSid;
            this.BSide = bSid;
        }
        public static Rectangle GetRectangleFromConsole()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter the X cordinate of the rectangle center: ");
                    double x = double.Parse(Console.ReadLine());
                    Console.Write("Enter the Y cordinate of the rectangle center: ");
                    double y = double.Parse(Console.ReadLine());
                    Console.Write("Enter the length of the a side of the rectangle: ");
                    double a = double.Parse(Console.ReadLine());
                    Console.Write("Enter the length of the b side of the rectangle: ");
                    double b = double.Parse(Console.ReadLine());
                    return new Rectangle(x, y, a, b);
                }
                catch (Exception) { }
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
    }
}
