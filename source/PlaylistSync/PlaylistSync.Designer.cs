namespace PlaylistSync
{
    partial class PlaylistSync
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.playlistLoc = new System.Windows.Forms.TextBox();
            this.destDir = new System.Windows.Forms.TextBox();
            this.PlaylistLoc_btn = new System.Windows.Forms.Button();
            this.destDir_btn = new System.Windows.Forms.Button();
            this.startSync_btn = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.deleteEmptyDirs = new System.Windows.Forms.CheckBox();
            this.deleteFromDest = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Playlist:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Destination folder:";
            // 
            // playlistLoc
            // 
            this.playlistLoc.Location = new System.Drawing.Point(110, 6);
            this.playlistLoc.Name = "playlistLoc";
            this.playlistLoc.Size = new System.Drawing.Size(278, 20);
            this.playlistLoc.TabIndex = 2;
            this.playlistLoc.TextChanged += new System.EventHandler(this.PlaylistLoc_TextChanged);
            // 
            // destDir
            // 
            this.destDir.Location = new System.Drawing.Point(110, 45);
            this.destDir.Name = "destDir";
            this.destDir.Size = new System.Drawing.Size(278, 20);
            this.destDir.TabIndex = 3;
            // 
            // PlaylistLoc_btn
            // 
            this.PlaylistLoc_btn.Location = new System.Drawing.Point(394, 4);
            this.PlaylistLoc_btn.Name = "PlaylistLoc_btn";
            this.PlaylistLoc_btn.Size = new System.Drawing.Size(75, 23);
            this.PlaylistLoc_btn.TabIndex = 4;
            this.PlaylistLoc_btn.Text = "Browse...";
            this.PlaylistLoc_btn.UseVisualStyleBackColor = true;
            this.PlaylistLoc_btn.Click += new System.EventHandler(this.PlaylistLoc_btn_Click);
            // 
            // destDir_btn
            // 
            this.destDir_btn.Location = new System.Drawing.Point(394, 43);
            this.destDir_btn.Name = "destDir_btn";
            this.destDir_btn.Size = new System.Drawing.Size(75, 23);
            this.destDir_btn.TabIndex = 5;
            this.destDir_btn.Text = "Browse...";
            this.destDir_btn.UseVisualStyleBackColor = true;
            this.destDir_btn.Click += new System.EventHandler(this.DestDir_btn_Click);
            // 
            // startSync_btn
            // 
            this.startSync_btn.Location = new System.Drawing.Point(12, 359);
            this.startSync_btn.Name = "startSync_btn";
            this.startSync_btn.Size = new System.Drawing.Size(457, 23);
            this.startSync_btn.TabIndex = 6;
            this.startSync_btn.Text = "Start sync";
            this.startSync_btn.UseVisualStyleBackColor = true;
            this.startSync_btn.Click += new System.EventHandler(this.StartSync_btn_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 177);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(457, 166);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.deleteEmptyDirs);
            this.groupBox1.Controls.Add(this.deleteFromDest);
            this.groupBox1.Location = new System.Drawing.Point(12, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(457, 81);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(85, 46);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(356, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "front.jpg; front.jpeg; front.png; cover.jpg; cover.png; folder.jpg; folder.png";
            this.toolTip1.SetToolTip(this.textBox1, "Include these files regardless of whether they are included or not in the playlis" +
        "t");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Include files:";
            this.toolTip1.SetToolTip(this.label3, "Include these files regardless of whether they are included or not in the playlis" +
        "t");
            // 
            // deleteEmptyDirs
            // 
            this.deleteEmptyDirs.AutoSize = true;
            this.deleteEmptyDirs.Location = new System.Drawing.Point(156, 19);
            this.deleteEmptyDirs.Name = "deleteEmptyDirs";
            this.deleteEmptyDirs.Size = new System.Drawing.Size(139, 17);
            this.deleteEmptyDirs.TabIndex = 1;
            this.deleteEmptyDirs.Text = "Delete empty directories";
            this.deleteEmptyDirs.UseVisualStyleBackColor = true;
            // 
            // deleteFromDest
            // 
            this.deleteFromDest.AccessibleDescription = "";
            this.deleteFromDest.AutoSize = true;
            this.deleteFromDest.Checked = true;
            this.deleteFromDest.CheckState = System.Windows.Forms.CheckState.Checked;
            this.deleteFromDest.Location = new System.Drawing.Point(16, 19);
            this.deleteFromDest.Name = "deleteFromDest";
            this.deleteFromDest.Size = new System.Drawing.Size(134, 17);
            this.deleteFromDest.TabIndex = 0;
            this.deleteFromDest.Text = "Delete from destination";
            this.toolTip1.SetToolTip(this.deleteFromDest, "Delete files and directories in destination which do not appear in source");
            this.deleteFromDest.UseVisualStyleBackColor = true;
            // 
            // PlaylistSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 397);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.startSync_btn);
            this.Controls.Add(this.destDir_btn);
            this.Controls.Add(this.PlaylistLoc_btn);
            this.Controls.Add(this.destDir);
            this.Controls.Add(this.playlistLoc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "PlaylistSync";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox playlistLoc;
        private System.Windows.Forms.TextBox destDir;
        private System.Windows.Forms.Button PlaylistLoc_btn;
        private System.Windows.Forms.Button destDir_btn;
        private System.Windows.Forms.Button startSync_btn;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox deleteFromDest;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox deleteEmptyDirs;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
    }
}

