namespace FirstAngularAPIAssignment.DTO
{
    public class EmployeeResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public string ContactNumber { get; set; }
        public List<string> Skills {get;set;}

    }
}
