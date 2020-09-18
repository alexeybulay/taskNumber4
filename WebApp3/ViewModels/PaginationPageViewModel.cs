using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp3.ViewModels
{
    public class PaginationPageViewModel
    {
        public int NumberPage { get; set; }
        public int TotalPage { get; set; }

        public PaginationPageViewModel()
        {
        }

        public PaginationPageViewModel(int count,int  numberPage, int pageSize)
        {
            NumberPage = numberPage;
            TotalPage = (int) Math.Ceiling(count / (double) pageSize);
        }

        /// <summary>
        /// Узнать, присутстсвует ли страница до
        /// </summary>
        public bool HasPreviousPage 
        {
            get => NumberPage > 1;
        }

        /// <summary>
        /// Узнать, присутстсвует ли страница после
        /// </summary>
        public bool HasNextPage
        {
            get => NumberPage < TotalPage;
        }
    }
}
