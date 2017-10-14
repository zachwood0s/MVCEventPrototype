namespace ExampleNumberGame
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
            this.uxTextbox = new System.Windows.Forms.TextBox();
            this.uxOKButton = new System.Windows.Forms.Button();
            this.uxOutputLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // uxTextbox
            // 
            this.uxTextbox.Location = new System.Drawing.Point(13, 13);
            this.uxTextbox.Name = "uxTextbox";
            this.uxTextbox.Size = new System.Drawing.Size(259, 20);
            this.uxTextbox.TabIndex = 0;
            // 
            // uxOKButton
            // 
            this.uxOKButton.Location = new System.Drawing.Point(104, 39);
            this.uxOKButton.Name = "uxOKButton";
            this.uxOKButton.Size = new System.Drawing.Size(75, 23);
            this.uxOKButton.TabIndex = 1;
            this.uxOKButton.Text = "OK";
            this.uxOKButton.UseVisualStyleBackColor = true;
            this.uxOKButton.Click += new System.EventHandler(this.uxOKButton_Click);
            // 
            // uxOutputLabel
            // 
            this.uxOutputLabel.AutoSize = true;
            this.uxOutputLabel.Location = new System.Drawing.Point(12, 68);
            this.uxOutputLabel.Name = "uxOutputLabel";
            this.uxOutputLabel.Size = new System.Drawing.Size(61, 13);
            this.uxOutputLabel.TabIndex = 2;
            this.uxOutputLabel.Text = "Let\'s Play...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.uxOutputLabel);
            this.Controls.Add(this.uxOKButton);
            this.Controls.Add(this.uxTextbox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox uxTextbox;
        private System.Windows.Forms.Button uxOKButton;
        private System.Windows.Forms.Label uxOutputLabel;
    }
}

