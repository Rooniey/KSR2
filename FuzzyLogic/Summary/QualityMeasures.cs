using System;
using System.Collections.Generic;
using System.Linq;
using FuzzyLogic.Operations;
using FuzzyLogic.utility;
using Microsoft.Win32;

namespace FuzzyLogic.Summary
{
    public class QualityMeasures
    {
        private List<double> _summarizerMemberships = new List<double>(); 
        private List<double> _qualifierMemberships = new List<double>();




        public MeasuresValues CalculateAll(LinguisticSummary summary)
        {
            MeasuresValues values = new MeasuresValues();
            values.T1 = CalculateT1(summary);
            values.T2 = CalculateT2(summary);
            values.T3 = CalculateT3(summary);
            values.T4 = CalculateT4(summary, values.T3);
            values.T5 = CalculateT5(summary);
            values.T1T5 = CalculateT1T5(values);
            values.T6 = CalculateT6(summary);
            values.T7 = CalculateT7(summary);
            values.T8 = CalculateT8(summary);
            values.T9 = CalculateT9(summary);
            values.T10 = CalculateT10(summary);
            values.T11 = CalculateT11();
            values.T1T11 = CalculateT1T11(values);
            return values;
        }

        public double CalculateT1(LinguisticSummary summary)
        {
            return summary.Qualifier != null ? CalculateT1WithQualifier(summary) : CalculateT1WithoutQualifier(summary);
        }

        private double CalculateT1WithoutQualifier(LinguisticSummary summary)
        {
            double sumS = 0.0;
            OperationType opType = summary.LogicalOperationSummarizer.GetOperationType();
            foreach (var tuple in summary.Data)
            {
                double compoundSummarizer = TwoFuzzySetOperation.PerformOperation(summary.Summarizers, tuple, opType);
                _summarizerMemberships.Add(compoundSummarizer);
                sumS += compoundSummarizer;
            }

            double r = sumS;
            return summary.Quantifier.IsAbsolute
                ? summary.Quantifier.CalculateMembership(r)
                : summary.Quantifier.CalculateMembership(r / summary.Data.Count);
        }

        private double CalculateT1WithQualifier(LinguisticSummary summary)
        {
            double numerator = 0.0;
            double denominator = 0.0;
            OperationType opType = summary.LogicalOperationSummarizer.GetOperationType();

            foreach (var tuple in summary.Data)
            {
                double compoundSummarizer = TwoFuzzySetOperation.PerformOperation(summary.Summarizers, tuple, opType);
                _summarizerMemberships.Add(compoundSummarizer);

                double w = summary.Qualifier.CalculateMembership(tuple.Get(summary.Qualifier.Column));
                _qualifierMemberships.Add(w);

                numerator += Math.Min(compoundSummarizer, w);
                denominator += w;
            }

            double r = numerator / denominator;
            return summary.Quantifier.CalculateMembership(r);
        }


        public double CalculateT2(LinguisticSummary summary)
        {
            double mulS = 1.0;
            foreach (var summarizer in summary.Summarizers)
            {
                mulS *= summarizer.DegreeOfFuzziness();
            }
            return 1 - Math.Pow(mulS, 1.0 / summary.Summarizers.Count);
        }

        private double CalculateT3(LinguisticSummary summarization)
        {
            if (summarization.Qualifier != null)
            {
                double sumT = _summarizerMemberships.Zip(_qualifierMemberships, (a, b) => a > 0 && b > 0)
                    .Count(t => t);
                double sumH = _qualifierMemberships.Count(val => val > 0);
                return sumH > 0 ? sumT / sumH : 0;
            }
            else
            {
                double sumT = _summarizerMemberships.Count(val => val > 0);
                double sumH = summarization.Data.Count;
                return sumH > 0 ? sumT / sumH : 0;
            }
           
        }

        private double CalculateT4(LinguisticSummary summarization, double t3)
        {
            double mulS = 1.0;
            foreach (var summarizer in summarization.Summarizers)
            {
                double sumG = 0.0;
                foreach (var tuple in summarization.Data)
                {
                    sumG += summarizer.CalculateMembership(tuple.Get(summarizer.Column)) > 0 ? 1.0 : 0;
                }
                mulS *= (sumG / summarization.Data.Count);
            }
            return Math.Abs(mulS - t3);
        }

        private double CalculateT5(LinguisticSummary summarization)
        {
            return 2 * Math.Pow(1.0 / 2.0, summarization.Summarizers.Count);
        }

        private double CalculateT1T5(MeasuresValues values)
            => 0.2 * values.T1 + 0.2 * values.T2 + 0.2 * values.T3 + 0.2 * values.T4 + 0.2 * values.T2 + 0.2 * values.T5;

        private double CalculateT6(LinguisticSummary summarization)
        {
            var cardXQ = 1d;
            if (summarization.Quantifier.IsAbsolute)
                cardXQ = summarization.Quantifier.X;

            return 1 - (summarization.Quantifier.MembershipFunction.Support / cardXQ);

        }
        private double CalculateT7(LinguisticSummary summarization)
        {
            var cardXQ = 1d;
            if (summarization.Quantifier.IsAbsolute)
                cardXQ = summarization.Quantifier.X;

            return 1 - (summarization.Quantifier.MembershipFunction.Cardinality / cardXQ);
        }

        private double CalculateT8(LinguisticSummary summarization)
        {
            double mulS = 1;
            foreach (var summarizer in summarization.Summarizers)
            {
                mulS *= summarizer.MembershipFunction.Cardinality / summarizer.X;
            }
            return 1 - Math.Pow(mulS, 1.0 / summarization.Summarizers.Count);

        }

        private double CalculateT9(LinguisticSummary summarization)
        {
            double mulS = 1;
            if (summarization.Qualifier != null)
            {
                mulS *= summarization.Qualifier.DegreeOfFuzziness();
            }
            return 1 - Math.Pow(mulS, 1.0 / summarization.Summarizers.Count);

        }

        private double CalculateT10(LinguisticSummary summarization)
        {
            double mulS = 1;
            if (summarization.Qualifier != null)
            {
                mulS *= summarization.Qualifier.MembershipFunction.Cardinality / summarization.Qualifier.X;
            }
            return 1 - mulS;

        }

        private double CalculateT11() => 2 * Math.Pow(1.0 / 2.0, 1);

        private double CalculateT1T11(MeasuresValues values)
        {
            return (
                1 / 11d * values.T1 +
                1 / 11d * values.T2 +
                1 / 11d * values.T3 +
                1 / 11d * values.T4 +
                1 / 11d * values.T5 +
                1 / 11d * values.T6 +
                1 / 11d * values.T7 +
                1 / 11d * values.T8 +
                1 / 11d * values.T9 +
                1 / 11d * values.T10 +
                1 / 11d * values.T11
            );
        }

    }
}
