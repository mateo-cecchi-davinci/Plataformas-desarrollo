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
            label19 = new Label();
            textBox_costo_vuelos = new TextBox();
            dateTimePicker_vuelos = new DateTimePicker();
            label18 = new Label();
            textBox_avion_vuelos = new TextBox();
            label17 = new Label();
            textBox_aerolineas_vuelos = new TextBox();
            label9 = new Label();
            Cargar_vuelos = new Button();
            label10 = new Label();
            label13 = new Label();
            label14 = new Label();
            textBox_origen_vuelos = new TextBox();
            textBox_destino_vuelos = new TextBox();
            textBox_ciudad_vuelos = new TextBox();
            Mostrar_vuelos = new Button();
            Eliminar_vuelos = new Button();
            Modificar_vuelos = new Button();
            Salir_vuelos = new Button();
            label15 = new Label();
            label16 = new Label();
            dataGridView2 = new DataGridView();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
            Fecha = new DataGridViewTextBoxColumn();
            Aerolinea = new DataGridViewTextBoxColumn();
            Avion = new DataGridViewTextBoxColumn();
            hoteles = new TabPage();
            label12 = new Label();
            textBoxCosto = new TextBox();
            Salir = new Button();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            textBoxNombre = new TextBox();
            textBoxCapacidad = new TextBox();
            textBoxCiudad = new TextBox();
            Eliminar = new Button();
            Modificar = new Button();
            Cargar = new Button();
            Mostrar = new Button();
            nombreUsuarioH = new Label();
            label11 = new Label();
            dataGridView2Hoteles = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
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
            vuelos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            hoteles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2Hoteles).BeginInit();
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
            vuelos.Controls.Add(label19);
            vuelos.Controls.Add(textBox_costo_vuelos);
            vuelos.Controls.Add(dateTimePicker_vuelos);
            vuelos.Controls.Add(label18);
            vuelos.Controls.Add(textBox_avion_vuelos);
            vuelos.Controls.Add(label17);
            vuelos.Controls.Add(textBox_aerolineas_vuelos);
            vuelos.Controls.Add(label9);
            vuelos.Controls.Add(Cargar_vuelos);
            vuelos.Controls.Add(label10);
            vuelos.Controls.Add(label13);
            vuelos.Controls.Add(label14);
            vuelos.Controls.Add(textBox_origen_vuelos);
            vuelos.Controls.Add(textBox_destino_vuelos);
            vuelos.Controls.Add(textBox_ciudad_vuelos);
            vuelos.Controls.Add(Mostrar_vuelos);
            vuelos.Controls.Add(Eliminar_vuelos);
            vuelos.Controls.Add(Modificar_vuelos);
            vuelos.Controls.Add(Salir_vuelos);
            vuelos.Controls.Add(label15);
            vuelos.Controls.Add(label16);
            vuelos.Controls.Add(dataGridView2);
            vuelos.Location = new Point(4, 24);
            vuelos.Name = "vuelos";
            vuelos.Size = new Size(793, 334);
            vuelos.TabIndex = 2;
            vuelos.Text = "Vuelos";
            vuelos.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(333, 37);
            label19.Name = "label19";
            label19.Size = new Size(59, 15);
            label19.TabIndex = 56;
            label19.Text = "Costo u$s";
            // 
            // textBox_costo_vuelos
            // 
            textBox_costo_vuelos.Location = new Point(333, 55);
            textBox_costo_vuelos.Name = "textBox_costo_vuelos";
            textBox_costo_vuelos.Size = new Size(100, 23);
            textBox_costo_vuelos.TabIndex = 55;
            // 
            // dateTimePicker_vuelos
            // 
            dateTimePicker_vuelos.Format = DateTimePickerFormat.Short;
            dateTimePicker_vuelos.Location = new Point(15, 100);
            dateTimePicker_vuelos.Name = "dateTimePicker_vuelos";
            dateTimePicker_vuelos.Size = new Size(100, 23);
            dateTimePicker_vuelos.TabIndex = 54;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(227, 83);
            label18.Name = "label18";
            label18.Size = new Size(38, 15);
            label18.TabIndex = 53;
            label18.Text = "Avion";
            // 
            // textBox_avion_vuelos
            // 
            textBox_avion_vuelos.Location = new Point(227, 101);
            textBox_avion_vuelos.Name = "textBox_avion_vuelos";
            textBox_avion_vuelos.Size = new Size(100, 23);
            textBox_avion_vuelos.TabIndex = 52;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(121, 83);
            label17.Name = "label17";
            label17.Size = new Size(57, 15);
            label17.TabIndex = 51;
            label17.Text = "Aerolinea";
            // 
            // textBox_aerolineas_vuelos
            // 
            textBox_aerolineas_vuelos.Location = new Point(121, 101);
            textBox_aerolineas_vuelos.Name = "textBox_aerolineas_vuelos";
            textBox_aerolineas_vuelos.Size = new Size(100, 23);
            textBox_aerolineas_vuelos.TabIndex = 50;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(15, 83);
            label9.Name = "label9";
            label9.Size = new Size(38, 15);
            label9.TabIndex = 49;
            label9.Text = "Fecha";
            // 
            // Cargar_vuelos
            // 
            Cargar_vuelos.Location = new Point(583, 55);
            Cargar_vuelos.Name = "Cargar_vuelos";
            Cargar_vuelos.Size = new Size(75, 23);
            Cargar_vuelos.TabIndex = 47;
            Cargar_vuelos.Text = "Cargar";
            Cargar_vuelos.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(121, 37);
            label10.Name = "label10";
            label10.Size = new Size(47, 15);
            label10.TabIndex = 46;
            label10.Text = "Destino";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(15, 37);
            label13.Name = "label13";
            label13.Size = new Size(43, 15);
            label13.TabIndex = 45;
            label13.Text = "Origen";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(227, 37);
            label14.Name = "label14";
            label14.Size = new Size(45, 15);
            label14.TabIndex = 44;
            label14.Text = "Ciudad";
            // 
            // textBox_origen_vuelos
            // 
            textBox_origen_vuelos.Location = new Point(15, 55);
            textBox_origen_vuelos.Name = "textBox_origen_vuelos";
            textBox_origen_vuelos.Size = new Size(100, 23);
            textBox_origen_vuelos.TabIndex = 43;
            // 
            // textBox_destino_vuelos
            // 
            textBox_destino_vuelos.Location = new Point(121, 55);
            textBox_destino_vuelos.Name = "textBox_destino_vuelos";
            textBox_destino_vuelos.Size = new Size(100, 23);
            textBox_destino_vuelos.TabIndex = 42;
            // 
            // textBox_ciudad_vuelos
            // 
            textBox_ciudad_vuelos.Location = new Point(227, 55);
            textBox_ciudad_vuelos.Name = "textBox_ciudad_vuelos";
            textBox_ciudad_vuelos.Size = new Size(100, 23);
            textBox_ciudad_vuelos.TabIndex = 41;
            // 
            // Mostrar_vuelos
            // 
            Mostrar_vuelos.Location = new Point(480, 55);
            Mostrar_vuelos.Name = "Mostrar_vuelos";
            Mostrar_vuelos.Size = new Size(75, 23);
            Mostrar_vuelos.TabIndex = 40;
            Mostrar_vuelos.Text = "Mostrar";
            Mostrar_vuelos.UseVisualStyleBackColor = true;
            // 
            // Eliminar_vuelos
            // 
            Eliminar_vuelos.Location = new Point(480, 96);
            Eliminar_vuelos.Name = "Eliminar_vuelos";
            Eliminar_vuelos.Size = new Size(75, 23);
            Eliminar_vuelos.TabIndex = 39;
            Eliminar_vuelos.Text = "Eliminar";
            Eliminar_vuelos.UseVisualStyleBackColor = true;
            // 
            // Modificar_vuelos
            // 
            Modificar_vuelos.Location = new Point(679, 55);
            Modificar_vuelos.Name = "Modificar_vuelos";
            Modificar_vuelos.Size = new Size(75, 23);
            Modificar_vuelos.TabIndex = 38;
            Modificar_vuelos.Text = "Modificar";
            Modificar_vuelos.UseVisualStyleBackColor = true;
            // 
            // Salir_vuelos
            // 
            Salir_vuelos.Location = new Point(583, 96);
            Salir_vuelos.Name = "Salir_vuelos";
            Salir_vuelos.Size = new Size(75, 23);
            Salir_vuelos.TabIndex = 37;
            Salir_vuelos.Text = "Salir";
            Salir_vuelos.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label15.Location = new Point(110, 6);
            label15.Name = "label15";
            label15.Size = new Size(52, 21);
            label15.TabIndex = 36;
            label15.Text = "label2";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label16.Location = new Point(10, 6);
            label16.Name = "label16";
            label16.Size = new Size(94, 21);
            label16.TabIndex = 35;
            label16.Text = "BIenvenido: ";
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7, dataGridViewTextBoxColumn8, dataGridViewTextBoxColumn9, dataGridViewTextBoxColumn10, Fecha, Aerolinea, Avion });
            dataGridView2.Location = new Point(10, 130);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.ReadOnly = true;
            dataGridView2.RowTemplate.Height = 25;
            dataGridView2.Size = new Size(744, 178);
            dataGridView2.TabIndex = 34;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.HeaderText = "Id";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            dataGridViewTextBoxColumn6.Visible = false;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.HeaderText = "Origen";
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewTextBoxColumn8.HeaderText = "Destino";
            dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            dataGridViewTextBoxColumn9.HeaderText = "Ciudad";
            dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            dataGridViewTextBoxColumn10.HeaderText = "Costo u$s";
            dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // Fecha
            // 
            Fecha.HeaderText = "Fecha";
            Fecha.Name = "Fecha";
            Fecha.ReadOnly = true;
            // 
            // Aerolinea
            // 
            Aerolinea.HeaderText = "Aerolinea";
            Aerolinea.Name = "Aerolinea";
            Aerolinea.ReadOnly = true;
            // 
            // Avion
            // 
            Avion.HeaderText = "Avion";
            Avion.Name = "Avion";
            Avion.ReadOnly = true;
            // 
            // hoteles
            // 
            hoteles.Controls.Add(label12);
            hoteles.Controls.Add(textBoxCosto);
            hoteles.Controls.Add(Salir);
            hoteles.Controls.Add(label6);
            hoteles.Controls.Add(label7);
            hoteles.Controls.Add(label8);
            hoteles.Controls.Add(textBoxNombre);
            hoteles.Controls.Add(textBoxCapacidad);
            hoteles.Controls.Add(textBoxCiudad);
            hoteles.Controls.Add(Eliminar);
            hoteles.Controls.Add(Modificar);
            hoteles.Controls.Add(Cargar);
            hoteles.Controls.Add(Mostrar);
            hoteles.Controls.Add(nombreUsuarioH);
            hoteles.Controls.Add(label11);
            hoteles.Controls.Add(dataGridView2Hoteles);
            hoteles.Location = new Point(4, 24);
            hoteles.Name = "hoteles";
            hoteles.Padding = new Padding(3);
            hoteles.Size = new Size(793, 334);
            hoteles.TabIndex = 1;
            hoteles.Text = "Hoteles";
            hoteles.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(527, 216);
            label12.Name = "label12";
            label12.Size = new Size(59, 15);
            label12.TabIndex = 33;
            label12.Text = "Costo u$s";
            // 
            // textBoxCosto
            // 
            textBoxCosto.Location = new Point(527, 234);
            textBoxCosto.Name = "textBoxCosto";
            textBoxCosto.Size = new Size(100, 23);
            textBoxCosto.TabIndex = 32;
            // 
            // Salir
            // 
            Salir.Location = new Point(655, 287);
            Salir.Name = "Salir";
            Salir.Size = new Size(75, 23);
            Salir.TabIndex = 31;
            Salir.Text = "Salir";
            Salir.UseVisualStyleBackColor = true;
            Salir.Click += Salir_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(527, 106);
            label6.Name = "label6";
            label6.Size = new Size(63, 15);
            label6.TabIndex = 30;
            label6.Text = "Capacidad";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(527, 53);
            label7.Name = "label7";
            label7.Size = new Size(51, 15);
            label7.TabIndex = 29;
            label7.Text = "Nombre";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(527, 159);
            label8.Name = "label8";
            label8.Size = new Size(45, 15);
            label8.TabIndex = 28;
            label8.Text = "Ciudad";
            // 
            // textBoxNombre
            // 
            textBoxNombre.Location = new Point(527, 71);
            textBoxNombre.Name = "textBoxNombre";
            textBoxNombre.Size = new Size(100, 23);
            textBoxNombre.TabIndex = 26;
            // 
            // textBoxCapacidad
            // 
            textBoxCapacidad.Location = new Point(527, 124);
            textBoxCapacidad.Name = "textBoxCapacidad";
            textBoxCapacidad.Size = new Size(100, 23);
            textBoxCapacidad.TabIndex = 25;
            // 
            // textBoxCiudad
            // 
            textBoxCiudad.Location = new Point(527, 177);
            textBoxCiudad.Name = "textBoxCiudad";
            textBoxCiudad.Size = new Size(100, 23);
            textBoxCiudad.TabIndex = 24;
            // 
            // Eliminar
            // 
            Eliminar.Location = new Point(655, 234);
            Eliminar.Name = "Eliminar";
            Eliminar.Size = new Size(75, 23);
            Eliminar.TabIndex = 22;
            Eliminar.Text = "Eliminar";
            Eliminar.UseVisualStyleBackColor = true;
            Eliminar.Click += Eliminar_Click;
            // 
            // Modificar
            // 
            Modificar.Location = new Point(655, 177);
            Modificar.Name = "Modificar";
            Modificar.Size = new Size(75, 23);
            Modificar.TabIndex = 21;
            Modificar.Text = "Modificar";
            Modificar.UseVisualStyleBackColor = true;
            Modificar.Click += Modificar_Click;
            // 
            // Cargar
            // 
            Cargar.Location = new Point(655, 124);
            Cargar.Name = "Cargar";
            Cargar.Size = new Size(75, 23);
            Cargar.TabIndex = 20;
            Cargar.Text = "Cargar";
            Cargar.UseVisualStyleBackColor = true;
            Cargar.Click += Cargar_Click;
            // 
            // Mostrar
            // 
            Mostrar.Location = new Point(655, 71);
            Mostrar.Name = "Mostrar";
            Mostrar.Size = new Size(75, 23);
            Mostrar.TabIndex = 19;
            Mostrar.Text = "Mostrar";
            Mostrar.UseVisualStyleBackColor = true;
            Mostrar.Click += Mostrar_Click;
            // 
            // nombreUsuarioH
            // 
            nombreUsuarioH.AutoSize = true;
            nombreUsuarioH.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            nombreUsuarioH.Location = new Point(167, 18);
            nombreUsuarioH.Name = "nombreUsuarioH";
            nombreUsuarioH.Size = new Size(52, 21);
            nombreUsuarioH.TabIndex = 18;
            nombreUsuarioH.Text = "label2";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(35, 18);
            label11.Name = "label11";
            label11.Size = new Size(94, 21);
            label11.TabIndex = 17;
            label11.Text = "BIenvenido: ";
            // 
            // dataGridView2Hoteles
            // 
            dataGridView2Hoteles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2Hoteles.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5 });
            dataGridView2Hoteles.Location = new Point(22, 53);
            dataGridView2Hoteles.Name = "dataGridView2Hoteles";
            dataGridView2Hoteles.ReadOnly = true;
            dataGridView2Hoteles.RowTemplate.Height = 25;
            dataGridView2Hoteles.Size = new Size(443, 257);
            dataGridView2Hoteles.TabIndex = 16;
            dataGridView2Hoteles.CellContentDoubleClick += dataGridView2Hoteles_CellDoubleClick;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Id";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Nombre";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Capacidad";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Costo u$s";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "Ciudad";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
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
            label5.Location = new Point(532, 159);
            label5.Name = "label5";
            label5.Size = new Size(51, 15);
            label5.TabIndex = 15;
            label5.Text = "Apellido";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(532, 106);
            label4.Name = "label4";
            label4.Size = new Size(51, 15);
            label4.TabIndex = 14;
            label4.Text = "Nombre";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(532, 212);
            label3.Name = "label3";
            label3.Size = new Size(30, 15);
            label3.TabIndex = 13;
            label3.Text = "Mail";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(532, 53);
            label2.Name = "label2";
            label2.Size = new Size(25, 15);
            label2.TabIndex = 12;
            label2.Text = "Dni";
            // 
            // tbNombre
            // 
            tbNombre.Location = new Point(532, 124);
            tbNombre.Name = "tbNombre";
            tbNombre.Size = new Size(100, 23);
            tbNombre.TabIndex = 11;
            // 
            // tbApellido
            // 
            tbApellido.Location = new Point(532, 177);
            tbApellido.Name = "tbApellido";
            tbApellido.Size = new Size(100, 23);
            tbApellido.TabIndex = 10;
            // 
            // tbMail
            // 
            tbMail.Location = new Point(532, 230);
            tbMail.Name = "tbMail";
            tbMail.Size = new Size(100, 23);
            tbMail.TabIndex = 9;
            // 
            // tbDni
            // 
            tbDni.Location = new Point(532, 71);
            tbDni.Name = "tbDni";
            tbDni.Size = new Size(100, 23);
            tbDni.TabIndex = 8;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(655, 230);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(75, 23);
            btnSalir.TabIndex = 7;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(655, 177);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 6;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(655, 124);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(75, 23);
            btnModificar.TabIndex = 5;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(655, 71);
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
            nombreUsuario.Location = new Point(167, 18);
            nombreUsuario.Name = "nombreUsuario";
            nombreUsuario.Size = new Size(52, 21);
            nombreUsuario.TabIndex = 2;
            nombreUsuario.Text = "label2";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(35, 18);
            label1.Name = "label1";
            label1.Size = new Size(94, 21);
            label1.TabIndex = 1;
            label1.Text = "BIenvenido: ";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Id, Dni, Nombre, Apellido, Mail, Clave });
            dataGridView1.Location = new Point(22, 53);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(443, 257);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            // 
            // Id
            // 
            Id.HeaderText = "Id";
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Visible = false;
            // 
            // Dni
            // 
            Dni.HeaderText = "Dni";
            Dni.Name = "Dni";
            Dni.ReadOnly = true;
            // 
            // Nombre
            // 
            Nombre.HeaderText = "Nombre";
            Nombre.Name = "Nombre";
            Nombre.ReadOnly = true;
            // 
            // Apellido
            // 
            Apellido.HeaderText = "Apellido";
            Apellido.Name = "Apellido";
            Apellido.ReadOnly = true;
            // 
            // Mail
            // 
            Mail.HeaderText = "Mail";
            Mail.Name = "Mail";
            Mail.ReadOnly = true;
            // 
            // Clave
            // 
            Clave.HeaderText = "Clave";
            Clave.Name = "Clave";
            Clave.ReadOnly = true;
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
            Text = "Form3";
            Load += Form3_Load;
            vuelos.ResumeLayout(false);
            vuelos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            hoteles.ResumeLayout(false);
            hoteles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2Hoteles).EndInit();
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
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox textBoxNombre;
        private TextBox textBoxCapacidad;
        private TextBox textBoxCiudad;
        private Button Eliminar;
        private Button Modificar;
        private Button Cargar;
        private Button Mostrar;
        private Label nombreUsuarioH;
        private Label label11;
        private DataGridView dataGridView2Hoteles;
        private Button Salir;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private Label label12;
        private TextBox textBoxCosto;
        private Label label9;
        private Button Cargar_vuelos;
        private Label label10;
        private Label label13;
        private Label label14;
        private TextBox textBox_origen_vuelos;
        private TextBox textBox_destino_vuelos;
        private TextBox textBox_ciudad_vuelos;
        private Button Mostrar_vuelos;
        private Button Eliminar_vuelos;
        private Button Modificar_vuelos;
        private Button Salir_vuelos;
        private Label label15;
        private Label label16;
        private DataGridView dataGridView2;
        private Label label18;
        private TextBox textBox_avion_vuelos;
        private Label label17;
        private TextBox textBox_aerolineas_vuelos;
        private DateTimePicker dateTimePicker_vuelos;
        private Label label19;
        private TextBox textBox_costo_vuelos;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn Fecha;
        private DataGridViewTextBoxColumn Aerolinea;
        private DataGridViewTextBoxColumn Avion;
    }
}