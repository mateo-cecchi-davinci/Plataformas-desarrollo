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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            txtMail = new TextBox();
            txtPass = new TextBox();
            btnIngresar = new Button();
            linkPass = new LinkLabel();
            linkRegistro = new LinkLabel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 122, 204);
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 479);
            panel1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 108);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(250, 200);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calibri", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.DimGray;
            label2.Location = new Point(427, 83);
            label2.Name = "label2";
            label2.Size = new Size(183, 33);
            label2.TabIndex = 8;
            label2.Text = "INICIAR SESION";
            // 
            // txtMail
            // 
            txtMail.BackColor = Color.FromArgb(15, 15, 15);
            txtMail.BorderStyle = BorderStyle.FixedSingle;
            txtMail.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtMail.ForeColor = Color.DimGray;
            txtMail.Location = new Point(313, 155);
            txtMail.Name = "txtMail";
            txtMail.PlaceholderText = "  Ingrese su mail";
            txtMail.Size = new Size(408, 27);
            txtMail.TabIndex = 2;
            txtMail.Enter += txtMail_Enter;
            txtMail.Leave += txtMail_Leave;
            // 
            // txtPass
            // 
            txtPass.BackColor = Color.FromArgb(15, 15, 15);
            txtPass.BorderStyle = BorderStyle.FixedSingle;
            txtPass.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtPass.ForeColor = Color.DimGray;
            txtPass.Location = new Point(313, 210);
            txtPass.Name = "txtPass";
            txtPass.PlaceholderText = "  Ingrese su contraseña";
            txtPass.Size = new Size(408, 27);
            txtPass.TabIndex = 3;
            txtPass.Enter += txtPass_Enter;
            txtPass.Leave += txtPass_Leave;
            // 
            // btnIngresar
            // 
            btnIngresar.BackColor = Color.FromArgb(40, 40, 40);
            btnIngresar.FlatAppearance.BorderSize = 0;
            btnIngresar.FlatAppearance.MouseDownBackColor = Color.FromArgb(28, 28, 28);
            btnIngresar.FlatAppearance.MouseOverBackColor = Color.FromArgb(64, 64, 64);
            btnIngresar.FlatStyle = FlatStyle.Flat;
            btnIngresar.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnIngresar.ForeColor = Color.LightGray;
            btnIngresar.Location = new Point(313, 285);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(408, 40);
            btnIngresar.TabIndex = 4;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = false;
            btnIngresar.Click += btnIniciarSesion;
            // 
            // linkPass
            // 
            linkPass.ActiveLinkColor = Color.FromArgb(0, 122, 204);
            linkPass.AutoSize = true;
            linkPass.Font = new Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point);
            linkPass.LinkColor = Color.DimGray;
            linkPass.Location = new Point(386, 348);
            linkPass.Name = "linkPass";
            linkPass.Size = new Size(163, 14);
            linkPass.TabIndex = 1;
            linkPass.TabStop = true;
            linkPass.Text = "¿Ha olvidado la contraseña?";
            // 
            // linkRegistro
            // 
            linkRegistro.ActiveLinkColor = Color.FromArgb(0, 122, 204);
            linkRegistro.AutoSize = true;
            linkRegistro.Font = new Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point);
            linkRegistro.LinkColor = Color.DimGray;
            linkRegistro.Location = new Point(565, 348);
            linkRegistro.Name = "linkRegistro";
            linkRegistro.Size = new Size(69, 14);
            linkRegistro.TabIndex = 0;
            linkRegistro.TabStop = true;
            linkRegistro.Text = "Registrarse";
            linkRegistro.Click += button2_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 15, 15);
            ClientSize = new Size(780, 479);
            Controls.Add(linkRegistro);
            Controls.Add(linkPass);
            Controls.Add(btnIngresar);
            Controls.Add(txtPass);
            Controls.Add(txtMail);
            Controls.Add(label2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form2";
            Opacity = 0.9D;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form2";
            WindowState = FormWindowState.Maximized;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel1;
        private TextBox txtMail;
        private TextBox txtPass;
        private Label label2;
        private Button btnIngresar;
        private LinkLabel linkPass;
        private LinkLabel linkRegistro;
        private PictureBox pictureBox1;
    }
}