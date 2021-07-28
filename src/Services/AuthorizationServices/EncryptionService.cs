using BCryptNet = BCrypt.Net;

namespace dotnetServer.Services.AuthorizationServices
{
    public class EncryptionService
    {
        public string EncryptPassword(string target){
            return BCryptNet.BCrypt.HashPassword(target);
        } 
        
        public bool isPassword(string target, string hashed){
            return BCryptNet.BCrypt.Verify(target, hashed);
        }
    }
}