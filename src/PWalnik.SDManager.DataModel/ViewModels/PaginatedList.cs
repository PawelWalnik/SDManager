using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PWalnik.SDManager.DataModel.ViewModels
{
    public class PaginatedList <T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int NumberOfItems { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize, int numberOfItems)
        {
            PageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            NumberOfItems = numberOfItems;
            AddRange(items);
        }

        public bool HasPreviousPage => (PageIndex > 1);
        public bool HasNextPage => (PageIndex < TotalPages);

        //Aync method is used instead of a cunstructor to create the PaginatedList<T>, because as everybody knows - constructors can`t run asynchronous code.
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize,
            int numberOfItems)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex,pageSize, numberOfItems);
        }
    }
}
