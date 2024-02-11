﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentsController : ControllerBase
	{
		[HttpGet]
		public IActionResult GetAllStudents()
		{
			string[] studentNames = new string[] { "Vaibhav", "Abhishek", "Anshul" };

			return Ok(studentNames);
		}
	}
}
