using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Todo.Web.Models
{
    public class LoginModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Rememberme { get; set; }
    }

    public class RegisterModel
    {
        [StringLength(250, ErrorMessage = "{0} length must be max. {1}!")]
        [Required]
        [System.Web.Mvc.Remote("ValidateName", "Account")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "{0} must be max. {1}, min {2}!", MinimumLength = 3)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "{0} and {1} fields aren't matching!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [StringLength(250, ErrorMessage = "{0} length must be max. {1}!")]
        [Required]
        [EmailAddress(ErrorMessage = "{0} is invalid!")]
        [System.Web.Mvc.Remote("ValidateEmail", "Account")]
        public string Email { get; set; }
    }

    public class ChangePasswordModel
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "{0} and {1} fields aren't matching!")]
        public string NewPasswordAgain { get; set; }
    }

    public class RolePermissionMatchModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public List<PermissionSelectModel> PermissionSelectModelList { get; set; }
    }

    public class PermissionSelectModel
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string PermissionDesc { get; set; }
        public int PermissionNo { get; set; }
    }
}