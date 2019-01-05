using Ultrix.Domain.Enumerations;

namespace Ultrix.Domain.Entities.Authentication
{
    public class Credential
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CredentialTypeId { get; set; }
        public string Identifier { get; set; }
        public string Secret { get; set; }
        public string Extra { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual CredentialType CredentialType { get; set; }
    }
}
