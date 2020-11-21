using CognitiveServiceRsMx.Helpers;
using CognitiveServiceRsMx.Providers;
using Newtonsoft.Json;
using System;

namespace CognitiveServiceRsMx.Models
{

    public class Menu
	{
		[JsonProperty("week")]
		public int WeekNumber { get; set; }

		[JsonProperty("day")]
		public string Day { get; set; }
		[JsonProperty("primo")]
		public string Primo { get; set; }

		[JsonProperty("secondo")]
		public string Secondo { get; set; }
		[JsonProperty("contorno")]
		public string Contorno { get; set; }

		[JsonProperty("dolce")]
		public string Dolce { get; set; }

		public string ToString(DateTime dated, IDateTimeProvider dateTimeProvider = null)
		{
			dateTimeProvider = dateTimeProvider ?? new DateTimeProvider();

			string verbTense = null;
			if (dated == dateTimeProvider.Now().Date) {
				verbTense = "is";
			} else if (dated < dateTimeProvider.Now().Date) {
				verbTense = "was";
			} else {
				verbTense = "will be";
			}

			return string.Format("On {0} {1}, the menu {2} {3}, then {4} with {5}, finishing with {6}", Day, dated.ToStringWithSuffix("d MMMM"), verbTense, Primo, Secondo, Contorno, Dolce);
		}
	}
}
