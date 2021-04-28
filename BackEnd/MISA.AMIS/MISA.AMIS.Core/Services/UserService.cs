﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Helpers;
using MISA.AMIS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MISA.AMIS.Core.Services
{
    /// <summary>
    /// Service implement người sử dụng
    /// </summary>
    /// CreatedBy: VVAn(22/04/2021)
    public class UserService: BaseService<User>, IUserService
    {
        IUserRepository _userRepository;

        private readonly AppSettings _appSettings;

        public UserService(IUserRepository userRepository, IOptions<AppSettings> appSettings) : base(userRepository)
        {
            this._userRepository = userRepository;
            this._appSettings = appSettings.Value;
        }

        public override ServiceResult Add(User t)
        {
            t.Password = BCrypt.Net.BCrypt.HashPassword(t.Password);
            return base.Add(t);
        }

        public override ServiceResult Update(User t)
        {
            t.Password = BCrypt.Net.BCrypt.HashPassword(t.Password);
            return base.Update(t);
        }

        public string Authenticate(string userName, string password)
        {
            User user = _userRepository.Authenticate(userName, password);

            // trả về string empty nếu không tìm thấy người dùng
            if (user == null)
                return "";

            return GenerateJSONWebToken(user);   
        }

        public IEnumerable<TreeviewItem> GetByPositionAndOffice(Guid userId)
        {
            var treeviewItems = new List<TreeviewItem>();
           
            return treeviewItems;
        }     

        /// <summary>
        /// Generate token khi người dùng đăng nhập thành công
        /// </summary>
        /// <param name="user">Tài khoản người dùng</param>
        /// <returns>Chuỗi token tự sinh</returns>
        private string GenerateJSONWebToken(User user) 
        {           
            var now = DateTime.UtcNow;            
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.UTF8.GetBytes(_appSettings.Secret);

            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.RoleName)
                }),
                Expires = now.AddMinutes(120),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDiscriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
