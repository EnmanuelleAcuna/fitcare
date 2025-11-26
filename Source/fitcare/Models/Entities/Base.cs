using System;

namespace fitcare.Models.Entities;

public class Base
{
	public DateTime DateCreated { get; set; }
	public string CreatedBy { get; set; }
	public DateTime? DateUpdated { get; set; }
	public string UpdatedBy { get; set; }
}
