using System;

namespace DataTransferSQLToEl
{
	class Program
	{
		static void Main(string[] args)
		{
			var repo = new Repo();
			
			repo.SaveToElasticsearchAddress();
			var addressX = repo.GetAddressFromElasticsearch(22);

			//repo.SaveToElasticsearchPerson();
			//var personX = repo.GetPersonFromElasticsearch(345);
			Console.WriteLine(addressX);
			//Console.WriteLine(personX);
		}
	}
}
