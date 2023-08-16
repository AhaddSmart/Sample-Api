//using Application.Common.Models;
//using Application.DTOs;
//using AutoMapper;
//using Infrastructure.Identity;
//using Infrastructure.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using WebUI.Controllers;

//namespace WebAPI.Controllers;

//public class AccountController : ApiControllerBase
//{
//    private readonly UserManager<ApplicationUser> _userManager;
//    private readonly SignInManager<ApplicationUser> _signInManager;
//    private readonly RoleManager<IdentityRole> _roleManager;
//    private readonly IMapper _mapper;
//    private readonly IConfiguration _configuration;

//    public AccountController(UserManager<ApplicationUser> userManager, IMapper mapper, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
//    {
//        _userManager = userManager;
//        _signInManager = signInManager;
//        _roleManager = roleManager;
//        _mapper = mapper;
//        _configuration = configuration;
//    }
//    //[Authorize(Policy = UserDefault.AllUserAccess)]
//    [HttpPost]
//    [Route("v1/Login")]
//    public async Task<ResponseHelper> Login([FromBody] UserLoginModel model)
//    {
//        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);
//        if (result.Succeeded)
//        {
//            var user = await _userManager.FindByNameAsync(model.UserName);
//            var role = await _userManager.GetRolesAsync(user);
//            var userClaims = await _userManager.GetClaimsAsync(user);
//            string roleName = "";
//            if (role.Count > 0)
//                roleName = role[0];

//            var authClaims = new List<Claim>
//                {
//                    new Claim(ClaimTypes.Name, user.UserName),
//                    new Claim("userId", user.Id),
//                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                };

//            foreach (var userRole in role)
//            {
//                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
//                var roles = await _roleManager.FindByNameAsync(userRole);
//                if (roles != null)
//                {
//                    var roleClaims = await _roleManager.GetClaimsAsync(roles);
//                    foreach (Claim roleClaim in roleClaims)
//                    {
//                        authClaims.Add(roleClaim);
//                    }
//                }
//            }
//            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

//            var token = new JwtSecurityToken(
//                issuer: _configuration["JWT:ValidIssuer"],
//                audience: _configuration["JWT:ValidAudience"],
//                expires: DateTime.Now.AddHours(24),
//                claims: authClaims,
//                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
//                );


//            var tokn = new JwtSecurityTokenHandler().WriteToken(token);
//            var expiration = token.ValidTo;

//            var data = new { Id = user.Id, UserName = user.UserName, Email = user.Email, Role = roleName, token = tokn };

//            return new ResponseHelper(1, data, new ErrorDef());
//        }
//        return new ResponseHelper(0, new object(), new ErrorDef(-1, "Error", "Invalid UserName or Password", "error"));
//    }


//    //[Authorize(Policy = Admin.defult+Users.Create)]
//    [HttpPost]
//    [Route("v1/Register")]
//    public async Task<IdentityResult> Register([FromBody] UserModel model)
//    {
//        if (ModelState.IsValid)
//        {
//            var user = new ApplicationUser
//            {
//                UserName = model.UserName,
//                Email = model.UserName,
//                PhoneNumber = model.PhoneNumber
//            };

//            var result = await _userManager.CreateAsync(user, model.Password);
//            if (result.Succeeded)
//            {
//                IdentityResult _identity = new IdentityResult();
//                var find = _roleManager.Roles.Where(r => r.Name == model.Role);
//                if (find.Count() > 0)
//                {
//                    var finduser = await _userManager.FindByNameAsync(model.UserName);
//                    result = await _userManager.AddToRoleAsync(finduser, model.Role);
//                }
//            }

//            return result;
//        }
//        return await Task.FromResult<IdentityResult>(null);

//    }
//    [Authorize(Policy = Admin.defult + Users.Edit)]
//    [HttpPut]
//    [Route("v1/UpdateUser")]
//    public async Task<ResponseHelper> EditUser([FromBody] UserModel model)
//    {

//        var currentUser = await _userManager.FindByIdAsync(model.UserId);
//        var uniqueUserName = await _userManager.FindByNameAsync(model.UserName);

