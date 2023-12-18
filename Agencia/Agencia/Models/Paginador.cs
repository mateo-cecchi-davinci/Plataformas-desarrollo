namespace Agencia.Models
{
    public class Paginador
    {
        public int items {  get; set; }
        public int pagina { get; set; }
        public int tamanio { get; set; }
        public int total { get; set; }
        public int inicio { get; set; }
        public int fin {  get; set; }

        public Paginador() { }

        public Paginador(int items, int pagina, int tamanio = 10)
        {
            int total = (int)Math.Ceiling((decimal)items / (decimal)tamanio);

            int pagina_inicial = pagina - 5;
            int pagina_final = pagina + 4;

            if (pagina_inicial <= 0)
            {
                pagina_final = pagina_final - (pagina_inicial - 1);
                pagina_inicial = 1;
            }

            if (pagina_final > total)
            {
                pagina_final = total;
                
                if (pagina_final > 10)
                {
                    pagina_inicial = pagina_final - 9;
                }
            }

            this.items = items;
            this.pagina = pagina;
            this.tamanio = tamanio;
            this.total = total;
            inicio = pagina_inicial;
            fin = pagina_final;
        }
    }
}
