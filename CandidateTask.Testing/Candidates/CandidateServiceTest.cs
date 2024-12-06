using AutoMapper;
using CandidateTask.Application.Common.Interface;
using CandidateTask.Application.Segregation.Candidates.Command;
using CandidateTask.Data.Entities;
using CandidateTask.Infrastructure.Services.Candidates;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Moq;

namespace CandidateTask.Testing.Candidates
{
    public class CandidateServiceTest
    {
        private readonly Mock<IApplicationDbContext> _mockCtx;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CandidateService _candidateService;
        private readonly Mock<ILogger<CandidateService>> _mockLogger;  // Mock ILogger

        public CandidateServiceTest()
        {
            _mockCtx = new Mock<IApplicationDbContext>();
            _mockMapper = new Mock<IMapper>();

            _mockLogger = new Mock<ILogger<CandidateService>>(); // Initialize the logger mock


            _candidateService = new CandidateService(_mockCtx.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task AddCandidate_ShouldAddNewCandidate()
        {
            var request = new AddUpdateCandidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "test@email.com",
                Comment = "Hi ! My name is John !"
            };

            var candidate = new Candidate
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Comment = request.Comment
            };

            _mockMapper.Setup(m => m.Map<Candidate>(It.IsAny<AddUpdateCandidate>())).Returns(candidate);
            _mockCtx.Setup(c => c.Candidates.AddAsync(candidate, It.IsAny<CancellationToken>())).ReturnsAsync((EntityEntry<Candidate>)null);
            _mockCtx.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            await _candidateService.Add(candidate, CancellationToken.None);

            _mockCtx.Verify(c => c.Candidates.AddAsync(It.IsAny<Candidate>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockCtx.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateCandidate_ShouldUpdateExistingCandidate()
        {
            var existingCandidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "test@email.com",
                Comment = "Hi ! My name is John !"
            };

            var request = new AddUpdateCandidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "test1@email.com",
                Comment = "Hi ! My name is John Updated !"
            };

            _mockMapper.Setup(m => m.Map(request, existingCandidate))
                .Callback<AddUpdateCandidate, Candidate>((src, dest) =>
                {
                    dest.FirstName = src.FirstName;
                    dest.LastName = src.LastName;
                    dest.Comment = src.Comment;
                });

            _mockCtx.Setup(c => c.Candidates.Update(existingCandidate)).Verifiable();
            _mockCtx.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            await _candidateService.Update(existingCandidate, CancellationToken.None);

            _mockCtx.Verify(c => c.Candidates.Update(It.IsAny<Candidate>()), Times.Once);
            _mockCtx.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
