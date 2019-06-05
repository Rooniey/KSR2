using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyLogic.Summary
{
    public class Measures
    {
        public double CalculateT1(LinguisticSummary summarization)
        {
            double sumSW = 0.0;
            double sumW = 0.0;
            var _summarizerMemberships = new List<double>();
            var _qualifierMemberships = new List<double>();
            foreach (var tuple in summarization.Data)
            {
                double s = summarization.Summarizer.MembershipFunction.GetMembershipDegree(tuple.Get(summarization.Summarizer.Column));

                double w = summarization.Qualifier.MembershipFunction.GetMembershipDegree(tuple.Get(summarization.Qualifier.Column));


                _summarizerMemberships.Add(s);
                _qualifierMemberships.Add(w);
                sumSW += Math.Min(s, w);
                sumW += w;
            }
            double r = sumSW / sumW;
            return summarization.Quantifier.MembershipFunction.GetMembershipDegree(r);
        }
    }
}
