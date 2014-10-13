using System;

namespace DataTransferSQLToEl
{
	class Program
	{
		static void Main(string[] args)
		{
			var repo = new Repo();
			repo.SaveToElasticsearchPerson();
			repo.SaveToElasticsearchAddress();

			var personX = repo.GetPersonFromElasticsearch(345);
			var addressX = repo.GetAddressFromElasticsearch(22);
			Console.WriteLine(addressX);
			Console.WriteLine(personX);
		}
	}
}
