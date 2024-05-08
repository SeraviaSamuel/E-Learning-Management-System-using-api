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
    //Maria
    public class TheQuizzesController : ControllerBase
    {
        private readonly IRepository<TheQuizzes> theQuizzesRepository;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public TheQuizzesController(IRepository<TheQuizzes> theQuizzesRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.theQuizzesRepository = theQuizzesRepository;
            this.mapper = mapper;
            this.userManager = userManager;
        }     

        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> AddTheQuizzes(TheQuizzesDTO theQuizzesDTO)
        //{
        //    var currentUser = await userManager.GetUserAsync(User);
        //    if (currentUser == null || currentUser.InstructorId == null)
        //        return BadRequest("Current user or InstructorId is null");

        //    int insId = currentUser.InstructorId.Value;
        //    var theQuizzes = mapper.Map<TheQuizzes>(theQuizzesDTO);
        //    theQuizzes.InstructorId = insId;
        //    theQuizzesRepository.insert(theQuizzes);
        //    theQuizzesRepository.save();

        //    return Ok("TheQuizzes added successfully");
        //}

        [HttpGet]
        [Authorize]
        public IActionResult GetAllTheQuizzes()
        {
            List<TheQuizzes> theQuizzes = theQuizzesRepository.GetAll();
            List<TheQuizzesDTO> theQuizzesDTOs = mapper.Map<List<TheQuizzesDTO>>(theQuizzes);
            return Ok(theQuizzesDTOs);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetTheQuizzesById(int id)
        {
            TheQuizzes theQuizzes = theQuizzesRepository.Get(q => q.Id == id);
            if (theQuizzes == null)
            {
                return NotFound("TheQuizzes not found");
            }
            TheQuizzesDTO theQuizzesDTO = mapper.Map<TheQuizzesDTO>(theQuizzes);
            return Ok(theQuizzesDTO);
        }

        //[HttpPut("{id}")]
        //[Authorize]
        //public async Task<IActionResult> UpdateTheQuizzes(int id, TheQuizzesDTO theQuizzesDTO)
        //{
        //    TheQuizzes existingTheQuizzes = theQuizzesRepository.Get(q => q.Id == id);
        //    var currenUser = await userManager.GetUserAsync(User);
        //    int insId = (int)currenUser.InstructorId;
        //    if (existingTheQuizzes == null)
        //    {
        //        return NotFound("TheQuizzes not found");
        //    }
        //    mapper.Map(theQuizzesDTO, existingTheQuizzes);
        //    theQuizzesRepository.update(existingTheQuizzes);
        //    theQuizzesRepository.save();
        //    return Ok("TheQuizzes updated successfully");
        //}

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteTheQuizzes(int id)
        {
            TheQuizzes existingTheQuizzes = theQuizzesRepository.Get(q => q.Id == id);
            if (existingTheQuizzes == null)
            {
                return NotFound("TheQuizzes not found");
            }
            theQuizzesRepository.delete(existingTheQuizzes);
            theQuizzesRepository.save();
            return Ok("TheQuizzes deleted successfully");
        }
    }
}
