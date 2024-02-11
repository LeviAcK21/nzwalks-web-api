﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
	public class UpdateRegionRequestDto
	{
		[Required]
		[MinLength(3, ErrorMessage = "The code has to be a minimum of 3 charachters.")]
		[MaxLength(3, ErrorMessage = "The code has to be a maximum of 3 charachters.")]
		public string Code { get; set; }
		[Required]
		public string Name { get; set; }
		public string? RegionImageUrl { get; set; }
	}
}
