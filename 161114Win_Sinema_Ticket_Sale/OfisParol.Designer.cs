namespace _161114Win_Sinema_Ticket_Sale
{
    partial class OfisParol
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtParol = new System.Windows.Forms.TextBox();
            this.btnCancelParol = new System.Windows.Forms.Button();
            this.BtnOkParol = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(19, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "İşçilər ilə bağlı hər hansı filtirləməni yerinə yetirmək üçün\r\n gərəkli parolu da" +
    "xil edin:";
            // 
            // txtParol
            // 
            this.txtParol.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtParol.Location = new System.Drawing.Point(22, 52);
            this.txtParol.Name = "txtParol";
            this.txtParol.PasswordChar = '*';
            this.txtParol.Size = new System.Drawing.Size(369, 27);
            this.txtParol.TabIndex = 1;
            // 
            // btnCancelParol
            // 
            this.btnCancelParol.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelParol.Location = new System.Drawing.Point(222, 86);
            this.btnCancelParol.Name = "btnCancelParol";
            this.btnCancelParol.Size = new System.Drawing.Size(75, 23);
            this.btnCancelParol.TabIndex = 2;
            this.btnCancelParol.Text = "İmtina";
            this.btnCancelParol.UseVisualStyleBackColor = true;
            this.btnCancelParol.Click += new System.EventHandler(this.btnCancelParol_Click);
            // 
            // BtnOkParol
            // 
            this.BtnOkParol.Location = new System.Drawing.Point(316, 86);
            this.BtnOkParol.Name = "BtnOkParol";
            this.BtnOkParol.Size = new System.Drawing.Size(75, 23);
            this.BtnOkParol.TabIndex = 3;
            this.BtnOkParol.Text = "Davam";
            this.BtnOkParol.UseVisualStyleBackColor = true;
            this.BtnOkParol.Click += new System.EventHandler(this.BtnOkParol_Click);
            // 
            // OfisParol
            // 
            this.AcceptButton = this.BtnOkParol;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.CancelButton = this.btnCancelParol;
            this.ClientSize = new System.Drawing.Size(411, 121);
            this.ControlBox = false;
            this.Controls.Add(this.BtnOkParol);
            this.Controls.Add(this.btnCancelParol);
            this.Controls.Add(this.txtParol);
            this.Controls.Add(this.label1);
            this.Name = "OfisParol";
            this.Text = "Parol";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtParol;
        private System.Windows.Forms.Button btnCancelParol;
        private System.Windows.Forms.Button BtnOkParol;
    }
}