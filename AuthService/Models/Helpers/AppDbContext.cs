using Microsoft.EntityFrameworkCore;
using Npgsql;
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
    public Boolean ExecuteStoredProcedure(string storedProcedureId, List<NpgsqlParameter> paramList)
    {
        var parmSql = "";

        if (paramList.Count > 0)
        {
            var count = 0;

            // Get parameter name
            foreach (NpgsqlParameter npgsqlParameter in paramList)
            {
                if (count != 0)
                {
                    parmSql += ", ";
                }

                // Get parameter name
                parmSql += npgsqlParameter.ParameterName;

                // Get parameter value
                if (npgsqlParameter.Value == null) npgsqlParameter.Value = DBNull.Value;
                count++;
            }
        }

        // Execute stored procedure
        var updateRownum = Database.ExecuteSqlRaw($"CALL {storedProcedureId}({parmSql})", paramList);
        _Logger.Debug($"{storedProcedureId} :{updateRownum}");

        // Get result
        NpgsqlParameter result = paramList.FirstOrDefault(x => x.ParameterName == "@o_rslt");
        if (result?.Value.ToString() == ((int)ConstantEnum.Result.Success).ToString())
        {
            _Logger.Debug($"{storedProcedureId} :{result?.Value}");
            return true;
        }
        else
        {
            _Logger.Error($"{storedProcedureId}:{result?.Value}");
            return false;
        }
    }}