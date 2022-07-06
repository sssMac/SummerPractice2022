using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONData
{
	[Serializable]
	public class BaseModel
	{
		public string? IpPort { get; set; }
		public string? Name { get; set; }
		public string? Data { get; set; }
	}
}
