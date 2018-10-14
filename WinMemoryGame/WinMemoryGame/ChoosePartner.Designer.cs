namespace WinMemoryGame
{
    partial class ChoosePartner
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvPartner = new System.Windows.Forms.DataGridView();
            this.timerGetUssers = new System.Windows.Forms.Timer(this.components);
            this.lblMessage = new System.Windows.Forms.Label();
            this.timerHasPartner = new System.Windows.Forms.Timer(this.components);
            this.lblUserName = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartner)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(177)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(63, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(758, 108);
            this.label1.TabIndex = 1;
            this.label1.Text = "choose a partner";
            // 
            // dgvPartner
            // 
            this.dgvPartner.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPartner.Location = new System.Drawing.Point(155, 154);
            this.dgvPartner.Name = "dgvPartner";
            this.dgvPartner.Size = new System.Drawing.Size(578, 262);
            this.dgvPartner.TabIndex = 2;
            this.dgvPartner.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPartner_CellClick);
            // 
            // timerGetUssers
            // 
            this.timerGetUssers.Enabled = true;
            this.timerGetUssers.Tick += new System.EventHandler(this.timerGetUsers);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(152, 419);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 3;
            // 
            // timerHasPartner
            // 
            this.timerHasPartner.Enabled = true;
            this.timerHasPartner.Tick += new System.EventHandler(this.timerHasPartner_Tick);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(95, 22);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(0, 13);
            this.lblUserName.TabIndex = 4;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(634, 435);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(187, 55);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "exit ";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ChoosePartner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 525);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.dgvPartner);
            this.Controls.Add(this.label1);
            this.Name = "ChoosePartner";
            this.Text = "ChoosePartner";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvPartner;
        private System.Windows.Forms.Timer timerGetUssers;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Timer timerHasPartner;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Button btnExit;
    }
}