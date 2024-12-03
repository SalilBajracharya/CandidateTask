﻿namespace CandidateTask.API.Models.Candidates
{
    public class CandidateVM
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Phone { get; set; }
        public required string Email { get; set; }
        public string? LinkedInURL { get; set; }
        public string? GitURL { get; set; }
        public required string Comment { get; set; }
    }
}