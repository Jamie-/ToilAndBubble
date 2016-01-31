namespace imageTester
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdEMPTY = new System.Windows.Forms.RadioButton();
            this.rdAVOID = new System.Windows.Forms.RadioButton();
            this.rdSOLID = new System.Windows.Forms.RadioButton();
            this.rdCOKPT = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(84, 567);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 500);
            this.panel1.TabIndex = 2;
            // 
            // rdEMPTY
            // 
            this.rdEMPTY.AutoSize = true;
            this.rdEMPTY.Location = new System.Drawing.Point(12, 518);
            this.rdEMPTY.Name = "rdEMPTY";
            this.rdEMPTY.Size = new System.Drawing.Size(62, 17);
            this.rdEMPTY.TabIndex = 3;
            this.rdEMPTY.TabStop = true;
            this.rdEMPTY.Text = "EMPTY";
            this.rdEMPTY.UseVisualStyleBackColor = true;
            // 
            // rdAVOID
            // 
            this.rdAVOID.AutoSize = true;
            this.rdAVOID.Location = new System.Drawing.Point(103, 518);
            this.rdAVOID.Name = "rdAVOID";
            this.rdAVOID.Size = new System.Drawing.Size(58, 17);
            this.rdAVOID.TabIndex = 4;
            this.rdAVOID.TabStop = true;
            this.rdAVOID.Text = "AVOID";
            this.rdAVOID.UseVisualStyleBackColor = true;
            // 
            // rdSOLID
            // 
            this.rdSOLID.AutoSize = true;
            this.rdSOLID.Location = new System.Drawing.Point(194, 518);
            this.rdSOLID.Name = "rdSOLID";
            this.rdSOLID.Size = new System.Drawing.Size(57, 17);
            this.rdSOLID.TabIndex = 5;
            this.rdSOLID.TabStop = true;
            this.rdSOLID.Text = "SOLID";
            this.rdSOLID.UseVisualStyleBackColor = true;
            // 
            // rdCOKPT
            // 
            this.rdCOKPT.AutoSize = true;
            this.rdCOKPT.Location = new System.Drawing.Point(285, 518);
            this.rdCOKPT.Name = "rdCOKPT";
            this.rdCOKPT.Size = new System.Drawing.Size(61, 17);
            this.rdCOKPT.TabIndex = 6;
            this.rdCOKPT.TabStop = true;
            this.rdCOKPT.Text = "COKPT";
            this.rdCOKPT.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(394, 529);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(59, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "mirror x";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(394, 552);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(59, 17);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.Text = "mirror y";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(526, 606);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.rdCOKPT);
            this.Controls.Add(this.rdSOLID);
            this.Controls.Add(this.rdAVOID);
            this.Controls.Add(this.rdEMPTY);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdEMPTY;
        private System.Windows.Forms.RadioButton rdAVOID;
        private System.Windows.Forms.RadioButton rdSOLID;
        private System.Windows.Forms.RadioButton rdCOKPT;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}

