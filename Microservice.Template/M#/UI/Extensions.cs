using System.Linq;
using System.Collections.Generic;
using MSharp;
using Olive;
using Olive.Entities;

namespace System
{
    static class Extensions
    {
        internal static IEnumerable<GenericFilterElement<T>> CustomSearches<T>(this ListModule<T> list, IEnumerable<KeyValuePair<string, object>> elements) where T : Olive.Entities.IEntity
        {
            foreach (var item in elements)
            {
                var label = item.Key;
                var searchElement = list.CustomSearch().Label(label);

                item.GetType().GetProperties().Do(p =>
                {

                });

                yield return searchElement;

            }

        }

        internal static CommonFilterElement<T> AddAllFieldsSearch<T>(this ListModule<T> module, string labeld = "Find") where T : Olive.Entities.IEntity
           => module.Search(GeneralSearch.AllFields).Label("Find:");

        internal static NavigateActivity Send(this NavigateActivity activity, object queryString)
        {
            var result = activity;

            foreach (var p in queryString.GetType().GetProperties())
            {
                var key = p.Name;
                var value = p.GetValue(queryString)?.ToString();

                if (value.HasValue())
                    result = result.Send(key, value);
            }

            return result;
        }

        internal static ListButton<T> EditButtonColumn<T, TEditPage>(this ListModule<T> list, string text = "Edit", object queryString = null)
            where T : IEntity
            where TEditPage : MSharp.ApplicationPage
        {
            var queryStringInfo = queryString ?? new { item = "item.ID" };

            return list.ButtonColumn("Edit")
                       .NoHeaderText()
                       .OnClick(x => x.Go<TEditPage>().Send(queryStringInfo)
                       .SendReturnUrl());
        }

        internal static ListButton<T> DeleteButtonColumn<T>(this ListModule<T> list, string text = "Delete") where T : IEntity
        {
            return list.ButtonColumn(text)
                .ConfirmQuestion("Are you sure you want to delete this item?")
                .NoHeaderText().OnClick(x =>
            {
                x.DeleteItem();
                x.RefreshPage();
            });
        }
    }
}

