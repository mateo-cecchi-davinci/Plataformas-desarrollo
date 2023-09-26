namespace Proyecto
{
    partial class Form2
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
            label1 = new Label();
            tbMail = new TextBox();
            tbClave = new TextBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(317, 70);
            label1.Name = "label1";
            label1.Size = new Size(169, 37);
            label1.TabIndex = 0;
            label1.Text = "Iniciar sesion";
            // 
            // tbMail
            // 
            tbMail.ForeColor = Color.DimGray;
            tbMail.Location = new Point(321, 166);
            tbMail.Name = "tbMail";
            tbMail.PlaceholderText = "Ingrese su mail";
            tbMail.Size = new Size(161, 23);
            tbMail.TabIndex = 1;
            // 
            // tbClave
            // 
            tbClave.ForeColor = Color.DimGray;
            tbClave.Location = new Point(321, 229);
            tbClave.Name = "tbClave";
            tbClave.PlaceholderText = "Ingrese su contraseña";
            tbClave.Size = new Size(161, 23);
            tbClave.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(321, 305);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 5;
            button1.Text = "Ingresar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnIngresar;
            // 
            // button2
            // 
            button2.Location = new Point(407, 305);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 6;
            button2.Text = "Registrarse";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(tbClave);
            Controls.Add(tbMail);
            Controls.Add(label1);
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form2";
            WindowState = FormWindowState.Maximized;
            Load += Form2_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tbMail;
        private TextBox tbClave;
        private Button button1;
        private Button button2;
    }
}