//        if (currentUser != null)
//        {
//            if (uniqueUserName != null && uniqueUserName.Id != currentUser.Id)
//            {
//                return new ResponseHelper(0, new { message = "UserName already exists" }, new ErrorDef(0, " ", ""));
//            }
//            if (model.OldPassword != "" && model.OldPassword != null)
//            {
//                var pass_changed = await _userManager.ChangePasswordAsync(currentUser, model.OldPassword, model.Password);
//                if (!pass_changed.Succeeded)
//                {
//                    return new ResponseHelper(0, new { message = "Old Password not match new Password" }, new ErrorDef(0, " ", ""));
//                }
//            }
//            //var token = await _userManager.GeneratePasswordResetTokenAsync(currentUser);

//            //await _userManager.ResetPasswordAsync(currentUser, token, model.Password);
//            currentUser.UserName = model.UserName;
//            currentUser.PhoneNumber = model.PhoneNumber;
//            currentUser.Email = model.Email;
//            var result = await _userManager.UpdateAsync(currentUser);
//            if (result.Succeeded)
//                return new ResponseHelper(1, new { message = "User has been Updated" }, new ErrorDef(0, " ", ""));

//            return new ResponseHelper(0, new { message = "User has not been Updated" }, new ErrorDef(0, " ", ""));
//        }
//        else
//        {
//            return new ResponseHelper(0, new { message = "User has been found" }, new ErrorDef(0, " ", ""));

//        }


//    }
//    [Authorize(Policy = Admin.defult + Users.Delete)]
//    [HttpDelete]
//    [Route("v1/DeleteUser")]
//    public async Task<ResponseHelper> DeleteUser(string id)
//    {
//        var user = await _userManager.FindByIdAsync(id);

//        var result = await _userManager.DeleteAsync(user);
//        if (result.Succeeded)
//        {
//            //TempData["UserDeleted"] = "User Successfully Deleted";
//            return new ResponseHelper(1, new { message = "User has been deleted" }, new ErrorDef(0, " ", ""));
//        }
//        else
//        {
//            //TempData["UserDeleted"] = "Error Deleting User";
//            return new ResponseHelper(1, new { message = "User has not been deleted" }, new ErrorDef(0, " ", ""));
//        }
//    }

//    [Authorize(Policy = Admin.defult + Roles.Create)]
//    [HttpPost]
//    [Route("v1/CreateRole")]
//    public async Task<ResponseHelper> CreateRole([FromBody] IdentityRole role)
//    {

//        IdentityResult result = new IdentityResult();
//        if (_roleManager.Roles.All(r => r.Name != role.Name))
//        {
//            var RoleName = new IdentityRole(role.Name);
//            result = await _roleManager.CreateAsync(RoleName);
//            return new ResponseHelper(1, new { message = "Role has been Added" }, new ErrorDef(0, " ", ""));
//        }
//        else
//            return new ResponseHelper(0, new { message = "This Role is Already Exist" }, new ErrorDef(0, " ", ""));
//    }



//    [Authorize(Policy = Admin.defult + Roles.Edit)]
//    [HttpPut]
//    [Route("v1/UpdateRole")]
//    public async Task<ResponseHelper> UpdateRole([FromBody] UserRoleModel role)
//    {
//        IdentityResult result = new IdentityResult();
//        IdentityRole roleToEdit = await _roleManager.FindByIdAsync(role.id);
//        roleToEdit.Name = role.name;
//        if (_roleManager.Roles.All(r => r.Name != role.name))
//        {
//            result = await _roleManager.UpdateAsync(roleToEdit);
//            return new ResponseHelper(1, new { message = "Role has been Updated" }, new ErrorDef(0, " ", ""));
//        }
//        else
//            return new ResponseHelper(0, new { message = "This Role is Already Exist" }, new ErrorDef(0, " ", ""));
//    }



//    [Authorize(Policy = Admin.defult + Roles.Create)]
//    [HttpPut]
//    [Route("v1/AssignRoleToUser")]
//    public async Task<IdentityResult> AssignRoleToUser([FromBody] AssignRoleModel assignRole)
//    {
//        IdentityResult result = new IdentityResult();
//        var find = _roleManager.Roles.Where(r => r.Name == assignRole.Role);
//        if (find.Count() > 0)
//        {
//            var user = await _userManager.FindByIdAsync(assignRole.UserID);
//            result = await _userManager.AddToRoleAsync(user, assignRole.Role);
//        }
//        return result;
//    }


