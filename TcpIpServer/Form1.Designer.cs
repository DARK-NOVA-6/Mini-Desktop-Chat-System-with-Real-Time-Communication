
namespace TcpIpServer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnStart = new System.Windows.Forms.Button();
            this.TextMsgs = new System.Windows.Forms.TextBox();
            this.TextClientList = new System.Windows.Forms.TextBox();
            this.TextBroadcastMeg = new System.Windows.Forms.TextBox();
            this.BtnSend = new System.Windows.Forms.Button();
            this.LabelClients = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(255, 408);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(483, 35);
            this.BtnStart.TabIndex = 0;
            this.BtnStart.Text = "Start Service";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // TextMsgs
            // 
            this.TextMsgs.Location = new System.Drawing.Point(255, 27);
            this.TextMsgs.Multiline = true;
            this.TextMsgs.Name = "TextMsgs";
            this.TextMsgs.ReadOnly = true;
            this.TextMsgs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextMsgs.Size = new System.Drawing.Size(483, 292);
            this.TextMsgs.TabIndex = 11;
            // 
            // TextClientList
            // 
            this.TextClientList.Location = new System.Drawing.Point(22, 52);
            this.TextClientList.Multiline = true;
            this.TextClientList.Name = "TextClientList";
            this.TextClientList.ReadOnly = true;
            this.TextClientList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextClientList.Size = new System.Drawing.Size(182, 391);
            this.TextClientList.TabIndex = 12;
            // 
            // TextBroadcastMeg
            // 
            this.TextBroadcastMeg.Location = new System.Drawing.Point(255, 340);
            this.TextBroadcastMeg.Multiline = true;
            this.TextBroadcastMeg.Name = "TextBroadcastMeg";
            this.TextBroadcastMeg.Size = new System.Drawing.Size(350, 41);
            this.TextBroadcastMeg.TabIndex = 13;
            this.TextBroadcastMeg.Text = " ";
            this.TextBroadcastMeg.TextChanged += new System.EventHandler(this.MsgTxt_TextChanged);
            // 
            // BtnSend
            // 
            this.BtnSend.Location = new System.Drawing.Point(611, 340);
            this.BtnSend.Name = "BtnSend";
            this.BtnSend.Size = new System.Drawing.Size(127, 41);
            this.BtnSend.TabIndex = 14;
            this.BtnSend.Text = "Send";
            this.BtnSend.UseVisualStyleBackColor = true;
            this.BtnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // LabelClients
            // 
            this.LabelClients.AutoSize = true;
            this.LabelClients.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LabelClients.Location = new System.Drawing.Point(22, 24);
            this.LabelClients.Name = "LabelClients";
            this.LabelClients.Size = new System.Drawing.Size(76, 25);
            this.LabelClients.TabIndex = 15;
            this.LabelClients.Text = "Clients:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 473);
            this.Controls.Add(this.LabelClients);
            this.Controls.Add(this.BtnSend);
            this.Controls.Add(this.TextBroadcastMeg);
            this.Controls.Add(this.TextClientList);
            this.Controls.Add(this.TextMsgs);
            this.Controls.Add(this.BtnStart);
            this.MaximumSize = new System.Drawing.Size(800, 520);
            this.MinimumSize = new System.Drawing.Size(800, 520);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.TextBox TextMsgs;
        private System.Windows.Forms.TextBox TextClientList;
        private System.Windows.Forms.TextBox TextBroadcastMeg;
        private System.Windows.Forms.Button BtnSend;
        private System.Windows.Forms.Label LabelClients;
    }
}

