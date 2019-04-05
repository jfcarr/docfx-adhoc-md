using System;
using System.IO;

namespace cagtoc
{
	class Program
	{
		static void Main(string[] args)
		{
			var isDebugging = false;

			try
			{
				if (args.Length == 2 || isDebugging == true)
				{

					var sourceDir = (isDebugging) ? "..\\input" : args[0];
					var targetDir = (isDebugging) ? "..\\output" : args[1];

					var myFileUtil = new FileUtil(sourceDir, targetDir);
					myFileUtil.WalkPath();
				}
				else
				{
					Console.WriteLine("USAGE:");
					Console.WriteLine("\tcagtoc {source directory} {target directory}");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"[ERROR] {ex.Message}");
			}
		}
	}
}
