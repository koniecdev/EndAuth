using EndAuth.Persistance.Contexts.IdentityDb;
using Microsoft.Extensions.Configuration;

namespace UnitTests.Application.Common;

public class CommandTestBase : IDisposable
{
    private bool _disposed;
    private readonly IDbContextMockFactory<IdentityContext> _dbContextMockFactory;

    protected readonly IdentityContext _context;
    protected readonly IConfiguration _configuration;

    public CommandTestBase()
    {

        _dbContextMockFactory = new IdentityContextMockFactory();
        _context = _dbContextMockFactory.Create().Object;
        var inMemorySettings = new Dictionary<string, string> {
            {"Secrets:GmailLogin", "x1@x.com"},
            {"Secrets:GmailPassword", "x122345$555555"}
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose of any managed resources here
            }

            _dbContextMockFactory.Destroy(_context);
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~CommandTestBase()
    {
        Dispose(false);
    }
}