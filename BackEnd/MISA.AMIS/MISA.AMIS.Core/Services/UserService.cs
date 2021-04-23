using Microsoft.IdentityModel.Tokens;
using MISA.AMIS.Core.Entities;
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

        public UserService(IUserRepository userRepository): base(userRepository)
        {
            this._userRepository = userRepository;
        }

        public string Authenticate(string userName, string password)
        {
            User user = _userRepository.Authenticate(userName, password);

            // trả về null nếu không tìm thấy người dùng
            if (user == null)
                return "Username or password is invalid!";

            return GenerateJSONWebToken(user);   
        }

        public IEnumerable<User> GetByPositionAndOffice(Guid positionId, Guid officeId)
        {
            List<List<User>> myList = new List<List<User>>();
            var users = _userRepository.GetByPositionAndOffice(positionId, officeId);
            myList.Add((List<User>) users);

            foreach (User user in users)
            {
                var items = _userRepository.GetByPositionAndOffice(user.PositionId, user.OfficeId);
                myList.Add((List<User>) items);
            }
            return (IEnumerable<User>) myList;
        }

        /// <summary>
        /// Generate token khi người dùng đăng nhập thành công
        /// </summary>
        /// <param name="user">Tài khoản người dùng</param>
        /// <returns>Chuỗi token tự sinh</returns>
        private string GenerateJSONWebToken(BaseUser user) 
        {           
            var now = DateTime.UtcNow;            

            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes("SecretToken The MISA As Long As Thinhk Big");
            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, "READ")
                }),
                Expires = now.AddMinutes(120),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDiscriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
