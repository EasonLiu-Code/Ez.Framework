using System.Text;
using Ez.Domain.CommonDto.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ez.Domain.CommonDto.Handlers;

/// <summary></summary>
public class ArticleInsertHandler(ILogger<ArticleInsertHandler> logger):INotificationHandler<ArticleInsertEvent>
{
    /// <summary>
    /// Handle
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    public async Task Handle(ArticleInsertEvent notification, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        logger.LogInformation(
            new StringBuilder().Append("ArticleInsertEventHandle ok !!!!!!!!!!!").ToString(),notification.EventData);
    }
}