using System;
using System.Collections.Generic;
using FuzzyLogic.Data;
using FuzzyLogic.Interfaces;

namespace FuzzyLogic.Membership
{
//    public class DiscreteMembershipFunction : IMembershipFunction
//    {
//        private readonly HashSet<ColumnValue> _universeOfDiscourse;
//        private readonly IReadOnlyDictionary<ColumnValue, double> _membersDictionary;
//
//        private DiscreteMembershipFunction(IReadOnlyDictionary<ColumnValue, double> membersDictionary)
//        {
//            _membersDictionary = membersDictionary ?? throw new ArgumentNullException(nameof(membersDictionary));
//            _universeOfDiscourse = new HashSet<ColumnValue>(_membersDictionary.Keys);
//        }
//
//        public double GetMembershipDegree(ColumnValue value)
//            => _membersDictionary.TryGetValue(value, out double grade)
//                ? grade : 0d;
//    }
}
