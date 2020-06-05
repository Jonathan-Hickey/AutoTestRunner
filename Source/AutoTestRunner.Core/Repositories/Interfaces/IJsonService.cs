namespace AutoTestRunner.Core.Repositories.Interfaces
{
    public interface IJsonService
    {
        string Serialize<T>(T input);

        T Deserialize<T>(string input);
    }
}
