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
      this.shootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.esciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.button2 = new System.Windows.Forms.Button();
      this.button3 = new System.Windows.Forms.Button();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.opzioniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.autostartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.contextMenuStrip1.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.button1.AutoSize = true;
      this.button1.Location = new System.Drawing.Point(12, 53);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(95, 25);
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
      this.linkLabel1.Location = new System.Drawing.Point(12, 24);
      this.linkLabel1.MaximumSize = new System.Drawing.Size(276, 25);
      this.linkLabel1.MinimumSize = new System.Drawing.Size(276, 25);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(276, 25);
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
      this.notifyIcon1.Text = "Scroto";
      this.notifyIcon1.Visible = true;
      this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shootToolStripMenuItem,
            this.cropToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.esciToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(119, 92);
      // 
      // shootToolStripMenuItem
      // 
      this.shootToolStripMenuItem.Name = "shootToolStripMenuItem";
      this.shootToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
      this.shootToolStripMenuItem.Text = "Shoot!";
      this.shootToolStripMenuItem.Click += new System.EventHandler(this.shootToolStripMenuItem_Click);
      // 
      // cropToolStripMenuItem
      // 
      this.cropToolStripMenuItem.Name = "cropToolStripMenuItem";
      this.cropToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
      this.cropToolStripMenuItem.Text = "Crop";
      this.cropToolStripMenuItem.Click += new System.EventHandler(this.cropToolStripMenuItem_Click);
      // 
      // windowToolStripMenuItem
      // 
      this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
      this.windowToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
      this.windowToolStripMenuItem.Text = "Window";
      this.windowToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
      // 
      // esciToolStripMenuItem
      // 
      this.esciToolStripMenuItem.Name = "esciToolStripMenuItem";
      this.esciToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
      this.esciToolStripMenuItem.Text = "Esci";
      this.esciToolStripMenuItem.Click += new System.EventHandler(this.esciToolStripMenuItem_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(207, 52);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(88, 26);
      this.button2.TabIndex = 2;
      this.button2.Text = "Window";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // button3
      // 
      this.button3.Location = new System.Drawing.Point(113, 52);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(88, 26);
      this.button3.TabIndex = 3;
      this.button3.Text = "Crop";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opzioniToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(304, 24);
      this.menuStrip1.TabIndex = 4;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // opzioniToolStripMenuItem
      // 
      this.opzioniToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autostartToolStripMenuItem});
      this.opzioniToolStripMenuItem.Name = "opzioniToolStripMenuItem";
      this.opzioniToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
      this.opzioniToolStripMenuItem.Text = "Opzioni";
      // 
      // autostartToolStripMenuItem
      // 
      this.autostartToolStripMenuItem.CheckOnClick = true;
      this.autostartToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.autostartToolStripMenuItem.Name = "autostartToolStripMenuItem";
      this.autostartToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.autostartToolStripMenuItem.Text = "Autostart";
      this.autostartToolStripMenuItem.CheckedChanged += new System.EventHandler(this.autostartToolStripMenuItem_CheckedChanged);
      // 
      // Scroto
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(304, 88);
      this.Controls.Add(this.menuStrip1);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.linkLabel1);
      this.Controls.Add(this.button1);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(320, 140);
      this.MinimumSize = new System.Drawing.Size(300, 110);
      this.Name = "Scroto";
      this.Opacity = 0.8D;
      this.ShowIcon = false;
      this.Text = "Scroto";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Scroto_FormClosed);
      this.contextMenuStrip1.ResumeLayout(false);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
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
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.ToolStripMenuItem cropToolStripMenuItem;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem opzioniToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem autostartToolStripMenuItem;
  }
}

