using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using webapi.Infrastructure.Repositories;
using webapi.Models.Auth;
using webapi.Models.Account;

namespace webapi.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountResponse>> GetAll();
        Task<AccountResponse> GetById(int id);
        Task<AccountResponse> Create(CreateRequest model);
        Task<AccountResponse> Update(int id, UpdateRequest model);
        Task Delete(int id);
    }
    public class AccountService : IAccountService
    {
        IAccountRepository accountRepository;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        public AccountService(IAccountRepository accountRepository, 
        IMapper mapper, 
        ILogger<AccountService> logger)
        {
            this.accountRepository = accountRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        public Task<AccountResponse> Create(CreateRequest model)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<AccountResponse>> GetAll()
        {
            var accountList =  await accountRepository.GetAllAsync();
            var mapped = mapper.Map<IEnumerable<AccountResponse>>(accountList);
            return mapped;
        }

        public Task<AccountResponse> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<AccountResponse> Update(int id, UpdateRequest model)
        {
            throw new System.NotImplementedException();
        }
    }
}