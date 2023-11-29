# IspManagementApplication

# Database Scaffold Command

  dotnet ef dbcontext scaffold "Server=localhost; Database=IspManagementDb; Trusted_Connection=True; Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer --context IspManagementApplicationDbContext --context-namespace Infrastructure.Context --context-dir ../Infrastructure/Context -f --output-dir ../Domain/Entities --namespace Domain.Entities --no-onconfiguring


  