namespace TNSTestAPI.Models
{
    public class User
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public string? full_name { get; set; }

        // Foreign key
        public int department_id { get; set; }

    }
}
