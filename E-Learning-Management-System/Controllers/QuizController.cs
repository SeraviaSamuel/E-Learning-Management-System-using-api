using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Learning_Management_System.DTO;
using E_Learning_Management_System.Models;
using E_Learning_Management_System.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;



namespace E_Learning_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IRepository<Quiz> quizRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public QuizController(IRepository<Quiz> quizRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.quizRepository = quizRepository;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddQuiz(QuizDTO quizDTO)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userManager.GetUserAsync(User);
                int learnerId = (int)currentUser.LearnerId;
                Quiz quiz = mapper.Map<Quiz>(quizDTO);
                quiz.LearnerId = learnerId;
                quizRepository.insert(quiz);
                quizRepository.save();
                return Ok("Quiz added successfully");
            }
            return BadRequest("Cannot add quiz");
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            List<Quiz> quizzes = quizRepository.GetAll();
            if (quizzes != null)
            {
                List<QuizDTO> dtos = new List<QuizDTO>();
                foreach (var quiz in quizzes)
                {
                    QuizDTO dto = new QuizDTO
                    {
                        Id = quiz.Id,
                        Mark = quiz.Mark,
                    };
                    dtos.Add(dto);
                }
                return Ok(dtos);
            }
            return NotFound("No quizzes found");
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetQuizById(int id)
        {
            var quiz = quizRepository.Get(q => q.Id == id);
            if (quiz == null)
            {
                return NotFound("Quiz not found");
            }
            QuizDTO dto = mapper.Map<QuizDTO>(quiz);
            return Ok(dto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateQuiz(int id, QuizDTO quizDTO)
        {
            Quiz existingQuiz = quizRepository.Get(q => q.Id == id);
            if (existingQuiz == null)
            {
                return NotFound("Quiz not found");
            }
            mapper.Map(quizDTO, existingQuiz);
            quizRepository.update(existingQuiz);
            quizRepository.save();
            return Ok("Quiz updated successfully");
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteQuiz(int id)
        {
            Quiz existingQuiz = quizRepository.Get(q => q.Id == id);
            if (existingQuiz == null)
            {
                return NotFound("Quiz not found");
            }
            quizRepository.delete(existingQuiz);
            quizRepository.save();
            return Ok("Quiz deleted successfully");
        }
    }
}
