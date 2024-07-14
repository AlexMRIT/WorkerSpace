using System;
using plib.Enums;

namespace plib
{
    public class ProfiledAttribute : Attribute
    {
        public ProfiledAttribute(string method, ProfiledOperationImplement profiledOperationImplement)
        {
            MethodName = method;
            OperationType = profiledOperationImplement;
        }

        public readonly string MethodName;
        public readonly ProfiledOperationImplement OperationType;
    }
}