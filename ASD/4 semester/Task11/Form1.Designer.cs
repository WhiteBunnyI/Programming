namespace asd
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.picture = new System.Windows.Forms.PictureBox();
            this.layout = new System.Windows.Forms.FlowLayoutPanel();
            this.pointsText = new System.Windows.Forms.TextBox();
            this.points = new System.Windows.Forms.NumericUpDown();
            this.linesText = new System.Windows.Forms.TextBox();
            this.lines = new System.Windows.Forms.NumericUpDown();
            this.button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.layout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.points)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lines)).BeginInit();
            this.SuspendLayout();
            // 
            // picture
            // 
            this.picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picture.Location = new System.Drawing.Point(0, 0);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(800, 450);
            this.picture.TabIndex = 0;
            this.picture.TabStop = false;
            this.picture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picture_MouseDown);
            this.picture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picture_MouseMove);
            this.picture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picture_MouseUp);
            // 
            // layout
            // 
            this.layout.Controls.Add(this.button);
            this.layout.Controls.Add(this.pointsText);
            this.layout.Controls.Add(this.points);
            this.layout.Controls.Add(this.linesText);
            this.layout.Controls.Add(this.lines);
            this.layout.Location = new System.Drawing.Point(588, 12);
            this.layout.Name = "layout";
            this.layout.Size = new System.Drawing.Size(200, 262);
            this.layout.TabIndex = 1;
            // 
            // pointsText
            // 
            this.pointsText.Location = new System.Drawing.Point(3, 32);
            this.pointsText.Name = "pointsText";
            this.pointsText.ReadOnly = true;
            this.pointsText.Size = new System.Drawing.Size(82, 20);
            this.pointsText.TabIndex = 1;
            this.pointsText.Text = "Кол-во точек:";
            // 
            // points
            // 
            this.points.Location = new System.Drawing.Point(91, 32);
            this.points.Name = "points";
            this.points.Size = new System.Drawing.Size(82, 20);
            this.points.TabIndex = 6;
            this.points.ValueChanged += new System.EventHandler(this.points_ValueChanged);
            // 
            // linesText
            // 
            this.linesText.Location = new System.Drawing.Point(3, 58);
            this.linesText.Name = "linesText";
            this.linesText.ReadOnly = true;
            this.linesText.Size = new System.Drawing.Size(82, 20);
            this.linesText.TabIndex = 3;
            this.linesText.Text = "Кол-во линий:";
            // 
            // lines
            // 
            this.lines.Location = new System.Drawing.Point(91, 58);
            this.lines.Name = "lines";
            this.lines.Size = new System.Drawing.Size(82, 20);
            this.lines.TabIndex = 7;
            this.lines.ValueChanged += new System.EventHandler(this.lines_ValueChanged);
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(3, 3);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(170, 23);
            this.button.TabIndex = 8;
            this.button.Text = "Расскрасить";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.layout);
            this.Controls.Add(this.picture);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.layout.ResumeLayout(false);
            this.layout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.points)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lines)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.FlowLayoutPanel layout;
        private System.Windows.Forms.TextBox pointsText;
        private System.Windows.Forms.TextBox linesText;
        private System.Windows.Forms.NumericUpDown points;
        private System.Windows.Forms.NumericUpDown lines;
        private System.Windows.Forms.Button button;
    }
}

