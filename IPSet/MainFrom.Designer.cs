namespace IPSet
{
    partial class MainFrom
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
            this.btnSettings = new System.Windows.Forms.Button();
            this.pnlHolder = new System.Windows.Forms.Panel();
            this.btnAddInterface = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.Image = global::IPSet.Properties.Resources.settings;
            this.btnSettings.Location = new System.Drawing.Point(558, 0);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(30, 31);
            this.btnSettings.TabIndex = 2;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // pnlHolder
            // 
            this.pnlHolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHolder.AutoScroll = true;
            this.pnlHolder.Location = new System.Drawing.Point(12, 48);
            this.pnlHolder.Name = "pnlHolder";
            this.pnlHolder.Size = new System.Drawing.Size(566, 437);
            this.pnlHolder.TabIndex = 1;
            // 
            // btnAddInterface
            // 
            this.btnAddInterface.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddInterface.Location = new System.Drawing.Point(12, 8);
            this.btnAddInterface.Name = "btnAddInterface";
            this.btnAddInterface.Size = new System.Drawing.Size(142, 34);
            this.btnAddInterface.TabIndex = 0;
            this.btnAddInterface.Text = "&Add interface";
            this.btnAddInterface.UseVisualStyleBackColor = true;
            this.btnAddInterface.Click += new System.EventHandler(this.btnAddInterface_Click);
            // 
            // MainFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 497);
            this.Controls.Add(this.btnAddInterface);
            this.Controls.Add(this.pnlHolder);
            this.Controls.Add(this.btnSettings);
            this.Icon = global::IPSet.Properties.Resources.networkicon;
            this.Name = "MainFrom";
            this.Text = "IP Set";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrom_FormClosing);
            this.Load += new System.EventHandler(this.MainFrom_Load);
            this.SizeChanged += new System.EventHandler(this.MainFrom_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Panel pnlHolder;
        private System.Windows.Forms.Button btnAddInterface;
    }
}

