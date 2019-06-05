﻿using System;
using FuzzyLogic.Data;
using FuzzyLogic.Interfaces;

namespace FuzzyLogic.Membership
{
    public class TriangularMembershipFunction : IMembershipFunction
    {
        public TriangularMembershipFunction(double a, double b, double c)
        {
            _a = a;
            _b = b;
            _c = c;
        }

        private readonly double _a;
        private readonly double _b;
        private readonly double _c;

        public double GetMembershipDegree(double columnValue)
        {
            double parsed = columnValue;
            if (Math.Abs(parsed - _b) < Constants.DOUBLE_COMPARISON_TOLERANCE)
            {
                return 1d;
            }
            else if (parsed > _a && parsed < _b)
            {
                return 1d / (_b - _a) * parsed + 1d - (1d / (_b - _a)) * _b;
            }
            else if (parsed > _b && parsed < _c)
            {
                return 1d / (_b - _c) * parsed + 1d - (1d / (_b - _c)) * _b;
            }
            else
            {
                return 0d;
            }
        }
    }
}