using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lozovoi_Lab4_Diagrammer
{
    public abstract class CustomPrimitive
    {
        public int X;
        public int Y;

        public int width;
        public int height;

        public string label = string.Empty;
        public abstract void Draw(Graphics g);
        public abstract string GetCustomType();
    }

    public abstract class CustomShape : CustomPrimitive
    {

    }
    public abstract class CustomEdge : CustomPrimitive
    {
        public CustomPrimitive pr1;
        public CustomPrimitive pr2;
    }

    public class Diagram
    {
        public List<CustomShape> shapes;
        public List<CustomEdge> edges;
        public Diagram()
        {
            shapes = new List<CustomShape>();
            edges = new List<CustomEdge>();
        }

        public CustomShape AddRectangle(int x, int y)
        {
            CustomRectangle pr = new CustomRectangle(x, y);
            shapes.Add(pr);
            return pr;
        }
        public CustomShape AddRectangle(int x, int y, int width, int height)
        {
            CustomRectangle pr = new CustomRectangle(x, y, width, height);
            shapes.Add(pr);
            return pr;
        }
        public CustomShape AddRhombus(int x, int y)
        {
            CustomRhombus pr = new CustomRhombus(x, y);
            shapes.Add(pr);
            return pr;
        }
        public CustomShape AddRhombus(int x, int y, int width, int height)
        {
            CustomRhombus pr = new CustomRhombus(x, y, width, height);
            shapes.Add(pr);
            return pr;
        }
        public CustomShape AddLabel(int x, int y)
        {
            CustomLabel pr = new CustomLabel(x, y);
            shapes.Add(pr);
            return pr;
        }

        public CustomEdge AddEdge(CustomPrimitive pr1, CustomPrimitive pr2, Point p1, Point p2)
        {
            if (p1.Y > p2.Y) // because we are assigning p1 - top left, p2 - bottom right, but this link (line) may be upside down (pr1 is always above pr2)
            {
                var t = p1;
                p1 = p2;
                p2 = t;

                var t1 = pr1;
                pr1 = pr2;
                pr2 = t1;
            }
            p1.X -= pr1.X;
            p1.Y -= pr1.Y;
            p2.X -= pr2.X;
            p2.Y -= pr2.Y;
            SimpleEdge edge = new SimpleEdge(pr1, pr2, p1, p2);
            edges.Add(edge);

            return edge;
        }

        public void Draw(Graphics g)
        {
            foreach (CustomEdge lk in edges)
            {
                lk.Draw(g);
            }
            foreach (CustomShape pr in shapes)
            {
                pr.Draw(g);
            }
        }

        public CustomPrimitive? FindAt(int x, int y)
        {
            foreach (CustomShape pr in shapes)
            {
                if ((pr.X < x) && (pr.X + pr.width > x) && (pr.Y < y) && (pr.Y + pr.height > y))
                {
                    return pr;
                }
            }
            foreach (CustomEdge lk in edges)
            {
                double calc1 = ((double)(x - lk.X - 5) / (lk.width)) - ((double)(y - lk.Y + 5) / (lk.height));
                double calc2 = ((double)(x - lk.X + 5) / (lk.width)) - ((double)(y - lk.Y - 5) / (lk.height));
                if (calc1 * calc2 < 0)
                {
                    return lk;
                }
            }
            return null;
        }

        public void Remove(CustomPrimitive _pr)
        {
            CustomPrimitive? pr = shapes.Find(x => x == _pr);
            if (pr != null)
            {
                shapes.Remove((CustomShape)_pr);
                foreach(var item in edges.FindAll(x => (x.pr1 == _pr) || (x.pr2 == _pr)))
                {
                    edges.Remove(item);
                }
            }
            else
            {
                edges.Remove((CustomEdge)_pr);
            }
        }
    }
}
