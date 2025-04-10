namespace OpenIdConnect.Utils.Consts;

public static partial class ConstantEnum
{
    public enum UserRole
    {
        Customer,
        SaleEmployee,
        PlannedCustomer,	
        Owner,
    }
    
    public enum Result
    {
        Success = 1,
        Failure = 0,
    }
    
    public enum MoneyUnit
    {
        VNĐ,
        USD,
        EUR,
        JPY,
        GBP,
        CNY,
        KRW,
        AUD,
    }
}