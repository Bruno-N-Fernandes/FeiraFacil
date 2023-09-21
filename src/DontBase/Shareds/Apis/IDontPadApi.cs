using System.Threading.Tasks;

namespace DontBase.Shareds.Apis
{
    public interface IDontPadApi
    {
        Task<string> Get(string resource, long lastModified = 0);
        Task<long> Post(string resource, string text);
    }
}