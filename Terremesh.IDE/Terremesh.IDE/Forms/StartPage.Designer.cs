namespace Terremesh.IDE.Forms
{
    partial class StartPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartPage));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chb_ShowOnStartup = new System.Windows.Forms.CheckBox();
            this.lnkAuthor1 = new System.Windows.Forms.LinkLabel();
            this.lnkAuthor2 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(683, 155);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // chb_ShowOnStartup
            // 
            this.chb_ShowOnStartup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chb_ShowOnStartup.AutoSize = true;
            this.chb_ShowOnStartup.Location = new System.Drawing.Point(12, 377);
            this.chb_ShowOnStartup.Name = "chb_ShowOnStartup";
            this.chb_ShowOnStartup.Size = new System.Drawing.Size(130, 17);
            this.chb_ShowOnStartup.TabIndex = 0;
            this.chb_ShowOnStartup.Text = "Show page on startup";
            this.chb_ShowOnStartup.UseVisualStyleBackColor = true;
            this.chb_ShowOnStartup.CheckedChanged += new System.EventHandler(this.chb_ShowOnStartup_CheckedChanged);
            // 
            // lnkAuthor1
            // 
            this.lnkAuthor1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkAuthor1.AutoSize = true;
            this.lnkAuthor1.Location = new System.Drawing.Point(506, 378);
            this.lnkAuthor1.Name = "lnkAuthor1";
            this.lnkAuthor1.Size = new System.Drawing.Size(88, 13);
            this.lnkAuthor1.TabIndex = 2;
            this.lnkAuthor1.TabStop = true;
            this.lnkAuthor1.Tag = "kgrzybowski89@gmail.com";
            this.lnkAuthor1.Text = "Karol Grzybowski";
            this.lnkAuthor1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.authorLinkClicked);
            // 
            // lnkAuthor2
            // 
            this.lnkAuthor2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkAuthor2.AutoSize = true;
            this.lnkAuthor2.Location = new System.Drawing.Point(600, 378);
            this.lnkAuthor2.Name = "lnkAuthor2";
            this.lnkAuthor2.Size = new System.Drawing.Size(71, 13);
            this.lnkAuthor2.TabIndex = 3;
            this.lnkAuthor2.TabStop = true;
            this.lnkAuthor2.Tag = "piotr.ruchwa@gmail.com";
            this.lnkAuthor2.Text = "Piotr Ruchwa";
            this.lnkAuthor2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.authorLinkClicked);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(454, 378);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Authors:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linkLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.SystemColors.Highlight;
            this.linkLabel1.Location = new System.Drawing.Point(18, 161);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(165, 23);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Open File";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(12, 201);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 170);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Recent Files";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.linkLabel2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(171, 151);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // linkLabel2
            // 
            this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linkLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel2.LinkColor = System.Drawing.SystemColors.Highlight;
            this.linkLabel2.Location = new System.Drawing.Point(3, 0);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(165, 23);
            this.linkLabel2.TabIndex = 8;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "linkLabel2";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.Location = new System.Drawing.Point(195, 161);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(476, 207);
            this.flowLayoutPanel2.TabIndex = 9;
            // 
            // StartPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(683, 406);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnkAuthor2);
            this.Controls.Add(this.lnkAuthor1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.chb_ShowOnStartup);
            this.HideOnClose = true;
            this.Name = "StartPage";
            this.Text = "StartPage";
            this.Load += new System.EventHandler(this.StartPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chb_ShowOnStartup;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel lnkAuthor2;
        private System.Windows.Forms.LinkLabel lnkAuthor1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;

    }
}