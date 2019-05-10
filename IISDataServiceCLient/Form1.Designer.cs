namespace IISDataServiceCLient
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.userId = new System.Windows.Forms.NumericUpDown();
            this.listBoxUsers = new System.Windows.Forms.ListBox();
            this.EF = new System.Windows.Forms.Button();
            this.listBoxSubs = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.getSubs = new System.Windows.Forms.Button();
            this.selectedUser = new System.Windows.Forms.Label();
            this.selectedSub = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.userId)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(68, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "GetUser";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.GetUsers);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Users";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Error:";
            // 
            // userId
            // 
            this.userId.Location = new System.Drawing.Point(208, 46);
            this.userId.Name = "userId";
            this.userId.Size = new System.Drawing.Size(120, 22);
            this.userId.TabIndex = 5;
            // 
            // listBoxUsers
            // 
            this.listBoxUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxUsers.FormattingEnabled = true;
            this.listBoxUsers.ItemHeight = 16;
            this.listBoxUsers.Location = new System.Drawing.Point(15, 188);
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.Size = new System.Drawing.Size(332, 180);
            this.listBoxUsers.TabIndex = 6;
            this.listBoxUsers.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // EF
            // 
            this.EF.Location = new System.Drawing.Point(385, 44);
            this.EF.Name = "EF";
            this.EF.Size = new System.Drawing.Size(75, 23);
            this.EF.TabIndex = 7;
            this.EF.Text = "EF GetUsers";
            this.EF.UseVisualStyleBackColor = true;
            this.EF.Click += new System.EventHandler(this.EF_Click);
            // 
            // listBoxSubs
            // 
            this.listBoxSubs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxSubs.FormattingEnabled = true;
            this.listBoxSubs.ItemHeight = 16;
            this.listBoxSubs.Location = new System.Drawing.Point(443, 188);
            this.listBoxSubs.Name = "listBoxSubs";
            this.listBoxSubs.Size = new System.Drawing.Size(476, 180);
            this.listBoxSubs.TabIndex = 8;
            this.listBoxSubs.SelectedIndexChanged += new System.EventHandler(this.listBoxSubs_SelectedIndexChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(416, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Subscriptions";
            // 
            // getSubs
            // 
            this.getSubs.Location = new System.Drawing.Point(495, 46);
            this.getSubs.Name = "getSubs";
            this.getSubs.Size = new System.Drawing.Size(105, 38);
            this.getSubs.TabIndex = 10;
            this.getSubs.Text = "getSubs";
            this.getSubs.UseVisualStyleBackColor = true;
            this.getSubs.Click += new System.EventHandler(this.getSubs_Click);
            // 
            // selectedUser
            // 
            this.selectedUser.AutoSize = true;
            this.selectedUser.Location = new System.Drawing.Point(62, 157);
            this.selectedUser.Name = "selectedUser";
            this.selectedUser.Size = new System.Drawing.Size(47, 17);
            this.selectedUser.TabIndex = 11;
            this.selectedUser.Text = "userId";
            // 
            // selectedSub
            // 
            this.selectedSub.AutoSize = true;
            this.selectedSub.Location = new System.Drawing.Point(529, 157);
            this.selectedSub.Name = "selectedSub";
            this.selectedSub.Size = new System.Drawing.Size(42, 17);
            this.selectedSub.TabIndex = 12;
            this.selectedSub.Text = "subId";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(611, 397);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Subscribe";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.subscribe_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.selectedSub);
            this.Controls.Add(this.selectedUser);
            this.Controls.Add(this.getSubs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBoxSubs);
            this.Controls.Add(this.EF);
            this.Controls.Add(this.listBoxUsers);
            this.Controls.Add(this.userId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.userId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown userId;
        private System.Windows.Forms.ListBox listBoxUsers;
        private System.Windows.Forms.Button EF;
        private System.Windows.Forms.ListBox listBoxSubs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button getSubs;
        private System.Windows.Forms.Label selectedUser;
        private System.Windows.Forms.Label selectedSub;
        private System.Windows.Forms.Button button2;
    }
}

