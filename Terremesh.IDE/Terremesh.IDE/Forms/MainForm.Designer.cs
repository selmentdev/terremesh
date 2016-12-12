namespace Terremesh.IDE.Forms
{
    partial class MainForm
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
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient1 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearWorkspaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mniViewShowProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.outputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.startPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rendererToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbrView = new System.Windows.Forms.ToolStrip();
            this.btnViewShowOutput = new System.Windows.Forms.ToolStripButton();
            this.btnViewShowProperties = new System.Windows.Forms.ToolStripButton();
            this.btnViewShowStartPage = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tcbxRemeshingMethod = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ttbxTriangleCount = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.ttbxRemovePercent = new System.Windows.Forms.ToolStripTextBox();
            this.tsbRemove = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pgrProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.lblProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTrianglesVertices = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStage = new System.Windows.Forms.ToolStripStatusLabel();
            this.dlgOpenMesh = new System.Windows.Forms.OpenFileDialog();
            this.dlgSaveMesh = new System.Windows.Forms.SaveFileDialog();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tbrView.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.dockPanel1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(720, 282);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(720, 331);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tbrView);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // dockPanel1
            // 
            this.dockPanel1.ActiveAutoHideContent = null;
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.DockBackColor = System.Drawing.SystemColors.Control;
            this.dockPanel1.DockBottomPortion = 180D;
            this.dockPanel1.DockLeftPortion = 180D;
            this.dockPanel1.DockRightPortion = 180D;
            this.dockPanel1.DockTopPortion = 180D;
            this.dockPanel1.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(720, 282);
            dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient1.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
            tabGradient1.EndColor = System.Drawing.SystemColors.Control;
            tabGradient1.StartColor = System.Drawing.SystemColors.Control;
            tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin1.TabGradient = tabGradient1;
            autoHideStripSkin1.TextFont = new System.Drawing.Font("Tahoma", 8.25F);
            dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
            tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
            dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
            tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
            dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
            dockPaneStripSkin1.TextFont = new System.Drawing.Font("Tahoma", 8.25F);
            tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
            tabGradient5.EndColor = System.Drawing.SystemColors.Control;
            tabGradient5.StartColor = System.Drawing.SystemColors.Control;
            tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
            dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
            tabGradient6.EndColor = System.Drawing.SystemColors.InactiveCaption;
            tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.TextColor = System.Drawing.SystemColors.InactiveCaptionText;
            dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
            tabGradient7.EndColor = System.Drawing.Color.Transparent;
            tabGradient7.StartColor = System.Drawing.Color.Transparent;
            tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
            dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
            dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
            this.dockPanel1.Skin = dockPanelSkin1;
            this.dockPanel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(720, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.clearWorkspaceToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // clearWorkspaceToolStripMenuItem
            // 
            this.clearWorkspaceToolStripMenuItem.Name = "clearWorkspaceToolStripMenuItem";
            this.clearWorkspaceToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.clearWorkspaceToolStripMenuItem.Text = "Clear Workspace";
            this.clearWorkspaceToolStripMenuItem.Click += new System.EventHandler(this.clearWorkspaceToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(159, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniViewShowProperties,
            this.outputToolStripMenuItem,
            this.toolStripMenuItem1,
            this.startPageToolStripMenuItem,
            this.rendererToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // mniViewShowProperties
            // 
            this.mniViewShowProperties.Name = "mniViewShowProperties";
            this.mniViewShowProperties.Size = new System.Drawing.Size(127, 22);
            this.mniViewShowProperties.Text = "Properties";
            this.mniViewShowProperties.Click += new System.EventHandler(this.actViewShowProperties);
            // 
            // outputToolStripMenuItem
            // 
            this.outputToolStripMenuItem.Name = "outputToolStripMenuItem";
            this.outputToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.outputToolStripMenuItem.Text = "Output";
            this.outputToolStripMenuItem.Click += new System.EventHandler(this.actViewShowOutput);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(124, 6);
            // 
            // startPageToolStripMenuItem
            // 
            this.startPageToolStripMenuItem.Name = "startPageToolStripMenuItem";
            this.startPageToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.startPageToolStripMenuItem.Text = "Start Page";
            this.startPageToolStripMenuItem.Click += new System.EventHandler(this.actViewShowStartPage);
            // 
            // rendererToolStripMenuItem
            // 
            this.rendererToolStripMenuItem.Name = "rendererToolStripMenuItem";
            this.rendererToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.rendererToolStripMenuItem.Text = "Renderer";
            this.rendererToolStripMenuItem.Click += new System.EventHandler(this.rendererToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tbrView
            // 
            this.tbrView.Dock = System.Windows.Forms.DockStyle.None;
            this.tbrView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnViewShowOutput,
            this.btnViewShowProperties,
            this.btnViewShowStartPage});
            this.tbrView.Location = new System.Drawing.Point(3, 24);
            this.tbrView.Name = "tbrView";
            this.tbrView.Size = new System.Drawing.Size(171, 25);
            this.tbrView.TabIndex = 1;
            this.tbrView.Text = "View";
            this.tbrView.Visible = false;
            // 
            // btnViewShowOutput
            // 
            this.btnViewShowOutput.Image = ((System.Drawing.Image)(resources.GetObject("btnViewShowOutput.Image")));
            this.btnViewShowOutput.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnViewShowOutput.Name = "btnViewShowOutput";
            this.btnViewShowOutput.Size = new System.Drawing.Size(65, 22);
            this.btnViewShowOutput.Text = "Output";
            this.btnViewShowOutput.Click += new System.EventHandler(this.actViewShowOutput);
            // 
            // btnViewShowProperties
            // 
            this.btnViewShowProperties.Image = ((System.Drawing.Image)(resources.GetObject("btnViewShowProperties.Image")));
            this.btnViewShowProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnViewShowProperties.Name = "btnViewShowProperties";
            this.btnViewShowProperties.Size = new System.Drawing.Size(80, 22);
            this.btnViewShowProperties.Text = "Properties";
            this.btnViewShowProperties.Click += new System.EventHandler(this.actViewShowProperties);
            // 
            // btnViewShowStartPage
            // 
            this.btnViewShowStartPage.Image = ((System.Drawing.Image)(resources.GetObject("btnViewShowStartPage.Image")));
            this.btnViewShowStartPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnViewShowStartPage.Name = "btnViewShowStartPage";
            this.btnViewShowStartPage.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.btnViewShowStartPage.Size = new System.Drawing.Size(80, 20);
            this.btnViewShowStartPage.Text = "Start Page";
            this.btnViewShowStartPage.Click += new System.EventHandler(this.actViewShowStartPage);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tcbxRemeshingMethod,
            this.toolStripLabel1,
            this.ttbxTriangleCount,
            this.toolStripLabel2,
            this.ttbxRemovePercent,
            this.tsbRemove});
            this.toolStrip1.Location = new System.Drawing.Point(3, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(652, 25);
            this.toolStrip1.TabIndex = 2;
            // 
            // tcbxRemeshingMethod
            // 
            this.tcbxRemeshingMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tcbxRemeshingMethod.Name = "tcbxRemeshingMethod";
            this.tcbxRemeshingMethod.Size = new System.Drawing.Size(250, 25);
            this.tcbxRemeshingMethod.SelectedIndexChanged += new System.EventHandler(this.tcbxRemeshingMethod_SelectedIndexChanged);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(55, 22);
            this.toolStripLabel1.Text = "Triangles";
            // 
            // ttbxTriangleCount
            // 
            this.ttbxTriangleCount.Name = "ttbxTriangleCount";
            this.ttbxTriangleCount.Size = new System.Drawing.Size(100, 25);
            this.ttbxTriangleCount.TextChanged += new System.EventHandler(this.ttbxVerticesCount_TextChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel2.Text = "Percent";
            // 
            // ttbxRemovePercent
            // 
            this.ttbxRemovePercent.Name = "ttbxRemovePercent";
            this.ttbxRemovePercent.Size = new System.Drawing.Size(100, 25);
            this.ttbxRemovePercent.TextChanged += new System.EventHandler(this.ttbxVerticesCount_TextChanged);
            // 
            // tsbRemove
            // 
            this.tsbRemove.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.tsbRemove.Image = ((System.Drawing.Image)(resources.GetObject("tsbRemove.Image")));
            this.tsbRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemove.Name = "tsbRemove";
            this.tsbRemove.Size = new System.Drawing.Size(82, 22);
            this.tsbRemove.Text = "Remove";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(168, 22);
            this.toolStripMenuItem3.Text = "Remove Triangles";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(168, 22);
            this.toolStripMenuItem4.Text = "Remove Percent";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.pgrProgress,
            this.lblProgress,
            this.lblTrianglesVertices,
            this.lblStage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 331);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip1.Size = new System.Drawing.Size(720, 22);
            this.statusStrip1.TabIndex = 1;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(150, 17);
            this.toolStripStatusLabel1.Text = "Welcome to Terremesh";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pgrProgress
            // 
            this.pgrProgress.Name = "pgrProgress";
            this.pgrProgress.Size = new System.Drawing.Size(200, 16);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = false;
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(180, 17);
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTrianglesVertices
            // 
            this.lblTrianglesVertices.Name = "lblTrianglesVertices";
            this.lblTrianglesVertices.Size = new System.Drawing.Size(0, 17);
            // 
            // lblStage
            // 
            this.lblStage.AutoSize = false;
            this.lblStage.Name = "lblStage";
            this.lblStage.Size = new System.Drawing.Size(173, 17);
            this.lblStage.Spring = true;
            this.lblStage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblStage.Click += new System.EventHandler(this.lblStage_Click);
            // 
            // dlgOpenMesh
            // 
            this.dlgOpenMesh.DefaultExt = "obj";
            this.dlgOpenMesh.Filter = "OBJ files|*.obj|All files|*.*";
            this.dlgOpenMesh.Title = "Open OBJ file";
            // 
            // dlgSaveMesh
            // 
            this.dlgSaveMesh.DefaultExt = "obj";
            this.dlgSaveMesh.Filter = "OBJ files|*.obj|All files|*.*";
            this.dlgSaveMesh.Title = "Save OBJ file";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 353);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tbrView.ResumeLayout(false);
            this.tbrView.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mniViewShowProperties;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startPageToolStripMenuItem;
        private System.Windows.Forms.ToolStrip tbrView;
        private System.Windows.Forms.ToolStripButton btnViewShowOutput;
        private System.Windows.Forms.ToolStripButton btnViewShowProperties;
        private System.Windows.Forms.ToolStripButton btnViewShowStartPage;
        private System.Windows.Forms.ToolStripMenuItem rendererToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox tcbxRemeshingMethod;
        private System.Windows.Forms.ToolStripTextBox ttbxTriangleCount;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog dlgOpenMesh;
        private System.Windows.Forms.SaveFileDialog dlgSaveMesh;
        private System.Windows.Forms.ToolStripTextBox ttbxRemovePercent;
        private System.Windows.Forms.ToolStripProgressBar pgrProgress;
        private System.Windows.Forms.ToolStripStatusLabel lblStage;
        private System.Windows.Forms.ToolStripStatusLabel lblProgress;
        private System.Windows.Forms.ToolStripMenuItem clearWorkspaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSplitButton tsbRemove;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripStatusLabel lblTrianglesVertices;
    }
}