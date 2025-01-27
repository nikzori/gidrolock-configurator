namespace Gidrolock_Modbus_Scanner
{
    partial class App
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(App));
            this.TextBox_Log = new System.Windows.Forms.TextBox();
            this.upDownModbusID = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cBoxPorts = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cBoxSpeed = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.upDownModbusID)).BeginInit();
            this.SuspendLayout();
            // 
            // TextBox_Log
            // 
            this.TextBox_Log.Location = new System.Drawing.Point(12, 53);
            this.TextBox_Log.Multiline = true;
            this.TextBox_Log.Name = "TextBox_Log";
            this.TextBox_Log.ReadOnly = true;
            this.TextBox_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox_Log.Size = new System.Drawing.Size(498, 160);
            this.TextBox_Log.TabIndex = 1;
            // 
            // upDownModbusID
            // 
            this.upDownModbusID.Location = new System.Drawing.Point(164, 27);
            this.upDownModbusID.Name = "upDownModbusID";
            this.upDownModbusID.Size = new System.Drawing.Size(66, 20);
            this.upDownModbusID.TabIndex = 1;
            this.upDownModbusID.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Modbus ID";
            // 
            // cBoxPorts
            // 
            this.cBoxPorts.FormattingEnabled = true;
            this.cBoxPorts.Location = new System.Drawing.Point(12, 26);
            this.cBoxPorts.Name = "cBoxPorts";
            this.cBoxPorts.Size = new System.Drawing.Size(65, 21);
            this.cBoxPorts.TabIndex = 1;
            this.cBoxPorts.Text = "COM1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Порт";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(414, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Подключиться";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ButtonConnect_Click);
            // 
            // cBoxSpeed
            // 
            this.cBoxSpeed.FormattingEnabled = true;
            this.cBoxSpeed.Location = new System.Drawing.Point(83, 26);
            this.cBoxSpeed.Name = "cBoxSpeed";
            this.cBoxSpeed.Size = new System.Drawing.Size(78, 21);
            this.cBoxSpeed.TabIndex = 7;
            this.cBoxSpeed.Text = "9600";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(80, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Скорость";
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 226);
            this.Controls.Add(this.cBoxSpeed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cBoxPorts);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.upDownModbusID);
            this.Controls.Add(this.TextBox_Log);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "App";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Gidrolock Modbus Scanner";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.App_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.upDownModbusID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TextBox_Log;
        private System.Windows.Forms.NumericUpDown upDownModbusID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cBoxPorts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cBoxSpeed;
        private System.Windows.Forms.Label label4;
    }
}

