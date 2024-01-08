namespace DAL.Interfaces
{
    public interface IMoneyRepository
    {
        Task<bool> SetMoney(int userId, int money);
        Task<bool> WithdrawMoney(int userId, int money);
    }
}
