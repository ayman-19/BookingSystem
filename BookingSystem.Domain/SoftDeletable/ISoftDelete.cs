namespace BookingSystem.Domain.SoftDeletable
{
	public interface ISoftDelete
	{
		public bool IsDeleted { get; set; }
		public DateTime DeletedOn { get; set; }
		public void Delete()
		{
			DeletedOn = DateTime.Now;
			IsDeleted = true;
		}
	}
}
