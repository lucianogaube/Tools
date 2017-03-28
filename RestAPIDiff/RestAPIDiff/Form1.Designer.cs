namespace RestAPIDiff
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
            this.txtInput = new System.Windows.Forms.RichTextBox();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.txtBaseJson = new System.Windows.Forms.RichTextBox();
            this.cboBaseType = new System.Windows.Forms.ComboBox();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblInputResult = new System.Windows.Forms.Label();
            this.lblBaseResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SpringGreen;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(596, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "Compare";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtInput
            // 
            this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInput.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInput.Location = new System.Drawing.Point(12, 63);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(365, 282);
            this.txtInput.TabIndex = 1;
            this.txtInput.Text = "";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(47, 15);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(330, 20);
            this.txtURL.TabIndex = 2;
            // 
            // txtBaseJson
            // 
            this.txtBaseJson.BackColor = System.Drawing.Color.White;
            this.txtBaseJson.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBaseJson.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBaseJson.Location = new System.Drawing.Point(383, 63);
            this.txtBaseJson.Name = "txtBaseJson";
            this.txtBaseJson.Size = new System.Drawing.Size(365, 282);
            this.txtBaseJson.TabIndex = 3;
            this.txtBaseJson.Text = "";
            // 
            // cboBaseType
            // 
            this.cboBaseType.FormattingEnabled = true;
            this.cboBaseType.Items.AddRange(new object[] {
            "Device",
            "Application"});
            this.cboBaseType.Location = new System.Drawing.Point(386, 15);
            this.cboBaseType.Name = "cboBaseType";
            this.cboBaseType.Size = new System.Drawing.Size(204, 21);
            this.cboBaseType.TabIndex = 4;
            this.cboBaseType.Text = "-- Select Base JSON --";
            this.cboBaseType.SelectedIndexChanged += new System.EventHandler(this.cboBaseType_SelectedIndexChanged);
            // 
            // txtResult
            // 
            this.txtResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtResult.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.Location = new System.Drawing.Point(12, 351);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(736, 160);
            this.txtResult.TabIndex = 5;
            this.txtResult.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "URL:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Input JSON:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(383, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Base JSON:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(697, 516);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Results";
            // 
            // lblInputResult
            // 
            this.lblInputResult.AutoSize = true;
            this.lblInputResult.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInputResult.Location = new System.Drawing.Point(362, 43);
            this.lblInputResult.Name = "lblInputResult";
            this.lblInputResult.Size = new System.Drawing.Size(15, 17);
            this.lblInputResult.TabIndex = 11;
            this.lblInputResult.Text = "0";
            // 
            // lblBaseResult
            // 
            this.lblBaseResult.AutoSize = true;
            this.lblBaseResult.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBaseResult.Location = new System.Drawing.Point(575, 43);
            this.lblBaseResult.Name = "lblBaseResult";
            this.lblBaseResult.Size = new System.Drawing.Size(15, 17);
            this.lblBaseResult.TabIndex = 12;
            this.lblBaseResult.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(763, 536);
            this.Controls.Add(this.lblBaseResult);
            this.Controls.Add(this.lblInputResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.cboBaseType);
            this.Controls.Add(this.txtBaseJson);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "iQuate JSON Comparer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox txtInput;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.RichTextBox txtBaseJson;
        private System.Windows.Forms.ComboBox cboBaseType;
        private System.Windows.Forms.RichTextBox txtResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblInputResult;
        private System.Windows.Forms.Label lblBaseResult;
    }
}

