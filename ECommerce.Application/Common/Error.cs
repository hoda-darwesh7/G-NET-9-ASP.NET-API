namespace ECommerce.Application.Common
{
    public sealed record Error(string Code , string Description ,ErrorTypes ErrorType= ErrorTypes.Failure)
    {
        public static Error Failure(string code = "General.Failure" , string description = "General Failure Has Occurred") 
            => new (code, description , ErrorTypes.Failure);

        public static Error Validation(string code = "General.Validation", string description = "General Validation Error Has Occurred")
            => new(code, description, ErrorTypes.Validation);

        public static Error NotFound(string code = "General.NotFound", string description = "Resource Not Found")
            => new(code, description, ErrorTypes.NotFound);

        public static Error Conflict(string code = "General.Conflict", string description = "General Conflict Has Occurred")
            => new(code, description, ErrorTypes.Conflict);

        public static Error Unauthorized(string code = "General.Unauthorized", string description = "Access Is Denied Due To Bad Authorization")
            => new(code, description, ErrorTypes.Unauthorized);

        public static Error Forbidden(string code = "General.Forbidden", string description = "This Operation Is Forbidden")
            => new(code, description, ErrorTypes.Forbidden);

        public static Error InvalidCredentials(string code = "General.InvalidCredentials", string description = "Provided Credentials are Invalid")
            => new(code, description, ErrorTypes.InvalidCredentials);

    }

    public enum ErrorTypes
    {
        Failure = 0 ,
        Validation,
        NotFound,
        Conflict,
        Unauthorized,
        Forbidden,
        InvalidCredentials
    }
}