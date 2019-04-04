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
				var sourceDir = "..\\input";
				var targetDir = "..\\output";

				WalkPath(sourceDir, targetDir);
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
				foreach (string sourceFile in Directory.EnumerateFiles(sourceDir, "*.*", SearchOption.AllDirectories))
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
