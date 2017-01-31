namespace WindowsFormsApplication1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Ext_Config_Button = new System.Windows.Forms.Button();
            this.Schedule_Button = new System.Windows.Forms.Button();
            this.Sync_Button = new System.Windows.Forms.Button();
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.Clien_Based_Panel = new System.Windows.Forms.Panel();
            this.treeView_Client = new System.Windows.Forms.TreeView();
            this.RemoveNode_Client_button = new System.Windows.Forms.Button();
            this.AddNode_Client = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.LastTimeSynchronizedLabel = new System.Windows.Forms.Label();
            this.LastSyncCompletedTimeLabel = new System.Windows.Forms.Label();
            this.SynchronizationStatusValueLabel = new System.Windows.Forms.Label();
            this.SynchronizationStatusLabel = new System.Windows.Forms.Label();
            this.Clien_Based_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Ext_Config_Button
            // 
            this.Ext_Config_Button.Location = new System.Drawing.Point(35, 329);
            this.Ext_Config_Button.Margin = new System.Windows.Forms.Padding(4);
            this.Ext_Config_Button.Name = "Ext_Config_Button";
            this.Ext_Config_Button.Size = new System.Drawing.Size(111, 28);
            this.Ext_Config_Button.TabIndex = 0;
            this.Ext_Config_Button.Text = "Ext. Config";
            this.Ext_Config_Button.UseVisualStyleBackColor = true;
            this.Ext_Config_Button.Click += new System.EventHandler(this.button1_Click);
            // 
            // Schedule_Button
            // 
            this.Schedule_Button.Location = new System.Drawing.Point(153, 329);
            this.Schedule_Button.Margin = new System.Windows.Forms.Padding(4);
            this.Schedule_Button.Name = "Schedule_Button";
            this.Schedule_Button.Size = new System.Drawing.Size(111, 28);
            this.Schedule_Button.TabIndex = 3;
            this.Schedule_Button.Text = "Schedule";
            this.Schedule_Button.UseVisualStyleBackColor = true;
            this.Schedule_Button.Click += new System.EventHandler(this.Schedule_Button_Click);
            // 
            // Sync_Button
            // 
            this.Sync_Button.Location = new System.Drawing.Point(440, 329);
            this.Sync_Button.Margin = new System.Windows.Forms.Padding(4);
            this.Sync_Button.Name = "Sync_Button";
            this.Sync_Button.Size = new System.Drawing.Size(139, 28);
            this.Sync_Button.TabIndex = 4;
            this.Sync_Button.Text = "SYNCHRONIZE!!!!";
            this.Sync_Button.UseVisualStyleBackColor = true;
            this.Sync_Button.Click += new System.EventHandler(this.Sync_Button_Click);
            // 
            // TrayIcon
            // 
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.Text = "Folder Synchronizator";
            this.TrayIcon.Visible = true;
            this.TrayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseClick);
            // 
            // Clien_Based_Panel
            // 
            this.Clien_Based_Panel.Controls.Add(this.treeView_Client);
            this.Clien_Based_Panel.Controls.Add(this.RemoveNode_Client_button);
            this.Clien_Based_Panel.Controls.Add(this.AddNode_Client);
            this.Clien_Based_Panel.Location = new System.Drawing.Point(35, 15);
            this.Clien_Based_Panel.Margin = new System.Windows.Forms.Padding(4);
            this.Clien_Based_Panel.Name = "Clien_Based_Panel";
            this.Clien_Based_Panel.Size = new System.Drawing.Size(559, 306);
            this.Clien_Based_Panel.TabIndex = 5;
            // 
            // treeView_Client
            // 
            this.treeView_Client.Location = new System.Drawing.Point(4, 4);
            this.treeView_Client.Margin = new System.Windows.Forms.Padding(4);
            this.treeView_Client.Name = "treeView_Client";
            this.treeView_Client.Size = new System.Drawing.Size(539, 258);
            this.treeView_Client.TabIndex = 3;
            // 
            // RemoveNode_Client_button
            // 
            this.RemoveNode_Client_button.Location = new System.Drawing.Point(365, 270);
            this.RemoveNode_Client_button.Margin = new System.Windows.Forms.Padding(4);
            this.RemoveNode_Client_button.Name = "RemoveNode_Client_button";
            this.RemoveNode_Client_button.Size = new System.Drawing.Size(179, 33);
            this.RemoveNode_Client_button.TabIndex = 2;
            this.RemoveNode_Client_button.Text = "Remove Node";
            this.RemoveNode_Client_button.UseVisualStyleBackColor = true;
            this.RemoveNode_Client_button.Click += new System.EventHandler(this.RemoveNode_Client_button_Click);
            // 
            // AddNode_Client
            // 
            this.AddNode_Client.Location = new System.Drawing.Point(0, 270);
            this.AddNode_Client.Margin = new System.Windows.Forms.Padding(4);
            this.AddNode_Client.Name = "AddNode_Client";
            this.AddNode_Client.Size = new System.Drawing.Size(179, 33);
            this.AddNode_Client.TabIndex = 1;
            this.AddNode_Client.Text = "Add Node";
            this.AddNode_Client.UseVisualStyleBackColor = true;
            this.AddNode_Client.Click += new System.EventHandler(this.AddNode_Client_Click);
            // 
            // LastTimeSynchronizedLabel
            // 
            this.LastTimeSynchronizedLabel.AutoSize = true;
            this.LastTimeSynchronizedLabel.ForeColor = System.Drawing.Color.Green;
            this.LastTimeSynchronizedLabel.Location = new System.Drawing.Point(32, 362);
            this.LastTimeSynchronizedLabel.Name = "LastTimeSynchronizedLabel";
            this.LastTimeSynchronizedLabel.Size = new System.Drawing.Size(164, 17);
            this.LastTimeSynchronizedLabel.TabIndex = 6;
            this.LastTimeSynchronizedLabel.Text = "Last Time Synchronized:";
            // 
            // LastSyncCompletedTimeLabel
            // 
            this.LastSyncCompletedTimeLabel.AutoSize = true;
            this.LastSyncCompletedTimeLabel.ForeColor = System.Drawing.Color.Green;
            this.LastSyncCompletedTimeLabel.Location = new System.Drawing.Point(202, 362);
            this.LastSyncCompletedTimeLabel.Name = "LastSyncCompletedTimeLabel";
            this.LastSyncCompletedTimeLabel.Size = new System.Drawing.Size(42, 17);
            this.LastSyncCompletedTimeLabel.TabIndex = 7;
            this.LastSyncCompletedTimeLabel.Text = "None";
            // 
            // SynchronizationStatusValueLabel
            // 
            this.SynchronizationStatusValueLabel.AutoSize = true;
            this.SynchronizationStatusValueLabel.ForeColor = System.Drawing.Color.Blue;
            this.SynchronizationStatusValueLabel.Location = new System.Drawing.Point(499, 362);
            this.SynchronizationStatusValueLabel.Name = "SynchronizationStatusValueLabel";
            this.SynchronizationStatusValueLabel.Size = new System.Drawing.Size(78, 17);
            this.SynchronizationStatusValueLabel.TabIndex = 9;
            this.SynchronizationStatusValueLabel.Text = "Not started";
            // 
            // SynchronizationStatusLabel
            // 
            this.SynchronizationStatusLabel.AutoSize = true;
            this.SynchronizationStatusLabel.ForeColor = System.Drawing.Color.Blue;
            this.SynchronizationStatusLabel.Location = new System.Drawing.Point(406, 362);
            this.SynchronizationStatusLabel.Name = "SynchronizationStatusLabel";
            this.SynchronizationStatusLabel.Size = new System.Drawing.Size(87, 17);
            this.SynchronizationStatusLabel.TabIndex = 8;
            this.SynchronizationStatusLabel.Text = "Sync Status:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 388);
            this.Controls.Add(this.SynchronizationStatusValueLabel);
            this.Controls.Add(this.SynchronizationStatusLabel);
            this.Controls.Add(this.LastSyncCompletedTimeLabel);
            this.Controls.Add(this.LastTimeSynchronizedLabel);
            this.Controls.Add(this.Sync_Button);
            this.Controls.Add(this.Schedule_Button);
            this.Controls.Add(this.Clien_Based_Panel);
            this.Controls.Add(this.Ext_Config_Button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(642, 435);
            this.MinimumSize = new System.Drawing.Size(642, 435);
            this.Name = "MainForm";
            this.Text = "Folder Synchronizator";
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.Clien_Based_Panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Ext_Config_Button;
        private System.Windows.Forms.Button Schedule_Button;
        private System.Windows.Forms.Button Sync_Button;
        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.Panel Clien_Based_Panel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button RemoveNode_Client_button;
        private System.Windows.Forms.Button AddNode_Client;
        private System.Windows.Forms.TreeView treeView_Client;
        private System.Windows.Forms.Label LastTimeSynchronizedLabel;
        private System.Windows.Forms.Label LastSyncCompletedTimeLabel;
        private System.Windows.Forms.Label SynchronizationStatusValueLabel;
        private System.Windows.Forms.Label SynchronizationStatusLabel;
    }
}

