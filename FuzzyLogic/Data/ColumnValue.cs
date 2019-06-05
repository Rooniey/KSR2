using System;

namespace FuzzyLogic.Data
{
    public class ColumnValue : IEquatable<ColumnValue>
    {
        public String RawValue { get; }

        public ColumnValue(string rawValue)
        {
            RawValue = rawValue ?? throw new ArgumentNullException(nameof(rawValue));
        }

        public bool Equals(ColumnValue other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(RawValue, other.RawValue, StringComparison.InvariantCulture);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ColumnValue) obj);
        }

        public override int GetHashCode()
        {
            return StringComparer.InvariantCulture.GetHashCode(RawValue);
        }

        public static bool operator ==(ColumnValue left, ColumnValue right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ColumnValue left, ColumnValue right)
        {
            return !Equals(left, right);
        }
    }
}
