namespace more_15
{
    partial class game
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(game));
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.oPENToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.нАЧАТЬToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оТМЕНАToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.уРОВЕНЬToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пОДСКАЗКАToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(637, 516);
            this.panel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oPENToolStripMenuItem,
            this.нАЧАТЬToolStripMenuItem,
            this.оТМЕНАToolStripMenuItem,
            this.уРОВЕНЬToolStripMenuItem,
            this.пОДСКАЗКАToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(639, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // oPENToolStripMenuItem
            // 
            this.oPENToolStripMenuItem.Name = "oPENToolStripMenuItem";
            this.oPENToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.oPENToolStripMenuItem.Text = "OPEN";
            this.oPENToolStripMenuItem.Click += new System.EventHandler(this.oPENToolStripMenuItem_Click);
            // 
            // нАЧАТЬToolStripMenuItem
            // 
            this.нАЧАТЬToolStripMenuItem.Name = "нАЧАТЬToolStripMenuItem";
            this.нАЧАТЬToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.нАЧАТЬToolStripMenuItem.Text = "НАЧАТЬ";
            this.нАЧАТЬToolStripMenuItem.Click += new System.EventHandler(this.нАЧАТЬToolStripMenuItem_Click);
            // 
            // оТМЕНАToolStripMenuItem
            // 
            this.оТМЕНАToolStripMenuItem.Name = "оТМЕНАToolStripMenuItem";
            this.оТМЕНАToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.оТМЕНАToolStripMenuItem.Text = "ОТМЕНА";
            this.оТМЕНАToolStripMenuItem.Click += new System.EventHandler(this.оТМЕНАToolStripMenuItem_Click);
            // 
            // уРОВЕНЬToolStripMenuItem
            // 
            this.уРОВЕНЬToolStripMenuItem.Name = "уРОВЕНЬToolStripMenuItem";
            this.уРОВЕНЬToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.уРОВЕНЬToolStripMenuItem.Text = "УРОВЕНЬ";
            this.уРОВЕНЬToolStripMenuItem.Click += new System.EventHandler(this.уРОВЕНЬToolStripMenuItem_Click);
            // 
            // пОДСКАЗКАToolStripMenuItem
            // 
            this.пОДСКАЗКАToolStripMenuItem.Name = "пОДСКАЗКАToolStripMenuItem";
            this.пОДСКАЗКАToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.пОДСКАЗКАToolStripMenuItem.Text = "ПОДСКАЗКА";
            this.пОДСКАЗКАToolStripMenuItem.Click += new System.EventHandler(this.пОДСКАЗКАToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.aboutToolStripMenuItem.Text = "об игре";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 542);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(655, 581);
            this.MinimumSize = new System.Drawing.Size(418, 362);
            this.Name = "game";
            this.Text = "15 с картинками";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem oPENToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem нАЧАТЬToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оТМЕНАToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem уРОВЕНЬToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пОДСКАЗКАToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

