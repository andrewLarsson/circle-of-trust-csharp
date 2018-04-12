using System.Collections.Generic;
using System.Linq;

namespace AndrewLarsson.Common.View {
	public class PagingRecordSet<TRecord> {
		public PagingMetaData PagingMetaData { get; set; }
		public RecordsMetaData RecordsMetaData { get; set; }
		public List<TRecord> Records { get; set; }

		public PagingRecordSet() {
		}

		public PagingRecordSet(PagingMetaData pagingMetaData, long totalCount, IEnumerable<TRecord> records) {
			PagingMetaData = pagingMetaData;
			RecordsMetaData = new RecordsMetaData(totalCount, PagingMetaData);
			Records = records.ToList();
		}
	}
}
