using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharePaint.Services.ServiceProviders
{
    public interface IReceiveDataHandler
    {
        (bool IsSucess, bool IsRefresh) Handle(string receivedData);
    }
}
