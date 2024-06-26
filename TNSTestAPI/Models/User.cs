namespace TNSTestAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        // Foreign key
        public int DepartmentId { get; set; }

        // Navigation property
        public Department? Department { get; set; }
    }
}
