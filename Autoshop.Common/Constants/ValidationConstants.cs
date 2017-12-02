namespace Autoshop.Common
{
    public static class ValidationConstants
    {
        public const int FirstNameMinLength = 2;

        public const string FirstNameMinLengthErrorMessgae = "{0} should be at least {1} characters long";

        public const int FirstNameMaxLength = 50;

        public const string FirstNameMaxLengthErrorMessgae = "{0} can't be more than {1} characters long";

        public const int LastNameMinLength = 2;

        public const string LastNameMinLengthErrorMessgae = "{0} should be at least {1} characters long";

        public const int LastNameMaxLength = 50;

        public const string LastNameMaxLengthErrorMessgae = "{0} can't be more than {1} characters long";

        public const string PhoneNumberErrorMessage = "Invalid phone number, should be 10 digit";

        public const string DateErrorMessage = "Invalid date";

        public const int SpecialDescriptionMinLength = 10;

        public const int SpecialDescriptionMaxLength = 1000;

        public const int UserPasswordMinLength = 4;

        public const int UserPasswordMaxLength = 100;

        public const string UserPasswordErrorMessage = "The {0} must be at least {2} and at max {1} characters long";

        public const string UserConfirmPasswordErrorMessage = "The password and confirmation password do not match";
    }
}
