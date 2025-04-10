using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OpenIdConnect.Systemserver;
using OpenIdConnect.Utils.Consts;

namespace AuthService.Models.Helpers;

public class AppDbContext : AuthServiceContext
{
    public NLog.Logger _Logger;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(new DbContextOptions<AuthServiceContext>())
    {
    }
    
    public IdentityEntity IdentityEntity { get; set; }
    
    /// <summary>
    /// Execute stored procedure
    /// </summary>
    /// <param name="storedProcedureId"></param>
    /// <param name="paramList"></param>
    /// <returns></returns>
    public Boolean ExecuteStoredProcedure(string storedProcedureId, List<SqlParameter> paramList)
    {
        var parmSql = "";

        if (paramList.Count > 0)
        {
            var count = 0;
            
            // Get parameter name
            foreach (SqlParameter sqlParameter in paramList)
            {
                if (count != 0)
                {
                    parmSql += ", ";
                }
                
                // Get parameter name
                parmSql += sqlParameter.ParameterName;

                // Get parameter value
                if (sqlParameter.Direction == ParameterDirection.Output)
                {
                    parmSql += " out";
                }
            
                // Get parameter value
                if (sqlParameter.Value == null) sqlParameter.Value = DBNull.Value;
                count++;
            }
        }
        
        // Execute stored procedure
        var updateRownum = Database.ExecuteSqlRaw($"EXECUTE {storedProcedureId} {parmSql}", paramList);
        _Logger.Debug($"{storedProcedureId} :{updateRownum}");

        // Get result
        SqlParameter result = paramList.Where(x => x.ParameterName == "@o_rslt").FirstOrDefault();
        if (result?.Value.ToString() == ((int) ConstantEnum.Result.Success).ToString())
        {
            _Logger.Debug($"{storedProcedureId} :{result?.Value}");
            return true;
        }
        else
        {
            _Logger.Error($"{storedProcedureId}:{result?.Value}");
            return false;
        }
    }
}