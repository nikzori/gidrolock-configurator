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
        TextBox_Log = new TextBox();
        upDownModbusID = new NumericUpDown();
        label2 = new Label();
        cBoxPorts = new ComboBox();
        label1 = new Label();
        button1 = new Button();
        cBoxSpeed = new ComboBox();
        label4 = new Label();
        ((System.ComponentModel.ISupportInitialize)upDownModbusID).BeginInit();
        SuspendLayout();
        // 
        // TextBox_Log
        // 
        TextBox_Log.Location = new Point(14, 61);
        TextBox_Log.Margin = new Padding(4, 3, 4, 3);
        TextBox_Log.Multiline = true;
        TextBox_Log.Name = "TextBox_Log";
        TextBox_Log.ReadOnly = true;
        TextBox_Log.ScrollBars = ScrollBars.Vertical;
        TextBox_Log.Size = new Size(580, 184);
        TextBox_Log.TabIndex = 1;
        // 
        // upDownModbusID
        // 
        upDownModbusID.Location = new Point(191, 31);
        upDownModbusID.Margin = new Padding(4, 3, 4, 3);
        upDownModbusID.Name = "upDownModbusID";
        upDownModbusID.Size = new Size(77, 23);
        upDownModbusID.TabIndex = 1;
        upDownModbusID.Value = new decimal(new int[] { 30, 0, 0, 0 });
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(188, 10);
        label2.Margin = new Padding(4, 0, 4, 0);
        label2.Name = "label2";
        label2.Size = new Size(65, 15);
        label2.TabIndex = 0;
        label2.Text = "Modbus ID";
        // 
        // cBoxPorts
        // 
        cBoxPorts.FormattingEnabled = true;
        cBoxPorts.Location = new Point(14, 30);
        cBoxPorts.Margin = new Padding(4, 3, 4, 3);
        cBoxPorts.Name = "cBoxPorts";
        cBoxPorts.Size = new Size(75, 23);
        cBoxPorts.TabIndex = 1;
        cBoxPorts.Text = "COM1";
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(10, 10);
        label1.Margin = new Padding(4, 0, 4, 0);
        label1.Name = "label1";
        label1.Size = new Size(35, 15);
        label1.TabIndex = 0;
        label1.Text = "Порт";
        // 
        // button1
        // 
        button1.Location = new Point(483, 28);
        button1.Margin = new Padding(4, 3, 4, 3);
        button1.Name = "button1";
        button1.Size = new Size(107, 27);
        button1.TabIndex = 2;
        button1.Text = "Подключиться";
        button1.UseVisualStyleBackColor = true;
        button1.Click += ButtonConnect_Click;
        // 
        // cBoxSpeed
        // 
        cBoxSpeed.FormattingEnabled = true;
        cBoxSpeed.Location = new Point(97, 30);
        cBoxSpeed.Margin = new Padding(4, 3, 4, 3);
        cBoxSpeed.Name = "cBoxSpeed";
        cBoxSpeed.Size = new Size(90, 23);
        cBoxSpeed.TabIndex = 7;
        cBoxSpeed.Text = "9600";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(93, 10);
        label4.Margin = new Padding(4, 0, 4, 0);
        label4.Name = "label4";
        label4.Size = new Size(59, 15);
        label4.TabIndex = 6;
        label4.Text = "Скорость";
        // 
        // App
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(604, 261);
        Controls.Add(cBoxSpeed);
        Controls.Add(label4);
        Controls.Add(button1);
        Controls.Add(cBoxPorts);
        Controls.Add(label1);
        Controls.Add(upDownModbusID);
        Controls.Add(TextBox_Log);
        Controls.Add(label2);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Icon = (Icon)resources.GetObject("$this.Icon");
        Margin = new Padding(4, 3, 4, 3);
        MaximizeBox = false;
        Name = "App";
        SizeGripStyle = SizeGripStyle.Hide;
        Text = "Gidrolock Modbus Scanner";
        FormClosed += App_FormClosed;
        Load += Form1_Load;
        ((System.ComponentModel.ISupportInitialize)upDownModbusID).EndInit();
        ResumeLayout(false);
        PerformLayout();

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