using Application.Commands;
using Application.DomainEventHandlers;
using Application.Models;
using Application.Queries;
using Core.Commands;
using Core.Events;
using Core.Queries;
using Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class Config
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services
                .AddAutoMapper(typeof(ApplicationMapper))
                .AddCommandHandlers()
                .AddDomainEventHandlers()
                .AddQueryHandlers();

            return services;
        }

        private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            services
                .AddCommandHandler<CreateUserCommand, CreateUserCommandHandler>()
                .AddCommandHandler<UpdateUserCommand, UpdateUserCommandHandler>()
                .AddCommandHandler<DeleteUserCommand, DeleteUserCommandHandler>();

            return services;
        }

        private static IServiceCollection AddDomainEventHandlers(this IServiceCollection services)
        {
            services
                .AddEventHandler<UserCreatedEvent, UpdateUserDetailEventHandler>()
                .AddEventHandler<UserUpdatedEvent, UpdateUserDetailEventHandler>()
                .AddEventHandler<UserDeletedEvent, UpdateUserDetailEventHandler>()
                .AddEventHandler<UserCreatedEvent, UpdateAddressHistoryEventHandler>()
                .AddEventHandler<UserUpdatedEvent, UpdateAddressHistoryEventHandler>()
                .AddEventHandler<UserDeletedEvent, UpdateAddressHistoryEventHandler>();

            return services;
        }

        private static IServiceCollection AddQueryHandlers(this IServiceCollection services)
        {
            services
                .AddQueryHandler<GetUsersQuery, PagedList<UserDetail>, GetUsersQueryHandler>()
                .AddQueryHandler<GetUserByIdQuery, UserDetail, GetUserByIdQueryHandler>();

            return services;
        }
    }
}
