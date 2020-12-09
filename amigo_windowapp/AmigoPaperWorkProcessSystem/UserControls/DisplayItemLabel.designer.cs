﻿namespace AmigoPaperWorkProcessSystem.UserControls
{
    partial class DisplayItemLabel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.displayItemAsteriskLabel1 = new AmigoPaperWorkProcessSystem.UserControls.DisplayItemAsteriskLabel();
            this.displayItemTextLabel1 = new AmigoPaperWorkProcessSystem.UserControls.DisplayItemTextLabel();
            this.SuspendLayout();
            // 
            // displayItemAsteriskLabel1
            // 
            this.displayItemAsteriskLabel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.displayItemAsteriskLabel1.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayItemAsteriskLabel1.ForeColor = System.Drawing.Color.Red;
            this.displayItemAsteriskLabel1.Location = new System.Drawing.Point(789, 0);
            this.displayItemAsteriskLabel1.Name = "displayItemAsteriskLabel1";
            this.displayItemAsteriskLabel1.Size = new System.Drawing.Size(15, 81);
            this.displayItemAsteriskLabel1.TabIndex = 0;
            this.displayItemAsteriskLabel1.Text = "*";
            // 
            // displayItemTextLabel1
            // 
            this.displayItemTextLabel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.displayItemTextLabel1.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayItemTextLabel1.Location = new System.Drawing.Point(0, 0);
            this.displayItemTextLabel1.Name = "displayItemTextLabel1";
            this.displayItemTextLabel1.Size = new System.Drawing.Size(790, 81);
            this.displayItemTextLabel1.TabIndex = 1;
            this.displayItemTextLabel1.Text = " 表示件数";
            this.displayItemTextLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DisplayItemLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.displayItemAsteriskLabel1);
            this.Controls.Add(this.displayItemTextLabel1);
            this.Name = "DisplayItemLabel";
            this.Size = new System.Drawing.Size(804, 81);
            this.ResumeLayout(false);

        }

        #endregion

        private DisplayItemAsteriskLabel displayItemAsteriskLabel1;
        private DisplayItemTextLabel displayItemTextLabel1;
    }
}
