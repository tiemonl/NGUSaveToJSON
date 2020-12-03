using System;
using System.IO;

namespace NGUSaveToJson {
	class Program {
		static void Main(string[] args) => new NGUSaveToJson()
			.MainAsync(path: args[0])
			.GetAwaiter()
			.GetResult();
	}
}
