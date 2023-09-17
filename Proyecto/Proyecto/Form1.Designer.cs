namespace Proyecto
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
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            button1 = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textBox3 = new TextBox();
            button2 = new Button();
            label3 = new Label();
            dataGridView2 = new DataGridView();
            ID = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Capacidad = new DataGridViewTextBoxColumn();
            Costo = new DataGridViewTextBoxColumn();
            Ciudad = new DataGridViewTextBoxColumn();
            dateTimePicker2 = new DateTimePicker();
            label4 = new Label();
            label5 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label6 = new Label();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.MintCream;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5 });
            dataGridView1.Location = new Point(12, 257);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(443, 181);
            dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            Column1.HeaderText = "ID";
            Column1.Name = "Column1";
            Column1.Visible = false;
            // 
            // Column2
            // 
            Column2.HeaderText = "Nombre";
            Column2.Name = "Column2";
            // 
            // Column3
            // 
            Column3.HeaderText = "Capacidad";
            Column3.Name = "Column3";
            // 
            // Column4
            // 
            Column4.HeaderText = "Costo u$s";
            Column4.Name = "Column4";
            // 
            // Column5
            // 
            Column5.HeaderText = "Ciudad";
            Column5.Name = "Column5";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ButtonHighlight;
            button1.Location = new Point(12, 228);
            button1.Name = "button1";
            button1.Size = new Size(115, 23);
            button1.TabIndex = 1;
            button1.Text = "Mostrar Hoteles";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 81);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 63);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 3;
            label1.Text = "Nombre";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(122, 63);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 5;
            label2.Text = "Ciudad";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(122, 81);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 6;
            // 
            // button2
            // 
            button2.Location = new Point(760, 79);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 8;
            button2.Text = "Buscar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 35);
            label3.Name = "label3";
            label3.Size = new Size(85, 15);
            label3.TabIndex = 9;
            label3.Text = "Buscar Hoteles";
            // 
            // dataGridView2
            // 
            dataGridView2.BackgroundColor = Color.MintCream;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { ID, Column6, Capacidad, Costo, Ciudad });
            dataGridView2.Location = new Point(188, 110);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowTemplate.Height = 25;
            dataGridView2.Size = new Size(443, 77);
            dataGridView2.TabIndex = 10;
            // 
            // ID
            // 
            ID.HeaderText = "ID";
            ID.Name = "ID";
            ID.Visible = false;
            // 
            // Column6
            // 
            Column6.HeaderText = "Nombre";
            Column6.Name = "Column6";
            // 
            // Capacidad
            // 
            Capacidad.HeaderText = "Capacidad";
            Capacidad.Name = "Capacidad";
            // 
            // Costo
            // 
            Costo.HeaderText = "Costo";
            Costo.Name = "Costo";
            // 
            // Ciudad
            // 
            Ciudad.HeaderText = "Ciudad";
            Ciudad.Name = "Ciudad";
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Location = new Point(541, 79);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(213, 23);
            dateTimePicker2.TabIndex = 12;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(228, 85);
            label4.Name = "label4";
            label4.Size = new Size(47, 15);
            label4.TabIndex = 13;
            label4.Text = "Entrada";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(497, 84);
            label5.Name = "label5";
            label5.Size = new Size(38, 15);
            label5.TabIndex = 14;
            label5.Text = "Salida";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(281, 81);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(210, 23);
            dateTimePicker1.TabIndex = 15;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(228, 63);
            label6.Name = "label6";
            label6.Size = new Size(43, 15);
            label6.TabIndex = 16;
            label6.Text = "Fechas";
            // 
            // button3
            // 
            button3.Location = new Point(637, 133);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 17;
            button3.Text = "Reservar";
            button3.UseVisualStyleBackColor = true;
            button3.Visible = false;
            button3.Click += button3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(861, 450);
            Controls.Add(button3);
            Controls.Add(label6);
            Controls.Add(dateTimePicker1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(dateTimePicker2);
            Controls.Add(dataGridView2);
            Controls.Add(label3);
            Controls.Add(button2);
            Controls.Add(textBox3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            ForeColor = Color.Black;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button1;
        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private TextBox textBox3;
        private Button button2;
        private Label label3;
        private DataGridView dataGridView2;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Capacidad;
        private DataGridViewTextBoxColumn Costo;
        private DataGridViewTextBoxColumn Ciudad;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DateTimePicker dateTimePicker2;
        private Label label4;
        private Label label5;
        private DateTimePicker dateTimePicker1;
        private Label label6;
        private Button button3;
    }
}