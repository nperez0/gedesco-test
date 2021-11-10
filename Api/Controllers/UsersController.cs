using Api.Requests;
using Application.Commands;
using Application.Models;
using Application.Queries;
using Core.Commands;
using Core.Extensions;
using Core.Ids;
using Core.Queries;
using Domain.Aggregates.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;
        private readonly IIdGenerator _idGenerator;
        public UsersController(ICommandBus commandBus, IQueryBus queryBus, IIdGenerator idGenerator)
        {
            _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
            _queryBus = queryBus ?? throw new ArgumentNullException(nameof(queryBus));
            _idGenerator = idGenerator ?? throw new ArgumentNullException(nameof(idGenerator));
        }

        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] CreateUserRequest request)
        {
            request.ThrowIfNull(nameof(request));

            var userId = _idGenerator.New();

            var command = CreateUserCommand.Create(
                userId,
                request.Username,
                request.Password,
                request.Name,
                request.Addresses
                    .Select(x => Address.Create(
                        _idGenerator.New(),
                        x.Street,
                        x.ZipCode,
                        x.City,
                        x.Country))
                    .ToArray());

            await _commandBus.Send(command);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest request)
        {
            request.ThrowIfNull(nameof(request));

            var command = UpdateUserCommand.Create(
                id,
                request.Name,
                request.Addresses
                    .Select(x => Address.Create(
                        x.AddressId.EnsureNotEmpty(_idGenerator.New()),
                        x.Street,
                        x.ZipCode,
                        x.City,
                        x.Country))
                    .ToArray());

            await _commandBus.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var command = DeleteUserCommand.Create(id);

            await _commandBus.Send(command);

            return Ok();
        }

        [HttpGet("{id}")]
        public Task<UserDetail> Get(Guid id)
            => _queryBus.Send<GetUserByIdQuery, UserDetail>(GetUserByIdQuery.Create(id));

        [HttpGet]
        public Task<PagedList<UserDetail>> Get([FromQuery] string keyword, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
            => _queryBus.Send<GetUsersQuery, PagedList<UserDetail>>(GetUsersQuery.Create(keyword, pageNumber, pageSize));
    }
}
