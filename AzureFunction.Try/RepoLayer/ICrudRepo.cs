namespace RepoLayer
{
    public interface ICrudRepo
    {
        Task<string> AddData(TestModel model);
        Task<List<string>> GetData();
    }
}