using Domain;
using Microsoft.AspNetCore.Mvc;
using Olive.GlobalSearch;
using Olive.Mvc;
using Olive.Security;
using Olive.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Controllers
{
    [JwtAuthenticate]
    public class ApiController : BaseController
    {
        [Route("Api/Q1")]
        public async Task<SearchResultList> Q1(string searcher, string SortExpression, int StartIndex, int Size)
        {
            var datalist = new List<SearchResult> {
                          new SearchResult() { Title = "ITEM Q1 1",  Description ="this is test data for ajax q1" , Url=@"https://www.geeks.ltd.uk/", IconUrl= @"http://www.freelogovectors.net/wp-content/uploads/2015/06/Seo-Tags-Icon.png" }
                        , new SearchResult() { Title = "ITEM Q1 2",  Description ="this is test data for ajax q1" , Url=@"https://www.geeks.ltd.uk/", IconUrl= @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSmpgcjmXSJhXMYJEb1aX5OBxpzvAvPcWm7ikL8uzUoKWFV_b8d" }
                        , new SearchResult() { Title = "ITEM Q1 3",  Description ="this is test data for ajax q1" , Url=@"https://www.geeks.ltd.uk/", IconUrl= @"https://3.imimg.com/data3/GK/IB/MY-5681630/logo-design-service-250x250.png" }
                    };
            var tempquery = datalist.Where(p => p.Title.ToLower().Contains(searcher.Trim().ToLower()));

            return new SearchResultList()
            {
                TotalCount = tempquery.Count(),
                SortExpression = SortExpression,
                StartIndex = StartIndex,
                Size = Size,
                Data = tempquery.Skip(StartIndex).Take(Size).ToList()
            };
        }

        [Route("Api/Q2")]
        public async Task<SearchResultList> Q2(string searcher, string SortExpression, int StartIndex, int Size)
        {
            var datalist = new List<SearchResult> {
                  new SearchResult() { Title = "ITEM Q2 1",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"https://svn.apache.org/repos/asf/lucene.net/tags/Lucene.Net_3_0_3_RC1/branding/logo/lucene-net-icon-256x256.png" }
                , new SearchResult() { Title = "ITEM Q2 2",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"http://icons.iconarchive.com/icons/graphicloads/colorful-long-shadow/256/Home-icon.png" }
                , new SearchResult() { Title = "ITEM Q2 3",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"https://dumielauxepices.net/sites/default/files/heart-icons-colorful-636035-3581300.png" }
                , new SearchResult() { Title = "ITEM Q2 4",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"https://svn.apache.org/repos/asf/lucene.net/tags/Lucene.Net_3_0_3_RC1/branding/logo/lucene-net-icon-256x256.png" }
                , new SearchResult() { Title = "ITEM Q2 5",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"http://icons.iconarchive.com/icons/graphicloads/colorful-long-shadow/256/Home-icon.png" }
                , new SearchResult() { Title = "ITEM Q2 6",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"https://dumielauxepices.net/sites/default/files/heart-icons-colorful-636035-3581300.png" }
                , new SearchResult() { Title = "ITEM Q2 7",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"https://svn.apache.org/repos/asf/lucene.net/tags/Lucene.Net_3_0_3_RC1/branding/logo/lucene-net-icon-256x256.png" }
                , new SearchResult() { Title = "ITEM Q2 8",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"http://icons.iconarchive.com/icons/graphicloads/colorful-long-shadow/256/Home-icon.png" }
                , new SearchResult() { Title = "ITEM Q2 9",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"https://dumielauxepices.net/sites/default/files/heart-icons-colorful-636035-3581300.png" }
                , new SearchResult() { Title = "ITEM Q2 10",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"http://icons.iconarchive.com/icons/graphicloads/colorful-long-shadow/256/Home-icon.png" }
                , new SearchResult() { Title = "ITEM Q2 11",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"https://dumielauxepices.net/sites/default/files/heart-icons-colorful-636035-3581300.png" }
                , new SearchResult() { Title = "ITEM Q2 12",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"https://svn.apache.org/repos/asf/lucene.net/tags/Lucene.Net_3_0_3_RC1/branding/logo/lucene-net-icon-256x256.png" }
                , new SearchResult() { Title = "ITEM Q2 13",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"http://icons.iconarchive.com/icons/graphicloads/colorful-long-shadow/256/Home-icon.png" }
                , new SearchResult() { Title = "ITEM Q2 14",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"https://dumielauxepices.net/sites/default/files/heart-icons-colorful-636035-3581300.png" }
                , new SearchResult() { Title = "ITEM Q2 15",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"https://svn.apache.org/repos/asf/lucene.net/tags/Lucene.Net_3_0_3_RC1/branding/logo/lucene-net-icon-256x256.png" }
                , new SearchResult() { Title = "ITEM Q2 16",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"http://icons.iconarchive.com/icons/graphicloads/colorful-long-shadow/256/Home-icon.png" }
                , new SearchResult() { Title = "ITEM Q2 17",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"https://dumielauxepices.net/sites/default/files/heart-icons-colorful-636035-3581300.png" }
                , new SearchResult() { Title = "ITEM Q2 18",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"http://icons.iconarchive.com/icons/graphicloads/colorful-long-shadow/256/Home-icon.png" }
                , new SearchResult() { Title = "ITEM Q2 19",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"https://dumielauxepices.net/sites/default/files/heart-icons-colorful-636035-3581300.png" }
                , new SearchResult() { Title = "ITEM Q2 20",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"https://svn.apache.org/repos/asf/lucene.net/tags/Lucene.Net_3_0_3_RC1/branding/logo/lucene-net-icon-256x256.png" }
                , new SearchResult() { Title = "ITEM Q2 21",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"http://icons.iconarchive.com/icons/graphicloads/colorful-long-shadow/256/Home-icon.png" }
                , new SearchResult() { Title = "ITEM Q2 22",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"https://dumielauxepices.net/sites/default/files/heart-icons-colorful-636035-3581300.png" }
                , new SearchResult() { Title = "ITEM Q2 23",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"https://svn.apache.org/repos/asf/lucene.net/tags/Lucene.Net_3_0_3_RC1/branding/logo/lucene-net-icon-256x256.png" }
                , new SearchResult() { Title = "ITEM Q2 24",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"http://icons.iconarchive.com/icons/graphicloads/colorful-long-shadow/256/Home-icon.png" }
                , new SearchResult() { Title = "ITEM Q2 25",  Description ="this is test data for ajax q2", Url=@"/admin/",IconUrl= @"https://dumielauxepices.net/sites/default/files/heart-icons-colorful-636035-3581300.png" }
            };
            var tempquery = datalist.Where(p => p.Title.ToLower().Contains(searcher.Trim().ToLower()));

            return new SearchResultList()
            {
                TotalCount = tempquery.Count(),
                SortExpression = SortExpression,
                StartIndex = StartIndex,
                Size = Size,
                Data = tempquery.Skip(StartIndex).Take(Size).ToList()
            };
        }

        [Route("Api/Q3")]
        public async Task<List<SearchResult>> Q3(string searcher)
        {
            throw new Exception("INVALID");
        }

        [Route("Api/Q4")]
        public async Task<string> Q4(string searcher, string SortExpression, int StartIndex, int Size)
        {           
            return "WRONG OBJECT FORMAT";
        }

        [Route("Api/Q5")]
        public async Task<SearchResultList> Q5(string searcher, string SortExpression, int StartIndex, int Size)
        {
            return new SearchResultList() { Data = new List<SearchResult>() };
        }
    }

    public class SearchResultList
    {
        public List<SearchResult> Data { set; get; }
        public long TotalCount { set; get; }
        public string SortExpression { set; get; }
        public long StartIndex { set; get; }
        public long Size { set; get; }
    }
}