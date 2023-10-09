using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace fitcare.Models.Identity;

[Keyless]
public class ApplicationUserRole : IdentityUserRole<string>
{
	public virtual ApplicationUser User { get; set; }
	public virtual ApplicationRole Role { get; set; }
}
