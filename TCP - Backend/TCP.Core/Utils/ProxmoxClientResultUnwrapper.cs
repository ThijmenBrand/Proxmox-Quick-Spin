using System.Dynamic;
using Newtonsoft.Json;

namespace TCP.Core.Utils;

public class ProxmoxClientResultUnwrapper
{
    public static T Unwrap<T>(dynamic wrapped) =>
        JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(wrapped.data));
}