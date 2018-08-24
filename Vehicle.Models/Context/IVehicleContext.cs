using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Models.Context
{
    public class VehicleContext : DbContext
    {
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<VehicleMake> VehicleMakes { get; set; }

        //Relationship between tables VehicleModels and VehicleMakes (Foreign Key)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleMake>()
                .HasMany(x => x.VehicleModels)
                .WithOptional(x => x.VehicleMake)
                .HasForeignKey(x => x.MakeID);
        }

        //If models change drop existing database and create a new one
        protected void Application_Start(object sender, EventArgs e)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<VehicleContext>());
        }

    }
}
