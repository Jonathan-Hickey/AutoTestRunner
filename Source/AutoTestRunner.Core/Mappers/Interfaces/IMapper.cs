using System.Collections.Generic;
using System.Linq;

namespace AutoTestRunner.Core.Mappers.Interfaces
{
    public interface IMapper<T_In, T_Out>
    {
        T_Out Map(T_In obj);

        IReadOnlyList<T_Out> Map(IEnumerable<T_In> objs) => objs.Select(Map).ToList();
    }

    public interface IMapper<T_In_One, T_In_Two, T_Out>
    {
        T_Out Map(T_In_One objOne, T_In_Two objTwo);
    }
}
