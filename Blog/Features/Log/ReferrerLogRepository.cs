using Blog.Data;
using Blog.Data.Log;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Features.Log
{
    public class ReferrerLogRepository : IReferrerLogRepository
    {
        private readonly BlogDbContext _context;

        public ReferrerLogRepository(BlogDbContext context)
        {
            _context = context;
        }

        public List<Referrer> ListRecent(int days)
        {
            var minDate = DateTime.UtcNow.AddDays(-days);
            return _context.Referrers.Where(p => p.Created > minDate).OrderByDescending(p => p.Created).ToList();
        }

        public void Log(string referrer)
        {
            _context.Referrers.Add(new Referrer
            {
                Ref = referrer,
                Created = DateTime.UtcNow
            });
            _context.SaveChanges();
        }
    }
}
