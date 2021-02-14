using System;
using System.Collections.Generic;
using System.Threading;

namespace DZ_13_02_21
{
	class Program
	{
		public static List<Client> clientList = new List<Client>();
		static void Main(string[] args)
		{
			Insert(new Client() { Id = 1, Balance = 0m });
			Insert(new Client() { Id = 2, Balance = 0m });
			Insert(new Client() { Id = 3, Balance = 0m });
			Insert(new Client() { Id = 4, Balance = 0m });
			Insert(new Client() { Id = 5, Balance = 0m });
			Insert(new Client() { Id = 6, Balance = 0m });
			Insert(new Client() { Id = 7, Balance = 0m });

			TimerCallback checkTimer = new TimerCallback(CheckBalanceDiff);
			Timer timer = new Timer(checkTimer, clientList, 0, 10);

			Random rnd = new Random();
			while (true)
			{
				var client = new Client()
				{
					Id = rnd.Next(1, 7),
					Balance = rnd.Next(0, 200)
				};
				Update(client);
				Thread.Sleep(500);
			}
		}
		public static void CheckBalanceDiff(object list)
		{
			var lst = list as List<Client>;
			foreach (var client in lst)
			{
				Monitor.Enter(client);
				if (client.isChecked) continue;
				client.isChecked = true;
				var diff = client.Balance - client.PrevBalance;
				if (diff > 0)
				{
					showGreen($"Id: {client.Id},\t BalanceBefore: {client.PrevBalance},\t BalanceAfter: {client.Balance},\t Diff: +{diff}");
				}
				if (diff < 0)
				{
					showRed($"Id: {client.Id},\t BalanceBefore: {client.PrevBalance},\t BalanceAfter: {client.Balance},\t Diff: {diff}");
				}
				Monitor.Exit(client);
			}
		}
		public static void showRed(string text)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			System.Console.WriteLine(text);
			Console.ForegroundColor = ConsoleColor.White;
		}
		public static void showGreen(string text)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			System.Console.WriteLine(text);
			Console.ForegroundColor = ConsoleColor.White;

		}
		public static void Insert(Client client)
		{
			clientList.Add(client);
		}
		public static void Update(Client client)
		{
			var clientToUpdate = clientList.Find(x => x.Id == client.Id);
			clientToUpdate.PrevBalance = clientToUpdate.Balance;
			clientToUpdate.Balance = client.Balance;
			clientToUpdate.isChecked = false;
		}
		public static void Delete(int id)
		{
			clientList.Remove(clientList.Find(x => x.Id == id));
		}
		public static void Select(Predicate<Client> match)
		{
			var selected = clientList.FindAll(match);
			foreach (var client in selected)
			{
				client.Show();
			}
		}
	}
}
