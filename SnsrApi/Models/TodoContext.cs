using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnsrApi.Models
{
    public class TodoContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }
    }
}
