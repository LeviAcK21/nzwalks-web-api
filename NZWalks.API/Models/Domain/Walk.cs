namespace NZWalks.API.Models.Domain
{
	public class Walk
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
        public int Description { get; set; }
		public int LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public Guid DifficultyId { get; set; }

		public Guid RegionId { get; set; }
		//Navigation Properties
		public Difficulty Difficulty { get; set; } // Through this we are telling EF Core that the Walk will have a Difficulty and we have a DifficultyId for it and it is samrt enough to run those migrations and create tables as per relationship

		public Region Region { get; set; } // Similarly we tell EF Core that Walk will have a region

		//Here we have a One to One relationship between Walk and Difficulty as well as Walk and Region
    }
}
