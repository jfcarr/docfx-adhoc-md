namespace cagtoc
{
	public interface IFileUtil
	{
		void WalkPath();

		FileModel BuildFileModel(string sourceFile);
	}
}