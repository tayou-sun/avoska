using CStuffControl.Infrastructure;
using System.Collections.Generic;
using System;
using System.Linq;
public class TagRepository : ITagRepository
{
    private AppDbContext appDbContext;
    public TagRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

   

    public IEnumerable<Tag> GetTags()
    {
        var a = new Tag{
           Id = 1, 
            
        };
        var rooms = appDbContext.Tags.Where(x=>x.Parent == null && x.IsVisible) ;
        return rooms.ToList();
    }


 public IEnumerable<Tag> GetChildsByParentId(int id)
    {

        var rooms = appDbContext.Tags.Where(x=>x.Parent != null && x.Parent.Id == id);
        var a = rooms.ToList();
        return rooms.ToList();
    }


}