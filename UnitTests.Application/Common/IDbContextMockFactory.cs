using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests.Application.Common;
public interface IDbContextMockFactory<TDbContext>
where TDbContext : DbContext
{
    public Mock<TDbContext> Create();
    public void Destroy(TDbContext context);
}
