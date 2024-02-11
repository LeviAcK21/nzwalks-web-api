using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{	//https://localhost:123/api/regions
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{
		private readonly NZWalksDbContext dbContext;
		private readonly IRegionRepository regionRepository;
		private readonly IMapper mapper;
		private readonly ILogger<RegionsController> logger;

		public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
			this.dbContext = dbContext;
			this.regionRepository = regionRepository;
			this.mapper = mapper;
			this.logger = logger;
		}
        [HttpGet]
		[Authorize(Roles = "Reader, Writer")]
		public async Task<IActionResult> GetAll()
		{
			logger.LogInformation("GetAllRegions Action method was invoked");
			//Get data from the database - Domain Models
			var regions = await regionRepository.GetAllAsync();

			/*//Map domain mdels to DTOs
			var regionDto = new List<RegionDto>();
			foreach (var region in regions) 
			{
				regionDto.Add(
					new RegionDto()
					{
						Id = region.Id,
						Name = region.Name,
						Code = region.Code,
						RegionImageUrl = region.RegionImageUrl,
					}
				);
			}
			*/
			var regionDto = mapper.Map<List<RegionDto>>(regions); // Mapping domain models to DTOs

			logger.LogInformation($"Finished GetAllRegions request with data : {JsonSerializer.Serialize(regions)}");

			return Ok(regionDto);
		}

		[HttpGet]
		[Route("{id:Guid}")]
		[Authorize(Roles = "Reader, Writer")]
		public async Task<IActionResult> GetById( [FromRoute] Guid id)
		{
			//var regions = dbContext.Regions.Find(id); works with the primary key

			var region = await regionRepository.GetByIdAsync(id);
			if ( region == null )
			{
				return NotFound();
			}

			//Map domain model to DTO
			var regionDto = mapper.Map<RegionDto>(region);
			return Ok(regionDto);
		}

		//POST To create new region
		[HttpPost]
		[ValidateModel]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
		{
				// Map or convert DTO to Domain Model
				var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
				// Use Domain Model to create region

				await regionRepository.CreateAsync(regionDomainModel);

				// Map domiain Model back to DTO

				var regionDto = mapper.Map<RegionDto>(regionDomainModel);

				return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
		}

		// Update region
		[HttpPut]
		[ValidateModel]
		[Route("{id:Guid}")]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
		{
				var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
				regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

				if (regionDomainModel == null)
				{
					return NotFound();
				}

				// Convert Domain Model to DTO
				var regionDto = mapper.Map<RegionDto>(regionDomainModel);

				return Ok(regionDto);
		}

		// Delete a region 
		[HttpDelete]
		[Route("{id:Guid}")]
		[Authorize(Roles = "Writer")] // Specifys that a user with specified role will be able to access the resource

		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var regionDomainModel = await regionRepository.DeleteAsync(id);
			if(regionDomainModel == null)
			{
				return NotFound();
			}
			// Map Domain Model to DTO
			var regionDto = mapper.Map<RegionDto>(regionDomainModel);

			return Ok(regionDto);
		}
	}
}
