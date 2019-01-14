using System;
using ServeUp.Models;

namespace ServeUp.System
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=true)]
    public class AuthAttribute: Attribute
    {
        public Claim Claim { get; set; }

        public bool HasClaim { get => Claim != null; }

        public AuthAttribute()
        {
            
        }

        public AuthAttribute(string key, string value)
        {
            Claim = new Claim { Key = key, Value = value };
        }        
    }
}