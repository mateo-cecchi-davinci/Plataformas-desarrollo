namespace Proyecto
{
    partial class Form3
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
            titulo = new Label();
            ciudades = new TabPage();
            vuelos = new TabPage();
            hoteles = new TabPage();
            usuarios = new TabPage();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            tbNombre = new TextBox();
            tbApellido = new TextBox();
            tbMail = new TextBox();
            tbDni = new TextBox();
            btnSalir = new Button();
            btnEliminar = new Button();
            btnModificar = new Button();
            btnBuscar = new Button();
            nombreUsuario = new Label();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            Dni = new DataGridViewTextBoxColumn();
            Nombre = new DataGridViewTextBoxColumn();
            Apellido = new DataGridViewTextBoxColumn();
            Mail = new DataGridViewTextBoxColumn();
            Clave = new DataGridViewTextBoxColumn();
            tabControl1 = new TabControl();
            usuarios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // titulo
            // 
            titulo.AutoSize = true;
            titulo.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            titulo.Location = new Point(353, 30);
            titulo.Name = "titulo";
            titulo.Size = new Size(84, 30);
            titulo.TabIndex = 0;
            titulo.Text = "TITULO";
            // 
            // ciudades
            // 
            ciudades.Location = new Point(4, 24);
            ciudades.Name = "ciudades";
            ciudades.Size = new Size(793, 334);
            ciudades.TabIndex = 4;
            ciudades.Text = "Ciudades";
            ciudades.UseVisualStyleBackColor = true;
            // 
            // vuelos
            // 
            vuelos.Location = new Point(4, 24);
            vuelos.Name = "vuelos";
            vuelos.Size = new Size(793, 334);
            vuelos.TabIndex = 2;
            vuelos.Text = "Vuelos";
            vuelos.UseVisualStyleBackColor = true;
            // 
            // hoteles
            // 
            hoteles.Location = new Point(4, 24);
            hoteles.Name = "hoteles";
            hoteles.Padding = new Padding(3);
            hoteles.Size = new Size(793, 334);
            hoteles.TabIndex = 1;
            hoteles.Text = "Hoteles";
            hoteles.UseVisualStyleBackColor = true;
            // 
            // usuarios
            // 
            usuarios.Controls.Add(label5);
            usuarios.Controls.Add(label4);
            usuarios.Controls.Add(label3);
            usuarios.Controls.Add(label2);
            usuarios.Controls.Add(tbNombre);
            usuarios.Controls.Add(tbApellido);
            usuarios.Controls.Add(tbMail);
            usuarios.Controls.Add(tbDni);
            usuarios.Controls.Add(btnSalir);
            usuarios.Controls.Add(btnEliminar);
            usuarios.Controls.Add(btnModificar);
            usuarios.Controls.Add(btnBuscar);
            usuarios.Controls.Add(nombreUsuario);
            usuarios.Controls.Add(label1);
            usuarios.Controls.Add(dataGridView1);
            usuarios.Location = new Point(4, 24);
            usuarios.Name = "usuarios";
            usuarios.Padding = new Padding(3);
            usuarios.Size = new Size(793, 334);
            usuarios.TabIndex = 0;
            usuarios.Text = "Usuarios";
            usuarios.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(539, 160);
            label5.Name = "label5";
            label5.Size = new Size(51, 15);
            label5.TabIndex = 15;
            label5.Text = "Apellido";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(539, 107);
            label4.Name = "label4";
            label4.Size = new Size(51, 15);
            label4.TabIndex = 14;
            label4.Text = "Nombre";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(539, 213);
            label3.Name = "label3";
            label3.Size = new Size(30, 15);
            label3.TabIndex = 13;
            label3.Text = "Mail";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(539, 54);
            label2.Name = "label2";
            label2.Size = new Size(25, 15);
            label2.TabIndex = 12;
            label2.Text = "Dni";
            // 
            // tbNombre
            // 
            tbNombre.Location = new Point(539, 125);
            tbNombre.Name = "tbNombre";
            tbNombre.Size = new Size(100, 23);
            tbNombre.TabIndex = 11;
            // 
            // tbApellido
            // 
            tbApellido.Location = new Point(539, 178);
            tbApellido.Name = "tbApellido";
            tbApellido.Size = new Size(100, 23);
            tbApellido.TabIndex = 10;
            // 
            // tbMail
            // 
            tbMail.Location = new Point(539, 231);
            tbMail.Name = "tbMail";
            tbMail.Size = new Size(100, 23);
            tbMail.TabIndex = 9;
            // 
            // tbDni
            // 
            tbDni.Location = new Point(539, 72);
            tbDni.Name = "tbDni";
            tbDni.Size = new Size(100, 23);
            tbDni.TabIndex = 8;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(662, 231);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(75, 23);
            btnSalir.TabIndex = 7;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(662, 178);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 6;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(662, 125);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(75, 23);
            btnModificar.TabIndex = 5;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(662, 72);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(75, 23);
            btnBuscar.TabIndex = 3;
            btnBuscar.Text = "Mostrar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // nombreUsuario
            // 
            nombreUsuario.AutoSize = true;
            nombreUsuario.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            nombreUsuario.Location = new Point(174, 19);
            nombreUsuario.Name = "nombreUsuario";
            nombreUsuario.Size = new Size(52, 21);
            nombreUsuario.TabIndex = 2;
            nombreUsuario.Text = "label2";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(42, 19);
            label1.Name = "label1";
            label1.Size = new Size(94, 21);
            label1.TabIndex = 1;
            label1.Text = "BIenvenido: ";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Id, Dni, Nombre, Apellido, Mail, Clave });
            dataGridView1.Location = new Point(29, 54);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(443, 257);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            // 
            // Id
            // 
            Id.HeaderText = "Id";
            Id.Name = "Id";
            Id.Visible = false;
            // 
            // Dni
            // 
            Dni.HeaderText = "Dni";
            Dni.Name = "Dni";
            // 
            // Nombre
            // 
            Nombre.HeaderText = "Nombre";
            Nombre.Name = "Nombre";
            // 
            // Apellido
            // 
            Apellido.HeaderText = "Apellido";
            Apellido.Name = "Apellido";
            // 
            // Mail
            // 
            Mail.HeaderText = "Mail";
            Mail.Name = "Mail";
            // 
            // Clave
            // 
            Clave.HeaderText = "Clave";
            Clave.Name = "Clave";
            Clave.Visible = false;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(usuarios);
            tabControl1.Controls.Add(hoteles);
            tabControl1.Controls.Add(vuelos);
            tabControl1.Controls.Add(ciudades);
            tabControl1.Location = new Point(0, 88);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(801, 362);
            tabControl1.TabIndex = 1;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Controls.Add(titulo);
            Name = "Form3";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form3";
            WindowState = FormWindowState.Maximized;
            Load += Form3_Load;
            usuarios.ResumeLayout(false);
            usuarios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label titulo;
        private TabPage ciudades;
        private TabPage vuelos;
        private TabPage hoteles;
        private TabPage usuarios;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private TextBox tbNombre;
        private TextBox tbApellido;
        private TextBox tbMail;
        private TextBox tbDni;
        private Button btnSalir;
        private Button btnEliminar;
        private Button btnModificar;
        private Button btnBuscar;
        private Label nombreUsuario;
        private Label label1;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Dni;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn Apellido;
        private DataGridViewTextBoxColumn Mail;
        private DataGridViewTextBoxColumn Clave;
        private TabControl tabControl1;
    }
}