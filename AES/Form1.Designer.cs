namespace AES
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            sourceTb = new TextBox();
            cipherTb = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            button1 = new Button();
            button2 = new Button();
            keyLabel = new Label();
            rbText = new RadioButton();
            rbNumbers = new RadioButton();
            label5 = new Label();
            textBox2 = new TextBox();
            SuspendLayout();
            // 
            // sourceTb
            // 
            sourceTb.Font = new Font("Segoe UI", 18F);
            sourceTb.Location = new Point(185, 73);
            sourceTb.Margin = new Padding(4);
            sourceTb.Multiline = true;
            sourceTb.Name = "sourceTb";
            sourceTb.ScrollBars = ScrollBars.Vertical;
            sourceTb.Size = new Size(447, 533);
            sourceTb.TabIndex = 2;
            // 
            // cipherTb
            // 
            cipherTb.Font = new Font("Segoe UI", 18F);
            cipherTb.Location = new Point(898, 73);
            cipherTb.Margin = new Padding(4);
            cipherTb.Multiline = true;
            cipherTb.Name = "cipherTb";
            cipherTb.ScrollBars = ScrollBars.Vertical;
            cipherTb.Size = new Size(430, 533);
            cipherTb.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(584, 671);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(129, 21);
            label1.TabIndex = 4;
            label1.Text = "Значение ключа";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F);
            label2.Location = new Point(292, 37);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(189, 32);
            label2.TabIndex = 4;
            label2.Text = "Исходный текст";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 18F);
            label4.Location = new Point(977, 37);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(263, 32);
            label4.TabIndex = 4;
            label4.Text = "Зашифрованный текст";
            // 
            // button1
            // 
            button1.BackColor = Color.SpringGreen;
            button1.Location = new Point(584, 734);
            button1.Name = "button1";
            button1.Size = new Size(129, 37);
            button1.TabIndex = 5;
            button1.Text = "Шифровать";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Lime;
            button2.Location = new Point(823, 734);
            button2.Name = "button2";
            button2.Size = new Size(130, 37);
            button2.TabIndex = 6;
            button2.Text = "Дешифровать";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // keyLabel
            // 
            keyLabel.AutoSize = true;
            keyLabel.Font = new Font("Segoe UI", 12F);
            keyLabel.Location = new Point(584, 855);
            keyLabel.Margin = new Padding(4, 0, 4, 0);
            keyLabel.Name = "keyLabel";
            keyLabel.Size = new Size(0, 21);
            keyLabel.TabIndex = 4;
            // 
            // rbText
            // 
            rbText.AutoSize = true;
            rbText.Location = new Point(599, 805);
            rbText.Name = "rbText";
            rbText.Size = new Size(150, 25);
            rbText.TabIndex = 7;
            rbText.TabStop = true;
            rbText.Text = "Работа с текстом";
            rbText.UseVisualStyleBackColor = true;
            // 
            // rbNumbers
            // 
            rbNumbers.AutoSize = true;
            rbNumbers.Location = new Point(799, 805);
            rbNumbers.Name = "rbNumbers";
            rbNumbers.Size = new Size(154, 25);
            rbNumbers.TabIndex = 8;
            rbNumbers.TabStop = true;
            rbNumbers.Text = "Работа с числами";
            rbNumbers.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Impact", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label5.Location = new Point(721, 37);
            label5.Name = "label5";
            label5.Size = new Size(100, 34);
            label5.TabIndex = 9;
            label5.Text = "AES-128";
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI", 18F);
            textBox2.Location = new Point(721, 659);
            textBox2.Margin = new Padding(4);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(197, 39);
            textBox2.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1507, 955);
            Controls.Add(label5);
            Controls.Add(rbNumbers);
            Controls.Add(rbText);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(keyLabel);
            Controls.Add(label1);
            Controls.Add(cipherTb);
            Controls.Add(sourceTb);
            Controls.Add(textBox2);
            Font = new Font("Segoe UI", 12F);
            Margin = new Padding(4);
            Name = "Form1";
            Text = "AES-128";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox sourceTb;
        private TextBox cipherTb;
        private Label label1;
        private Label label2;
        private Label label4;
        private Button button1;
        private Button button2;
        private Label keyLabel;
        private RadioButton rbText;
        private RadioButton rbNumbers;
        private Label label5;
        private TextBox textBox2;
    }
}
