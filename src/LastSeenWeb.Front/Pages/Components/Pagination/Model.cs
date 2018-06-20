namespace LastSeenWeb.Front.Pages.Components.Pagination
{
	public class Model
	{
		public int ItemCount { get; set; }
		public int PageNumber { get; set; }

		public bool HasItems => ItemCount != 0;
	}
}
