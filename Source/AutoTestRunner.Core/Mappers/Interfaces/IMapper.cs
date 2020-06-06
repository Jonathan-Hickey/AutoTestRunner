namespace AutoTestRunner.Core.Mappers.Interfaces
{
    public interface IMapper<T_In, T_Out>
    {
        T_Out Map(T_In obj);
    }
}
