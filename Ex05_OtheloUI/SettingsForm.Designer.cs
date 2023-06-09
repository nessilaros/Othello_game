﻿namespace Ex05_OtheloUI
{
    public partial class SettingsForm
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
            this.buttonBoardSize = new System.Windows.Forms.Button();
            this.buttonPlayAgainstComputer = new System.Windows.Forms.Button();
            this.buttonPlayAgainstFriend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonBoardSize
            // 
            this.buttonBoardSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonBoardSize.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonBoardSize.Location = new System.Drawing.Point(13, 28);
            this.buttonBoardSize.Name = "buttonBoardSize";
            this.buttonBoardSize.Size = new System.Drawing.Size(492, 58);
            this.buttonBoardSize.TabIndex = 0;
            this.buttonBoardSize.Text = "Board Size : 6x6 (click to increase)";
            this.buttonBoardSize.UseVisualStyleBackColor = true;
            this.buttonBoardSize.Click += new System.EventHandler(this.buttonBoardSize_Click);
            // 
            // buttonPlayAgainstComputer
            // 
            this.buttonPlayAgainstComputer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonPlayAgainstComputer.Location = new System.Drawing.Point(12, 136);
            this.buttonPlayAgainstComputer.Name = "buttonPlayAgainstComputer";
            this.buttonPlayAgainstComputer.Size = new System.Drawing.Size(233, 67);
            this.buttonPlayAgainstComputer.TabIndex = 1;
            this.buttonPlayAgainstComputer.Text = "Play against the computer";
            this.buttonPlayAgainstComputer.UseVisualStyleBackColor = true;
            this.buttonPlayAgainstComputer.Click += new System.EventHandler(this.buttonPlayAgainstComputer_Click);
            // 
            // buttonPlayAgainstFriend
            // 
            this.buttonPlayAgainstFriend.BackColor = System.Drawing.SystemColors.Control;
            this.buttonPlayAgainstFriend.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonPlayAgainstFriend.Location = new System.Drawing.Point(271, 136);
            this.buttonPlayAgainstFriend.Name = "buttonPlayAgainstFriend";
            this.buttonPlayAgainstFriend.Size = new System.Drawing.Size(233, 67);
            this.buttonPlayAgainstFriend.TabIndex = 2;
            this.buttonPlayAgainstFriend.Text = "Play against your friend";
            this.buttonPlayAgainstFriend.UseVisualStyleBackColor = false;
            this.buttonPlayAgainstFriend.Click += new System.EventHandler(this.buttonPlayAgainstFriend_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 233);
            this.Controls.Add(this.buttonPlayAgainstFriend);
            this.Controls.Add(this.buttonPlayAgainstComputer);
            this.Controls.Add(this.buttonBoardSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Othello - Game Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonBoardSize;
        private System.Windows.Forms.Button buttonPlayAgainstComputer;
        private System.Windows.Forms.Button buttonPlayAgainstFriend;
    }
}