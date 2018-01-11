namespace _7_dec_2017_DragRace {
    partial class SerialMonitor {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.rtbLogDSte = new System.Windows.Forms.RichTextBox();
            this.btnClearDSte = new System.Windows.Forms.Button();
            this.cbShowExtensiveDSte = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // rtbLogDSte
            // 
            this.rtbLogDSte.BackColor = System.Drawing.Color.Black;
            this.rtbLogDSte.ForeColor = System.Drawing.Color.White;
            this.rtbLogDSte.Location = new System.Drawing.Point(12, 12);
            this.rtbLogDSte.Name = "rtbLogDSte";
            this.rtbLogDSte.ReadOnly = true;
            this.rtbLogDSte.Size = new System.Drawing.Size(707, 298);
            this.rtbLogDSte.TabIndex = 0;
            this.rtbLogDSte.Text = "";
            // 
            // btnClearDSte
            // 
            this.btnClearDSte.Font = new System.Drawing.Font("Comic Sans MS", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearDSte.Location = new System.Drawing.Point(556, 323);
            this.btnClearDSte.Name = "btnClearDSte";
            this.btnClearDSte.Size = new System.Drawing.Size(163, 64);
            this.btnClearDSte.TabIndex = 1;
            this.btnClearDSte.Text = "Clear";
            this.btnClearDSte.UseVisualStyleBackColor = true;
            this.btnClearDSte.Click += new System.EventHandler(this.btnClearDSte_Click);
            // 
            // cbShowExtensiveDSte
            // 
            this.cbShowExtensiveDSte.AutoSize = true;
            this.cbShowExtensiveDSte.Location = new System.Drawing.Point(12, 354);
            this.cbShowExtensiveDSte.Name = "cbShowExtensiveDSte";
            this.cbShowExtensiveDSte.Size = new System.Drawing.Size(156, 21);
            this.cbShowExtensiveDSte.TabIndex = 2;
            this.cbShowExtensiveDSte.Text = "Show Extensive Log";
            this.cbShowExtensiveDSte.UseVisualStyleBackColor = true;
            this.cbShowExtensiveDSte.CheckedChanged += new System.EventHandler(this.cbShowExtensiveDSte_CheckedChanged);
            // 
            // SerialMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 399);
            this.ControlBox = false;
            this.Controls.Add(this.cbShowExtensiveDSte);
            this.Controls.Add(this.btnClearDSte);
            this.Controls.Add(this.rtbLogDSte);
            this.Name = "SerialMonitor";
            this.Text = "SerialMonitor";
            this.Move += new System.EventHandler(this.SerialMonitor_Move);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbLogDSte;
        private System.Windows.Forms.Button btnClearDSte;
        private System.Windows.Forms.CheckBox cbShowExtensiveDSte;
    }
}