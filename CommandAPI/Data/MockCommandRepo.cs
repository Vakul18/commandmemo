
namespace CommandAPI.Data
{
    using System.Collections.Generic;
    using CommandAPI.Models;
    
    internal class MockCommandRepo : ICommandRepo
    {
        public void CreateCommand(Command command)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command command)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return new List<Command>(){
                new Command{ Id =0, HowTo="Type it",Line="Type" , Platform="Laptop" },
                new Command{ Id =1, HowTo="Read it",Line="Read" , Platform="Laptop" },
                new Command{ Id =2, HowTo="Delete it",Line="Del" , Platform="Desktop" }
            };
        }

        public Command GetCommandById(int commandId)
        {
            return new Command{ Id =0, HowTo="Type it",Line="Type" , Platform="Laptop" };
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command command)
        {
            throw new System.NotImplementedException();
        }
    }
}