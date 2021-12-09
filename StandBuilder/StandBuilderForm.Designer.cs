
namespace StandBuilder
{
    partial class StandBuilderForm
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
            this.ConsoleOutputTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ServerTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.UsernameTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.ScriptAutoCloseCheckBox = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.VDSwitchNameTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.GroupFolderNameTextBox = new System.Windows.Forms.TextBox();
            this.StandNameTextBox = new System.Windows.Forms.TextBox();
            this.BuildStandButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SnapshotNameTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.CloseButton = new System.Windows.Forms.Button();
            this.BuildAnotherButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.DatacenterComboBox = new System.Windows.Forms.ComboBox();
            this.DatastoreComboBox = new System.Windows.Forms.ComboBox();
            this.HostComboBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.MachineTemplatesFolderNameTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConsoleOutputTextBox
            // 
            this.ConsoleOutputTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ConsoleOutputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ConsoleOutputTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ConsoleOutputTextBox.Location = new System.Drawing.Point(12, 221);
            this.ConsoleOutputTextBox.Multiline = true;
            this.ConsoleOutputTextBox.Name = "ConsoleOutputTextBox";
            this.ConsoleOutputTextBox.ReadOnly = true;
            this.ConsoleOutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.ConsoleOutputTextBox.Size = new System.Drawing.Size(758, 323);
            this.ConsoleOutputTextBox.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 356F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 252F));
            this.tableLayoutPanel1.Controls.Add(this.ServerTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.UsernameTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.PasswordTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.ConnectButton, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.ScriptAutoCloseCheckBox, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(758, 203);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // ServerTextBox
            // 
            this.ServerTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ServerTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ServerTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ServerTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ServerTextBox.Location = new System.Drawing.Point(153, 15);
            this.ServerTextBox.Name = "ServerTextBox";
            this.ServerTextBox.Size = new System.Drawing.Size(341, 20);
            this.ServerTextBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "vCenter Server IP Address:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Username:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Password:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.UsernameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.UsernameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UsernameTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.UsernameTextBox.Location = new System.Drawing.Point(153, 65);
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.Size = new System.Drawing.Size(341, 20);
            this.UsernameTextBox.TabIndex = 6;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.PasswordTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.PasswordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PasswordTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.PasswordTextBox.Location = new System.Drawing.Point(153, 115);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(341, 20);
            this.PasswordTextBox.TabIndex = 7;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ConnectButton.AutoSize = true;
            this.ConnectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ConnectButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ConnectButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ConnectButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.ConnectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConnectButton.Location = new System.Drawing.Point(649, 164);
            this.ConnectButton.Margin = new System.Windows.Forms.Padding(10);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(99, 25);
            this.ConnectButton.TabIndex = 2;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = false;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // ScriptAutoCloseCheckBox
            // 
            this.ScriptAutoCloseCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ScriptAutoCloseCheckBox.AutoSize = true;
            this.ScriptAutoCloseCheckBox.Location = new System.Drawing.Point(153, 168);
            this.ScriptAutoCloseCheckBox.Name = "ScriptAutoCloseCheckBox";
            this.ScriptAutoCloseCheckBox.Size = new System.Drawing.Size(188, 17);
            this.ScriptAutoCloseCheckBox.TabIndex = 8;
            this.ScriptAutoCloseCheckBox.Text = "Close script windows automatically";
            this.ScriptAutoCloseCheckBox.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 429F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 129F));
            this.tableLayoutPanel3.Controls.Add(this.label10, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.VDSwitchNameTextBox, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.SnapshotNameTextBox, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.StandNameTextBox, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.GroupFolderNameTextBox, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label11, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.MachineTemplatesFolderNameTextBox, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.BuildStandButton, 2, 4);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(758, 203);
            this.tableLayoutPanel3.TabIndex = 5;
            this.tableLayoutPanel3.Visible = false;
            // 
            // VDSwitchNameTextBox
            // 
            this.VDSwitchNameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.VDSwitchNameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.VDSwitchNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VDSwitchNameTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.VDSwitchNameTextBox.Location = new System.Drawing.Point(203, 171);
            this.VDSwitchNameTextBox.Name = "VDSwitchNameTextBox";
            this.VDSwitchNameTextBox.Size = new System.Drawing.Size(341, 20);
            this.VDSwitchNameTextBox.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 175);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(152, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Virtual distributed switch name:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Stand name:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Group folder name:";
            // 
            // GroupFolderNameTextBox
            // 
            this.GroupFolderNameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.GroupFolderNameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.GroupFolderNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GroupFolderNameTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.GroupFolderNameTextBox.Location = new System.Drawing.Point(203, 50);
            this.GroupFolderNameTextBox.Name = "GroupFolderNameTextBox";
            this.GroupFolderNameTextBox.Size = new System.Drawing.Size(341, 20);
            this.GroupFolderNameTextBox.TabIndex = 9;
            // 
            // StandNameTextBox
            // 
            this.StandNameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.StandNameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.StandNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StandNameTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.StandNameTextBox.Location = new System.Drawing.Point(203, 90);
            this.StandNameTextBox.Name = "StandNameTextBox";
            this.StandNameTextBox.Size = new System.Drawing.Size(341, 20);
            this.StandNameTextBox.TabIndex = 10;
            // 
            // BuildStandButton
            // 
            this.BuildStandButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BuildStandButton.AutoSize = true;
            this.BuildStandButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.BuildStandButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.BuildStandButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.BuildStandButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.BuildStandButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BuildStandButton.Location = new System.Drawing.Point(655, 170);
            this.BuildStandButton.Margin = new System.Windows.Forms.Padding(10);
            this.BuildStandButton.Name = "BuildStandButton";
            this.BuildStandButton.Size = new System.Drawing.Size(93, 23);
            this.BuildStandButton.TabIndex = 7;
            this.BuildStandButton.Text = "Build stand";
            this.BuildStandButton.UseVisualStyleBackColor = false;
            this.BuildStandButton.Click += new System.EventHandler(this.BuildStandButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Snapshot name:";
            // 
            // SnapshotNameTextBox
            // 
            this.SnapshotNameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.SnapshotNameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.SnapshotNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SnapshotNameTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SnapshotNameTextBox.Location = new System.Drawing.Point(203, 130);
            this.SnapshotNameTextBox.Name = "SnapshotNameTextBox";
            this.SnapshotNameTextBox.Size = new System.Drawing.Size(341, 20);
            this.SnapshotNameTextBox.TabIndex = 14;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.CloseButton, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.BuildAnotherButton, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(758, 203);
            this.tableLayoutPanel4.TabIndex = 6;
            this.tableLayoutPanel4.Visible = false;
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CloseButton.AutoSize = true;
            this.CloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CloseButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.CloseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Location = new System.Drawing.Point(522, 89);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(10);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(93, 25);
            this.CloseButton.TabIndex = 8;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // BuildAnotherButton
            // 
            this.BuildAnotherButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BuildAnotherButton.AutoSize = true;
            this.BuildAnotherButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.BuildAnotherButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.BuildAnotherButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.BuildAnotherButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.BuildAnotherButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BuildAnotherButton.Location = new System.Drawing.Point(133, 89);
            this.BuildAnotherButton.Margin = new System.Windows.Forms.Padding(10);
            this.BuildAnotherButton.Name = "BuildAnotherButton";
            this.BuildAnotherButton.Size = new System.Drawing.Size(113, 25);
            this.BuildAnotherButton.TabIndex = 7;
            this.BuildAnotherButton.Text = "Build Another Stand";
            this.BuildAnotherButton.UseVisualStyleBackColor = false;
            this.BuildAnotherButton.Click += new System.EventHandler(this.BuildAnotherButton_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 356F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 252F));
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.ConfirmButton, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.DatacenterComboBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.DatastoreComboBox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.HostComboBox, 1, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(758, 203);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Datacenter:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Datastore:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 118);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Host:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ConfirmButton.AutoSize = true;
            this.ConfirmButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ConfirmButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ConfirmButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ConfirmButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.ConfirmButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConfirmButton.Location = new System.Drawing.Point(649, 164);
            this.ConfirmButton.Margin = new System.Windows.Forms.Padding(10);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(99, 25);
            this.ConfirmButton.TabIndex = 2;
            this.ConfirmButton.Text = "Confirm";
            this.ConfirmButton.UseVisualStyleBackColor = false;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // DatacenterComboBox
            // 
            this.DatacenterComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DatacenterComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.DatacenterComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DatacenterComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DatacenterComboBox.FormattingEnabled = true;
            this.DatacenterComboBox.Location = new System.Drawing.Point(153, 14);
            this.DatacenterComboBox.Name = "DatacenterComboBox";
            this.DatacenterComboBox.Size = new System.Drawing.Size(317, 21);
            this.DatacenterComboBox.TabIndex = 3;
            // 
            // DatastoreComboBox
            // 
            this.DatastoreComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DatastoreComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.DatastoreComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DatastoreComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DatastoreComboBox.FormattingEnabled = true;
            this.DatastoreComboBox.Location = new System.Drawing.Point(153, 64);
            this.DatastoreComboBox.Name = "DatastoreComboBox";
            this.DatastoreComboBox.Size = new System.Drawing.Size(317, 21);
            this.DatastoreComboBox.TabIndex = 4;
            // 
            // HostComboBox
            // 
            this.HostComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.HostComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.HostComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HostComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.HostComboBox.FormattingEnabled = true;
            this.HostComboBox.Location = new System.Drawing.Point(153, 114);
            this.HostComboBox.Name = "HostComboBox";
            this.HostComboBox.Size = new System.Drawing.Size(317, 21);
            this.HostComboBox.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(157, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Machine templates folder name:";
            // 
            // MachineTemplatesFolderNameTextBox
            // 
            this.MachineTemplatesFolderNameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.MachineTemplatesFolderNameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.MachineTemplatesFolderNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MachineTemplatesFolderNameTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MachineTemplatesFolderNameTextBox.Location = new System.Drawing.Point(203, 10);
            this.MachineTemplatesFolderNameTextBox.Name = "MachineTemplatesFolderNameTextBox";
            this.MachineTemplatesFolderNameTextBox.Size = new System.Drawing.Size(341, 20);
            this.MachineTemplatesFolderNameTextBox.TabIndex = 9;
            // 
            // StandBuilderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(782, 556);
            this.Controls.Add(this.ConsoleOutputTextBox);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel4);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StandBuilderForm";
            this.Text = "StandBuilder";
            this.Load += new System.EventHandler(this.StandBuilderForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ConsoleOutputTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ServerTextBox;
        private System.Windows.Forms.TextBox UsernameTextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button BuildStandButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox GroupFolderNameTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button BuildAnotherButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox StandNameTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button ConfirmButton;
        private System.Windows.Forms.ComboBox DatacenterComboBox;
        private System.Windows.Forms.ComboBox DatastoreComboBox;
        private System.Windows.Forms.ComboBox HostComboBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox VDSwitchNameTextBox;
        private System.Windows.Forms.TextBox SnapshotNameTextBox;
        private System.Windows.Forms.CheckBox ScriptAutoCloseCheckBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox MachineTemplatesFolderNameTextBox;
    }
}

