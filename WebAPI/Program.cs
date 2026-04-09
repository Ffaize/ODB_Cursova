using WebAPI.DataServices;
using WebAPI.DataServices.Views;
using WebAPI.Helpers;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ActionLogService>();
            builder.Services.AddScoped<BillingNumberService>();
            builder.Services.AddScoped<BillingOperationService>();
            builder.Services.AddScoped<BranchService>();
            builder.Services.AddScoped<CardService>();
            builder.Services.AddScoped<CreditService>();
            builder.Services.AddScoped<CustomerService>();
            builder.Services.AddScoped<EmployeeService>();

            builder.Services.AddScoped<AnomalyService>();
            builder.Services.AddScoped<AuditTrailService>();
            builder.Services.AddScoped<BalanceDailyService>();
            builder.Services.AddScoped<BranchPerformanceService>();
            builder.Services.AddScoped<CardPortfolioService>();
            builder.Services.AddScoped<CreditPortfolioService>();
            builder.Services.AddScoped<CustomerAccountsService>();
            builder.Services.AddScoped<EmployeeBranchStatsService>();
            builder.Services.AddScoped<LoanRiskService>();
            builder.Services.AddScoped<TransactionHistoryService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
