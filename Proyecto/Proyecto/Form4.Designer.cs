namespace Proyecto
{
    partial class Form4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            label1 = new Label();
            tbMail = new TextBox();
            tbRepClave = new TextBox();
            registrarse = new Button();
            tbNombre = new TextBox();
            button1 = new Button();
            tbClave = new TextBox();
            tbDni = new TextBox();
            tbApellido = new TextBox();
            checkBox_IsAdmin = new CheckBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(317, 64);
            label1.Name = "label1";
            label1.Size = new Size(146, 37);
            label1.TabIndex = 1;
            label1.Text = "Registrarse";
            // 
            // tbMail
            // 
            tbMail.ForeColor = Color.DimGray;
            tbMail.Location = new Point(317, 240);
            tbMail.Name = "tbMail";
            tbMail.PlaceholderText = "Ingrese su mail";
            tbMail.Size = new Size(161, 23);
            tbMail.TabIndex = 5;
            tbMail.Enter += tbMail_Enter_1;
            tbMail.Leave += tbMail_Leave_1;
            // 
            // tbRepClave
            // 
            tbRepClave.ForeColor = Color.DimGray;
            tbRepClave.Location = new Point(317, 317);
            tbRepClave.Name = "tbRepClave";
            tbRepClave.PlaceholderText = "Repita su contraseña";
            tbRepClave.Size = new Size(161, 23);
            tbRepClave.TabIndex = 7;
            tbRepClave.Enter += tbRepClave_Enter_1;
            tbRepClave.Leave += tbRepClave_Leave_1;
            // 
            // registrarse
            // 
            registrarse.Location = new Point(357, 390);
            registrarse.Name = "registrarse";
            registrarse.Size = new Size(75, 23);
            registrarse.TabIndex = 8;
            registrarse.Text = "Registrarse";
            registrarse.UseVisualStyleBackColor = true;
            registrarse.Click += button2_Click;
            // 
            // tbNombre
            // 
            tbNombre.ForeColor = Color.DimGray;
            tbNombre.Location = new Point(317, 120);
            tbNombre.Name = "tbNombre";
            tbNombre.PlaceholderText = "Ingrese su nombre";
            tbNombre.Size = new Size(161, 23);
            tbNombre.TabIndex = 2;
            tbNombre.Enter += tbNombre_Enter_1;
            tbNombre.Leave += tbNombre_Leave_1;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ActiveCaption;
            button1.ForeColor = SystemColors.ControlText;
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(27, 23);
            button1.TabIndex = 9;
            button1.Text = "<";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // tbClave
            // 
            tbClave.ForeColor = Color.DimGray;
            tbClave.Location = new Point(317, 279);
            tbClave.Name = "tbClave";
            tbClave.PlaceholderText = "Ingrese su contraseña";
            tbClave.Size = new Size(161, 23);
            tbClave.TabIndex = 6;
            tbClave.Enter += tbClave_Enter_1;
            tbClave.Leave += tbClave_Leave_1;
            // 
            // tbDni
            // 
            tbDni.ForeColor = Color.DimGray;
            tbDni.Location = new Point(317, 199);
            tbDni.Name = "tbDni";
            tbDni.PlaceholderText = "Ingrese su DNI";
            tbDni.Size = new Size(161, 23);
            tbDni.TabIndex = 4;
            tbDni.Enter += tbDni_Enter_1;
            tbDni.Leave += tbDni_Leave_1;
            // 
            // tbApellido
            // 
            tbApellido.ForeColor = Color.DimGray;
            tbApellido.Location = new Point(317, 158);
            tbApellido.Name = "tbApellido";
            tbApellido.PlaceholderText = "Ingrese su apellido";
            tbApellido.Size = new Size(161, 23);
            tbApellido.TabIndex = 3;
            tbApellido.Enter += tbApellido_Enter_1;
            tbApellido.Leave += tbApellido_Leave_1;
            // 
            // checkBox_IsAdmin
            // 
            checkBox_IsAdmin.ForeColor = Color.FromArgb(64, 64, 64);
            checkBox_IsAdmin.Location = new Point(317, 356);
            checkBox_IsAdmin.Name = "checkBox_IsAdmin";
            checkBox_IsAdmin.Size = new Size(161, 19);
            checkBox_IsAdmin.TabIndex = 10;
            checkBox_IsAdmin.Text = "Administrador";
            checkBox_IsAdmin.UseVisualStyleBackColor = true;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(800, 450);
            Controls.Add(checkBox_IsAdmin);
            Controls.Add(tbApellido);
            Controls.Add(tbDni);
            Controls.Add(tbClave);
            Controls.Add(button1);
            Controls.Add(tbNombre);
            Controls.Add(registrarse);
            Controls.Add(tbRepClave);
            Controls.Add(tbMail);
            Controls.Add(label1);
            Name = "Form4";
            Text = "Form4";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbMail;
        private Label label1;
        private TextBox tbRepClave;
        private Button registrarse;
        private TextBox tbNombre;
        private Button button1;
        private TextBox tbClave;
        private TextBox tbDni;
        private TextBox tbApellido;
        private CheckBox checkBox_IsAdmin;
    }
}