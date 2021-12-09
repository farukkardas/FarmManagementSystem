using System;
using System.Collections.Generic;
using Core.Entities.Concrete;

namespace Core.Utilities.Security.JWT.Concrete
{
    public  class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public int Id { get; set; }
        public string SecurityKey { get; set; }
        public List<OperationClaim> OperationClaims { get; set; }
    }
}