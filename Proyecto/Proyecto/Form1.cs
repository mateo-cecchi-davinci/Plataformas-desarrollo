using System.Drawing.Text;

namespace Proyecto
{
    public partial class Form1 : Form
    {
        private Agencia agencia;
        public Form1()
        {
            InitializeComponent();
            agencia = new Agencia();

            Ciudad ciudadA = new Ciudad("Ciudad A");
            Ciudad ciudadB = new Ciudad("Ciudad B");
            Ciudad ciudadC = new Ciudad("Ciudad C");
            Ciudad ciudadD = new Ciudad("Ciudad D");
            Ciudad ciudadE = new Ciudad("Ciudad E");

            agencia.agregarHoteles(ciudadA, 100, 150.0, "Hotel 1");
            agencia.agregarHoteles(ciudadB, 90, 50.0, "Hotel 2");
            agencia.agregarHoteles(ciudadC, 110, 20.0, "Hotel 3");
            agencia.agregarHoteles(ciudadD, 85, 10.0, "Hotel 4");
            agencia.agregarHoteles(ciudadE, 70, 100.0, "Hotel 5");



        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            mostrarHoteles();
        }


        private void mostrarHoteles()
        {
            dataGridView1.Rows.Clear();
            foreach (Hotel h in agencia.obtenerHoteles())
                dataGridView1.Rows.Add(h.ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string textoBusqueda1 = textBox1.Text;
            string textoBusqueda2 = textBox3.Text;

            if (string.IsNullOrEmpty(textoBusqueda1) && string.IsNullOrEmpty(textoBusqueda2))
            {
                MessageBox.Show("Por favor, introduzca una búsqueda.");
                return;
            }

            List<Hotel> resultados = new List<Hotel>();
            bool seEncontraronResultados = false;

            foreach (Hotel hotel in agencia.obtenerHoteles())
            {
                if (hotel.nombre.Contains(textoBusqueda1) && hotel.ubicacion.nombre.Contains(textoBusqueda2))
                {
                    resultados.Add(hotel);
                    seEncontraronResultados = true;
                    button3.Visible = true;
                }
            }

            if (!seEncontraronResultados)
            {
                MessageBox.Show("No se encontraron resultados.");
                button3.Visible = false;
            }

            dataGridView2.Rows.Clear();
            foreach (Hotel H in resultados)
            {
                dataGridView2.Rows.Add(H.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}