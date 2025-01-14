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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudModbusID = new System.Windows.Forms.NumericUpDown();
            this.labelModel = new System.Windows.Forms.Label();
            this.labelFirmware = new System.Windows.Forms.Label();
            this.buttonSetID = new System.Windows.Forms.Button();
            this.buttonPoll = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudModbusID)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonPoll);
            this.groupBox1.Controls.Add(this.buttonSetID);
            this.groupBox1.Controls.Add(this.labelFirmware);
            this.groupBox1.Controls.Add(this.labelModel);
            this.groupBox1.Controls.Add(this.nudModbusID);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(369, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(211, 101);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Инфо";
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Прошивка:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Modbus ID:";
            // 
            // nudModbusID
            // 
            this.nudModbusID.Location = new System.Drawing.Point(9, 71);
            this.nudModbusID.Name = "nudModbusID";
            this.nudModbusID.Size = new System.Drawing.Size(59, 20);
            this.nudModbusID.TabIndex = 3;
            // 
            // labelModel
            // 
            this.labelModel.AutoSize = true;
            this.labelModel.Location = new System.Drawing.Point(61, 16);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(50, 13);
            this.labelModel.TabIndex = 4;
            this.labelModel.Text = "Standard";
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
            // buttonSetID
            // 
            this.buttonSetID.Location = new System.Drawing.Point(74, 68);
            this.buttonSetID.Name = "buttonSetID";
            this.buttonSetID.Size = new System.Drawing.Size(75, 23);
            this.buttonSetID.TabIndex = 6;
            this.buttonSetID.Text = "Изменить";
            this.buttonSetID.UseVisualStyleBackColor = true;
            this.buttonSetID.Click += new System.EventHandler(this.buttonSetID_Click);
            // 
            // buttonPoll
            // 
            this.buttonPoll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPoll.Location = new System.Drawing.Point(130, 19);
            this.buttonPoll.Name = "buttonPoll";
            this.buttonPoll.Size = new System.Drawing.Size(75, 23);
            this.buttonPoll.TabIndex = 7;
            this.buttonPoll.Text = "Опрос";
            this.buttonPoll.UseVisualStyleBackColor = true;
            // 
            // Datasheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 500);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Datasheet";
            this.Text = "Datasheet";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudModbusID)).EndInit();
            this.ResumeLayout(false);

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
    }
}