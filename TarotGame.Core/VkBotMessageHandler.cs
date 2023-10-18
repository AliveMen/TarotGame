using MediatR;

namespace TarotGame.Shared;

public class VkBotMessageHandler: IRequestHandler<MessageReceived>
{
    private readonly ITaroReader _taroReader;

    public VkBotMessageHandler(ITaroReader taroReader)
    {
        _taroReader = taroReader;
    }

    public async Task Handle(MessageReceived request, CancellationToken cancellationToken)
    {
        await _taroReader.MessageReceived(request.Message);
    }
}