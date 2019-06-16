using FuzzyLogic.Interfaces;
using FuzzyLogic.Membership;
using GUI.Base;
using System.Collections.Generic;

namespace GUI.ViewModels
{
    public class MembershipViewModel : BindableBase
    {

        public List<string> MEMBERSHIP_TYPES { get; } = new List<string> { "trapezoidal", "triangular" };

        private string _membershipType;

        public string MembershipType
        {
            get => _membershipType;
            set => SetProperty(ref _membershipType, value);
        }

        private string _parametersString;

        public string ParameterString
        {
            get => _parametersString;
            set => SetProperty(ref _parametersString, value);
        }

        public MembershipViewModel()
        {

        }

        public IMembershipFunction GetMembershipFunction()
        {
            if (string.IsNullOrEmpty(ParameterString)) return null;
            var memParams = ParameterString.Trim().Split(' ');

            switch (MembershipType)
            {
                case "trapezoidal":
                    if (memParams.Length != 4) return null;
                    return new TrapezoidalMembershipFunction(memParams[0], memParams[1], memParams[2], memParams[3]);
                case "triangular":
                    if (memParams.Length != 3) return null;
                    return new TriangularMembershipFunction(memParams[0], memParams[1], memParams[2]);
            }

            return null;
        }

    }
}
