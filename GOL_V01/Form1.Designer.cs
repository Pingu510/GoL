namespace GOL
{
    partial class Form1
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
            this.GameGrid_Panel = new System.Windows.Forms.Panel();
            this.lstBxSavedGames = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lblNameOfGame = new System.Windows.Forms.Label();
            this.txbNameOfTheGame = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStopSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.PlayTimer = new System.Windows.Forms.Timer(this.components);
            this.btnRandom = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GameGrid_Panel
            // 
            this.GameGrid_Panel.BackColor = System.Drawing.Color.White;
            this.GameGrid_Panel.Location = new System.Drawing.Point(230, 12);
            this.GameGrid_Panel.Name = "GameGrid_Panel";
            this.GameGrid_Panel.Size = new System.Drawing.Size(600, 600);
            this.GameGrid_Panel.TabIndex = 0;
            // 
            // lstBxSavedGames
            // 
            this.lstBxSavedGames.FormattingEnabled = true;
            this.lstBxSavedGames.Location = new System.Drawing.Point(6, 19);
            this.lstBxSavedGames.Name = "lstBxSavedGames";
            this.lstBxSavedGames.Size = new System.Drawing.Size(179, 147);
            this.lstBxSavedGames.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnLoad);
            this.groupBox1.Controls.Add(this.lstBxSavedGames);
            this.groupBox1.Controls.Add(this.lblNameOfGame);
            this.groupBox1.Controls.Add(this.txbNameOfTheGame);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(202, 264);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Saved games";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(110, 172);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(6, 172);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 9;
            this.btnLoad.Tag = "Load";
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lblNameOfGame
            // 
            this.lblNameOfGame.AutoSize = true;
            this.lblNameOfGame.Location = new System.Drawing.Point(6, 208);
            this.lblNameOfGame.Name = "lblNameOfGame";
            this.lblNameOfGame.Size = new System.Drawing.Size(63, 13);
            this.lblNameOfGame.TabIndex = 7;
            this.lblNameOfGame.Text = "GameName";
            // 
            // txbNameOfTheGame
            // 
            this.txbNameOfTheGame.Location = new System.Drawing.Point(6, 227);
            this.txbNameOfTheGame.Name = "txbNameOfTheGame";
            this.txbNameOfTheGame.Size = new System.Drawing.Size(100, 20);
            this.txbNameOfTheGame.TabIndex = 4;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(72, 335);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStopSave
            // 
            this.btnStopSave.Location = new System.Drawing.Point(60, 429);
            this.btnStopSave.Name = "btnStopSave";
            this.btnStopSave.Size = new System.Drawing.Size(107, 23);
            this.btnStopSave.TabIndex = 6;
            this.btnStopSave.Text = "STOPP/SAVE";
            this.btnStopSave.UseVisualStyleBackColor = true;
            this.btnStopSave.Click += new System.EventHandler(this.btnStopSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(6, 583);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(72, 378);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 9;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // PlayTimer
            // 
            this.PlayTimer.Interval = 50;
            this.PlayTimer.Tick += new System.EventHandler(this.PlayTimer_Tick);
            // 
            // btnRandom
            // 
            this.btnRandom.Location = new System.Drawing.Point(72, 294);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(75, 23);
            this.btnRandom.TabIndex = 11;
            this.btnRandom.Text = "Randomize";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 618);
            this.Controls.Add(this.btnRandom);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStopSave);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GameGrid_Panel);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "GOL";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel GameGrid_Panel;
        private System.Windows.Forms.ListBox lstBxSavedGames;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txbNameOfTheGame;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStopSave;
        private System.Windows.Forms.Label lblNameOfGame;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Timer PlayTimer;
        private System.Windows.Forms.Button btnRandom;
    }
}

