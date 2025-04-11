namespace Server.Utils.Consts;

/// <summary>
/// Common Url
/// </summary>
public class CommonUrl
{
    /// <summary>
    /// https://localhost:5090/api/v1/UpdateRole
    /// </summary>
    public static readonly string Localhost5090UpdateRole = "https://localhost:5090/api/v1/UpdateRole";
    
    /// <summary>
    /// Url for web tracking success
    /// </summary>
    public static readonly string WebUrlTrackingSuccess = "http://localhost:3000/transaction/order?success=true";
    
    /// <summary>
    /// Url for web tracking fail
    /// </summary>
    public static readonly string WebUrlTrackingFail = "http://localhost:3000/transaction/order?success=false";
    
    /// <summary>
    /// Url for mobile tracking success
    /// </summary>
    public static readonly string MobileUrlTrackingSuccess = "https://localhost:5090/api/v1/UpdateRole";
    
    /// <summary>
    /// Url for mobile tracking fail
    /// </summary>
    public static readonly string MobileUrlTrackingFail = "https://localhost:5090/api/v1/UpdateRole";

    /// <summary>
    /// Url for Plan Buying success
    /// </summary>
    public static readonly string PlanBuyingSuccess = "https://localhost:3000/transaction/order?success=true";

    /// <summary>
    /// Url for Plan Buying fail
    /// </summary>
    public static readonly string PlanBuyingFail = "https://localhost:3000/transaction/order?success=true";
}