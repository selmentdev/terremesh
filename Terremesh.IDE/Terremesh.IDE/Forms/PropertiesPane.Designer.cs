namespace Terremesh.IDE.Forms
{
    partial class PropertiesPane
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
            this.objectsList = new System.Windows.Forms.ComboBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // objectsList
            // 
            this.objectsList.Dock = System.Windows.Forms.DockStyle.Top;
            this.objectsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.objectsList.FormattingEnabled = true;
            this.objectsList.Location = new System.Drawing.Point(0, 0);
            this.objectsList.Name = "objectsList";
            this.objectsList.Size = new System.Drawing.Size(250, 21);
            this.objectsList.TabIndex = 1;
            this.objectsList.SelectedIndexChanged += new System.EventHandler(this.objectsList_SelectedIndexChanged);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 21);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(250, 450);
            this.propertyGrid1.TabIndex = 2;
            // 
            // PropertiesPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 471);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.objectsList);
            this.Name = "PropertiesPane";
            this.Text = "Properties";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox objectsList;
        private System.Windows.Forms.PropertyGrid propertyGrid1;

    }
}