
namespace TcpIpClient
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
            this.BtnSend = new System.Windows.Forms.Button();
            this.LabelName = new System.Windows.Forms.Label();
            this.TextInput = new System.Windows.Forms.TextBox();
            this.TextMsgs = new System.Windows.Forms.TextBox();
            this.LabelMsgs = new System.Windows.Forms.Label();
            this.PanelUsers = new System.Windows.Forms.Panel();
            this.LabelUsers = new System.Windows.Forms.Label();
            this.BtnConnect = new System.Windows.Forms.Button();
            this.LabelUserName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnSend
            // 
            this.BtnSend.Location = new System.Drawing.Point(814, 481);
            this.BtnSend.Name = "BtnSend";
            this.BtnSend.Size = new System.Drawing.Size(117, 41);
            this.BtnSend.TabIndex = 14;
            this.BtnSend.Text = "Send";
            this.BtnSend.UseVisualStyleBackColor = true;
            this.BtnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // LabelName
            // 
            this.LabelName.AutoSize = true;
            this.LabelName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LabelName.Location = new System.Drawing.Point(318, 453);
            this.LabelName.Name = "LabelName";
            this.LabelName.Size = new System.Drawing.Size(64, 25);
            this.LabelName.TabIndex = 13;
            this.LabelName.Text = "Name";
            // 
            // TextInput
            // 
            this.TextInput.Location = new System.Drawing.Point(318, 481);
            this.TextInput.Multiline = true;
            this.TextInput.Name = "TextInput";
            this.TextInput.Size = new System.Drawing.Size(473, 41);
            this.TextInput.TabIndex = 11;
            this.TextInput.Text = " ";
            this.TextInput.TextChanged += new System.EventHandler(this.TextInput_TextChanged);
            // 
            // TextMsgs
            // 
            this.TextMsgs.Location = new System.Drawing.Point(320, 53);
            this.TextMsgs.Multiline = true;
            this.TextMsgs.Name = "TextMsgs";
            this.TextMsgs.ReadOnly = true;
            this.TextMsgs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextMsgs.Size = new System.Drawing.Size(611, 397);
            this.TextMsgs.TabIndex = 10;
            // 
            // LabelMsgs
            // 
            this.LabelMsgs.AutoSize = true;
            this.LabelMsgs.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LabelMsgs.Location = new System.Drawing.Point(320, 25);
            this.LabelMsgs.Name = "LabelMsgs";
            this.LabelMsgs.Size = new System.Drawing.Size(96, 25);
            this.LabelMsgs.TabIndex = 16;
            this.LabelMsgs.Text = "Messages";
            // 
            // PanelUsers
            // 
            this.PanelUsers.Location = new System.Drawing.Point(13, 53);
            this.PanelUsers.Name = "PanelUsers";
            this.PanelUsers.Size = new System.Drawing.Size(265, 527);
            this.PanelUsers.TabIndex = 17;
            // 
            // LabelUsers
            // 
            this.LabelUsers.AutoSize = true;
            this.LabelUsers.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LabelUsers.Location = new System.Drawing.Point(13, 25);
            this.LabelUsers.Name = "LabelUsers";
            this.LabelUsers.Size = new System.Drawing.Size(60, 25);
            this.LabelUsers.TabIndex = 18;
            this.LabelUsers.Text = "Users";
            // 
            // BtnConnect
            // 
            this.BtnConnect.Location = new System.Drawing.Point(320, 541);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(611, 39);
            this.BtnConnect.TabIndex = 19;
            this.BtnConnect.Text = "Connect";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // LabelUserName
            // 
            this.LabelUserName.AutoSize = true;
            this.LabelUserName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LabelUserName.Location = new System.Drawing.Point(707, 0);
            this.LabelUserName.Name = "LabelUserName";
            this.LabelUserName.Size = new System.Drawing.Size(0, 25);
            this.LabelUserName.TabIndex = 20;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 603);
            this.Controls.Add(this.LabelUserName);
            this.Controls.Add(this.BtnConnect);
            this.Controls.Add(this.LabelUsers);
            this.Controls.Add(this.PanelUsers);
            this.Controls.Add(this.LabelMsgs);
            this.Controls.Add(this.BtnSend);
            this.Controls.Add(this.LabelName);
            this.Controls.Add(this.TextInput);
            this.Controls.Add(this.TextMsgs);
            this.MaximumSize = new System.Drawing.Size(1000, 650);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1000, 650);
            this.Name = "Form1";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnSend;
        private System.Windows.Forms.Label LabelName;
        private System.Windows.Forms.TextBox TextInput;
        private System.Windows.Forms.TextBox TextMsgs;
        private System.Windows.Forms.Label LabelMsgs;
        private System.Windows.Forms.Panel PanelUsers;
        private System.Windows.Forms.Label LabelUsers;
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.Label LabelUserName;
    }
}

