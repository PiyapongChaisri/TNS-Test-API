namespace TNSTestAPI.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string? Name { get; set; }

        // Navigation property
        public List<User>? Users { get; set; }
    }
}

