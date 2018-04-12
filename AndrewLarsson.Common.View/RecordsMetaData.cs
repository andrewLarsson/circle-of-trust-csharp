namespace AndrewLarsson.Common.View {
	public class RecordsMetaData {
		public long TotalRecordCount { get; set; }
		public long TotalPagesCount { get; set; }

		public RecordsMetaData() {
		}

		public RecordsMetaData(long totalCount, PagingMetaData pagingMetaData) {
			TotalRecordCount = totalCount;
			TotalPagesCount = ((TotalRecordCount <= 0 || pagingMetaData.PageSize <= 0)
				? 0
				: (
					(TotalRecordCount / pagingMetaData.PageSize)
					+ (((TotalRecordCount % pagingMetaData.PageSize) == 0)
						? 0
						: 1
					)
				)
			);
		}
	}
}
