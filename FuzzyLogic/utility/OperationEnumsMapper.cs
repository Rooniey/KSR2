using System;
using FuzzyLogic.Operations;
using FuzzyLogic.Summary;

namespace FuzzyLogic.utility
{
    public static class OperationEnumsMapper
    {
        public static OperationType GetOperationType(this LogicalOperation op)
        {
            switch (op)
            {
                case LogicalOperation.And:
                    return OperationType.Intersection;
                case LogicalOperation.Or:
                    return OperationType.Union;
                default:
                    throw new ArgumentException("Unknown logical operation");
            }
        }
    }
}
