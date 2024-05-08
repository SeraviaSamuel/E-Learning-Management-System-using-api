using AutoMapper;
using E_Learning_Management_System.DTO;
using E_Learning_Management_System.Models;
using E_Learning_Management_System.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearnerController : ControllerBase
    {
        private readonly IRepository<Learner> learnerRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        //Maria controller
        public LearnerController(IRepository<Learner> learnerRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.learnerRepository = learnerRepository;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddLearner(LearnerDTO learnerDTO)
        {
            if (ModelState.IsValid)
            {
                var currenUser = await userManager.GetUserAsync(User);
                Learner learner = mapper.Map<Learner>(learnerDTO);
                learner.AccountId = currenUser.Id;
                learnerRepository.insert(learner);
                learnerRepository.save();
                return Ok("Learner added successfuly");                         

            }
            return BadRequest("can not add");
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            List<Learner> learners = learnerRepository.GetAll();
            if (learners != null)
            {
                List<LearnerDTO> dTOs = new List<LearnerDTO>();
                foreach (Learner learner in learners)
                {
                    LearnerDTO dTO = new LearnerDTO();
                    dTO.Name = learner.Name;
                    dTOs.Add(dTO);
                }
                return Ok(dTOs);
            }
            return NotFound("There is no Learners");
        }
        [HttpGet("ByLearnerId/{learnerId:int}")]
        [Authorize]
        public IActionResult GetLearnerByLearnerId(int learnerId)
        {
            var learner = learnerRepository.Get(c => c.Id == learnerId);
            if (learner == null)
            {
                return NotFound("Learner not found");
            }
            LearnerDTO learnerDTO = mapper.Map<LearnerDTO>(learner);
            return Ok(learnerDTO);
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateLearner(int id, LearnerDTO learnerDTO)
        {
            Learner existingLearner = learnerRepository.Get(l => l.Id == id);
            var currenUser = await userManager.GetUserAsync(User);
            if (existingLearner == null)
            {
                return NotFound("Learner not found");
            }
            var currentUser = await userManager.GetUserAsync(User);
            existingLearner.Name = learnerDTO.Name;
            existingLearner.AccountId = currentUser.Id;
            learnerRepository.update(existingLearner);
            learnerRepository.save();

            return Ok("Learner updated successfully");

        }
        [HttpDelete("{id}")]
        //[Authorize]
        public IActionResult DeleteLearner(int id)
        {
            Learner existingLearner = learnerRepository.Get(l => l.Id == id);
            if (existingLearner == null)
            {
                return NotFound("Learner not found");
            }
            learnerRepository.delete(existingLearner);
            learnerRepository.save();

            return Ok("Learner deleted successfully");
        }
    }
}
