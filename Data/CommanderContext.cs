using dotnetWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetWebApi.Data
{
    public class CommanderContext : DbContext
    {
        public DbSet<Command> Commands { get; set; }

        public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt)
        {
        }
    }
}