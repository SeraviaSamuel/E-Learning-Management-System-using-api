using AutoMapper;
using E_Learning_Management_System.DTO;
using E_Learning_Management_System.Models;
using E_Learning_Management_System.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace E_Learning_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly IRepository<Certificate> certificateRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IRepository<Quiz> quizRepository;

        public CertificateController(IRepository<Certificate> certificateRepository, UserManager<ApplicationUser> userManager, IMapper mapper , IRepository<Quiz> quizRepository)
        {
            this.certificateRepository = certificateRepository;
            this.userManager = userManager;
            this.mapper = mapper;
            this.quizRepository = quizRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCertificate(CertificateDTO certificateDTO)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userManager.GetUserAsync(User);
                var certificate = mapper.Map<Certificate>(certificateDTO);
                certificate.Date = DateTime.UtcNow; 
                certificateRepository.insert(certificate); 
                 certificateRepository.save();

                return Ok("Certificate added successfully");
            }
            return BadRequest("Invalid Certificate");
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCertificate(int id, CertificateDTO certificateDTO)
        {
            Certificate certificate = certificateRepository.Get(e => e.Id == id);

            if (certificate == null)
            {
                return NotFound("I not found");
            }
            certificate.Date = certificateDTO.Date;
            certificateRepository.update(certificate);  
            certificateRepository.save();
            return Ok("Certificate updated successfully");

        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteCertificate(int id)
        {
           Certificate certificate = certificateRepository.Get(e => e.Id == id);
            if (certificate == null)
            {
                return NotFound("Certtificate not found");
            }
            certificateRepository.delete(certificate);
            certificateRepository.save();
            return Ok("Certificate deleted successfully");


        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            List<Certificate> certificates = certificateRepository.GetAll();
            if (certificates != null)
            {
                List<CertificateDTO> certificateDTOs = certificates.Select(e => new CertificateDTO
                {
                    Date = e.Date,
                }).ToList();
                return Ok(certificateDTOs);
            }

            return NotFound("There is no Certificates");
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetCertficateById(int id)
        {
            Certificate certificate = certificateRepository.Get(e => e.Id == id); 
            if (certificate == null)
            {
                return NotFound("Certtificate not found");
            }
            CertificateDTO certificateDTO = mapper.Map<CertificateDTO>(certificate);
            return Ok(certificateDTO);

        }



        [HttpPost("print")]
        [Authorize]
        public async Task<IActionResult> PrintCertificateForCourse(int theQuizzesId)
        {
            var quizzesForTheQuizzes = quizRepository.GetAll().Where(q => q.TheQuizzesId == theQuizzesId).ToList();

            if (quizzesForTheQuizzes == null || !quizzesForTheQuizzes.Any())
            {
                return NotFound("No quizzes found for the specified TheQuizzes entity");
            }

            var currentUser = await userManager.GetUserAsync(User);

            // Check if the learner has passed all quizzes associated with the specified TheQuizzes entity
            var hasPassedAllQuizzes = CheckIfLearnerPassedAllQuizzes(quizzesForTheQuizzes);

            if (hasPassedAllQuizzes)
            {
                // Generate and issue the certificate
                var certificate = GenerateCertificateForTheQuizzes(currentUser);

                certificateRepository.insert(certificate);
                certificateRepository.save();

                return Ok("Certificate issued successfully");
            }

            return BadRequest("Learner has not passed all quizzes associated with the specified TheQuizzes entity");
        }

        private bool CheckIfLearnerPassedAllQuizzes(IEnumerable<Quiz> quizzesForTheQuizzes)
        {
            foreach (var quiz in quizzesForTheQuizzes)
            {
                if (!CheckIfLearnerPassedQuiz(quiz))
                {
                    // If the learner fails any quiz, return false immediately
                    return false;
                }
            }

            // If the loop completes without returning false, it means the learner passed all quizzes
            return true;
        }

        private Certificate GenerateCertificateForTheQuizzes(ApplicationUser currentUser)
        {
            // Generate certificate for completing all quizzes associated with the TheQuizzes entity
            var certificate = new Certificate
            {
                Date = DateTime.UtcNow,
               // LearnerId = int.Parse(currentUser.Id),
                // Add other relevant information for the certificate
            };

            return certificate;
        }

        private bool CheckIfLearnerPassedQuiz(Quiz quiz)
        {
            return quiz.Mark > 55;
        }




    }
}
