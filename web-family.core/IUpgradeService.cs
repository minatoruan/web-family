using System.ServiceModel;

namespace web_family.core
{
    [ServiceContract]
    public interface IUpgradeService
    {
        [OperationContract]
        void Upgrade();
    }
}
