namespace Scroto
{
  partial class Scroto
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scroto));
      this.button1 = new System.Windows.Forms.Button();
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.esciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.shootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.button1.AutoSize = true;
      this.button1.Location = new System.Drawing.Point(112, 43);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(88, 26);
      this.button1.TabIndex = 0;
      this.button1.Text = "Shoot!";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // linkLabel1
      // 
      this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.linkLabel1.Location = new System.Drawing.Point(12, 9);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(288, 28);
      this.linkLabel1.TabIndex = 1;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "linkLabel1";
      this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.linkLabel1.Visible = false;
      this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      // 
      // notifyIcon1
      // 
      this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
      this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
      this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
      this.notifyIcon1.Text = "notifyIcon1";
      this.notifyIcon1.Visible = true;
      this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shootToolStripMenuItem,
            this.esciToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(107, 48);
      // 
      // esciToolStripMenuItem
      // 
      this.esciToolStripMenuItem.Name = "esciToolStripMenuItem";
      this.esciToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
      this.esciToolStripMenuItem.Text = "Esci";
      this.esciToolStripMenuItem.Click += new System.EventHandler(this.esciToolStripMenuItem_Click);
      // 
      // shootToolStripMenuItem
      // 
      this.shootToolStripMenuItem.Name = "shootToolStripMenuItem";
      this.shootToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
      this.shootToolStripMenuItem.Text = "Shoot!";
      this.shootToolStripMenuItem.Click += new System.EventHandler(this.shootToolStripMenuItem_Click);
      // 
      // Scroto
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(312, 83);
      this.Controls.Add(this.linkLabel1);
      this.Controls.Add(this.button1);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(320, 110);
      this.MinimumSize = new System.Drawing.Size(320, 110);
      this.Name = "Scroto";
      this.Opacity = 0.8D;
      this.ShowIcon = false;
      this.Text = "Scroto";
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.NotifyIcon notifyIcon1;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem esciToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem shootToolStripMenuItem;
  }
}

