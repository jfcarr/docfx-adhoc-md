using System;
using System.IO;

namespace cagtoc
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				if (args.Length == 2)
				{

					/* -- debugging
					var sourceDir = "..\\input";
					var targetDir = "..\\output";
					*/

					var sourceDir = args[0];
					var targetDir = args[1];

					WalkPath(sourceDir, targetDir);
				} else {
					Console.WriteLine ("USAGE:");
					Console.WriteLine("\tcagtoc {source directory} {target directory}");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"[ERROR] {ex.Message}");
			}
		}

		static void WalkPath(string sourceDir, string targetDir)
		{
			if (!Directory.Exists(targetDir))
			{
				Directory.CreateDirectory(targetDir);
			}
			var tocFile = Path.Combine(targetDir, "toc.yml");

			using (StreamWriter tocFileStream = new StreamWriter(tocFile))
			{
				foreach (string sourceFile in Directory.EnumerateFiles(sourceDir, "*.md", SearchOption.AllDirectories))
				{
					var fileName = Path.GetFileName(sourceFile);
					var relativeFile = Path.GetRelativePath(sourceDir, sourceFile);
					var dirName = Path.GetDirectoryName(relativeFile);

					var fullTargetDir = Path.Combine(targetDir, dirName);
					var targetFile = Path.Combine(fullTargetDir, fileName);
					if (!Directory.Exists(fullTargetDir))
					{
						Directory.CreateDirectory(fullTargetDir);
					}
					if (!File.Exists(targetFile))
					{
						File.Copy(sourceFile, targetFile);
					}

					// Heading entry:
					tocFileStream.WriteLine((string.IsNullOrEmpty(dirName)) ? $"- {fileName}" : $"- {fileName} in {dirName}");
					// Path entry:
					tocFileStream.WriteLine($"  {relativeFile}");
				}
			}
		}
	}
}
