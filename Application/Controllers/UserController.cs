﻿using Application.Specifications;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    public class UserController:ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers([FromQuery] string? userName, [FromQuery] string? name)
        {
            return Ok(_unitOfWork.Repository<User>()
                .Find(new UserSpecification.UserSearchSpecification(userName: userName, name: name)).ToList());
        }

        [HttpGet("id")]
        public ActionResult<User> GetById(int id)
        {
            var user = _unitOfWork.Repository<User>().GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            try
            {
                _unitOfWork.Repository<User>().Add(user);

                _unitOfWork.Complete();
                return CreatedAtAction("GetById", new { id = user.Id }, user);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}