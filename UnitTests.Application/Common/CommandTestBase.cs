using EndAuth.Persistance.Contexts.IdentityDb;

namespace UnitTests.Application.Common;

public class CommandTestBase : IDisposable
{
    private bool _disposed;
    private readonly IDbContextMockFactory<IdentityContext> _dbContextMockFactory;

    protected readonly IdentityContext _context;

    public CommandTestBase()
    {

        _dbContextMockFactory = new IdentityContextMockFactory();
        _context = _dbContextMockFactory.Create().Object;
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