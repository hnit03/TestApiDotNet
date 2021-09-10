using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using Task3.IRepositories;
using Task3.Models;
using System.Linq;

namespace Task3.Ropositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DbContext context;
        private readonly DbSet<Member> dbSet;
        private readonly ILogger<AccountRepository> logger;

        public AccountRepository(DbContext context, ILogger<AccountRepository> logger)
        {
            this.context = context;
            dbSet = this.context.Set<Member>();
            this.logger = logger;
        }

        public Member Login(string username, string password)
        {
            Member accountDTO = null;
            try
            {
                var account = dbSet.Where(member => member.Username.Equals(username) && member.Password.Equals(password)).First();
                if (account != null)
                {
                    if(account.Password == password)
                    {
                        accountDTO = account;
                    }
                }
                else
                {
                    accountDTO = null;
                }
                return accountDTO;
            }
            catch (Exception ex)
            {
                logger.LogError("AccountRepository (Login): " + ex.Message);
                return null;
            }
        }
    }
}
