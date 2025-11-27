using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAppCoreMVC.Areas.ADMIN.Interfaces;
using WebAppCoreMVC.Helpers;

namespace WebAppCoreMVC.Areas.ADMIN.Services
{
    public class FindRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        private readonly DbSet<T> _dbSet;

        public FindRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T> FindElementByNameAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();
        }

    }
}
