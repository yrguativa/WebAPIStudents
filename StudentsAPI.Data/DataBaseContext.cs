using System;
using Microsoft.EntityFrameworkCore;

namespace StudentsAPI.Data
{
     public class DataBaseContext: DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }
    }
}
