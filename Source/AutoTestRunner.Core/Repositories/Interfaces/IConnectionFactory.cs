namespace AutoTestRunner.Core.Repositories.Interfaces
{
    public interface IConnectionFactory
    {
        IThreadSafeLiteDatabase CreateConnection();
    }
}