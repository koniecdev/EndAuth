using EndAuth.Persistance.DesignTimeFactory;
using Microsoft.EntityFrameworkCore;

namespace EndAuth.Persistance.Contexts.IdentityDb;
public class IdentityContextFactory : DesignTimeDbContextFactoryBase<IdentityContext>
{
    protected override IdentityContext CreateNewInstance(DbContextOptions<IdentityContext> options)
    {
        var db = new IdentityContext(options);
        return db;
    }
}