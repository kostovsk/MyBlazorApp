using Radzen;
using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using System.Text.Encodings.Web;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using MyBlazorApp.Data;

namespace MyBlazorApp
{
    public partial class MyBlazorAppDbService
    {
        MyBlazorAppDbContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly MyBlazorAppDbContext context;
        private readonly NavigationManager navigationManager;

        public MyBlazorAppDbService(MyBlazorAppDbContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public async Task ExportItemsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/myblazorappdb/items/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/myblazorappdb/items/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportItemsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/myblazorappdb/items/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/myblazorappdb/items/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnItemsRead(ref IQueryable<Models.MyBlazorAppDb.Item> items);

        public async Task<IQueryable<Models.MyBlazorAppDb.Item>> GetItems(Query query = null)
        {
            var items = Context.Items.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnItemsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnItemCreated(Models.MyBlazorAppDb.Item item);
        partial void OnAfterItemCreated(Models.MyBlazorAppDb.Item item);

        public async Task<Models.MyBlazorAppDb.Item> CreateItem(Models.MyBlazorAppDb.Item item)
        {
            OnItemCreated(item);

            var existingItem = Context.Items
                              .Where(i => i.ITEM_ID == item.ITEM_ID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Items.Add(item);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(item).State = EntityState.Detached;
                throw;
            }

            OnAfterItemCreated(item);

            return item;
        }
        public async Task ExportToDoListsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/myblazorappdb/todolists/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/myblazorappdb/todolists/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportToDoListsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/myblazorappdb/todolists/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/myblazorappdb/todolists/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnToDoListsRead(ref IQueryable<Models.MyBlazorAppDb.ToDoList> items);

        public async Task<IQueryable<Models.MyBlazorAppDb.ToDoList>> GetToDoLists(Query query = null)
        {
            var items = Context.ToDoLists.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnToDoListsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnToDoListCreated(Models.MyBlazorAppDb.ToDoList item);
        partial void OnAfterToDoListCreated(Models.MyBlazorAppDb.ToDoList item);

        public async Task<Models.MyBlazorAppDb.ToDoList> CreateToDoList(Models.MyBlazorAppDb.ToDoList toDoList)
        {
            OnToDoListCreated(toDoList);

            var existingItem = Context.ToDoLists
                              .Where(i => i.LIST_ID == toDoList.LIST_ID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ToDoLists.Add(toDoList);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(toDoList).State = EntityState.Detached;
                throw;
            }

            OnAfterToDoListCreated(toDoList);

            return toDoList;
        }

        partial void OnItemDeleted(Models.MyBlazorAppDb.Item item);
        partial void OnAfterItemDeleted(Models.MyBlazorAppDb.Item item);

        public async Task<Models.MyBlazorAppDb.Item> DeleteItem(int? itemId)
        {
            var itemToDelete = Context.Items
                              .Where(i => i.ITEM_ID == itemId)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnItemDeleted(itemToDelete);

            Context.Items.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterItemDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnItemGet(Models.MyBlazorAppDb.Item item);

        public async Task<Models.MyBlazorAppDb.Item> GetItemByItemId(int? itemId)
        {
            var items = Context.Items
                              .AsNoTracking()
                              .Where(i => i.ITEM_ID == itemId);

            var itemToReturn = items.FirstOrDefault();

            OnItemGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.MyBlazorAppDb.Item> CancelItemChanges(Models.MyBlazorAppDb.Item item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnItemUpdated(Models.MyBlazorAppDb.Item item);
        partial void OnAfterItemUpdated(Models.MyBlazorAppDb.Item item);

        public async Task<Models.MyBlazorAppDb.Item> UpdateItem(int? itemId, Models.MyBlazorAppDb.Item item)
        {
            OnItemUpdated(item);

            var itemToUpdate = Context.Items
                              .Where(i => i.ITEM_ID == itemId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(item);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterItemUpdated(item);

            return item;
        }

        partial void OnToDoListDeleted(Models.MyBlazorAppDb.ToDoList item);
        partial void OnAfterToDoListDeleted(Models.MyBlazorAppDb.ToDoList item);

        public async Task<Models.MyBlazorAppDb.ToDoList> DeleteToDoList(int? listId)
        {
            var itemToDelete = Context.ToDoLists
                              .Where(i => i.LIST_ID == listId)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnToDoListDeleted(itemToDelete);

            Context.ToDoLists.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterToDoListDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnToDoListGet(Models.MyBlazorAppDb.ToDoList item);

        public async Task<Models.MyBlazorAppDb.ToDoList> GetToDoListByListId(int? listId)
        {
            var items = Context.ToDoLists
                              .AsNoTracking()
                              .Where(i => i.LIST_ID == listId);

            var itemToReturn = items.FirstOrDefault();

            OnToDoListGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.MyBlazorAppDb.ToDoList> CancelToDoListChanges(Models.MyBlazorAppDb.ToDoList item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnToDoListUpdated(Models.MyBlazorAppDb.ToDoList item);
        partial void OnAfterToDoListUpdated(Models.MyBlazorAppDb.ToDoList item);

        public async Task<Models.MyBlazorAppDb.ToDoList> UpdateToDoList(int? listId, Models.MyBlazorAppDb.ToDoList toDoList)
        {
            OnToDoListUpdated(toDoList);

            var itemToUpdate = Context.ToDoLists
                              .Where(i => i.LIST_ID == listId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(toDoList);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterToDoListUpdated(toDoList);

            return toDoList;
        }
    }
}
