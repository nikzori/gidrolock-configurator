namespace Gidrolock_Modbus_Scanner
{
    partial class Datasheet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Datasheet));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelBattery = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonPoll = new System.Windows.Forms.Button();
            this.buttonSetID = new System.Windows.Forms.Button();
            this.labelFirmware = new System.Windows.Forms.Label();
            this.labelModel = new System.Windows.Forms.Label();
            this.nudModbusID = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelCleaning = new System.Windows.Forms.Label();
            this.buttonCleaning = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.labelAlarm = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonAlarm = new System.Windows.Forms.Button();
            this.buttonValve = new System.Windows.Forms.Button();
            this.labelValve = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.sensorPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cBoxSpeed = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudModbusID)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cBoxSpeed);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.labelBattery);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.buttonPoll);
            this.groupBox1.Controls.Add(this.buttonSetID);
            this.groupBox1.Controls.Add(this.labelFirmware);
            this.groupBox1.Controls.Add(this.labelModel);
            this.groupBox1.Controls.Add(this.nudModbusID);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(291, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(234, 126);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Инфо";
            // 
            // labelBattery
            // 
            this.labelBattery.AutoSize = true;
            this.labelBattery.Location = new System.Drawing.Point(59, 48);
            this.labelBattery.Name = "labelBattery";
            this.labelBattery.Size = new System.Drawing.Size(15, 13);
            this.labelBattery.TabIndex = 9;
            this.labelBattery.Text = "%";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Батарея:";
            // 
            // buttonPoll
            // 
            this.buttonPoll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPoll.Location = new System.Drawing.Point(153, 43);
            this.buttonPoll.Name = "buttonPoll";
            this.buttonPoll.Size = new System.Drawing.Size(75, 23);
            this.buttonPoll.TabIndex = 7;
            this.buttonPoll.Text = "Опрос";
            this.buttonPoll.UseVisualStyleBackColor = true;
            this.buttonPoll.Click += new System.EventHandler(this.buttonPoll_Click);
            // 
            // buttonSetID
            // 
            this.buttonSetID.Location = new System.Drawing.Point(153, 68);
            this.buttonSetID.Name = "buttonSetID";
            this.buttonSetID.Size = new System.Drawing.Size(75, 23);
            this.buttonSetID.TabIndex = 6;
            this.buttonSetID.Text = "Изменить";
            this.buttonSetID.UseVisualStyleBackColor = true;
            this.buttonSetID.Click += new System.EventHandler(this.buttonSetID_Click);
            // 
            // labelFirmware
            // 
            this.labelFirmware.AutoSize = true;
            this.labelFirmware.Location = new System.Drawing.Point(74, 31);
            this.labelFirmware.Name = "labelFirmware";
            this.labelFirmware.Size = new System.Drawing.Size(37, 13);
            this.labelFirmware.TabIndex = 5;
            this.labelFirmware.Text = "v1.0.4";
            // 
            // labelModel
            // 
            this.labelModel.AutoSize = true;
            this.labelModel.Location = new System.Drawing.Point(61, 16);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(46, 13);
            this.labelModel.TabIndex = 4;
            this.labelModel.Text = "Модель";
            // 
            // nudModbusID
            // 
            this.nudModbusID.Location = new System.Drawing.Point(74, 71);
            this.nudModbusID.Name = "nudModbusID";
            this.nudModbusID.Size = new System.Drawing.Size(49, 20);
            this.nudModbusID.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Modbus ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Прошивка:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Модель:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelCleaning);
            this.groupBox2.Controls.Add(this.buttonCleaning);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.labelAlarm);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.buttonAlarm);
            this.groupBox2.Controls.Add(this.buttonValve);
            this.groupBox2.Controls.Add(this.labelValve);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(13, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(272, 126);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Общее";
            // 
            // labelCleaning
            // 
            this.labelCleaning.AutoSize = true;
            this.labelCleaning.Location = new System.Drawing.Point(52, 77);
            this.labelCleaning.Name = "labelCleaning";
            this.labelCleaning.Size = new System.Drawing.Size(33, 13);
            this.labelCleaning.TabIndex = 9;
            this.labelCleaning.Text = "выкл";
            // 
            // buttonCleaning
            // 
            this.buttonCleaning.Location = new System.Drawing.Point(125, 72);
            this.buttonCleaning.Name = "buttonCleaning";
            this.buttonCleaning.Size = new System.Drawing.Size(140, 23);
            this.buttonCleaning.TabIndex = 8;
            this.buttonCleaning.Text = "Включить";
            this.buttonCleaning.UseVisualStyleBackColor = true;
            this.buttonCleaning.Click += new System.EventHandler(this.buttonCleaning_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Уборка:";
            // 
            // labelAlarm
            // 
            this.labelAlarm.AutoSize = true;
            this.labelAlarm.Location = new System.Drawing.Point(61, 48);
            this.labelAlarm.Name = "labelAlarm";
            this.labelAlarm.Size = new System.Drawing.Size(24, 13);
            this.labelAlarm.TabIndex = 6;
            this.labelAlarm.Text = "нет";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Протечка:";
            // 
            // buttonAlarm
            // 
            this.buttonAlarm.Location = new System.Drawing.Point(125, 43);
            this.buttonAlarm.Name = "buttonAlarm";
            this.buttonAlarm.Size = new System.Drawing.Size(140, 23);
            this.buttonAlarm.TabIndex = 3;
            this.buttonAlarm.Text = "Авария";
            this.buttonAlarm.UseVisualStyleBackColor = true;
            this.buttonAlarm.Click += new System.EventHandler(this.buttonAlarm_Click);
            // 
            // buttonValve
            // 
            this.buttonValve.Location = new System.Drawing.Point(125, 14);
            this.buttonValve.Name = "buttonValve";
            this.buttonValve.Size = new System.Drawing.Size(140, 23);
            this.buttonValve.TabIndex = 2;
            this.buttonValve.Text = "Закрыть";
            this.buttonValve.UseVisualStyleBackColor = true;
            this.buttonValve.Click += new System.EventHandler(this.buttonValve_Click);
            // 
            // labelValve
            // 
            this.labelValve.AutoSize = true;
            this.labelValve.Location = new System.Drawing.Point(39, 19);
            this.labelValve.Name = "labelValve";
            this.labelValve.Size = new System.Drawing.Size(45, 13);
            this.labelValve.TabIndex = 1;
            this.labelValve.Text = "Открыт";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Кран:";
            // 
            // sensorPanel
            // 
            this.sensorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sensorPanel.AutoScroll = true;
            this.sensorPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.sensorPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.sensorPanel.Location = new System.Drawing.Point(13, 168);
            this.sensorPanel.Name = "sensorPanel";
            this.sensorPanel.Size = new System.Drawing.Size(512, 248);
            this.sensorPanel.TabIndex = 4;
            this.sensorPanel.WrapContents = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 152);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Датчики";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(153, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Изменить";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Скорость:";
            // 
            // cBoxSpeed
            // 
            this.cBoxSpeed.FormattingEnabled = true;
            this.cBoxSpeed.Location = new System.Drawing.Point(74, 97);
            this.cBoxSpeed.Name = "cBoxSpeed";
            this.cBoxSpeed.Size = new System.Drawing.Size(73, 21);
            this.cBoxSpeed.TabIndex = 13;
            this.cBoxSpeed.Text = "9600";
            // 
            // Datasheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 428);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.sensorPanel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Datasheet";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Config";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudModbusID)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonSetID;
        private System.Windows.Forms.Label labelFirmware;
        private System.Windows.Forms.Label labelModel;
        private System.Windows.Forms.NumericUpDown nudModbusID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonPoll;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelValve;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonValve;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonAlarm;
        private System.Windows.Forms.Label labelCleaning;
        private System.Windows.Forms.Button buttonCleaning;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelAlarm;
        private System.Windows.Forms.FlowLayoutPanel sensorPanel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelBattery;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cBoxSpeed;
    }
}