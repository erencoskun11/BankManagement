namespace Abp.BankManagement.ExceptionCodes
{
   
        public static class CustomerExceptionCodes
        {
            public const string NotFoundException = "Customer.NotFound";
            public const string NationalIdAlreadyExists = "Customer.NationalIdAlreadyExists";
            public const string InvalidRiskLimit = "Customer.InvalidRiskLimit";
            public const string InvalidBirthDate = "Customer.InvalidBirthDate";
        }
    
}
