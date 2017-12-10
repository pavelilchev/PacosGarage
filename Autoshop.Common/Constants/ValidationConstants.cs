namespace Autoshop.Common
{
    public static class ValidationConstants
    {
        public const string MinLengthErrorMessgae = "{0} should be at least {1} characters long";

        public const string MaxLengthErrorMessgae = "{0} can't be more than {1} characters long";

        public const int FirstNameMinLength = 2;

        public const int FirstNameMaxLength = 50;        

        public const int LastNameMinLength = 2;

        public const int LastNameMaxLength = 50;

        public const string PhoneNumberErrorMessage = "Invalid phone number, should be 10 digit";

        public const string EmailErrorMessage = "Invalid email format";

        public const string DateErrorMessage = "Invalid date";

        public const int SpecialDescriptionMinLength = 10;

        public const int SpecialDescriptionMaxLength = 1000;

        public const int UserPasswordMinLength = 4;

        public const int UserPasswordMaxLength = 100;        

        public const string UserConfirmPasswordErrorMessage = "The password and confirmation password do not match";

        public const int ReviewTextMinLength = 5;

        public const int ReviewTextMaxLength = 500;

        public const int ReviewRatingMinValue = 1;

        public const int ReviewRatingMaxValue = 5;

        public const int PostTitleMinLength = 5;

        public const int PostTitleMaxLength = 50;

        public const int PostTextMinLength = 50;

        public const int PostTextMaxLength = 50000;

        public const int CategoryMinLength = 2;

        public const int CategoryMaxLength = 50;
    }
}
