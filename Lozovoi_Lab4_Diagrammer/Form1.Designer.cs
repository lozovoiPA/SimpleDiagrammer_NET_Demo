namespace Lozovoi_Lab4_Diagrammer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            contextMenuStrip1 = new ContextMenuStrip(components);
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem10 = new ToolStripMenuItem();
            contextMenuStrip2 = new ContextMenuStrip(components);
            toolStripMenuItem4 = new ToolStripMenuItem();
            toolStripMenuItem6 = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            toolStripMenuItem5 = new ToolStripMenuItem();
            toolStripMenuItem7 = new ToolStripMenuItem();
            toolStripMenuItem8 = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripMenuItem9 = new ToolStripMenuItem();
            openFileDialog1 = new OpenFileDialog();
            contextMenuStrip1.SuspendLayout();
            contextMenuStrip2.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1 });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(127, 26);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem10 });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(126, 22);
            toolStripMenuItem1.Text = "Создать...";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(202, 22);
            toolStripMenuItem2.Text = "Множество сущностей";
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(202, 22);
            toolStripMenuItem3.Text = "Множество связей";
            toolStripMenuItem3.Click += toolStripMenuItem3_Click;
            // 
            // toolStripMenuItem10
            // 
            toolStripMenuItem10.Name = "toolStripMenuItem10";
            toolStripMenuItem10.Size = new Size(202, 22);
            toolStripMenuItem10.Text = "Подпись";
            toolStripMenuItem10.Click += toolStripMenuItem10_Click;
            // 
            // contextMenuStrip2
            // 
            contextMenuStrip2.Items.AddRange(new ToolStripItem[] { toolStripMenuItem4, toolStripMenuItem6 });
            contextMenuStrip2.Name = "contextMenuStrip2";
            contextMenuStrip2.Size = new Size(168, 48);
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(167, 22);
            toolStripMenuItem4.Text = "Добавить связь...";
            toolStripMenuItem4.Click += toolStripMenuItem4_Click;
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new Size(167, 22);
            toolStripMenuItem6.Text = "Удалить";
            toolStripMenuItem6.Click += toolStripMenuItem6_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.GripStyle = ToolStripGripStyle.Visible;
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem5 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem7, toolStripMenuItem8, toolStripSeparator1, toolStripMenuItem9 });
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(48, 20);
            toolStripMenuItem5.Text = "Файл";
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new Size(185, 22);
            toolStripMenuItem7.Text = "Создать диаграмму";
            toolStripMenuItem7.Click += toolStripMenuItem7_Click;
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            toolStripMenuItem8.Size = new Size(185, 22);
            toolStripMenuItem8.Text = "Открыть диаграмму";
            toolStripMenuItem8.Click += toolStripMenuItem8_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(182, 6);
            // 
            // toolStripMenuItem9
            // 
            toolStripMenuItem9.Enabled = false;
            toolStripMenuItem9.Name = "toolStripMenuItem9";
            toolStripMenuItem9.Size = new Size(185, 22);
            toolStripMenuItem9.Text = "Сохранить";
            toolStripMenuItem9.Click += toolStripMenuItem9_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            MouseDoubleClick += Form1_MouseDoubleClick;
            MouseDown += Form1_MouseDown;
            MouseMove += Form1_MouseMove;
            MouseUp += Form1_MouseUp;
            contextMenuStrip1.ResumeLayout(false);
            contextMenuStrip2.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem6;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem7;
        private ToolStripMenuItem toolStripMenuItem8;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem toolStripMenuItem9;
        private ToolStripMenuItem toolStripMenuItem10;
        private OpenFileDialog openFileDialog1;
    }
}
