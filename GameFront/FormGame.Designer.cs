namespace GameFront
{
    partial class FormGame
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
            this.GameBoardPanel = new System.Windows.Forms.Panel();
            this.Player1NameLabel = new System.Windows.Forms.Label();
            this.Player1ScoreLabel = new System.Windows.Forms.Label();
            this.Player2ScoreLabel = new System.Windows.Forms.Label();
            this.Player2NameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GameBoardPanel
            // 
            this.GameBoardPanel.Location = new System.Drawing.Point(87, 58);
            this.GameBoardPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GameBoardPanel.Name = "GameBoardPanel";
            this.GameBoardPanel.Size = new System.Drawing.Size(1133, 666);
            this.GameBoardPanel.TabIndex = 0;
            // 
            // Player1NameLabel
            // 
            this.Player1NameLabel.AutoSize = true;
            this.Player1NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Player1NameLabel.Location = new System.Drawing.Point(288, 23);
            this.Player1NameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player1NameLabel.Name = "Player1NameLabel";
            this.Player1NameLabel.Size = new System.Drawing.Size(84, 25);
            this.Player1NameLabel.TabIndex = 1;
            this.Player1NameLabel.Text = "Player1:";
            // 
            // Player1ScoreLabel
            // 
            this.Player1ScoreLabel.AutoSize = true;
            this.Player1ScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Player1ScoreLabel.Location = new System.Drawing.Point(422, 23);
            this.Player1ScoreLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player1ScoreLabel.Name = "Player1ScoreLabel";
            this.Player1ScoreLabel.Size = new System.Drawing.Size(23, 25);
            this.Player1ScoreLabel.TabIndex = 2;
            this.Player1ScoreLabel.Text = "0";
            // 
            // Player2ScoreLabel
            // 
            this.Player2ScoreLabel.AutoSize = true;
            this.Player2ScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Player2ScoreLabel.Location = new System.Drawing.Point(766, 23);
            this.Player2ScoreLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player2ScoreLabel.Name = "Player2ScoreLabel";
            this.Player2ScoreLabel.Size = new System.Drawing.Size(23, 25);
            this.Player2ScoreLabel.TabIndex = 4;
            this.Player2ScoreLabel.Text = "0";
            // 
            // Player2NameLabel
            // 
            this.Player2NameLabel.AutoSize = true;
            this.Player2NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Player2NameLabel.Location = new System.Drawing.Point(632, 23);
            this.Player2NameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Player2NameLabel.Name = "Player2NameLabel";
            this.Player2NameLabel.Size = new System.Drawing.Size(84, 25);
            this.Player2NameLabel.TabIndex = 3;
            this.Player2NameLabel.Text = "Player2:";
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 764);
            this.Controls.Add(this.Player2ScoreLabel);
            this.Controls.Add(this.Player2NameLabel);
            this.Controls.Add(this.Player1ScoreLabel);
            this.Controls.Add(this.Player1NameLabel);
            this.Controls.Add(this.GameBoardPanel);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "checkers";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel GameBoardPanel;
        private System.Windows.Forms.Label Player1NameLabel;
        private System.Windows.Forms.Label Player1ScoreLabel;
        private System.Windows.Forms.Label Player2ScoreLabel;
        private System.Windows.Forms.Label Player2NameLabel;
    }
}

