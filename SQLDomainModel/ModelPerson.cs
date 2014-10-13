namespace DataTransferSQLToEl.SQLDomainModel
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class ModelPerson : DbContext
	{
		public ModelPerson()
			: base("name=ModelPerson")
		{
			//this.Configuration.LazyLoadingEnabled = false;
		}

		public virtual DbSet<Address> Address { get; set; }
		public virtual DbSet<AddressType> AddressType { get; set; }
		public virtual DbSet<ContactType> ContactType { get; set; }
		public virtual DbSet<CountryRegion> CountryRegion { get; set; }
		public virtual DbSet<EmailAddress> EmailAddress { get; set; }
		public virtual DbSet<Person> Person { get; set; }
		public virtual DbSet<PersonPhone> PersonPhone { get; set; }
		public virtual DbSet<PhoneNumberType> PhoneNumberType { get; set; }
		public virtual DbSet<StateProvince> StateProvince { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CountryRegion>()
				.HasMany(e => e.StateProvince)
				.WithRequired(e => e.CountryRegion)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Person>()
				.Property(e => e.PersonType)
				.IsFixedLength();

			modelBuilder.Entity<Person>()
				.HasMany(e => e.EmailAddress)
				.WithRequired(e => e.Person)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Person>()
				.HasMany(e => e.PersonPhone)
				.WithRequired(e => e.Person)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<PhoneNumberType>()
				.HasMany(e => e.PersonPhone)
				.WithRequired(e => e.PhoneNumberType)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<StateProvince>()
				.Property(e => e.StateProvinceCode)
				.IsFixedLength();

			modelBuilder.Entity<StateProvince>()
				.HasMany(e => e.Address)
			.WithRequired(e => e.StateProvince)
			.WillCascadeOnDelete(false);
		}
	}
}
