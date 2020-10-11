using System;
using System.ComponentModel.DataAnnotations;

namespace BeerWorld.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false)]
    public sealed class EnumExistsAttribute : DataTypeAttribute
    {
        public EnumExistsAttribute(Type enumType) : base("Enumeration")
        {
            this.EnumType = enumType;
        }

        public override bool IsValid(object value)
        {
            if (this.EnumType == null)
            {
                throw new InvalidOperationException("Type cannot be null");
            }
            if (!this.EnumType.IsEnum)
            {
                throw new InvalidOperationException("Type must be an enum");
            }
            if (!Enum.IsDefined(EnumType, value))
            {
                return false;
            }

            return true;
        }

        public Type EnumType
        {
            get;
            set;
        }
    }
}
