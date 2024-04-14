using Ez.Infrastructure.LocalEventExtension;

namespace Ez.Domain.CommonDto.Events;

/// <summary>
/// </summary>
/// <param name="Id"></param>
public record ArticleInsertEvent(Guid Id) : IntegrationEvent(Id);
