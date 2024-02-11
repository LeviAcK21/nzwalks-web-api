using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
	public class UpdateWalkRequestDto
	{
		[Required]
		[MinLength(5, ErrorMessage = "The name has to be a minimum of 3 charachters.")]
		[MaxLength(10, ErrorMessage = "The name has to be a maximum of 3 charachters.")]
		public string Name { get; set; }
		public int Description { get; set; }
		public int LengthInKm { get; set; }
		public string? WalkImageUrl { get; set; }

		public Guid DifficultyId { get; set; }

		public Guid RegionId { get; set; }
	}
}
