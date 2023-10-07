namespace Proyecto
{
    partial class Form5
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
            tabControl1 = new TabControl();
            hoteles = new TabPage();
            label11 = new Label();
            tb_pagoH = new TextBox();
            cb_hotel = new ComboBox();
            cb_ciudadH = new ComboBox();
            cb_cantPersonasH = new ComboBox();
            label10 = new Label();
            btn_buscarHotel = new Button();
            btnSalir = new Button();
            btn_comprarHotel = new Button();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            Ciudad = new Label();
            dateTimePicker2 = new DateTimePicker();
            dateTimePicker1 = new DateTimePicker();
            dataGridView_hoteles_UC = new DataGridView();
            Column13 = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            vuelos = new TabPage();
            label12 = new Label();
            tb_pagoV = new TextBox();
            cb_origenV = new ComboBox();
            cb_destinoV = new ComboBox();
            cb_cantPersonasV = new ComboBox();
            btn_buscarVuelo = new Button();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label2 = new Label();
            dateTimePicker3 = new DateTimePicker();
            dataGridView_vuelos_UC = new DataGridView();
            Column18 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column11 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            Column9 = new DataGridViewTextBoxColumn();
            Column10 = new DataGridViewTextBoxColumn();
            btn_salir = new Button();
            btn_comprarVuelo = new Button();
            ciudades = new TabPage();
            button1 = new Button();
            dataGridView_ciudades_UC = new DataGridView();
            Column12 = new DataGridViewTextBoxColumn();
            tabPage1 = new TabPage();
            button3 = new Button();
            label_credito = new Label();
            label9 = new Label();
            button2 = new Button();
            dataGridView_perfil_UC = new DataGridView();
            Column14 = new DataGridViewTextBoxColumn();
            Column15 = new DataGridViewTextBoxColumn();
            Column16 = new DataGridViewTextBoxColumn();
            Column17 = new DataGridViewTextBoxColumn();
            btnLimpiar = new Button();
            button4 = new Button();
            tabControl1.SuspendLayout();
            hoteles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_hoteles_UC).BeginInit();
            vuelos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_vuelos_UC).BeginInit();
            ciudades.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_ciudades_UC).BeginInit();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_perfil_UC).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(293, 57);
            label1.Name = "label1";
            label1.Size = new Size(176, 30);
            label1.TabIndex = 0;
            label1.Text = "EXPLORA MUNDI";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(hoteles);
            tabControl1.Controls.Add(vuelos);
            tabControl1.Controls.Add(ciudades);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Location = new Point(1, 118);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(801, 333);
            tabControl1.TabIndex = 1;
            // 
            // hoteles
            // 
            hoteles.Controls.Add(btnLimpiar);
            hoteles.Controls.Add(label11);
            hoteles.Controls.Add(tb_pagoH);
            hoteles.Controls.Add(cb_hotel);
            hoteles.Controls.Add(cb_ciudadH);
            hoteles.Controls.Add(cb_cantPersonasH);
            hoteles.Controls.Add(label10);
            hoteles.Controls.Add(btn_buscarHotel);
            hoteles.Controls.Add(btnSalir);
            hoteles.Controls.Add(btn_comprarHotel);
            hoteles.Controls.Add(label5);
            hoteles.Controls.Add(label4);
            hoteles.Controls.Add(label3);
            hoteles.Controls.Add(Ciudad);
            hoteles.Controls.Add(dateTimePicker2);
            hoteles.Controls.Add(dateTimePicker1);
            hoteles.Controls.Add(dataGridView_hoteles_UC);
            hoteles.Location = new Point(4, 24);
            hoteles.Name = "hoteles";
            hoteles.Padding = new Padding(3);
            hoteles.Size = new Size(793, 305);
            hoteles.TabIndex = 0;
            hoteles.Text = "Hoteles";
            hoteles.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(423, 44);
            label11.Name = "label11";
            label11.Size = new Size(34, 15);
            label11.TabIndex = 18;
            label11.Text = "Pago";
            // 
            // tb_pagoH
            // 
            tb_pagoH.Location = new Point(423, 69);
            tb_pagoH.Name = "tb_pagoH";
            tb_pagoH.Size = new Size(100, 23);
            tb_pagoH.TabIndex = 17;
            // 
            // cb_hotel
            // 
            cb_hotel.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_hotel.FormattingEnabled = true;
            cb_hotel.Location = new Point(59, 69);
            cb_hotel.Name = "cb_hotel";
            cb_hotel.Size = new Size(100, 23);
            cb_hotel.TabIndex = 16;
            // 
            // cb_ciudadH
            // 
            cb_ciudadH.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_ciudadH.FormattingEnabled = true;
            cb_ciudadH.Location = new Point(174, 69);
            cb_ciudadH.Name = "cb_ciudadH";
            cb_ciudadH.Size = new Size(100, 23);
            cb_ciudadH.TabIndex = 15;
            // 
            // cb_cantPersonasH
            // 
            cb_cantPersonasH.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_cantPersonasH.FormattingEnabled = true;
            cb_cantPersonasH.Location = new Point(288, 69);
            cb_cantPersonasH.Name = "cb_cantPersonasH";
            cb_cantPersonasH.Size = new Size(121, 23);
            cb_cantPersonasH.TabIndex = 14;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(59, 44);
            label10.Name = "label10";
            label10.Size = new Size(36, 15);
            label10.TabIndex = 13;
            label10.Text = "Hotel";
            // 
            // btn_buscarHotel
            // 
            btn_buscarHotel.Location = new Point(668, 127);
            btn_buscarHotel.Name = "btn_buscarHotel";
            btn_buscarHotel.Size = new Size(75, 23);
            btn_buscarHotel.TabIndex = 11;
            btn_buscarHotel.Text = "Buscar";
            btn_buscarHotel.UseVisualStyleBackColor = true;
            btn_buscarHotel.Click += btn_buscarHotel_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(668, 240);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(75, 23);
            btnSalir.TabIndex = 10;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // btn_comprarHotel
            // 
            btn_comprarHotel.Location = new Point(668, 185);
            btn_comprarHotel.Name = "btn_comprarHotel";
            btn_comprarHotel.Size = new Size(75, 23);
            btn_comprarHotel.TabIndex = 9;
            btn_comprarHotel.Text = "Comprar";
            btn_comprarHotel.UseVisualStyleBackColor = true;
            btn_comprarHotel.Click += btn_comprarHotel_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(288, 44);
            label5.Name = "label5";
            label5.Size = new Size(121, 15);
            label5.TabIndex = 8;
            label5.Text = "Cantidad de personas";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(540, 113);
            label4.Name = "label4";
            label4.Size = new Size(69, 15);
            label4.TabIndex = 7;
            label4.Text = "Fecha hasta";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(540, 45);
            label3.Name = "label3";
            label3.Size = new Size(72, 15);
            label3.TabIndex = 6;
            label3.Text = "Fecha desde";
            // 
            // Ciudad
            // 
            Ciudad.AutoSize = true;
            Ciudad.Location = new Point(174, 44);
            Ciudad.Name = "Ciudad";
            Ciudad.Size = new Size(45, 15);
            Ciudad.TabIndex = 5;
            Ciudad.Text = "Ciudad";
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.Location = new Point(540, 138);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(100, 23);
            dateTimePicker2.TabIndex = 4;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(540, 70);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(100, 23);
            dateTimePicker1.TabIndex = 3;
            // 
            // dataGridView_hoteles_UC
            // 
            dataGridView_hoteles_UC.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_hoteles_UC.Columns.AddRange(new DataGridViewColumn[] { Column13, Column1, Column2, Column3, Column4 });
            dataGridView_hoteles_UC.Location = new Point(59, 113);
            dataGridView_hoteles_UC.Name = "dataGridView_hoteles_UC";
            dataGridView_hoteles_UC.RowTemplate.Height = 25;
            dataGridView_hoteles_UC.Size = new Size(464, 150);
            dataGridView_hoteles_UC.TabIndex = 0;
            dataGridView_hoteles_UC.CellDoubleClick += doble_click_hoteles;
            // 
            // Column13
            // 
            Column13.HeaderText = "Id";
            Column13.Name = "Column13";
            Column13.ReadOnly = true;
            Column13.Visible = false;
            // 
            // Column1
            // 
            Column1.HeaderText = "Hotel";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // Column2
            // 
            Column2.HeaderText = "Ciudad";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 120;
            // 
            // Column3
            // 
            Column3.HeaderText = "Costo";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            // 
            // Column4
            // 
            Column4.HeaderText = "Disponibilidad";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            // 
            // vuelos
            // 
            vuelos.Controls.Add(button4);
            vuelos.Controls.Add(label12);
            vuelos.Controls.Add(tb_pagoV);
            vuelos.Controls.Add(cb_origenV);
            vuelos.Controls.Add(cb_destinoV);
            vuelos.Controls.Add(cb_cantPersonasV);
            vuelos.Controls.Add(btn_buscarVuelo);
            vuelos.Controls.Add(label8);
            vuelos.Controls.Add(label7);
            vuelos.Controls.Add(label6);
            vuelos.Controls.Add(label2);
            vuelos.Controls.Add(dateTimePicker3);
            vuelos.Controls.Add(dataGridView_vuelos_UC);
            vuelos.Controls.Add(btn_salir);
            vuelos.Controls.Add(btn_comprarVuelo);
            vuelos.Location = new Point(4, 24);
            vuelos.Name = "vuelos";
            vuelos.Padding = new Padding(3);
            vuelos.Size = new Size(793, 305);
            vuelos.TabIndex = 1;
            vuelos.Text = "Vuelos";
            vuelos.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(533, 47);
            label12.Name = "label12";
            label12.Size = new Size(34, 15);
            label12.TabIndex = 16;
            label12.Text = "Pago";
            // 
            // tb_pagoV
            // 
            tb_pagoV.Location = new Point(533, 74);
            tb_pagoV.Name = "tb_pagoV";
            tb_pagoV.Size = new Size(100, 23);
            tb_pagoV.TabIndex = 15;
            // 
            // cb_origenV
            // 
            cb_origenV.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_origenV.FormattingEnabled = true;
            cb_origenV.Location = new Point(37, 74);
            cb_origenV.Name = "cb_origenV";
            cb_origenV.Size = new Size(100, 23);
            cb_origenV.TabIndex = 14;
            // 
            // cb_destinoV
            // 
            cb_destinoV.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_destinoV.FormattingEnabled = true;
            cb_destinoV.Location = new Point(158, 74);
            cb_destinoV.Name = "cb_destinoV";
            cb_destinoV.Size = new Size(100, 23);
            cb_destinoV.TabIndex = 13;
            // 
            // cb_cantPersonasV
            // 
            cb_cantPersonasV.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_cantPersonasV.FormattingEnabled = true;
            cb_cantPersonasV.Location = new Point(395, 74);
            cb_cantPersonasV.Name = "cb_cantPersonasV";
            cb_cantPersonasV.Size = new Size(121, 23);
            cb_cantPersonasV.TabIndex = 12;
            // 
            // btn_buscarVuelo
            // 
            btn_buscarVuelo.Location = new Point(664, 133);
            btn_buscarVuelo.Name = "btn_buscarVuelo";
            btn_buscarVuelo.Size = new Size(100, 23);
            btn_buscarVuelo.TabIndex = 11;
            btn_buscarVuelo.Text = "Buscar";
            btn_buscarVuelo.UseVisualStyleBackColor = true;
            btn_buscarVuelo.Click += btn_buscarVuelo_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(395, 47);
            label8.Name = "label8";
            label8.Size = new Size(121, 15);
            label8.TabIndex = 10;
            label8.Text = "Cantidad de personas";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(277, 47);
            label7.Name = "label7";
            label7.Size = new Size(38, 15);
            label7.TabIndex = 9;
            label7.Text = "Fecha";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(158, 47);
            label6.Name = "label6";
            label6.Size = new Size(47, 15);
            label6.TabIndex = 8;
            label6.Text = "Destino";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(37, 47);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 7;
            label2.Text = "Origen";
            // 
            // dateTimePicker3
            // 
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.Location = new Point(277, 74);
            dateTimePicker3.Name = "dateTimePicker3";
            dateTimePicker3.Size = new Size(100, 23);
            dateTimePicker3.TabIndex = 6;
            // 
            // dataGridView_vuelos_UC
            // 
            dataGridView_vuelos_UC.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_vuelos_UC.Columns.AddRange(new DataGridViewColumn[] { Column18, Column5, Column6, Column11, Column7, Column8, Column9, Column10 });
            dataGridView_vuelos_UC.Location = new Point(37, 118);
            dataGridView_vuelos_UC.Name = "dataGridView_vuelos_UC";
            dataGridView_vuelos_UC.RowTemplate.Height = 25;
            dataGridView_vuelos_UC.Size = new Size(596, 150);
            dataGridView_vuelos_UC.TabIndex = 2;
            dataGridView_vuelos_UC.CellDoubleClick += doble_click_vuelos;
            // 
            // Column18
            // 
            Column18.HeaderText = "ID";
            Column18.Name = "Column18";
            Column18.ReadOnly = true;
            Column18.Visible = false;
            // 
            // Column5
            // 
            Column5.HeaderText = "Origen";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            // 
            // Column6
            // 
            Column6.HeaderText = "Destino";
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            // 
            // Column11
            // 
            Column11.HeaderText = "Costo";
            Column11.Name = "Column11";
            Column11.ReadOnly = true;
            // 
            // Column7
            // 
            Column7.HeaderText = "Capacidad";
            Column7.Name = "Column7";
            Column7.ReadOnly = true;
            // 
            // Column8
            // 
            Column8.HeaderText = "Fecha";
            Column8.Name = "Column8";
            Column8.ReadOnly = true;
            // 
            // Column9
            // 
            Column9.HeaderText = "Aerolínea";
            Column9.Name = "Column9";
            Column9.ReadOnly = true;
            // 
            // Column10
            // 
            Column10.HeaderText = "Avion";
            Column10.Name = "Column10";
            Column10.ReadOnly = true;
            // 
            // btn_salir
            // 
            btn_salir.Location = new Point(664, 245);
            btn_salir.Name = "btn_salir";
            btn_salir.Size = new Size(100, 23);
            btn_salir.TabIndex = 1;
            btn_salir.Text = "Salir";
            btn_salir.UseVisualStyleBackColor = true;
            btn_salir.Click += btnSalir_Click;
            // 
            // btn_comprarVuelo
            // 
            btn_comprarVuelo.Location = new Point(664, 190);
            btn_comprarVuelo.Name = "btn_comprarVuelo";
            btn_comprarVuelo.Size = new Size(100, 23);
            btn_comprarVuelo.TabIndex = 0;
            btn_comprarVuelo.Text = "Comprar";
            btn_comprarVuelo.UseVisualStyleBackColor = true;
            btn_comprarVuelo.Click += btn_comprarVuelo_Click;
            // 
            // ciudades
            // 
            ciudades.Controls.Add(button1);
            ciudades.Controls.Add(dataGridView_ciudades_UC);
            ciudades.Location = new Point(4, 24);
            ciudades.Name = "ciudades";
            ciudades.Size = new Size(793, 305);
            ciudades.TabIndex = 2;
            ciudades.Text = "Ciudades";
            ciudades.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(348, 237);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Salir";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnSalir_Click;
            // 
            // dataGridView_ciudades_UC
            // 
            dataGridView_ciudades_UC.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_ciudades_UC.Columns.AddRange(new DataGridViewColumn[] { Column12 });
            dataGridView_ciudades_UC.Location = new Point(265, 62);
            dataGridView_ciudades_UC.Name = "dataGridView_ciudades_UC";
            dataGridView_ciudades_UC.RowTemplate.Height = 25;
            dataGridView_ciudades_UC.Size = new Size(240, 150);
            dataGridView_ciudades_UC.TabIndex = 0;
            // 
            // Column12
            // 
            Column12.HeaderText = "Ciudad";
            Column12.Name = "Column12";
            Column12.ReadOnly = true;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(button3);
            tabPage1.Controls.Add(label_credito);
            tabPage1.Controls.Add(label9);
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(dataGridView_perfil_UC);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(793, 305);
            tabPage1.TabIndex = 3;
            tabPage1.Text = "Perfil";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(662, 92);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 4;
            button3.Text = "Actualizar";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label_credito
            // 
            label_credito.AutoSize = true;
            label_credito.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label_credito.Location = new Point(181, 29);
            label_credito.Name = "label_credito";
            label_credito.Size = new Size(61, 21);
            label_credito.TabIndex = 3;
            label_credito.Text = "label10";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(73, 29);
            label9.Name = "label9";
            label9.Size = new Size(68, 21);
            label9.TabIndex = 2;
            label9.Text = "Credito: ";
            // 
            // button2
            // 
            button2.Location = new Point(662, 143);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 1;
            button2.Text = "Salir";
            button2.UseVisualStyleBackColor = true;
            button2.Click += btnSalir_Click;
            // 
            // dataGridView_perfil_UC
            // 
            dataGridView_perfil_UC.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_perfil_UC.Columns.AddRange(new DataGridViewColumn[] { Column14, Column15, Column16, Column17 });
            dataGridView_perfil_UC.Location = new Point(73, 67);
            dataGridView_perfil_UC.Name = "dataGridView_perfil_UC";
            dataGridView_perfil_UC.RowTemplate.Height = 25;
            dataGridView_perfil_UC.Size = new Size(523, 187);
            dataGridView_perfil_UC.TabIndex = 0;
            // 
            // Column14
            // 
            Column14.HeaderText = "Reservas Hoteles";
            Column14.Name = "Column14";
            Column14.ReadOnly = true;
            Column14.Width = 120;
            // 
            // Column15
            // 
            Column15.HeaderText = "Reservas Vuelos";
            Column15.Name = "Column15";
            Column15.ReadOnly = true;
            Column15.Width = 120;
            // 
            // Column16
            // 
            Column16.HeaderText = "Vuelos Tomados";
            Column16.Name = "Column16";
            Column16.ReadOnly = true;
            Column16.Width = 120;
            // 
            // Column17
            // 
            Column17.HeaderText = "Hoteles Visitados";
            Column17.Name = "Column17";
            Column17.ReadOnly = true;
            Column17.Width = 120;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(668, 70);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(75, 23);
            btnLimpiar.TabIndex = 19;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // button4
            // 
            button4.Location = new Point(664, 73);
            button4.Name = "button4";
            button4.Size = new Size(100, 23);
            button4.TabIndex = 20;
            button4.Text = "Limpiar";
            button4.UseVisualStyleBackColor = true;
            button4.Click += btnLimpiar_Click;
            // 
            // Form5
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Controls.Add(label1);
            Name = "Form5";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form5";
            WindowState = FormWindowState.Maximized;
            Load += Form5_Load;
            tabControl1.ResumeLayout(false);
            hoteles.ResumeLayout(false);
            hoteles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_hoteles_UC).EndInit();
            vuelos.ResumeLayout(false);
            vuelos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_vuelos_UC).EndInit();
            ciudades.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView_ciudades_UC).EndInit();
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_perfil_UC).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TabControl tabControl1;
        private TabPage hoteles;
        private TabPage vuelos;
        private DataGridView dataGridView_hoteles_UC;
        private TabPage ciudades;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label Ciudad;
        private DateTimePicker dateTimePicker2;
        private DateTimePicker dateTimePicker1;
        private Button btnSalir;
        private Button btn_comprarHotel;
        private Button btn_salir;
        private Button btn_comprarVuelo;
        private DataGridView dataGridView_vuelos_UC;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label2;
        private DateTimePicker dateTimePicker3;
        private Button button1;
        private DataGridView dataGridView_ciudades_UC;
        private DataGridViewTextBoxColumn Column12;
        private TabPage tabPage1;
        private DataGridView dataGridView_perfil_UC;
        private Button btn_buscarHotel;
        private Button btn_buscarVuelo;
        private Button button2;
        private DataGridViewTextBoxColumn Column14;
        private DataGridViewTextBoxColumn Column15;
        private DataGridViewTextBoxColumn Column16;
        private DataGridViewTextBoxColumn Column17;
        private Label label_credito;
        private Label label9;
        private DataGridViewTextBoxColumn Column13;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private Label label10;
        private DataGridViewTextBoxColumn Column18;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column10;
        private ComboBox cb_cantPersonasH;
        private ComboBox cb_cantPersonasV;
        private ComboBox cb_hotel;
        private ComboBox cb_ciudadH;
        private ComboBox cb_origenV;
        private ComboBox cb_destinoV;
        private Button button3;
        private Label label11;
        private TextBox tb_pagoH;
        private Label label12;
        private TextBox tb_pagoV;
        private Button btnLimpiar;
        private Button button4;
    }
}