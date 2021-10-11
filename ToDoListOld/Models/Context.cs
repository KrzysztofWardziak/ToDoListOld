using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ToDoListOld.Models
{
    public class Context : IdentityDbContext
    {
        public DbSet<ToDo> ToDoLists { get; set; }

        public Context(DbContextOptions options) : base(options)
        {

        }
    }
}
