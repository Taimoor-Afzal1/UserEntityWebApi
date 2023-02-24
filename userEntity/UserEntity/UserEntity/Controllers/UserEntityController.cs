using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserEntity.Dtos;
using UserEntity.Model;
using UserEntity.Service.UserServices;

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

        [HttpGet("{Currentpage}/ {PageItems}")]
        public async Task<IActionResult> Get(int Currentpage, int PageItems)
        {
            return Ok(new { Data = await _user.GetAll(Currentpage, PageItems) }, "Data fetched");
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
            catch {
                return BadRequest("User not found");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _user.DelUserById(id);
                return Ok("User Deleted");
            }
            catch
            {
                return BadRequest("User not found");
            }
        }
    }
}
