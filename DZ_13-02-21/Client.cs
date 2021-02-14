using System;

namespace DZ_13_02_21
{
	class Client
	{
		public int Id { get; set; }
		public decimal Balance { get; set; } = 0;
		public decimal PrevBalance { get; set; } = 0;
		public bool isChecked { get; set; } = true;

		public void Show()
		{
			Console.WriteLine($"Id: {this.Id},\t Balance: {this.Balance}");
		}
	}
}
