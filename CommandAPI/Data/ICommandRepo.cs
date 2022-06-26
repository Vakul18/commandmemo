
namespace CommandAPI.Data
{
    using System.Collections.Generic;
    using CommandAPI.Models;

    public interface ICommandRepo
    {
        bool SaveChanges();
        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int commandId);
        void CreateCommand(Command command);
        void UpdateCommand(Command command);
        void DeleteCommand(Command command);
    }
}