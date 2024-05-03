using AutoMapper;
using E_Learning_Management_System.DTO;
using E_Learning_Management_System.Models;
using E_Learning_Management_System.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace E_Learning_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IRepository<Instructor> Insrepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public InstructorController(IRepository<Instructor> insrepository, UserManager<ApplicationUser> userManager,  IMapper mapper)
        {
            Insrepository = insrepository;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task <IActionResult> AddInstructor(InstructorDTO instructorDTO)
        {
            if(ModelState.IsValid)
            {
                var currentUser = await userManager.GetUserAsync(User);
                Instructor instructor= mapper.Map<Instructor>(instructorDTO);
                instructor.AccountId = currentUser.Id;
                Insrepository.insert(instructor);
                Insrepository.save();
                return Ok("Instructor added successfully");


            }
            return BadRequest("Invalid instructor data");

        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateInstructor( int id,InstructorDTO instructorDTO)
        {
            Instructor instructor = Insrepository.Get(e=>e.Id == id);

                    if (instructor == null)
                    {
                        return NotFound("Instructor not found");
                    }
            var currentUser = await userManager.GetUserAsync(User);
           instructor.Name = instructorDTO.Name;
            instructor.AccountId = currentUser.Id;
            Insrepository.update(instructor);
            Insrepository.save();
            return Ok("Instructor updated successfully");

        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteInstructor(int id) {
            Instructor instructor = Insrepository.Get(e => e.Id == id);
            if (instructor == null)
            {
                return NotFound("Instructor not found");
            }
            Insrepository.delete(instructor);
            Insrepository.save();
            return Ok("Instructor deleted successfully");


        }
        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            List<Instructor> instructors = Insrepository.GetAll();
            if(instructors != null)
            {
                List<InstructorDTO> instructorDTOs = instructors.Select(e=> new InstructorDTO
                {
                    Name = e.Name,
                }).ToList();
                return Ok(instructorDTOs);
            }

            return NotFound("There is no Instructors");
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetInstructorById(int id) {

            Instructor instructor = Insrepository.Get(e => e.Id == id);
            if (instructor == null)
            {
                return NotFound("Instructor not found");
            }
            InstructorDTO instructorDTO = mapper.Map<InstructorDTO>(instructor);
            return Ok(instructorDTO);

        }


    }
}
