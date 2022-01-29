using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlazorApp.Data;

namespace MyBlazorApp
{
    public partial class ExportMyBlazorAppDbController : ExportController
    {
        private readonly MyBlazorAppDbContext context;
        private readonly MyBlazorAppDbService service;
        public ExportMyBlazorAppDbController(MyBlazorAppDbContext context, MyBlazorAppDbService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/MyBlazorAppDb/items/csv")]
        [HttpGet("/export/MyBlazorAppDb/items/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportItemsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetItems(), Request.Query), fileName);
        }

        [HttpGet("/export/MyBlazorAppDb/items/excel")]
        [HttpGet("/export/MyBlazorAppDb/items/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportItemsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetItems(), Request.Query), fileName);
        }
        [HttpGet("/export/MyBlazorAppDb/todolists/csv")]
        [HttpGet("/export/MyBlazorAppDb/todolists/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportToDoListsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetToDoLists(), Request.Query), fileName);
        }

        [HttpGet("/export/MyBlazorAppDb/todolists/excel")]
        [HttpGet("/export/MyBlazorAppDb/todolists/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportToDoListsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetToDoLists(), Request.Query), fileName);
        }
    }
}
