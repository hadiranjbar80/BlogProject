using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Domain.ViewModels
{
    public class ShowUserListViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class EditUserViewModel
    {
        public string Id { get; set; } = string.Empty;
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; } = string.Empty;
        [Display(Name = "غیرفعال کردن کاربر")]
        public bool AccountDisabled { get; set; } = false;
    }

    public class UserRolesViewModel
    {
        public UserRolesViewModel()
        {

        }

        public UserRolesViewModel(string roleName)
        {
            RoleName = roleName;
        }

        public string RoleName { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
    }

    public class AddUserToRoleViewModel
    {
        public AddUserToRoleViewModel()
        {
            UserRoles = new List<UserRolesViewModel>();
        }

        public AddUserToRoleViewModel(string userId, IList<UserRolesViewModel> userRoles)
        {
            UserId = userId;
            UserRoles = userRoles;
        }

        public string UserId { get; set; }
        public IList<UserRolesViewModel> UserRoles { get; set; }
    }
}
