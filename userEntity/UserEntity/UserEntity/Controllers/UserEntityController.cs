using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserEntity.Dtos;
using UserEntity.Model;
using UserEntity.Service;

namespace UserEntity.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UserEntityController : BaseController
    {

        private readonly IUserService _user;

        public UserEntityController(IUserService user)
        {
            _user = user;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _user.GetAll());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok(await _user.GetUserById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserDto newUser)
        {
            return Ok(await _user.AddNewUser(newUser));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateUser(User User)
        {
            ServiceResponse<List<User>> response = await _user.UpdateOldUser(User);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Ok(await _user.DelUserById(id));
        }
    }
}
