using System;
using System.Diagnostics;
using System.Linq;
using DataTransferSQLToEl.SQLDomainModel;
using ElasticsearchCRUD;
using ElasticsearchCRUD.Tracing;

namespace DataTransferSQLToEl
{
	public class Repo
	{
		public Person GetPersonFromElasticsearch(int id)
		{
			Person person; 
			IElasticsearchMappingResolver ElasticsearchMappingResolver = new ElasticsearchMappingResolver();
			using (var ElasticsearchContext = new ElasticsearchContext("http://localhost:9200/", ElasticsearchMappingResolver))
			{
				person = ElasticsearchContext.GetDocument<Person>(id);
			}

			return person;
		}

		public Address GetAddressFromElasticsearch(int id)
		{
			Address address;
			IElasticsearchMappingResolver ElasticsearchMappingResolver = new ElasticsearchMappingResolver();
			using (var ElasticsearchContext = new ElasticsearchContext("http://localhost:9200/", ElasticsearchMappingResolver))
			{
				address = ElasticsearchContext.GetDocument<Address>(id);
			}

			return address;
		}

		public void SaveToElasticsearchAddress()
		{
			IElasticsearchMappingResolver ElasticsearchMappingResolver = new ElasticsearchMappingResolver();
			using (var ElasticsearchContext = new ElasticsearchContext("http://localhost:9200/", ElasticsearchMappingResolver))
			{
				//ElasticsearchContext.TraceProvider = new ConsoleTraceProvider();
				using (var modelPerson = new ModelPerson())
				{
					int pointer = 0;
					const int interval = 100;
					int length = modelPerson.Address.Count();

					while (pointer < length)
					{
						stopwatch.Start();
						var collection = modelPerson.Address.OrderBy(t => t.AddressID).Skip(pointer).Take(interval).ToList<Address>();
						stopwatch.Stop();
						Console.WriteLine("Time taken for select {0} AddressID: {1}", interval, stopwatch.Elapsed);
						stopwatch.Reset();

						foreach (var item in collection)
						{
							ElasticsearchContext.AddUpdateDocument(item, item.AddressID);
							string t = "yes";
						}

						stopwatch.Start();
						ElasticsearchContext.SaveChanges();
						stopwatch.Stop();
						Console.WriteLine("Time taken to insert {0} AddressID documents: {1}", interval, stopwatch.Elapsed);
						stopwatch.Reset();
						pointer = pointer + interval;
						Console.WriteLine("Transferred: {0} items", pointer);
					}
				}
			}
		}

		private Stopwatch stopwatch = new Stopwatch();
		public void SaveToElasticsearchPerson()
		{
			IElasticsearchMappingResolver ElasticsearchMappingResolver = new ElasticsearchMappingResolver();
			using (var ElasticsearchContext = new ElasticsearchContext("http://localhost:9200/", ElasticsearchMappingResolver))
			{
				//ElasticsearchContext.TraceProvider = new ConsoleTraceProvider();
				using (var modelPerson = new ModelPerson())
				{
					int pointer = 0;
					const int interval = 500;
					int length = modelPerson.Person.Count();

					while (pointer < length)
					{
						stopwatch.Start();
						var collection = modelPerson.Person.OrderBy(t => t.BusinessEntityID).Skip(pointer).Take(interval).ToList<Person>();
						stopwatch.Stop();
						Console.WriteLine("Time taken for select {0} persons: {1}", interval,stopwatch.Elapsed);
						stopwatch.Reset();

						
						foreach (var item in collection)
						{
							ElasticsearchContext.AddUpdateDocument(item, item.BusinessEntityID);
							string t = "yes";
						}

						stopwatch.Start();
						ElasticsearchContext.SaveChanges();
						stopwatch.Stop();
						Console.WriteLine("Time taken to insert {0} person documents: {1}", interval, stopwatch.Elapsed);
						stopwatch.Reset();
						pointer = pointer + interval;
						Console.WriteLine("Transferred: {0} items", pointer);
					}
				}
			}
		}
	}
}
