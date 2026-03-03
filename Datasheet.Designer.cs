partial class Datasheet : Form
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
        groupBox1 = new GroupBox();
        cBoxSpeed = new ComboBox();
        buttonSetSpeed = new Button();
        label8 = new Label();
        labelBattery = new Label();
        label6 = new Label();
        buttonPoll = new Button();
        buttonSetID = new Button();
        labelFirmware = new Label();
        labelModel = new Label();
        nudModbusID = new NumericUpDown();
        label3 = new Label();
        label2 = new Label();
        label1 = new Label();
        groupBox2 = new GroupBox();
        labelCleaning = new Label();
        buttonCleaning = new Button();
        label7 = new Label();
        labelAlarm = new Label();
        label5 = new Label();
        buttonAlarm = new Button();
        buttonValve = new Button();
        labelValve = new Label();
        label4 = new Label();
        sensorPanel = new FlowLayoutPanel();
        label9 = new Label();
        BrowseFirmware = new Button();
        label11 = new Label();
        firmwarePathLabel = new Label();
        WriteFirmware = new Button();
        groupBox3 = new GroupBox();
        firmwareProgressBar = new ProgressBar();
        groupBox1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudModbusID).BeginInit();
        groupBox2.SuspendLayout();
        groupBox3.SuspendLayout();
        SuspendLayout();
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(cBoxSpeed);
        groupBox1.Controls.Add(buttonSetSpeed);
        groupBox1.Controls.Add(label8);
        groupBox1.Controls.Add(labelBattery);
        groupBox1.Controls.Add(label6);
        groupBox1.Controls.Add(buttonPoll);
        groupBox1.Controls.Add(buttonSetID);
        groupBox1.Controls.Add(labelFirmware);
        groupBox1.Controls.Add(labelModel);
        groupBox1.Controls.Add(nudModbusID);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(label1);
        groupBox1.Location = new Point(340, 14);
        groupBox1.Margin = new Padding(4, 3, 4, 3);
        groupBox1.Name = "groupBox1";
        groupBox1.Padding = new Padding(4, 3, 4, 3);
        groupBox1.Size = new Size(273, 145);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "Инфо";
        // 
        // cBoxSpeed
        // 
        cBoxSpeed.FormattingEnabled = true;
        cBoxSpeed.Location = new Point(86, 112);
        cBoxSpeed.Margin = new Padding(4, 3, 4, 3);
        cBoxSpeed.Name = "cBoxSpeed";
        cBoxSpeed.Size = new Size(84, 23);
        cBoxSpeed.TabIndex = 13;
        cBoxSpeed.Text = "9600";
        // 
        // buttonSetSpeed
        // 
        buttonSetSpeed.Location = new Point(178, 108);
        buttonSetSpeed.Margin = new Padding(4, 3, 4, 3);
        buttonSetSpeed.Name = "buttonSetSpeed";
        buttonSetSpeed.Size = new Size(88, 27);
        buttonSetSpeed.TabIndex = 12;
        buttonSetSpeed.Text = "Изменить";
        buttonSetSpeed.UseVisualStyleBackColor = true;
        buttonSetSpeed.Click += buttonSetSpeed_Click;
        // 
        // label8
        // 
        label8.AutoSize = true;
        label8.Location = new Point(7, 114);
        label8.Margin = new Padding(4, 0, 4, 0);
        label8.Name = "label8";
        label8.Size = new Size(62, 15);
        label8.TabIndex = 10;
        label8.Text = "Скорость:";
        // 
        // labelBattery
        // 
        labelBattery.AutoSize = true;
        labelBattery.Location = new Point(69, 55);
        labelBattery.Margin = new Padding(4, 0, 4, 0);
        labelBattery.Name = "labelBattery";
        labelBattery.Size = new Size(17, 15);
        labelBattery.TabIndex = 9;
        labelBattery.Text = "%";
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Location = new Point(7, 55);
        label6.Margin = new Padding(4, 0, 4, 0);
        label6.Name = "label6";
        label6.Size = new Size(53, 15);
        label6.TabIndex = 8;
        label6.Text = "Батарея:";
        // 
        // buttonPoll
        // 
        buttonPoll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        buttonPoll.Location = new Point(178, 50);
        buttonPoll.Margin = new Padding(4, 3, 4, 3);
        buttonPoll.Name = "buttonPoll";
        buttonPoll.Size = new Size(88, 27);
        buttonPoll.TabIndex = 7;
        buttonPoll.Text = "Опрос";
        buttonPoll.UseVisualStyleBackColor = true;
        buttonPoll.Click += buttonPoll_Click;
        // 
        // buttonSetID
        // 
        buttonSetID.Location = new Point(178, 78);
        buttonSetID.Margin = new Padding(4, 3, 4, 3);
        buttonSetID.Name = "buttonSetID";
        buttonSetID.Size = new Size(88, 27);
        buttonSetID.TabIndex = 6;
        buttonSetID.Text = "Изменить";
        buttonSetID.UseVisualStyleBackColor = true;
        buttonSetID.Click += buttonSetID_Click;
        // 
        // labelFirmware
        // 
        labelFirmware.AutoSize = true;
        labelFirmware.Location = new Point(86, 36);
        labelFirmware.Margin = new Padding(4, 0, 4, 0);
        labelFirmware.Name = "labelFirmware";
        labelFirmware.Size = new Size(37, 15);
        labelFirmware.TabIndex = 5;
        labelFirmware.Text = "v1.0.4";
        // 
        // labelModel
        // 
        labelModel.AutoSize = true;
        labelModel.Location = new Point(71, 18);
        labelModel.Margin = new Padding(4, 0, 4, 0);
        labelModel.Name = "labelModel";
        labelModel.Size = new Size(50, 15);
        labelModel.TabIndex = 4;
        labelModel.Text = "Модель";
        // 
        // nudModbusID
        // 
        nudModbusID.Location = new Point(86, 82);
        nudModbusID.Margin = new Padding(4, 3, 4, 3);
        nudModbusID.Name = "nudModbusID";
        nudModbusID.Size = new Size(57, 23);
        nudModbusID.TabIndex = 3;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(7, 84);
        label3.Margin = new Padding(4, 0, 4, 0);
        label3.Name = "label3";
        label3.Size = new Size(68, 15);
        label3.TabIndex = 2;
        label3.Text = "Modbus ID:";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(7, 36);
        label2.Margin = new Padding(4, 0, 4, 0);
        label2.Name = "label2";
        label2.Size = new Size(69, 15);
        label2.TabIndex = 1;
        label2.Text = "Прошивка:";
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(7, 18);
        label1.Margin = new Padding(4, 0, 4, 0);
        label1.Name = "label1";
        label1.Size = new Size(53, 15);
        label1.TabIndex = 0;
        label1.Text = "Модель:";
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(labelCleaning);
        groupBox2.Controls.Add(buttonCleaning);
        groupBox2.Controls.Add(label7);
        groupBox2.Controls.Add(labelAlarm);
        groupBox2.Controls.Add(label5);
        groupBox2.Controls.Add(buttonAlarm);
        groupBox2.Controls.Add(buttonValve);
        groupBox2.Controls.Add(labelValve);
        groupBox2.Controls.Add(label4);
        groupBox2.Location = new Point(15, 14);
        groupBox2.Margin = new Padding(4, 3, 4, 3);
        groupBox2.Name = "groupBox2";
        groupBox2.Padding = new Padding(4, 3, 4, 3);
        groupBox2.Size = new Size(317, 145);
        groupBox2.TabIndex = 1;
        groupBox2.TabStop = false;
        groupBox2.Text = "Общее";
        // 
        // labelCleaning
        // 
        labelCleaning.AutoSize = true;
        labelCleaning.Location = new Point(61, 89);
        labelCleaning.Margin = new Padding(4, 0, 4, 0);
        labelCleaning.Name = "labelCleaning";
        labelCleaning.Size = new Size(35, 15);
        labelCleaning.TabIndex = 9;
        labelCleaning.Text = "выкл";
        // 
        // buttonCleaning
        // 
        buttonCleaning.Location = new Point(146, 83);
        buttonCleaning.Margin = new Padding(4, 3, 4, 3);
        buttonCleaning.Name = "buttonCleaning";
        buttonCleaning.Size = new Size(163, 27);
        buttonCleaning.TabIndex = 8;
        buttonCleaning.Text = "Включить";
        buttonCleaning.UseVisualStyleBackColor = true;
        buttonCleaning.Click += buttonCleaning_Click;
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Location = new Point(7, 89);
        label7.Margin = new Padding(4, 0, 4, 0);
        label7.Name = "label7";
        label7.Size = new Size(50, 15);
        label7.TabIndex = 7;
        label7.Text = "Уборка:";
        // 
        // labelAlarm
        // 
        labelAlarm.AutoSize = true;
        labelAlarm.Location = new Point(71, 55);
        labelAlarm.Margin = new Padding(4, 0, 4, 0);
        labelAlarm.Name = "labelAlarm";
        labelAlarm.Size = new Size(25, 15);
        labelAlarm.TabIndex = 6;
        labelAlarm.Text = "нет";
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(7, 55);
        label5.Margin = new Padding(4, 0, 4, 0);
        label5.Name = "label5";
        label5.Size = new Size(63, 15);
        label5.TabIndex = 5;
        label5.Text = "Протечка:";
        // 
        // buttonAlarm
        // 
        buttonAlarm.Location = new Point(146, 50);
        buttonAlarm.Margin = new Padding(4, 3, 4, 3);
        buttonAlarm.Name = "buttonAlarm";
        buttonAlarm.Size = new Size(163, 27);
        buttonAlarm.TabIndex = 3;
        buttonAlarm.Text = "Авария";
        buttonAlarm.UseVisualStyleBackColor = true;
        buttonAlarm.Click += buttonAlarm_Click;
        // 
        // buttonValve
        // 
        buttonValve.Location = new Point(146, 16);
        buttonValve.Margin = new Padding(4, 3, 4, 3);
        buttonValve.Name = "buttonValve";
        buttonValve.Size = new Size(163, 27);
        buttonValve.TabIndex = 2;
        buttonValve.Text = "Закрыть";
        buttonValve.UseVisualStyleBackColor = true;
        buttonValve.Click += buttonValve_Click;
        // 
        // labelValve
        // 
        labelValve.AutoSize = true;
        labelValve.Location = new Point(46, 22);
        labelValve.Margin = new Padding(4, 0, 4, 0);
        labelValve.Name = "labelValve";
        labelValve.Size = new Size(48, 15);
        labelValve.TabIndex = 1;
        labelValve.Text = "Открыт";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(7, 22);
        label4.Margin = new Padding(4, 0, 4, 0);
        label4.Name = "label4";
        label4.Size = new Size(37, 15);
        label4.TabIndex = 0;
        label4.Text = "Кран:";
        // 
        // sensorPanel
        // 
        sensorPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        sensorPanel.AutoScroll = true;
        sensorPanel.BackColor = Color.Gainsboro;
        sensorPanel.FlowDirection = FlowDirection.TopDown;
        sensorPanel.Location = new Point(15, 194);
        sensorPanel.Margin = new Padding(4, 3, 4, 3);
        sensorPanel.Name = "sensorPanel";
        sensorPanel.Size = new Size(597, 302);
        sensorPanel.TabIndex = 4;
        sensorPanel.WrapContents = false;
        // 
        // label9
        // 
        label9.AutoSize = true;
        label9.Location = new Point(12, 175);
        label9.Margin = new Padding(4, 0, 4, 0);
        label9.Name = "label9";
        label9.Size = new Size(53, 15);
        label9.TabIndex = 5;
        label9.Text = "Датчики";
        // 
        // BrowseFirmware
        // 
        BrowseFirmware.Location = new Point(7, 44);
        BrowseFirmware.Margin = new Padding(4, 3, 4, 3);
        BrowseFirmware.Name = "BrowseFirmware";
        BrowseFirmware.Size = new Size(88, 27);
        BrowseFirmware.TabIndex = 14;
        BrowseFirmware.Text = "Обзор";
        BrowseFirmware.UseVisualStyleBackColor = true;
        BrowseFirmware.Click += BrowseFirmware_Click;
        // 
        // label11
        // 
        label11.AutoSize = true;
        label11.Location = new Point(5, 25);
        label11.Margin = new Padding(4, 0, 4, 0);
        label11.Name = "label11";
        label11.Size = new Size(36, 15);
        label11.TabIndex = 16;
        label11.Text = "Путь:";
        // 
        // firmwarePathLabel
        // 
        firmwarePathLabel.AutoSize = true;
        firmwarePathLabel.Location = new Point(51, 25);
        firmwarePathLabel.Margin = new Padding(4, 0, 4, 0);
        firmwarePathLabel.Name = "firmwarePathLabel";
        firmwarePathLabel.Size = new Size(138, 15);
        firmwarePathLabel.TabIndex = 17;
        firmwarePathLabel.Text = "C:\\Path\\To\\Firmware.bin";
        // 
        // WriteFirmware
        // 
        WriteFirmware.Location = new Point(102, 44);
        WriteFirmware.Margin = new Padding(4, 3, 4, 3);
        WriteFirmware.Name = "WriteFirmware";
        WriteFirmware.Size = new Size(88, 27);
        WriteFirmware.TabIndex = 18;
        WriteFirmware.Text = "Прошить";
        WriteFirmware.UseVisualStyleBackColor = true;
        WriteFirmware.Click += WriteFirmware_Click;
        // 
        // groupBox3
        // 
        groupBox3.Controls.Add(firmwareProgressBar);
        groupBox3.Controls.Add(BrowseFirmware);
        groupBox3.Controls.Add(WriteFirmware);
        groupBox3.Controls.Add(label11);
        groupBox3.Controls.Add(firmwarePathLabel);
        groupBox3.Location = new Point(15, 503);
        groupBox3.Margin = new Padding(4, 3, 4, 3);
        groupBox3.Name = "groupBox3";
        groupBox3.Padding = new Padding(4, 3, 4, 3);
        groupBox3.Size = new Size(597, 83);
        groupBox3.TabIndex = 19;
        groupBox3.TabStop = false;
        groupBox3.Text = "Прошивка";
        // 
        // firmwareProgressBar
        // 
        firmwareProgressBar.Location = new Point(205, 44);
        firmwareProgressBar.Margin = new Padding(4, 3, 4, 3);
        firmwareProgressBar.Name = "firmwareProgressBar";
        firmwareProgressBar.Size = new Size(385, 27);
        firmwareProgressBar.TabIndex = 19;
        // 
        // Datasheet
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(626, 590);
        Controls.Add(groupBox3);
        Controls.Add(label9);
        Controls.Add(sensorPanel);
        Controls.Add(groupBox2);
        Controls.Add(groupBox1);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Margin = new Padding(4, 3, 4, 3);
        MaximizeBox = false;
        Name = "Datasheet";
        SizeGripStyle = SizeGripStyle.Hide;
        Text = "Config";
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudModbusID).EndInit();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        ResumeLayout(false);
        PerformLayout();

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
    private System.Windows.Forms.Button buttonSetSpeed;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.ComboBox cBoxSpeed;
    private System.Windows.Forms.Button BrowseFirmware;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label firmwarePathLabel;
    private System.Windows.Forms.Button WriteFirmware;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.ProgressBar firmwareProgressBar;
}
