
namespace CommandAPI.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CommandAPI.Models;

    internal class PostgreCommandRepo : ICommandRepo
    {
        private CommandRepoContext _dbContext;

        public PostgreCommandRepo(CommandRepoContext repoContext,TestServ t)
        {
            _dbContext = repoContext;
        }

        public void CreateCommand(Command command)
        {
            if(command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            _dbContext.Commands.Add(command);
        }

        public void DeleteCommand(Command command)
        {
            if(command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            _dbContext.Commands.Remove(command);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _dbContext.Commands.ToList<Command>();
        }

        public Command GetCommandById(int commandId)
        {
            return _dbContext.Commands.FirstOrDefault(command => command.Id == commandId);
        }

        public bool SaveChanges()
        {
            return _dbContext.SaveChanges()>=0;
        }

        public void UpdateCommand(Command command)
        {
            
        }
    }
}