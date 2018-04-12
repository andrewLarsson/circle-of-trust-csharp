using AndrewLarsson.Common.View;

namespace AndrewLarsson.CircleOfTrust.View.Queries {
	public class ReadAllPlayerStatsQuery : IQuery {
		public PagingMetaData PagingMetaData { get; set; }
	}
}
