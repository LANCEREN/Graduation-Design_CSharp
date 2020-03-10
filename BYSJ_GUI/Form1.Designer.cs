namespace BYSJ_GUI
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.BrowseFilesButton = new System.Windows.Forms.Button();
            this.BrowseFoldersButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BrowseFilesButton
            // 
            this.BrowseFilesButton.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrowseFilesButton.Location = new System.Drawing.Point(521, 478);
            this.BrowseFilesButton.Name = "BrowseFilesButton";
            this.BrowseFilesButton.Size = new System.Drawing.Size(95, 31);
            this.BrowseFilesButton.TabIndex = 0;
            this.BrowseFilesButton.Text = "Browse Files";
            this.BrowseFilesButton.UseVisualStyleBackColor = true;
            this.BrowseFilesButton.Click += new System.EventHandler(this.BrowseFilesButton_Click);
            // 
            // BrowseFoldersButton
            // 
            this.BrowseFoldersButton.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrowseFoldersButton.Location = new System.Drawing.Point(622, 478);
            this.BrowseFoldersButton.Name = "BrowseFoldersButton";
            this.BrowseFoldersButton.Size = new System.Drawing.Size(95, 31);
            this.BrowseFoldersButton.TabIndex = 1;
            this.BrowseFoldersButton.Text = "Browse Folders";
            this.BrowseFoldersButton.UseVisualStyleBackColor = true;
            this.BrowseFoldersButton.Click += new System.EventHandler(this.BrowseFoldersButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1381, 744);
            this.Controls.Add(this.BrowseFoldersButton);
            this.Controls.Add(this.BrowseFilesButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BrowseFilesButton;
        private System.Windows.Forms.Button BrowseFoldersButton;
    }
}

