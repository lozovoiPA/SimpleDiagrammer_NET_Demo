using Microsoft.VisualBasic.Devices;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Lozovoi_Lab4_Diagrammer
{
    public partial class Form1 : Form
    {
        Diagram? diagram;

        TextBox? active_tb;
        CustomPrimitive? active_pr;

        Rectangle active_at = Rectangle.Empty;
        bool edge_draw = false;
        bool hold = false;
        public Form1()
        {
            InitializeComponent();

            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (diagram != null)
            {
                if (active_tb != null)
                {
                    EndRenameActivity();
                }
                hold = false;
                CustomPrimitive? pr = diagram.FindAt(e.X, e.Y);
                Console.WriteLine("\n" + e.X + " " + e.Y);
                if (e.Button == MouseButtons.Right)
                {
                    if (pr == null)
                    {
                        contextMenuStrip1.Show(this, e.X, e.Y);
                    }
                    else
                    {
                        active_pr = pr;
                        contextMenuStrip2.Show(this, e.X, e.Y);
                    }
                }
                else if (e.Button == MouseButtons.Left)
                {
                    if (pr != null)
                    {
                        if (!edge_draw) // we are not currently drawing a link so we can start holding
                        {
                            hold = true;
                            active_pr = pr;
                            active_at = new Rectangle(pr.X, pr.Y, e.X - pr.X, e.Y - pr.Y);
                        }
                        else
                        {
                            diagram.AddEdge(active_pr, pr, new Point(active_at.X, active_at.Y), new Point(active_at.Width, active_at.Height));
                            edge_draw = false;
                            this.Invalidate();
                        }
                    }
                    else if (edge_draw)
                    {
                        edge_draw = false;
                        this.Invalidate();
                    }
                }
            }
        }
        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (diagram != null)
            {
                Point point = new Point(e.X, e.Y);
                Console.WriteLine(point);
                CustomPrimitive? pr = diagram.FindAt(point.X, point.Y);
                if (pr != null)
                {
                    active_pr = pr;
                    RenamePrimitive(pr);
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (diagram != null)
            {
                if (hold)
                {
                    active_pr.X = e.X - active_at.Width;
                    active_pr.Y = e.Y - active_at.Height;

                    this.Invalidate();
                }
                else if (edge_draw)
                {
                    active_at.Width = e.X;
                    active_at.Height = e.Y;

                    this.Invalidate();
                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (diagram != null)
            {
                hold = false;
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Point point = PointToClient(new Point(contextMenuStrip1.Bounds.X, contextMenuStrip1.Bounds.Y));


            CustomPrimitive pr = diagram.AddRectangle(point.X, point.Y);
            active_pr = pr;
            this.Invalidate();
            this.RenamePrimitive(pr);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (diagram != null)
            {
                if (edge_draw) // links are drawn under the figures
                {
                    Console.WriteLine(active_at);
                    e.Graphics.DrawLine(new Pen(Color.Black), active_at.X, active_at.Y, active_at.Width, active_at.Height);
                }
                diagram.Draw(e.Graphics);
            }
        }

        private void RenamePrimitive(CustomPrimitive pr)
        {
            hold = false;
            TextBox tb = new TextBox();
            active_tb = tb;
            tb.Left = (int)(pr.X + pr.width * 0.5);
            tb.Top = (int)(pr.Y + pr.height * 0.5);
            tb.Width = 100;
            tb.Height = 30;
            tb.Text = pr.label;

            tb.KeyDown += new KeyEventHandler(this.EndRename);
            this.Controls.Add(tb);
            tb.Focus();

            this.Invalidate();
        }

        private void EndRenameActivity()
        {
            active_pr.label = active_tb.Text;
            active_tb.Dispose();
            active_tb = null;
            active_pr = null;
            this.Invalidate();
        }

        private void EndRename(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EndRenameActivity();
            }
        }
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            edge_draw = true;
            Point point = PointToClient(new Point(contextMenuStrip2.Bounds.X, contextMenuStrip2.Bounds.Y));
            Console.WriteLine(point);
            active_at = new Rectangle(point.X, point.Y, point.X, point.Y);
        }
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Point point = PointToClient(new Point(contextMenuStrip2.Bounds.X, contextMenuStrip2.Bounds.Y));
            CustomPrimitive? pr = diagram.FindAt(point.X, point.Y);
            if (pr != null)
            {
                diagram.Remove(pr);
                this.Invalidate();
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Point point = PointToClient(new Point(contextMenuStrip1.Bounds.X, contextMenuStrip1.Bounds.Y));


            CustomPrimitive pr = diagram.AddRhombus(point.X, point.Y);
            active_pr = pr;
            this.Invalidate();
            this.RenamePrimitive(pr);
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            Point point = PointToClient(new Point(contextMenuStrip1.Bounds.X, contextMenuStrip1.Bounds.Y));


            CustomPrimitive pr = diagram.AddLabel(point.X, point.Y);
            active_pr = pr;
            this.Invalidate();
            this.RenamePrimitive(pr);
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            DialogResult dr = form2.ShowDialog();
            if (dr == DialogResult.OK)
            {
                // create diagram
                string diagram_path = Path.Combine(form2.filePath, form2.fileName + ".dgr");
                diagram = DiagramDbMapper.Instance().CreateEmptyDiagram(diagram_path);

                toolStripMenuItem9.Enabled = true;
                this.Invalidate();
            }
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "dgr files (*.dgr)|*.dgr";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string diagram_path = openFileDialog1.FileName;
                diagram = DiagramDbMapper.Instance().GetDiagramFromFile(diagram_path);

                toolStripMenuItem9.Enabled = true;
                this.Invalidate();
            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            DiagramDbMapper.Instance().SaveDiagram(diagram);
        }
    }
}
