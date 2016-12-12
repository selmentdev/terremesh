namespace Terremesh.IDE.Rendering
{
    partial class RenderDocument
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
            this.renderPanel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.renderPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // renderPanel
            // 
            this.renderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderPanel.Location = new System.Drawing.Point(0, 0);
            this.renderPanel.Name = "renderPanel";
            this.renderPanel.Size = new System.Drawing.Size(679, 460);
            this.renderPanel.TabIndex = 2;
            this.renderPanel.TabStop = false;
            this.renderPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.renderPanel_MouseDown);
            this.renderPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.renderPanel_MouseMove);
            this.renderPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.renderPanel_MouseUp);
            this.renderPanel.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.renderPanel_PreviewKeyDown);
            // 
            // RenderDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 460);
            this.Controls.Add(this.renderPanel);
            this.HideOnClose = true;
            this.Name = "RenderDocument";
            this.Text = "Preview";
            this.Load += new System.EventHandler(this.RenderDocument_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RenderDocument_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RenderDocument_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.renderPanel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox renderPanel;


    }
}