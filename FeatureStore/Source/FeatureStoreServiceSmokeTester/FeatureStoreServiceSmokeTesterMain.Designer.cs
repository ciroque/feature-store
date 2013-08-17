namespace FeatureStoreServiceSmokeTester
{
    partial class FeatureStoreServiceSmokeTesterMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FeatureStoreServiceSmokeTesterMain));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.m_MasterTabs = new System.Windows.Forms.TabControl();
            this.m_MasterToolStrip = new System.Windows.Forms.ToolStrip();
            this.m_ExitTSB = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.m_MasterToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.m_MasterTabs);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(955, 666);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(955, 691);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "m_MasterToolStripContainer";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.m_MasterToolStrip);
            // 
            // m_MasterTabs
            // 
            this.m_MasterTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_MasterTabs.Location = new System.Drawing.Point(0, 0);
            this.m_MasterTabs.Name = "m_MasterTabs";
            this.m_MasterTabs.SelectedIndex = 0;
            this.m_MasterTabs.Size = new System.Drawing.Size(955, 666);
            this.m_MasterTabs.TabIndex = 0;
            this.m_MasterTabs.Tag = "";
            // 
            // m_MasterToolStrip
            // 
            this.m_MasterToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.m_MasterToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ExitTSB});
            this.m_MasterToolStrip.Location = new System.Drawing.Point(3, 0);
            this.m_MasterToolStrip.Name = "m_MasterToolStrip";
            this.m_MasterToolStrip.Size = new System.Drawing.Size(57, 25);
            this.m_MasterToolStrip.TabIndex = 0;
            this.m_MasterToolStrip.Text = "Main Tools";
            // 
            // m_ExitTSB
            // 
            this.m_ExitTSB.Image = ((System.Drawing.Image)(resources.GetObject("m_ExitTSB.Image")));
            this.m_ExitTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_ExitTSB.Name = "m_ExitTSB";
            this.m_ExitTSB.Size = new System.Drawing.Size(45, 22);
            this.m_ExitTSB.Tag = "Exit";
            this.m_ExitTSB.Text = "E&xit";
            this.m_ExitTSB.Click += new System.EventHandler(this.HandleExitTsbClick);
            // 
            // FeatureStoreServiceSmokeTesterMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 691);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FeatureStoreServiceSmokeTesterMain";
            this.Text = "Feature Store Service Smoke Tester";
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.m_MasterToolStrip.ResumeLayout(false);
            this.m_MasterToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip m_MasterToolStrip;
        private System.Windows.Forms.ToolStripButton m_ExitTSB;
        private System.Windows.Forms.TabControl m_MasterTabs;
    }
}

