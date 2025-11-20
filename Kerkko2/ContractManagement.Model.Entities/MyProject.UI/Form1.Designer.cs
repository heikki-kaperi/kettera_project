using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace MyProject.UI
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
                textBox1 = new TextBox();
                textBox2 = new TextBox();
                button1 = new Button();
                button2 = new Button();
                button3 = new Button();
                label1 = new Label();
                label2 = new Label();
                txtPassword = new TextBox();
                txtUsername = new TextBox();
                SuspendLayout();
                // 
                // textBox1
                // 
                textBox1.Location = new Point(258, 100);
                textBox1.Name = "textBox1";
                textBox1.Size = new Size(255, 27);
                textBox1.TabIndex = 0;
                textBox1.Text = "CONTRACT MANAGEMENT SYSTEM";
                // 
                // textBox2
                // 
                textBox2.Location = new Point(341, 148);
                textBox2.Name = "textBox2";
                textBox2.Size = new Size(52, 27);
                textBox2.TabIndex = 1;
                textBox2.Text = "LOGIN";
                // 
                // button1
                // 
                button1.Location = new Point(282, 195);
                button1.Name = "button1";
                button1.Size = new Size(172, 29);
                button1.TabIndex = 2;
                button1.Text = "Administrator Login";
                button1.UseVisualStyleBackColor = true;
                button1.Click += btnAdmin_Click;
                // 
                // button2
                // 
                button2.Location = new Point(285, 241);
                button2.Name = "button2";
                button2.Size = new Size(169, 29);
                button2.TabIndex = 3;
                button2.Text = "Internal User Login";
                button2.UseVisualStyleBackColor = true;
                button2.Click += btnInternal_Click;
                // 
                // button3
                // 
                button3.Location = new Point(285, 299);
                button3.Name = "button3";
                button3.Size = new Size(169, 29);
                button3.TabIndex = 4;
                button3.Text = "External User Login";
                button3.UseVisualStyleBackColor = true;
                button3.Click += btnExternal_Click;
                // 
                // label1
                // 
                label1.AutoSize = true;
                label1.Location = new Point(326, 199);
                label1.Name = "label1";
                label1.Size = new Size(78, 20);
                label1.TabIndex = 5;
                label1.Text = "Username:";
                // 
                // label2
                // 
                label2.AutoSize = true;
                label2.Location = new Point(331, 273);
                label2.Name = "label2";
                label2.Size = new Size(73, 20);
                label2.TabIndex = 7;
                label2.Text = "Password:";
                // 
                // txtPassword
                // 
                txtPassword.Location = new Point(305, 311);
                txtPassword.Name = "txtPassword";
                txtPassword.Size = new Size(125, 27);
                txtPassword.TabIndex = 8;
                txtPassword.UseSystemPasswordChar = true;
                // 
                // txtUsername
                // 
                txtUsername.Location = new Point(305, 230);
                txtUsername.Name = "txtUsername";
                txtUsername.Size = new Size(125, 27);
                txtUsername.TabIndex = 6;
                // 
                // Form1
                // 
                AutoScaleDimensions = new SizeF(8F, 20F);
                AutoScaleMode = AutoScaleMode.Font;
                ClientSize = new Size(800, 450);
                Controls.Add(txtPassword);
                Controls.Add(label2);
                Controls.Add(txtUsername);
                Controls.Add(label1);
                Controls.Add(button3);
                Controls.Add(button2);
                Controls.Add(button1);
                Controls.Add(textBox2);
                Controls.Add(textBox1);
                Name = "Form1";
                Text = "Form1";
                ResumeLayout(false);
                PerformLayout();
            }

            #endregion

            private TextBox textBox1;
            private TextBox textBox2;
            private Button button1;
            private Button button2;
            private Button button3;
            private Label label1;
            private Label label2;
            private TextBox txtPassword;
            private TextBox txtUsername;
        }
    }


