using System;
using System.Collections.Generic;
using System.Linq;
using FuzzyLogic.Exceptions;
using FuzzyLogic.Summary;
using Model;

namespace FuzzyLogic.Operations
{
    public static class TwoFuzzySetOperation
    {

        public static double PerformChain(double left, FuzzySet right, OperationType opType, double value)
        {
            switch (opType)
            {
                case OperationType.Intersection:
                    return IntersectChain(left, right, value);
                case OperationType.Union:
                    return UnionChain(left, right, value);
                default:
                    throw new UnknownFuzzySetsOperationException("Unknown operation");
                
            }
        }

        public static double IntersectChain(double left, FuzzySet right, double value)
        {
            double rightMembership = right.MembershipFunction.GetMembershipDegree(value);

            return Math.Min(left, rightMembership);
        }

        public static double UnionChain(double left, FuzzySet right, double value)
        {
            double rightMembership = right.MembershipFunction.GetMembershipDegree(value);
            return Math.Max(left, rightMembership);
        }

        public static double PerformOperation(List<Summarizer> sets, Player tuple, OperationType op)
        {
            IEnumerable<double> summarizersMembership = sets
                .Select(summarizer => summarizer.CalculateMembership(tuple.Get(summarizer.Column)));

            if (op == OperationType.Intersection)
                return summarizersMembership.Min();
//            else if (op == OperationType.Union)
            else
                return summarizersMembership.Max();
        }
    }

    public enum OperationType
    {
        Intersection,
        Union
    }
}
