namespace Agencia.Models
{
    public class ReservaHabitacionJson
    {
        public int hotel_id { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public double total { get; set; }
        public int sm_rooms { get; set; }
        public int md_rooms { get; set; }
        public int xl_rooms { get; set; }
        public int people { get; set; }
    }
}
