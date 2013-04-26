using System;

namespace Camp.Models
{
    public class FullNameComposer
    {
        private readonly string _firstName;
        private readonly string _lastName; 

        public FullNameComposer(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }


        public override string ToString()
        {
            return String.Format("{0} {1}", _lastName, _firstName);
        }


        public static implicit operator string(FullNameComposer composer)
        {
            return composer.ToString();
        }
    }
}
