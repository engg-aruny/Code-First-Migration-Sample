### What is Code First Database Migration in Entity Framework?

Code First Database Migration is a feature of Entity Framework, tool developers use to help map their code to a database. With Code First, developers can define the structure of their database using code, and Entity Framework can automatically generate and apply scripts to migrate the database based on changes made to the code. This makes it easier for developers to manage and maintain their databases as their code changes over time.

> The [source code](https://github.com/engg-aruny/Code-First-Migration-Sample) for this article can be found on Github. 

> This article is based on .Net Core 6 + Entity Framework Core 7.0

> All commands need to run in Visual Studio and open the Package Manager Console from Tools

### Add NuGet Package

```bash
install-package Microsoft.EntityFrameworkCore
install-package Microsoft.EntityFrameworkCore.Tools
install-package Microsoft.EntityFrameworkCore.SqlServer
```

### A step-by-step guide to implementing Code First Database Migration using Entity Framework

1. **Define:** Connection string/Register Context in the Program.cs

```json
{
 "ConnectionStrings": {
        "SchoolDb": "Server=localhost;Database=DPSSchoolDb;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true"
    }
}
```

**Program.cs**

```csharp
var connectionString = builder.Configuration.GetConnectionString("SchoolDb");

builder.Services.AddDbContextPool<SchoolDbContext>(option =>
{
    option.UseSqlServer(connectionString);
});
```

2. **Define your Entities/Models:** Two classes are defined: `StudentEntity` and `TeacherEntity`

**StudentEntity.cs**

```csharp
public class StudentEntity
{
	public int ID { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public DateTime DateOfBirth { get; set; }

	[NotMapped]
	public List<string> ClassesEnrolled { get; set; }
}
```

**TeacherEntity.cs**
```csharp
public class TeacherEntity
{
	public int ID { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string PhoneNumber { get; set; }

	[NotMapped]
	public List<string> ClassesTaught { get; set; }
}
```

3. **Add Context and Configure Model with Rules:** Use fluent API to configure a model, you can override the `OnModelCreating` method, Fluent API configuration takes the highest priority when defining the mapping between a code entity and the corresponding database table.

```csharp
	public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TeacherEntity> Teachers { get; set; }

        public DbSet<StudentEntity> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure Teacher
            modelBuilder.Entity<TeacherEntity>().
                 ToTable("Teachers");

            modelBuilder.Entity<TeacherEntity>()
                .HasKey(x => x.ID);

           modelBuilder.Entity<TeacherEntity>().Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<TeacherEntity>().Property(t => t.PhoneNumber)
               .IsRequired()
               .HasMaxLength(10);

            //Configure Student
            modelBuilder.Entity<StudentEntity>().
                 ToTable("Students");
            
            modelBuilder.Entity<StudentEntity>()
                .HasKey(x => x.ID);

            modelBuilder.Entity<StudentEntity>()
               .Property(x => x.DateOfBirth)
                .IsRequired();

            modelBuilder.Entity<StudentEntity>()
               .Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(50);
        }
    }
```
_The configuration includes defining the table name, setting the primary key, and defining constraints on properties such as `IsRequired` and `HasMaxLength`._

4. **Seed Initial Data:** into the database. This is done using the `HasData` method in the `OnModelCreating` method

```csharp
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeacherEntity>().HasData(
            new TeacherEntity
            {
                ID = 1,
                FirstName = "Ramesh",
                LastName = "Kumar",
                Email = "testramesh@gmail.com",
                PhoneNumber = "1234567890",
            },
            new TeacherEntity
            {
                ID = 2,
                FirstName = "Amit ",
                LastName = "Sharma",
                Email = "testamitsharma@gmail.com",
                PhoneNumber = "1234517890",
            });

            modelBuilder.Entity<StudentEntity>().HasData(
            new StudentEntity
            {
                ID = 1,
                FirstName = "Mohit",
                LastName = "Yadav",
                Email = "testmohit@gmail.com",
                DateOfBirth = DateTime.Now,
            },
            new StudentEntity
            {
                ID = 2,
                FirstName = "Ankit",
                LastName = "Sharma",
                Email = "testankitsharma@gmail.com",
                DateOfBirth = DateTime.Now,
            });
        }
```

5. **Add New Migration:** Now, you can add a migration using Add-Migration. For example:

```bash
Add-Migration InitialCreate
```

_This command will generate a migration file to apply in the database. **Here is a screenshot**_

![Migration](https://www.dropbox.com/s/rqrdc9awfndf7c9/migration.jpg?raw=1 "Migration")

6. **Update Database:** Finally, you can update the database with the changes by running the Update-Database

```bash
Update-Database
```

**Here is a screenshot of the PowerShell output.**

![PowerShell output](https://www.dropbox.com/s/fq81m7slo9huj6y/update_database_output.jpg?raw=1 "PowerShell output")

**Here is a screenshot from the database**

![from the database](https://www.dropbox.com/s/wvhdk8zcyyoa6i4/database_after_update.jpg?raw=1 "from the database")

### Handle Schema Changes
**_Schema Changes Scenario 01:_** Let's say we want to add a new property to our `Teacher` entity called `Gender`. We can add this property to our `Teacher` class and update the configuration to include it. Here is a screenshot of the changes.

![New Property Added](https://www.dropbox.com/s/4nrfluxk3w9ydy8/new_property_teachers.jpg?raw=1 "New Property Added")

![Configuration New Property](https://www.dropbox.com/s/wudofxyjw0p269v/configuration_new_property.jpg?raw=1 "Configuration New Property")

Add new migration and update the database to apply changes with the following commands.

```bash
Add-Migration AddGenderColumn
```

```bash
update-database AddGenderColumn
```
**Here is a screenshot of the database after you add a new column**

![database after you add a new column](https://www.dropbox.com/s/5kv1loifjj9hha6/database_after_column_update.jpg?raw=1 "database after you add a new column")

**_Schema Changes Scenario 02:_** Let's say we want to rename the `Teacher` table to `Educators` and also need indexing to be applied to the 'Email' column

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
	// Rename the Teachers table to Educators
	modelBuilder.Entity<TeacherEntity>()
		.ToTable("Educators");

	// Add an index to the Educators table on the Name column
	modelBuilder.Entity<TeacherEntity>()
		.HasIndex(x => x.Email)
		.HasDatabaseName("IX_Educators_Email")
		.HasFilter(null)
		.IsUnique();
}
```

```bash
Add-Migration RenameTeacherTableAndAddIndexToEmail
```

```bash
update-database
```

**Here is a screenshot from the database**

![Rename Teacher Table Add Index To Email](https://www.dropbox.com/s/x91qap5w24r2hi7/database_rename_table_index.jpg?raw=1 "Rename Teacher Table Add Index To Email")

### Push These Changes out to a test server and eventually production.

We need a SQL Script to hand over to DBA so they can apply changes in the same sequence manner. This time we will run the `Script-Migration`
to write changes as a script rather than applied. You can also provide the range to apply script.

```bash
Script-Migration
```

![SQL Script](https://www.dropbox.com/s/85ekqu9z6xfedcs/sql-script.jpg?raw=1 "SQL Script")

### Apply Migration at runtime

```csharp
public static void Main(string[] args)
{
    var host = CreateHostBuilder(args).Build();

    using (var scope = host.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        //To apply migrations programmatically
        db.Database.Migrate();
    }

    host.Run();
}
```

> The [source code](https://github.com/engg-aruny/Code-First-Migration-Sample) for this article can be found on Github. 

### Summary
Code First Database Migration using Entity Framework. Code First is a feature of Entity Framework that allows developers to define the structure of their database using code. The code uses .NET Core 6 and Entity Framework Core 7.0. The article provides a step-by-step guide to implementing Code First Database Migration. The steps include defining a connection string and registering a context, defining model entities, configuring the model using fluent API, seeding initial data, adding a new migration, and updating the database. The code demonstrates how to add a migration using Add-Migration and update the database using Update-Database. The output includes screenshots of the migration file, PowerShell output, and the updated database.
