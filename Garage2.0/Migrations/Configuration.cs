namespace Garage2._0.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;


    internal sealed class Configuration : DbMigrationsConfiguration<Garage2._0.DAL.VehicleDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Garage2._0.DAL.VehicleDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //int[] n2 = new int[] {2, 4, 6, 8};
            // var courses = new Course[]
            //{
            //     new Course() { Title = ".NET ND16" },
            //     new Course() { Title = ".NET ND17" },
            //     new Course() { Title = "Java JD16" },
            //     new Course() { Title = "Support SC16" },
            //     new Course() { Title = "Java4Women JWA16" }
            //}            context.Courses.AddOrUpdate(c => c.Title, courses);
            //context.SaveChanges();
            var vehicleType = new VehicleType[]
         {
          new VehicleType() {Type= "Car" },
          new VehicleType() {Type= "Bus" },
          new VehicleType() {Type= "Boat" },
          new VehicleType() {Type= "Airplane" },
          new VehicleType() {Type= "Motorcycle" }
         };

            context.VehicleTypes.AddOrUpdate(v => v.Type, vehicleType); //templösning för att kunna lägga in id korrekt i databasen
            context.SaveChanges();

           // var vehicle = new Vechicle { Id = 1, VehicleTypeId = vehicleType[0].Id, RegNo = "XHY449", Color = WColor.Red, Brand = "Volvo", ParkingTime = new DateTime(2016, 10, 17, 8, 00, 00), Model = "C70", NrOfWeels = 4, };


           var member = new Member[]
            {
                new Member() {Name="Anna Bok" },
                new Member() {Name="Lena Svensson" },
                new Member() {Name="Amir Amini" },
                new Member() {Name="Boula Bubbla" },
            };
            context.Members.AddOrUpdate(m => m.Name, member);
            context.SaveChanges();



            context.Vehicles.AddOrUpdate(
    v => v.Id, //given a vechicle v where there is a v.id, create what is below if it doesnt exist in the database. which doesnt make sense.
    new Vechicle { Id = 1, VehicleTypeId = vehicleType[0].Id, RegNo = "XHY449", Color = WColor.Red, Brand = "Volvo", ParkingTime = new DateTime(2016, 10, 17, 8, 00, 00), Model = "C70", NrOfWeels = 4, MemberId = context.Members.FirstOrDefault().Id },
    new Vechicle { Id = 2, VehicleTypeId = vehicleType[0].Id, RegNo = "ABC123", Color = WColor.White, Brand = "SAAB", ParkingTime = new DateTime(2016, 10, 16, 10, 00, 00), Model = "Aero", NrOfWeels = 4, MemberId = context.Members.FirstOrDefault().Id },
    new Vechicle { Id = 3, VehicleTypeId = vehicleType[3].Id, RegNo = "DEF345", Color = WColor.Blue, Brand = "Ford", ParkingTime = new DateTime(2016, 10, 15, 12, 00, 00), Model = "Escort", NrOfWeels = 4, MemberId = context.Members.FirstOrDefault().Id },
    new Vechicle { Id = 4, VehicleTypeId = vehicleType[4].Id, RegNo = "YZG666", Color = WColor.Black, Brand = "Harley Davidsson", ParkingTime = new DateTime(2016, 10, 14, 18, 00, 00), Model = "Night Rod", NrOfWeels = 2, MemberId = context.Members.FirstOrDefault().Id },
    new Vechicle { Id = 5, VehicleTypeId = vehicleType[2].Id, RegNo = "YRT552", Color = WColor.White, Brand = "Evinrude", ParkingTime = new DateTime(2016, 10, 14, 22, 00, 00), Model = "Unknown", NrOfWeels = 12, MemberId = context.Members.FirstOrDefault().Id },
    new Vechicle { Id = 6, VehicleTypeId = vehicleType[1].Id, RegNo = "TTY112", Color = WColor.Red, Brand = "Scania", ParkingTime = new DateTime(2016, 10, 13, 23, 00, 00), Model = "R420", NrOfWeels = 8, MemberId = context.Members.FirstOrDefault().Id },
    new Vechicle { Id = 7, VehicleTypeId = vehicleType[1].Id, RegNo = "ONT999", Color = WColor.Orange, Brand = "Volvo", ParkingTime = new DateTime(2016, 10, 12, 23, 30, 00), Model = "240", NrOfWeels = 4, MemberId = context.Members.FirstOrDefault().Id }
    );
        }
    }
}
