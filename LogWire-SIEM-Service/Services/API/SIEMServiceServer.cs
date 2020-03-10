using System;
using System.Threading.Tasks;
using Grpc.Core;
using LogWire.SIEM.Service.Data.Model;
using LogWire.SIEM.Service.Data.Repository;
using LogWire.SIEM.Service.Protos;

namespace LogWire.SIEM.Service.Services.API
{
    public class SIEMServiceServer : SIEMService.SIEMServiceBase
    {

        private readonly IDataRepository<UserEntry> _repository;

        public SIEMServiceServer(IDataRepository<UserEntry> repository)
        {
            _repository = repository;
        }

        public override Task<UserListResponse> GetUserList(UserListMessage request, ServerCallContext context)
        {
            var repo = _repository as UserRepository;

            var result = repo.GetPagedList(request.ResultsPerPage, request.PageNumber);

            var ret = new UserListResponse();
            ret.TotalPages = result.PageCount;

            foreach (var siemUserEntry in result.Results)
            {
                ret.Users.Add(new SIEMUser
                {
                    Username = siemUserEntry.Username,
                    Id = siemUserEntry.Id.ToString()
                });
            }

            return Task.FromResult(ret);

        }

        public override Task<AddUserResposne> AddUser(AddUserMessage request, ServerCallContext context)
        {
            Guid id = Guid.NewGuid();

            _repository.Add(new UserEntry
            {
                Id = id,
                Username = request.Username
            });

            return Task.FromResult(new AddUserResposne { Id = id.ToString() });

        }
    }
}
