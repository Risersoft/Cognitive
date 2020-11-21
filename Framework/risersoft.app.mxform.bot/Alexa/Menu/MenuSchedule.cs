using Microsoft.VisualBasic;
using risersoft.shared.portable.Models.Nav;
using risersoft.shared.web;
using risersoft.shared.cloud.Cache;
using risersoft.shared.web.Controllers;
using risersoft.shared.cloud.IRepository;
using risersoft.shared.cloud.Providers;
using risersoft.shared.cloud.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Newtonsoft.Json;

namespace CognitiveServiceRsMx.Models
{

	public class MenuSchedule
	{
		[JsonProperty("initialDate")]
		public DateTime InitialDate {
			get { return m_InitialDate; }
			set { m_InitialDate = value; }
		}

		private DateTime m_InitialDate;
		[JsonProperty("menu")]
		public List<Menu> Menu {
			get { return m_Menu; }
			set { m_Menu = value; }
		}

		private List<Menu> m_Menu;
		public Menu GetMenuForDate(DateTime date)
		{
			var daysBetween = (date - InitialDate).TotalDays;
			if (daysBetween < 0) {
				return null;
			}

			// Count forward to find index of menu for the requested date
			var dayOfWeek__1 = DayOfWeek.Monday;
			var menuIndex = 0;
			for (var i = 0; i <= daysBetween - 1; i++) {
				dayOfWeek__1 = IncrementDayOfWeek(dayOfWeek__1);

				// If a week-day, advance to next menu item
				if (IsWeekDay(dayOfWeek__1)) {
					menuIndex += 1;

					// Reset to first item in menu once we've got to the end
					if (menuIndex == Menu.Count) {
						menuIndex = 0;
					}
				}
			}

			return Menu[menuIndex];
		}

		private static DayOfWeek IncrementDayOfWeek(DayOfWeek dayOfWeek__1)
		{
			if (dayOfWeek__1 == DayOfWeek.Saturday) {
				return DayOfWeek.Sunday;
			}

			return (DayOfWeek)Convert.ToInt32(dayOfWeek__1) + 1;
		}

		private static bool IsWeekDay(DayOfWeek dayOfWeek__1)
		{
			return !(dayOfWeek__1 == DayOfWeek.Saturday || dayOfWeek__1 == DayOfWeek.Sunday);
		}
	}
}
