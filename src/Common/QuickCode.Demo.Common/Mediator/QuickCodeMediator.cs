namespace QuickCode.Demo.Common.Mediator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    
    public class QuickCodeMediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public QuickCodeMediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            dynamic handler = _serviceProvider.GetService(handlerType);
            if (handler == null)
                throw new InvalidOperationException($"Handler not found for {request.GetType().Name}");
            return await handler.Handle((dynamic)request, cancellationToken);
        }

        public async Task Publish<TNotification>(INotification notification, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(INotificationHandler<>).MakeGenericType(notification.GetType());
            var handlers = (IEnumerable<object>)_serviceProvider.GetService(typeof(IEnumerable<>).MakeGenericType(handlerType));
            if (handlers != null)
            {
                foreach (dynamic handler in handlers)
                {
                    await handler.Handle((dynamic)notification, cancellationToken);
                }
            }
        }
    }
}