using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeseetaProject.Core.Models
{
	public enum Gender
	{
		Female,
		Male
	}
	public enum Days
	{
		Saturday,
		Sunday,
		Monday,
		Tuesday,
		Wednesday,
		Thursday,
		Friday
	}
	public enum DiscountType
	{
        Percentage,
		Value
    }
    public enum BookingStatus
    {
        Pending,
        Completed,
        Canceled,
        NotSet
    }
	public enum AccountType
	{
		Admin,
		Doctor,
		Patient,
	}
}