//    [Authorize(Policy = Admin.defult + Roles.View)]
//    [HttpGet]
//    [Route("v1/GetAllRoles")]
//    public async Task<ResponseHelper> GetAllRoles()
//    {
//        var result = _roleManager.Roles.Where(x => x.Name != "Administrator").OrderByDescending(x => x.Id).ToList();
//        if (result.Count() > 0)
//            return new ResponseHelper(1, result, new ErrorDef());
//        else
//            return new ResponseHelper(0, new object(), new ErrorDef());

//    }
//    [Authorize(Policy = Admin.defult + Users.View)]
//    [HttpGet]
//    [Route("v1/GetAllUser")]
//    public async Task<ResponseHelper> GetAllUser()
//    {
//        try
//        {

//            List<UserModel> allUsers = new List<UserModel>();
//            var _User = _userManager.Users.OrderByDescending(x => x.Id).ToList();
//            for (int i = 0; i < _User.Count(); i++)
//            {
//                var UserRole = await _userManager.GetRolesAsync(_User[i]);
//                UserModel user = new UserModel();
//                user.UserId = _User[i].Id;
//                user.UserName = _User[i].UserName;
//                user.Email = _User[i].Email;
//                user.PhoneNumber = _User[i].PhoneNumber;
//                if (UserRole.Count() > 0)
//                    user.Role = UserRole[0];
//                else
//                    user.Role = "";
//                if (user.Role != "Administrator")
//                    allUsers.Add(user);
//            }
//            if (allUsers.Count > 0)
//            {
//                return new ResponseHelper(1, allUsers, new ErrorDef());
//            }
//            else
//            {
//                return new ResponseHelper(0, new object(), new ErrorDef());
//            }
//        }
//        catch (Exception ex)
//        {
//            return new ResponseHelper(0, new object(), new ErrorDef(-1, "Error", ex.Message, "error"));
//        }
//    }

//    [Authorize(Policy = Admin.admin)]
//    [HttpPost]
//    [Route("v1/AssignPermission")]
//    public async Task<ResponseHelper> AssignPermission(PermissionModel permissionModel)
//    {
//        IdentityRole _role = await _roleManager.FindByNameAsync(permissionModel.role);
//        if (_role != null)
//        {
//            var claims = await _roleManager.GetClaimsAsync(_role);
//            foreach (var claim in claims)
//            {
//                await _roleManager.RemoveClaimAsync(_role, claim);
//            }
//            foreach (var item in permissionModel.Permission)
//            {
//                await _roleManager.AddClaimAsync(_role, new Claim("Permission", item));
//            }

//            return new ResponseHelper(1, new { message = "Permission has been Assigned" }, new ErrorDef(0, " ", ""));
//        }
//        else
//            return new ResponseHelper(0, new { message = "Permission Not Assigned" }, new ErrorDef(0, " ", ""));

//    }


//    [Authorize(Policy = UserDefault.AllUserAccess)]
//    [HttpGet]
//    [Route("v1/GetAssignPermission")]
//    public async Task<ResponseHelper> GetUserAssignPermission(string role)
//    {
//        IdentityRole _role = await _roleManager.FindByNameAsync(role);
//        if (_role != null)
//        {
//            var result = await _roleManager.GetClaimsAsync(_role);
//            if (result.Count > 0)
//                return new ResponseHelper(1, result, new ErrorDef(0, " ", ""));
//            else
//                return new ResponseHelper(0, new object(), new ErrorDef(0, " ", ""));

//        }
//        else
//            return new ResponseHelper(0, new object(), new ErrorDef(0, " ", ""));

//    }
//}


using Application.Common.Models;
using Application.DTOs;
using AutoMapper;
using Infrastructure.Identity;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebUI.Controllers;

namespace WebAPI.Controllers;

