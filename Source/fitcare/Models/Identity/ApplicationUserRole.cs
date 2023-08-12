using Microsoft.AspNetCore.Identity;

namespace fitcare.Models.Identity;

public class ApplicationUserRole : IdentityUserRole<string>
{
	public virtual ApplicationUser User { get; set; }
	public virtual ApplicationRole Role { get; set; }
}
