namespace SimpleElasticsearchTester
{
    partial class ElasticManager
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
            groupBox1 = new GroupBox();
            fieldChallengeId = new TextBox();
            fieldScore = new NumericUpDown();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            fieldUserId = new TextBox();
            label5 = new Label();
            fieldDeviceId = new TextBox();
            button2 = new Button();
            fieldStartAt = new DateTimePicker();
            fieldDuration = new NumericUpDown();
            label4 = new Label();
            label3 = new Label();
            btnSubmit = new Button();
            tabPage4 = new TabPage();
            rbCreate = new RadioButton();
            groupBox3 = new GroupBox();
            rbDelete = new RadioButton();
            rbRead = new RadioButton();
            groupBox2 = new GroupBox();
            groupBox4 = new GroupBox();
            fieldIndex = new TextBox();
            button1 = new Button();
            button3 = new Button();
            fieldDocIdSearch = new TextBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)fieldScore).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fieldDuration).BeginInit();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(fieldChallengeId);
            groupBox1.Controls.Add(fieldScore);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(fieldUserId);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(fieldDeviceId);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(fieldStartAt);
            groupBox1.Controls.Add(fieldDuration);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Location = new Point(22, 191);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(837, 228);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Exercise class fields";
            // 
            // fieldChallengeId
            // 
            fieldChallengeId.Location = new Point(99, 126);
            fieldChallengeId.Margin = new Padding(3, 4, 3, 4);
            fieldChallengeId.Name = "fieldChallengeId";
            fieldChallengeId.Size = new Size(230, 27);
            fieldChallengeId.TabIndex = 21;
            // 
            // fieldScore
            // 
            fieldScore.Location = new Point(503, 83);
            fieldScore.Name = "fieldScore";
            fieldScore.Size = new Size(118, 27);
            fieldScore.TabIndex = 20;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(442, 86);
            label8.Name = "label8";
            label8.Size = new Size(46, 20);
            label8.TabIndex = 19;
            label8.Text = "Score";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(10, 126);
            label7.Name = "label7";
            label7.Size = new Size(90, 20);
            label7.TabIndex = 17;
            label7.Text = "ChallengeID";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(45, 87);
            label6.Name = "label6";
            label6.Size = new Size(53, 20);
            label6.TabIndex = 16;
            label6.Text = "UserID";
            // 
            // fieldUserId
            // 
            fieldUserId.Location = new Point(99, 83);
            fieldUserId.Margin = new Padding(3, 4, 3, 4);
            fieldUserId.Name = "fieldUserId";
            fieldUserId.Size = new Size(230, 27);
            fieldUserId.TabIndex = 15;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(29, 52);
            label5.Name = "label5";
            label5.Size = new Size(69, 20);
            label5.TabIndex = 14;
            label5.Text = "DeviceID";
            // 
            // fieldDeviceId
            // 
            fieldDeviceId.Location = new Point(99, 45);
            fieldDeviceId.Margin = new Padding(3, 4, 3, 4);
            fieldDeviceId.Name = "fieldDeviceId";
            fieldDeviceId.Size = new Size(230, 27);
            fieldDeviceId.TabIndex = 13;
            // 
            // button2
            // 
            button2.Location = new Point(693, 124);
            button2.Name = "button2";
            button2.Size = new Size(65, 27);
            button2.TabIndex = 12;
            button2.Text = "NOW";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // fieldStartAt
            // 
            fieldStartAt.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            fieldStartAt.Format = DateTimePickerFormat.Custom;
            fieldStartAt.Location = new Point(502, 124);
            fieldStartAt.MinDate = new DateTime(2023, 4, 1, 0, 0, 0, 0);
            fieldStartAt.Name = "fieldStartAt";
            fieldStartAt.Size = new Size(185, 27);
            fieldStartAt.TabIndex = 11;
            // 
            // fieldDuration
            // 
            fieldDuration.Location = new Point(502, 45);
            fieldDuration.Name = "fieldDuration";
            fieldDuration.Size = new Size(118, 27);
            fieldDuration.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(437, 124);
            label4.Name = "label4";
            label4.Size = new Size(59, 20);
            label4.TabIndex = 4;
            label4.Text = "Start At";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(401, 45);
            label3.Name = "label3";
            label3.Size = new Size(87, 20);
            label3.TabIndex = 3;
            label3.Text = "Duration (s)";
            // 
            // btnSubmit
            // 
            btnSubmit.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSubmit.AutoSize = true;
            btnSubmit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSubmit.FlatStyle = FlatStyle.System;
            btnSubmit.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnSubmit.Location = new Point(776, 447);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(83, 37);
            btnSubmit.TabIndex = 0;
            btnSubmit.Text = "GO ->";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += btnSubmit_Click;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(0, 0);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(200, 100);
            tabPage4.TabIndex = 0;
            tabPage4.Text = "Delete";
            // 
            // rbCreate
            // 
            rbCreate.AutoSize = true;
            rbCreate.Location = new Point(123, 28);
            rbCreate.Name = "rbCreate";
            rbCreate.Size = new Size(128, 24);
            rbCreate.TabIndex = 4;
            rbCreate.TabStop = true;
            rbCreate.Text = "Create/Update";
            rbCreate.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(rbDelete);
            groupBox3.Controls.Add(rbCreate);
            groupBox3.Controls.Add(rbRead);
            groupBox3.Location = new Point(259, 15);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(599, 75);
            groupBox3.TabIndex = 5;
            groupBox3.TabStop = false;
            groupBox3.Text = "Select an option:";
            // 
            // rbDelete
            // 
            rbDelete.AutoSize = true;
            rbDelete.Location = new Point(365, 28);
            rbDelete.Name = "rbDelete";
            rbDelete.Size = new Size(74, 24);
            rbDelete.TabIndex = 7;
            rbDelete.TabStop = true;
            rbDelete.Text = "Delete";
            rbDelete.UseVisualStyleBackColor = true;
            // 
            // rbRead
            // 
            rbRead.AutoSize = true;
            rbRead.Location = new Point(270, 28);
            rbRead.Name = "rbRead";
            rbRead.Size = new Size(74, 24);
            rbRead.TabIndex = 5;
            rbRead.TabStop = true;
            rbRead.Text = "Search";
            rbRead.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(fieldDocIdSearch);
            groupBox2.Location = new Point(22, 95);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(231, 73);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "ID to Search/Delete:";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(fieldIndex);
            groupBox4.Location = new Point(22, 15);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(231, 75);
            groupBox4.TabIndex = 7;
            groupBox4.TabStop = false;
            groupBox4.Text = "Index to use (eg. 'exercises')";
            // 
            // fieldIndex
            // 
            fieldIndex.Location = new Point(7, 27);
            fieldIndex.Name = "fieldIndex";
            fieldIndex.Size = new Size(217, 27);
            fieldIndex.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(121, 452);
            button1.Name = "button1";
            button1.Size = new Size(94, 32);
            button1.TabIndex = 8;
            button1.Text = "Testing...";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button3
            // 
            button3.Location = new Point(21, 452);
            button3.Name = "button3";
            button3.Size = new Size(94, 32);
            button3.TabIndex = 9;
            button3.Text = "About";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // fieldDocIdSearch
            // 
            fieldDocIdSearch.Location = new Point(10, 26);
            fieldDocIdSearch.Name = "fieldDocIdSearch";
            fieldDocIdSearch.Size = new Size(214, 27);
            fieldDocIdSearch.TabIndex = 0;
            // 
            // ElasticManager
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(879, 508);
            Controls.Add(button3);
            Controls.Add(button1);
            Controls.Add(groupBox4);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(groupBox3);
            Controls.Add(btnSubmit);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "ElasticManager";
            Text = "Simple Elasticsearch tester";
            Load += ElasticManager_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)fieldScore).EndInit();
            ((System.ComponentModel.ISupportInitialize)fieldDuration).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TabPage tabPage4;
        private GroupBox groupBox1;
        private Button button2;
        private DateTimePicker fieldStartAt;
        private NumericUpDown fieldDuration;
        private Label label4;
        private Label label3;
        private Button btnSubmit;
        private RadioButton rbCreate;
        private GroupBox groupBox3;
        private RadioButton rbDelete;
        private RadioButton rbRead;
        private GroupBox groupBox2;
        private GroupBox groupBox4;
        private TextBox fieldIndex;
        private Button button1;
        private Button button3;
        private TextBox fieldDeviceId;
        private Label label6;
        private TextBox fieldUserId;
        private Label label5;
        private Label label7;
        private NumericUpDown fieldScore;
        private Label label8;
        private TextBox fieldChallengeId;
        private TextBox fieldDocIdSearch;
    }
}