public class AccountController : ApiControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AccountController(UserManager<ApplicationUser> userManager, IMapper mapper, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _configuration = configuration;
    }
    //[Authorize(Policy = UserDefault.AllUserAccess)]
    [HttpPost]
    [Route("v1/Login")]
    public async Task<ResponseHelper> Login([FromBody] UserLoginModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            var role = await _userManager.GetRolesAsync(user);
            var userClaims = await _userManager.GetClaimsAsync(user);
            string roleName = "";
            if (role.Count > 0)
                roleName = role[0];

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("userId", user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in role)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                var roles = await _roleManager.FindByNameAsync(userRole);
                if (roles != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(roles);
                    foreach (Claim roleClaim in roleClaims)
                    {
                        authClaims.Add(roleClaim);
                    }
                }
            }
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(24),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );


            var tokn = new JwtSecurityTokenHandler().WriteToken(token);
            var expiration = token.ValidTo;

            var data = new { Id = user.Id, UserName = user.UserName, Email = user.Email, Role = roleName, token = tokn };

            return new ResponseHelper(1, data, new ErrorDef());
        }
        return new ResponseHelper(0, new object(), new ErrorDef(-1, "Error", "Invalid UserName or Password", "error"));
    }


    //[Authorize(Policy = Admin.defult+Users.Create)]
    [HttpPost]
    [Route("v1/Register")]
    public async Task<IdentityResult> Register([FromBody] UserModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.UserName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                IdentityResult _identity = new IdentityResult();
                var find = _roleManager.Roles.Where(r => r.Name == model.Role);
                if (find.Count() > 0)
                {
                    var finduser = await _userManager.FindByNameAsync(model.UserName);
                    result = await _userManager.AddToRoleAsync(finduser, model.Role);
                }
            }

            return result;
        }
        return await Task.FromResult<IdentityResult>(null);

    }
    [Authorize(Policy = Admin.defult + Users.Edit)]
    [HttpPut]
    [Route("v1/UpdateUser")]
    public async Task<ResponseHelper> EditUser([FromBody] UserModel model)
    {

        var currentUser = await _userManager.FindByIdAsync(model.UserId);
        var uniqueUserName = await _userManager.FindByNameAsync(model.UserName);

        if (currentUser != null)
        {
            if (uniqueUserName != null && uniqueUserName.Id != currentUser.Id)
            {
                return new ResponseHelper(0, new { message = "UserName already exists" }, new ErrorDef(0, " ", ""));
            }
            if (model.OldPassword != "" && model.OldPassword != null)
            {
                var pass_changed = await _userManager.ChangePasswordAsync(currentUser, model.OldPassword, model.Password);
                if (!pass_changed.Succeeded)
                {
                    return new ResponseHelper(0, new { message = "Old Password not match new Password" }, new ErrorDef(0, " ", ""));
                }
            }
            //var token = await _userManager.GeneratePasswordResetTokenAsync(currentUser);

            //await _userManager.ResetPasswordAsync(currentUser, token, model.Password);
            currentUser.UserName = model.UserName;
            currentUser.PhoneNumber = model.PhoneNumber;
            currentUser.Email = model.Email;
            var result = await _userManager.UpdateAsync(currentUser);
            if (result.Succeeded)
                return new ResponseHelper(1, new { message = "User has been Updated" }, new ErrorDef(0, " ", ""));

            return new ResponseHelper(0, new { message = "User has not been Updated" }, new ErrorDef(0, " ", ""));
        }
        else
        {
            return new ResponseHelper(0, new { message = "User has been found" }, new ErrorDef(0, " ", ""));

        }


    }
    [Authorize(Policy = Admin.defult + Users.Delete)]
    [HttpDelete]
    [Route("v1/DeleteUser")]
    public async Task<ResponseHelper> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            //TempData["UserDeleted"] = "User Successfully Deleted";
            return new ResponseHelper(1, new { message = "User has been deleted" }, new ErrorDef(0, " ", ""));
        }
        else
        {
            //TempData["UserDeleted"] = "Error Deleting User";
            return new ResponseHelper(1, new { message = "User has not been deleted" }, new ErrorDef(0, " ", ""));
        }
    }

    [Authorize(Policy = Admin.defult + Roles.Create)]
    [HttpPost]
    [Route("v1/CreateRole")]
    public async Task<ResponseHelper> CreateRole([FromBody] IdentityRole role)
    {

        IdentityResult result = new IdentityResult();
        if (_roleManager.Roles.All(r => r.Name != role.Name))
        {
            var RoleName = new IdentityRole(role.Name);
            result = await _roleManager.CreateAsync(RoleName);
            return new ResponseHelper(1, new { message = "Role has been Added" }, new ErrorDef(0, " ", ""));
        }
        else
            return new ResponseHelper(0, new { message = "This Role is Already Exist" }, new ErrorDef(0, " ", ""));
    }



    [Authorize(Policy = Admin.defult + Roles.Edit)]
    [HttpPut]
    [Route("v1/UpdateRole")]
    public async Task<ResponseHelper> UpdateRole([FromBody] UserRoleModel role)
    {
        IdentityResult result = new IdentityResult();
        IdentityRole roleToEdit = await _roleManager.FindByIdAsync(role.id);
        roleToEdit.Name = role.name;
        if (_roleManager.Roles.All(r => r.Name != role.name))
        {
            result = await _roleManager.UpdateAsync(roleToEdit);
            return new ResponseHelper(1, new { message = "Role has been Updated" }, new ErrorDef(0, " ", ""));
        }
        else
            return new ResponseHelper(0, new { message = "This Role is Already Exist" }, new ErrorDef(0, " ", ""));
    }



    [Authorize(Policy = Admin.defult + Roles.Create)]
    [HttpPut]
    [Route("v1/AssignRoleToUser")]
    public async Task<IdentityResult> AssignRoleToUser([FromBody] AssignRoleModel assignRole)
    {
        IdentityResult result = new IdentityResult();
        var find = _roleManager.Roles.Where(r => r.Name == assignRole.Role);
        if (find.Count() > 0)
        {
            var user = await _userManager.FindByIdAsync(assignRole.UserID);
            result = await _userManager.AddToRoleAsync(user, assignRole.Role);
        }
        return result;
    }


    [Authorize(Policy = Admin.defult + Roles.View)]
    [HttpGet]
    [Route("v1/GetAllRoles")]
    public async Task<ResponseHelper> GetAllRoles()
    {
        var result = _roleManager.Roles.Where(x => x.Name != "Administrator").OrderByDescending(x => x.Id).ToList();
        if (result.Count() > 0)
            return new ResponseHelper(1, result, new ErrorDef());
        else
            return new ResponseHelper(0, new object(), new ErrorDef());

    }
    [Authorize(Policy = Admin.defult + Users.View)]
    [HttpGet]
    [Route("v1/GetAllUser")]
    public async Task<ResponseHelper> GetAllUser()
    {
        try
        {

            List<UserModel> allUsers = new List<UserModel>();
            var _User = _userManager.Users.OrderByDescending(x => x.Id).ToList();
            for (int i = 0; i < _User.Count(); i++)
            {
                var UserRole = await _userManager.GetRolesAsync(_User[i]);
                UserModel user = new UserModel();
                user.UserId = _User[i].Id;
                user.UserName = _User[i].UserName;
                user.Email = _User[i].Email;
                user.PhoneNumber = _User[i].PhoneNumber;
                if (UserRole.Count() > 0)
                    user.Role = UserRole[0];
                else
                    user.Role = "";
                if (user.Role != "Administrator")
                    allUsers.Add(user);
            }
            if (allUsers.Count > 0)
            {
                return new ResponseHelper(1, allUsers, new ErrorDef());
            }
            else
            {
                return new ResponseHelper(0, new object(), new ErrorDef());
            }
        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object(), new ErrorDef(-1, "Error", ex.Message, "error"));
        }
    }

    [Authorize(Policy = Admin.admin)]
    [HttpPost]
    [Route("v1/AssignPermission")]
    public async Task<ResponseHelper> AssignPermission(PermissionModel permissionModel)
    {
        IdentityRole _role = await _roleManager.FindByNameAsync(permissionModel.role);
        if (_role != null)
        {
            var claims = await _roleManager.GetClaimsAsync(_role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(_role, claim);
            }
            foreach (var item in permissionModel.Permission)
            {
                await _roleManager.AddClaimAsync(_role, new Claim("Permission", item));
            }

            return new ResponseHelper(1, new { message = "Permission has been Assigned" }, new ErrorDef(0, " ", ""));
        }
        else
            return new ResponseHelper(0, new { message = "Permission Not Assigned" }, new ErrorDef(0, " ", ""));

    }


    [Authorize(Policy = UserDefault.AllUserAccess)]
    [HttpGet]
    [Route("v1/GetAssignPermission")]
    public async Task<ResponseHelper> GetUserAssignPermission(string role)
    {
        IdentityRole _role = await _roleManager.FindByNameAsync(role);
        if (_role != null)
        {
            var result = await _roleManager.GetClaimsAsync(_role);
            if (result.Count > 0)
                return new ResponseHelper(1, result, new ErrorDef(0, " ", ""));
            else
                return new ResponseHelper(0, new object(), new ErrorDef(0, " ", ""));

        }
        else
            return new ResponseHelper(0, new object(), new ErrorDef(0, " ", ""));

    }
}




