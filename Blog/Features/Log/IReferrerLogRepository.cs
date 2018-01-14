using Blog.Data.Log;
using System.Collections.Generic;

namespace Blog.Features.Log
{
    public interface IReferrerLogRepository
    {
        void Log(string referrer);
        List<Referrer> ListRecent(int days);
    }
}