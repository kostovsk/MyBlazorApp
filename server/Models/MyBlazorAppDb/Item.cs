using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlazorApp.Models.MyBlazorAppDb
{
  [Table("Items", Schema = "dbo")]
  public partial class Item
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ITEM_ID
    {
      get;
      set;
    }
    public int LIST_ID
    {
      get;
      set;
    }

    [Column("ITEM")]
    public string ITEM1
    {
      get;
      set;
    }
  }
}
