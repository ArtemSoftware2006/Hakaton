using Domain.Entity;

namespace Services.Interfaces
{
    public interface IContractService
    {
        Task<Contract> create();
    }
}