using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlazorApp.Models.MyBlazorAppDb
{
  [Table("ToDoList", Schema = "dbo")]
  public partial class ToDoList
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LIST_ID
    {
      get;
      set;
    }
    public string NAME
    {
      get;
      set;
    }
    public DateTime DATE_CREATED
    {
      get;
      set;
    }
  }
}
