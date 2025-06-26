using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lozovoi_Lab4_Diagrammer
{
    public class SimpleEdge : CustomEdge
    {
        public Point margin1;
        public Point margin2;

        public SimpleEdge(CustomPrimitive pr1, CustomPrimitive pr2, Point margin1, Point margin2)
        {
            this.pr1 = pr1;
            this.pr2 = pr2;

            this.margin1 = margin1;
            this.margin2 = margin2;
        }

        public override void Draw(Graphics g)
        {
            SolidBrush brush1 = new SolidBrush(Color.Black);
            X = pr1.X + margin1.X;
            Y = pr1.Y + margin1.Y;
            width = pr2.X + margin2.X;
            height = pr2.Y + margin2.Y;
            g.DrawLine(new Pen(brush1), X, Y, width, height);
            width -= X;
            height -= Y;
            //width = Math.Abs(width);
            //height = Math.Abs(height);

            brush1.Dispose();
        }

        public override string GetCustomType()
        {
            return "edge";
        }
    }
}
