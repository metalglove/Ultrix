using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Ultrix.Application.Converters;
using Ultrix.Application.DTOs;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Domain.Enumerations;
using Ultrix.Domain.Services;

namespace Ultrix.Application.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IRepository<ApplicationUser> _applicationUserRepository;
        private readonly IRepository<CredentialType> _credentialTypeRepository;
        private readonly IRepository<Credential> _credentialRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<RolePermission> _rolePermissionRepository;
        private readonly IRepository<Permission> _permissionRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserManager(
            IRepository<ApplicationUser> applicationUserRepository, 
            IRepository<CredentialType> credentialTypeRepository,
            IRepository<Credential> credentialRepository,
            IRepository<Role> roleRepository,
            IRepository<UserRole> userRoleRepository,
            IRepository<RolePermission> rolePermissionRepository,
            IRepository<Permission> permissionRepository,
            IHttpContextAccessor contextAccessor)
        {
            _applicationUserRepository = applicationUserRepository;
            _credentialTypeRepository = credentialTypeRepository;
            _credentialRepository = credentialRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _permissionRepository = permissionRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<SignUpResultDto> SignUpAsync(string name, string credentialTypeCode, string identifier)
        {
            return await SignUpAsync(name, credentialTypeCode, identifier, null);
        }
        public async Task<SignUpResultDto> SignUpAsync(string name, string credentialTypeCode, string identifier, string secret)
        {
            if (await _applicationUserRepository.ExistsAsync(anyUser => anyUser.UserName.Equals(name, StringComparison.OrdinalIgnoreCase)))
                return new SignUpResultDto(success: false, error: SignUpResultError.UserNameAlreadyExists);

            ApplicationUser user = new ApplicationUser
            {
                UserName = name,
                UserDetail = new UserDetail { ProfilePictureData = "test" }
            };
            await _applicationUserRepository.CreateAsync(user);

            if (!await _credentialTypeRepository.ExistsAsync(ct => string.Equals(ct.Code, credentialTypeCode, StringComparison.OrdinalIgnoreCase)))
                return new SignUpResultDto(success: false, error: SignUpResultError.CredentialTypeNotFound);

            CredentialType credentialType = await _credentialTypeRepository.FindSingleByExpressionAsync(ct => string.Equals(ct.Code, credentialTypeCode, StringComparison.OrdinalIgnoreCase));
            Credential credential = new Credential
            {
                UserId = user.Id,
                CredentialTypeId = credentialType.Id,
                Identifier = identifier
            };

            if (!string.IsNullOrEmpty(secret))
            {
                byte[] salt = PasswordHasher.GenerateRandomSalt();
                string hash = PasswordHasher.ComputeHash(secret, salt);

                credential.Secret = hash;
                credential.Extra = Convert.ToBase64String(salt);
            }

            await _credentialRepository.CreateAsync(credential);

            user = await _applicationUserRepository.FindSingleByExpressionAsync(actualUser => actualUser.UserName.Equals(name)); //
            ApplicationUserDto applicationUserDto = EntityToDtoConverter.Convert<ApplicationUserDto, ApplicationUser>(user);
            return new SignUpResultDto(user: applicationUserDto, success: true);
        }
        public async Task<AddToRoleResultDto> AddToRoleAsync(ApplicationUser user, string roleCode)
        {
            if (!await _roleRepository.ExistsAsync(r => string.Equals(r.Code, roleCode, StringComparison.OrdinalIgnoreCase)))
                return new AddToRoleResultDto { Error = AddToRoleResultError.RoleDoesNotExist }; 

            Role role = await _roleRepository.FindSingleByExpressionAsync(r => string.Equals(r.Code, roleCode, StringComparison.OrdinalIgnoreCase));
            return await AddToRoleAsync(user, role);
        }
        public async Task<AddToRoleResultDto> AddToRoleAsync(ApplicationUser user, Role role)
        {
            AddToRoleResultDto addToRoleResultDto = new AddToRoleResultDto();
            if (await _userRoleRepository.ExistsAsync(anyRole => anyRole.UserId.Equals(user.Id) && anyRole.RoleId.Equals(role.Id)))
            {
                addToRoleResultDto.Error = AddToRoleResultError.UserAlreadyHasRole;
                return addToRoleResultDto;
            }

            UserRole userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            };
            if (await _userRoleRepository.CreateAsync(userRole))
                addToRoleResultDto.Success = true;
            else
                addToRoleResultDto.Error = AddToRoleResultError.FailedToCreateUserRole;

            return addToRoleResultDto;
        }
        public async Task<RemoveFromRoleResultDto> RemoveFromRoleAsync(ApplicationUser user, string roleCode)
        {
            if (!await _roleRepository.ExistsAsync(r => string.Equals(r.Code, roleCode, StringComparison.OrdinalIgnoreCase)))
                return new RemoveFromRoleResultDto { Error = RemoveFromRoleResultError.RoleDoesNotExist };

            Role role = await _roleRepository.FindSingleByExpressionAsync(r => string.Equals(r.Code, roleCode, StringComparison.OrdinalIgnoreCase));

            return await RemoveFromRoleAsync(user, role);
        }
        public async Task<RemoveFromRoleResultDto> RemoveFromRoleAsync(ApplicationUser user, Role role)
        {
            RemoveFromRoleResultDto removeFromRoleResultDto = new RemoveFromRoleResultDto();
            if (!await _userRoleRepository.ExistsAsync(anyRole => anyRole.UserId.Equals(user.Id) && anyRole.RoleId.Equals(role.Id)))
            {
                removeFromRoleResultDto.Error = RemoveFromRoleResultError.UserNotInRole;
                return removeFromRoleResultDto;
            }

            UserRole userRole = await _userRoleRepository.FindSingleByExpressionAsync(anyRole => anyRole.UserId.Equals(user.Id) && anyRole.RoleId.Equals(role.Id));
            if (await _userRoleRepository.DeleteAsync(userRole))
                removeFromRoleResultDto.Success = true;
            else
                removeFromRoleResultDto.Error = RemoveFromRoleResultError.FailedToDeleteUserRole;

            return removeFromRoleResultDto;
        }
        public async Task<ChangeSecretResultDto> ChangeSecretAsync(string credentialTypeCode, string identifier, string secret)
        {
            ChangeSecretResultDto changeSecretResultDto = new ChangeSecretResultDto();
            if (!await _credentialTypeRepository.ExistsAsync(ct => string.Equals(ct.Code, credentialTypeCode, StringComparison.OrdinalIgnoreCase)))
            {
                changeSecretResultDto.Error = ChangeSecretResultError.CredentialTypeNotFound;
                return changeSecretResultDto;
            }

            CredentialType credentialType = await _credentialTypeRepository.FindSingleByExpressionAsync(ct => string.Equals(ct.Code, credentialTypeCode, StringComparison.OrdinalIgnoreCase));

            if (!await _credentialRepository.ExistsAsync(c => c.CredentialTypeId == credentialType.Id && c.Identifier == identifier))
            {
                changeSecretResultDto.Error = ChangeSecretResultError.CredentialNotFound;
                return changeSecretResultDto;
            }

            Credential credential = await _credentialRepository.FindSingleByExpressionAsync(c => c.CredentialTypeId == credentialType.Id && c.Identifier == identifier);

            byte[] salt = PasswordHasher.GenerateRandomSalt();
            string hash = PasswordHasher.ComputeHash(secret, salt);

            credential.Secret = hash;
            credential.Extra = Convert.ToBase64String(salt);

            if (await _credentialRepository.UpdateAsync(credential))
            {
                changeSecretResultDto.Success = true;
            }
            else
            {
                changeSecretResultDto.Error = ChangeSecretResultError.FailedToUpdateSecret;
            }

            return changeSecretResultDto;
        }
        public async Task<ValidateResultDto> ValidateAsync(string credentialTypeCode, string identifier)
        {
            return await ValidateAsync(credentialTypeCode, identifier, null);
        }
        public async Task<ValidateResultDto> ValidateAsync(string credentialTypeCode, string identifier, string secret)
        {
            ValidateResultDto validateResultDto = new ValidateResultDto();
            if (string.IsNullOrEmpty(secret))
            {
                validateResultDto.Error = ValidateResultError.SecretIsNullOrEmpty;
                return validateResultDto;
            }

            if (!await _credentialTypeRepository.ExistsAsync(ct => string.Equals(ct.Code, credentialTypeCode, StringComparison.OrdinalIgnoreCase)))
            {
                validateResultDto.Error = ValidateResultError.CredentialTypeNotFound;
                return validateResultDto;
            }

            CredentialType credentialType = await _credentialTypeRepository.FindSingleByExpressionAsync(ct => string.Equals(ct.Code, credentialTypeCode, StringComparison.OrdinalIgnoreCase));
            if (!await _credentialRepository.ExistsAsync(c => c.CredentialTypeId == credentialType.Id && c.Identifier == identifier))
            {
                validateResultDto.Error = ValidateResultError.CredentialNotFound;
                return validateResultDto;
            }

            Credential credential = await _credentialRepository.FindSingleByExpressionAsync(c => c.CredentialTypeId.Equals(credentialType.Id) && c.Identifier.Equals(identifier));
            

            byte[] salt = Convert.FromBase64String(credential.Extra);
            string hash = PasswordHasher.ComputeHash(secret, salt);

            if (credential.Secret != hash)
            {
                validateResultDto.Error = ValidateResultError.SecretNotValid;
            }
            else
            {
                validateResultDto.Success = true;
                validateResultDto.User = await _applicationUserRepository.FindSingleByExpressionAsync(user => user.Id.Equals(credential.UserId));
            }

            return validateResultDto;
        }
        public async Task SignInAsync(ApplicationUser user, bool isPersistent = false)
        {
            ClaimsIdentity identity = new ClaimsIdentity(await GetUserClaimsTaskAsync(user), CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await _contextAccessor.HttpContext.SignInAsync(
              CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = isPersistent }
            );
        }
        public async Task SignOutAsync()
        {
            // TODO: clean up
            await _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            foreach (string key in _contextAccessor.HttpContext.Request.Cookies.Keys)
            {
                _contextAccessor.HttpContext.Response.Cookies.Append(key, "", new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
            }
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                await _contextAccessor.HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
                _contextAccessor.HttpContext.User = null;
            }
        }
        public Task<int> GetCurrentUserIdAsync()
        {
            int currentUserId = -1;
            if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return Task.FromResult(currentUserId);

            Claim claim = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
                return Task.FromResult(currentUserId);

            int.TryParse(claim.Value, out currentUserId);

            return Task.FromResult(currentUserId);
        }
        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            int currentUserId = await GetCurrentUserIdAsync();

            return currentUserId == -1 ? null : await _applicationUserRepository.FindSingleByExpressionAsync(user => user.Id.Equals(currentUserId));
        }
        public async Task<int> FindUserIdByEmailAsync(string email)
        {
            // TODO: fix
            Credential credential = await _credentialRepository.FindSingleByExpressionAsync(c => c.Identifier.Equals(email));
            return credential?.UserId ?? -1;
        }
        public async Task<string> FindUserNameByIdAsync(int userId)
        {
            Credential credential = await _credentialRepository.FindSingleByExpressionAsync(c => c.UserId.Equals(userId));
            return credential?.User?.UserName ?? "";
        }

        private async Task<IEnumerable<Claim>> GetUserClaimsTaskAsync(ApplicationUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            claims.AddRange(await GetUserRoleClaimsTaskAsync(user));
            return claims;
        }
        private async Task<IEnumerable<Claim>> GetUserRoleClaimsTaskAsync(ApplicationUser user)
        {
            List<Claim> claims = new List<Claim>();
            List<int> roleIds = (await _userRoleRepository.FindManyByExpressionAsync(ur => ur.UserId == user.Id)).Select(ur => ur.RoleId).ToList();

            foreach (int roleId in roleIds)
            {
                Role role = await _roleRepository.FindSingleByExpressionAsync(findRole => findRole.Id.Equals(roleId));
                claims.Add(new Claim(ClaimTypes.Role, role.Code));
                claims.AddRange(await GetUserPermissionClaimsTaskAsync(role));
            }

            return claims;
        }
        private async Task<IEnumerable<Claim>> GetUserPermissionClaimsTaskAsync(Role role)
        {
            List<Claim> claims = new List<Claim>();
            List<int> permissionIds = (await _rolePermissionRepository.FindManyByExpressionAsync(rp => rp.RoleId == role.Id)).Select(rp => rp.PermissionId).ToList();

            foreach (int permissionId in permissionIds)
            {
                Permission permission = await _permissionRepository.FindSingleByExpressionAsync(findPermission => findPermission.Id.Equals(permissionId));
                claims.Add(new Claim("Permission", permission.Code));
            }

            return claims;
        }
    }
}
