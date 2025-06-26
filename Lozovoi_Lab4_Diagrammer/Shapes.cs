using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lozovoi_Lab4_Diagrammer
{
    public class CustomRectangle : CustomShape
    {
        public CustomRectangle(int _x, int _y)
        {
            X = _x;
            Y = _y;

            width = 100;
            height = 30;
        }

        public CustomRectangle(int _x, int _y, int _width, int _height)
        {
            X = _x;
            Y = _y;

            width = _width;
            height = _height;
        }

        public override void Draw(Graphics g)
        {
            SolidBrush brush1 = new SolidBrush(Color.Black);
            SolidBrush brush = new SolidBrush(Color.White);
            g.DrawRectangle(new Pen(brush1), X, Y, width, height);
            g.FillRectangle(brush, X + 1, Y + 1, width - 2, height - 2);

            Font font = new Font(FontFamily.GenericSansSerif, 10);
            SizeF size = g.MeasureString(label, font);
            g.DrawString(label, new Font(FontFamily.GenericSansSerif, 10), brush1, X + width / 2 - size.Width / 2, Y + height / 2 - size.Height / 2);

            brush1.Dispose();
            brush.Dispose();
        }

        public override string GetCustomType()
        {
            return "rect";
        }
    }
    public class CustomRhombus : CustomShape
    {
        public CustomRhombus(int _x, int _y)
        {
            X = _x;
            Y = _y;

            width = 100;
            height = 50;
        }
        public CustomRhombus(int _x, int _y, int _width, int _height)
        {
            X = _x;
            Y = _y;

            width = _width;
            height = _height;
        }

        public override void Draw(Graphics g)
        {
            SolidBrush brush1 = new SolidBrush(Color.Black);
            SolidBrush brush = new SolidBrush(Color.White);

            Point[] points =
                [
                new Point(X + width / 2, Y + 1),
                new Point(X + width - 2, Y + height / 2),
                new Point(X + width / 2, Y + height - 2),
                new Point(X + 1, Y + height / 2)
                ];
            byte[] point_types = [(byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line];
            GraphicsPath path = new GraphicsPath(points, point_types);
            Region region = new Region(path);

            g.DrawLines(new Pen(brush1), points);
            g.FillRegion(brush, region);

            Font font = new Font(FontFamily.GenericSansSerif, 10);
            SizeF size = g.MeasureString(label, font);
            g.DrawString(label, font, brush1, X + width / 2 - size.Width / 2, Y + height / 2 - size.Height / 2);


            brush1.Dispose();
            brush.Dispose();
        }

        public override string GetCustomType()
        {
            return "rhombus";
        }
    }

    public class CustomLabel : CustomShape
    {
        public CustomLabel(int _x, int _y)
        {
            X = _x;
            Y = _y;
        }
        public override void Draw(Graphics g)
        {
            SolidBrush brush1 = new SolidBrush(Color.Black);
            Font font = new Font(FontFamily.GenericSansSerif, 10);

            g.DrawString(label, font, brush1, X, Y);
            SizeF size = g.MeasureString(label, font);
            width = (int)size.Width + 2;
            height = (int)size.Height + 2;
            brush1.Dispose();
        }

        public override string GetCustomType()
        {
            return "label";
        }
    }
}
