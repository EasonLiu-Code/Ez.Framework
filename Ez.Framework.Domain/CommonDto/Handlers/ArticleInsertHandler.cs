using Ez.Domain.CommonDto.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ez.Domain.CommonDto.Handlers;

/// <summary></summary>
public class ArticleInsertHandler(ILogger logger):INotificationHandler<ArticleInsertEvent>
{
    /// <summary>
    /// Handle
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    public async Task Handle(ArticleInsertEvent notification, CancellationToken cancellationToken)
    {
       logger.LogInformation("ArticleInsertEvent ok !!!!!!!!!!!");
    }
}