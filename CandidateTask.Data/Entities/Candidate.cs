using System.ComponentModel.DataAnnotations;

namespace CandidateTask.Data.Entities
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        public string? Phone { get; set; }
        [Required]
        public required string Email { get; set; }
        public string? LinkedInURL { get; set; }
        public string? GitURL { get; set; }
        [Required]
        public required string Comment { get; set; }
    }
}
