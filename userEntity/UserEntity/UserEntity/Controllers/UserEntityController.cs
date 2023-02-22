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
            return Ok(new { Data = await _user.GetAll() }, "Data fetched");
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                return Ok(await _user.GetUserById(id), "Data fetched");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserDto newUser)
        {
            try
            {
                return Ok(new { Data = await _user.AddNewUser(newUser) }, "New User Added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateUser(User User)
        {
            try
            {
                return Ok(new { Data = await _user.UpdateOldUser(User) }, "User Updated");
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                return Ok(new { Data = await _user.DelUserById(id) }, "User Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
