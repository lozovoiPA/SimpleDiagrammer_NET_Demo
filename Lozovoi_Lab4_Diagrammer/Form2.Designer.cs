namespace Lozovoi_Lab4_Diagrammer
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            folderBrowserDialog1 = new FolderBrowserDialog();
            label1 = new Label();
            label2 = new Label();
            schemaNameField = new TextBox();
            schemaPathField = new TextBox();
            folderDialogButton = new Button();
            createButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 23);
            label1.Name = "label1";
            label1.Size = new Size(126, 15);
            label1.TabIndex = 0;
            label1.Text = "Название диаграммы";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(25, 102);
            label2.Name = "label2";
            label2.Size = new Size(106, 15);
            label2.TabIndex = 1;
            label2.Text = "Путь к диаграмме";
            // 
            // schemaNameField
            // 
            schemaNameField.Location = new Point(25, 41);
            schemaNameField.Name = "schemaNameField";
            schemaNameField.Size = new Size(383, 23);
            schemaNameField.TabIndex = 2;
            // 
            // schemaPathField
            // 
            schemaPathField.Location = new Point(25, 120);
            schemaPathField.Name = "schemaPathField";
            schemaPathField.Size = new Size(383, 23);
            schemaPathField.TabIndex = 3;
            // 
            // folderDialogButton
            // 
            folderDialogButton.Location = new Point(411, 119);
            folderDialogButton.Name = "folderDialogButton";
            folderDialogButton.Size = new Size(75, 24);
            folderDialogButton.TabIndex = 4;
            folderDialogButton.Text = "...";
            folderDialogButton.UseVisualStyleBackColor = true;
            folderDialogButton.Click += folderDialogButton_Click;
            // 
            // createButton
            // 
            createButton.Location = new Point(382, 196);
            createButton.Name = "createButton";
            createButton.Size = new Size(75, 23);
            createButton.TabIndex = 5;
            createButton.Text = "Create";
            createButton.UseVisualStyleBackColor = true;
            createButton.Click += createButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(291, 196);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 6;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(498, 232);
            Controls.Add(cancelButton);
            Controls.Add(createButton);
            Controls.Add(folderDialogButton);
            Controls.Add(schemaPathField);
            Controls.Add(schemaNameField);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FolderBrowserDialog folderBrowserDialog1;
        private Label label1;
        private Label label2;
        private TextBox schemaNameField;
        private TextBox schemaPathField;
        private Button folderDialogButton;
        private Button createButton;
        private Button cancelButton;
    }
}