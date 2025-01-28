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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.points = new System.Windows.Forms.NumericUpDown();
            this.pointsText = new System.Windows.Forms.TextBox();
            this.calculate = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.FlowLayoutPanel();
            this.windowResTextBox = new System.Windows.Forms.TextBox();
            this.type = new System.Windows.Forms.ComboBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mouseText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.points)).BeginInit();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // points
            // 
            resources.ApplyResources(this.points, "points");
            this.points.Name = "points";
            this.points.ValueChanged += new System.EventHandler(this.points_ValueChanged);
            // 
            // pointsText
            // 
            resources.ApplyResources(this.pointsText, "pointsText");
            this.pointsText.Name = "pointsText";
            this.pointsText.ReadOnly = true;
            // 
            // calculate
            // 
            resources.ApplyResources(this.calculate, "calculate");
            this.calculate.Name = "calculate";
            this.calculate.UseVisualStyleBackColor = true;
            this.calculate.Click += new System.EventHandler(this.calculate_Click);
            // 
            // panel
            // 
            resources.ApplyResources(this.panel, "panel");
            this.panel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel.Controls.Add(this.mouseText);
            this.panel.Controls.Add(this.windowResTextBox);
            this.panel.Controls.Add(this.calculate);
            this.panel.Controls.Add(this.pointsText);
            this.panel.Controls.Add(this.points);
            this.panel.Controls.Add(this.type);
            this.panel.Name = "panel";
            // 
            // windowResTextBox
            // 
            resources.ApplyResources(this.windowResTextBox, "windowResTextBox");
            this.windowResTextBox.Name = "windowResTextBox";
            this.windowResTextBox.ReadOnly = true;
            // 
            // type
            // 
            this.type.FormattingEnabled = true;
            this.type.Items.AddRange(new object[] {
            resources.GetString("type.Items"),
            resources.GetString("type.Items1"),
            resources.GetString("type.Items2")});
            resources.ApplyResources(this.type, "type");
            this.type.Name = "type";
            this.type.SelectedIndexChanged += new System.EventHandler(this.type_SelectedIndexChanged);
            this.type.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressed);
            // 
            // menuStrip
            // 
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            // 
            // mouseText
            // 
            resources.ApplyResources(this.mouseText, "mouseText");
            this.mouseText.Name = "mouseText";
            this.mouseText.ReadOnly = true;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.pictureBox1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.points)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown points;
        private System.Windows.Forms.TextBox pointsText;
        private System.Windows.Forms.Button calculate;
        private System.Windows.Forms.FlowLayoutPanel panel;
        private System.Windows.Forms.TextBox windowResTextBox;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ComboBox type;
        private System.Windows.Forms.TextBox mouseText;
    }
}

