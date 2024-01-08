using DAL.Interfaces;

namespace DAL.Repository
{
    public class MoneyRepository : IMoneyRepository
    {
        private readonly AppDbContext _context;

        public MoneyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SetMoney(int userId, int money)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var user = _context.Users.FirstOrDefault(x => x.Id == userId);
                    if (user != null)
                    {
                        user.Balance += money;
                        var result = await _context.SaveChangesAsync() > 0;
                        transaction.Commit();
                        return result;
                    }
                    transaction.Commit();
                    return false;
                }
                catch (System.Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> WithdrawMoney(int userId, int money)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var user = _context.Users.FirstOrDefault(x => x.Id == userId);
                    if (user != null)
                    {
                        user.Balance -= money;
                        var result = await _context.SaveChangesAsync() > 0;
                        transaction.Commit();
                        return result;
                    }
                    transaction.Commit();
                    return false;
                }
                catch (System.Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
