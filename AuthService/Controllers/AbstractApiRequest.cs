namespace Client.Controllers;

/// <summary>
/// API Request (Common) Inheritance Class
/// </summary>
public abstract class AbstractApiRequest
{
    /// <summary>
    /// true: input value validation only/false: input value validation + main processing
    /// </summary>
    public bool IsOnlyValidation { get; set; }
}