using System.Data.Entity;

namespace WhereShouldWeEatLunch.Models
{
    public class WhereShouldWeEatLunchContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<WhereFoodShouldWeEatLunch.Models.WhereShouldWeEatLunchContext>());

        public WhereShouldWeEatLunchContext()
        {
            this.Database.Connection.ConnectionString =
                "Server=820a774d-8122-4d90-93e2-a12401182188.sqlserver.sequelizer.com;Database=db820a774d81224d9093e2a12401182188;User ID=azsurtcqjwakvohi;Password=R8KqsCTBPRcRmRtvZ3SgtyErgBhiAdutzm35AFQDkrh7SvzNGNF7YjbrBsjXvJaL;";
        }

        public DbSet<FoodStyleModel> FoodStyleModels { get; set; }

        public DbSet<EateryModel> EateryModels { get; set; }
    }
}
