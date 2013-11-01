namespace TPL1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnProcessImages = new System.Windows.Forms.Button();
            this.btnProcessImages_Parallel = new System.Windows.Forms.Button();
            this.btnProcessImages_Task = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(38, 46);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(608, 25);
            this.textBox1.TabIndex = 0;
            // 
            // btnProcessImages
            // 
            this.btnProcessImages.Location = new System.Drawing.Point(38, 148);
            this.btnProcessImages.Name = "btnProcessImages";
            this.btnProcessImages.Size = new System.Drawing.Size(258, 52);
            this.btnProcessImages.TabIndex = 1;
            this.btnProcessImages.Text = "Process Images ForEach";
            this.btnProcessImages.UseVisualStyleBackColor = true;
            this.btnProcessImages.Click += new System.EventHandler(this.btnProcessImages_Click);
            // 
            // btnProcessImages_Parallel
            // 
            this.btnProcessImages_Parallel.Location = new System.Drawing.Point(324, 148);
            this.btnProcessImages_Parallel.Name = "btnProcessImages_Parallel";
            this.btnProcessImages_Parallel.Size = new System.Drawing.Size(258, 52);
            this.btnProcessImages_Parallel.TabIndex = 2;
            this.btnProcessImages_Parallel.Text = "Process Images Parallel.ForEach";
            this.btnProcessImages_Parallel.UseVisualStyleBackColor = true;
            this.btnProcessImages_Parallel.Click += new System.EventHandler(this.btnProcessImages_Parallel_Click);
            // 
            // btnProcessImages_Task
            // 
            this.btnProcessImages_Task.Location = new System.Drawing.Point(612, 148);
            this.btnProcessImages_Task.Name = "btnProcessImages_Task";
            this.btnProcessImages_Task.Size = new System.Drawing.Size(258, 52);
            this.btnProcessImages_Task.TabIndex = 3;
            this.btnProcessImages_Task.Text = "Process Images Task";
            this.btnProcessImages_Task.UseVisualStyleBackColor = true;
            this.btnProcessImages_Task.Click += new System.EventHandler(this.btnProcessImages_Task_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 348);
            this.Controls.Add(this.btnProcessImages_Task);
            this.Controls.Add(this.btnProcessImages_Parallel);
            this.Controls.Add(this.btnProcessImages);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnProcessImages;
        private System.Windows.Forms.Button btnProcessImages_Parallel;
        private System.Windows.Forms.Button btnProcessImages_Task;
    }
}

