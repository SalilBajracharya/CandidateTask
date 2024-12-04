using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CandidateTask.Application.Segregation.Candidates.Command;
using CandidateTask.Data.Entities;

namespace CandidateTask.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<AddUpdateCandidate, Candidate>();
        }
    }
}
