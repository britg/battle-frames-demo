public interface IServerCommandProcessor {
    void Process (ServerCommand command, ServerCommandHandler.Callback callback);
}